using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//MongoDB
using MongoDB.Bson;
using MongoDB.Driver;
using MongoDB.Driver.Builders;  
using MongoDB.Driver.GridFS;  
using MongoDB.Driver.Linq;
using System.IO;
using UnityEngine.UI;

public class ConexionMongoDB {

	static string pathMongoDB = "mongodb://localhost:27017";
	static MongoClient client;
	static MongoServer server;
	static MongoDatabase database;
	static MongoCollection<BsonDocument> pokemoncollection;

	//Para conectar con la base de datos de MongoDB
	public static void Conectar(){
		client = new MongoClient(pathMongoDB);
		server = client.GetServer();
		database = server.GetDatabase("pokedex");
		pokemoncollection = database.GetCollection<BsonDocument>("pokemon");

		Debug.Log ("Conectado a la base de datos de MongoDB");
	}

	//Borra los archivos que tiene MongoDB y sube las imagenes de los pokemon
	public static void CargarImagenesPokemon(){
		//Eliminamos los archivos que habia antes
		database.GridFS.Files.Drop();
		database.GridFS.Chunks.Drop();

		string ruta = "Assets/pokemon/";

		foreach (string n in Directory.GetFiles(ruta))
		{
			//.meta lo usa unity, no queremos meterlos
			if (!n.Contains(".meta")){
				using(FileStream file = File.OpenRead(n))
				{
					//Metemos en GRIDFS los archivos con su nombre
					database.GridFS.Upload(file, n.Split('/')[2]);
				}
			}
		}
	}

	public static Sprite LoadTexture(string FilePath) {
		//Debug.Log(FilePath);
		BsonValue id = database.GridFS.Files.FindOne(Query.EQ("filename",FilePath))["_id"];

		//Debug.Log(id);
		//Debug.Log(database.GridFS.Chunks.FindOne(Query.EQ("files_id",database.GridFS.Files.FindOne(Query.EQ("filename",FilePath))["_id"])).ToJson());

		BsonDocument temp = database.GridFS.Chunks.FindOne(Query.EQ("files_id", id));

		Texture2D tex = new Texture2D(2,2);
		tex.LoadImage(temp["data"].AsByteArray);

		return Sprite.Create(tex, new Rect(0, 0, tex.width,tex.height), new Vector2(0, 0));
	}

	public static List<string> BuscarMegaEvoluciones(int pokedex_number){
		List<string> s = new List<string>();
		BsonDocument t = database.GridFS.Files.FindOne(Query.EQ("filename",pokedex_number.ToString()+"-mega.png"));
		if (t != null){
			//Debug.Log("Mega "+t["_id"].ToString());
			s.Add(t["filename"].ToString());
		}
		t = database.GridFS.Files.FindOne(Query.EQ("filename",pokedex_number.ToString()+"-mega-x.png"));
		if (t != null){
			//Debug.Log("Mega x "+t["_id"].ToString());
			s.Add(t["filename"].ToString());
		}
		t = database.GridFS.Files.FindOne(Query.EQ("filename",pokedex_number.ToString()+"-mega-y.png"));
		if (t != null){
			//Debug.Log("Mega y "+t["_id"].ToString());
			s.Add(t["filename"].ToString());
		}
		t = database.GridFS.Files.FindOne(Query.EQ("filename",pokedex_number.ToString()+"-primal.png"));
		if (t != null){
			//Debug.Log("Mega y "+t["_id"].ToString());
			s.Add(t["filename"].ToString());
		}

		return s;
	}

	public static List<Pokemon> CogerPokemons(){
		List<Pokemon> pokemons = new List<Pokemon>();
		foreach (BsonDocument document in pokemoncollection.FindAll()){
			pokemons.Add(FormarPokemon(document));
		}

		return pokemons;
	}

	//Parametros: array de generaciones seleccionadas, tipos y si se filtra por legendario
	public static List<Pokemon> CogerPokemons(int[] generacion, string[] tipos, bool legendario){
		int nfiltros = 0;
		if (generacion.Length != 0){
			nfiltros += 1;
		}
		if (tipos.Length != 0){
			nfiltros += 1;
		}
		if (legendario){
			nfiltros += 1;
		}
		List<Pokemon> pokemons = new List<Pokemon>();
		//Se usa cuando hay filtros seleccionados
		if (nfiltros != 0){
			string filtro = "";
			string tmp = "";
			//Inicio del JSON
			if (nfiltros > 1){
				filtro = "{ $and: [";
			}
			//legendarios
			if (legendario){
				filtro += " { is_legendary: 1 },";
			}
			//generaciones
			if (generacion.Length != 0){
				tmp = " { $or: [ ";
				for (int i =0;i<generacion.Length;i++){
					if (i==0){
						tmp += " { generation: "+generacion[i]+" }";
					}else{
						tmp += ", { generation: "+generacion[i]+" }";
					}
				}
				tmp += " ] } ";
				filtro += tmp;
			}
			//tipos
			if (tipos.Length != 0){
				tmp = " { $or: [ ";
				for (int i =0;i<tipos.Length;i++){
					if (i==0){
						tmp += " { $or: [ { type1: '"+tipos[i]+"' }, { type2: '"+tipos[i]+"' } ] } ";
					}else{
						tmp += ", { $or: [ { type1: '"+tipos[i]+"' }, { type2: '"+tipos[i]+"' } ] } ";
					}
				}
				tmp += " ] } ";
				filtro += tmp;
			}
			//Fin del JSON
			if (nfiltros > 1){
				filtro += " ] } ";
			}
			Debug.Log(filtro);

			AddPokemons(pokemons, SacarPokemons(pokemoncollection.Find(new QueryDocument(BsonDocument.Parse(filtro)))));
		}else{
			//Se usa para coger todos los pokemons
			//(Cuando no hay filtros seleccionados)
			AddPokemons(pokemons, CogerPokemons());
		}
		
		return pokemons;
	}

	//Extrae los datos de los pokemon de MongoDB y los pasa a la clase Pokemon, y devuelve la lista
	static List<Pokemon> SacarPokemons(MongoCursor<BsonDocument> datos){
		List<Pokemon> pokemons = new List<Pokemon>();
		foreach (BsonDocument document in datos){
			pokemons.Add(FormarPokemon(document));
		}

		return pokemons;
	}

	//Cuando se añaden varias busquedas de pokemon, para que no se dupliquen si estan en ambas listas
	static void AddPokemons(List<Pokemon> pokemons, List<Pokemon> toAdd){
		foreach (Pokemon p in toAdd){
			if (!pokemons.Contains(p)){
				pokemons.Add(p);
				Debug.Log(p.ToString());
			}
		}
	}

	static Pokemon FormarPokemon(BsonDocument document){
		Pokemon p = new Pokemon(
			document["pokedex_number"].AsInt32,
			document["name"].AsString,
			document["generation"].AsInt32,
			document["is_legendary"].AsInt32,
			document["type1"].AsString,
			document["type2"].AsString,
			BuscarMegaEvoluciones(document["pokedex_number"].AsInt32),
			document["classfication"].AsString,
			document["japanese_name"].AsString
		);
		return p;
	}
}