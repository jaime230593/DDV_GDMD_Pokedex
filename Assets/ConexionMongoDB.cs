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

	static MongoClient client;
	static MongoServer server;
	static MongoDatabase database;
	static MongoCollection<BsonDocument> pokemoncollection;

	//Para conectar con la base de datos de MongoDB
	public static void Conectar(string conexion){
		client = new MongoClient(conexion);
		server = client.GetServer();
		database = server.GetDatabase("pokedex");
		pokemoncollection = database.GetCollection<BsonDocument>("pokemon");

		Debug.Log ("Conectado a la base de datos de MongoDB");

		/*Debug.Log("Buscando 'Bulbasaur'");
		foreach (var document in pokemoncollection.Find(new QueryDocument("name", "Bulbasaur"))){
			ImprimirPokemonBase(document);
		}*/
	}

	//Borra los archivos que tiene MongoDB y sube las imagenes de los pokemon
	public static void CargarImagenesPokemon(){

	}

	public static Texture2D LoadTexture(Image imagen, string FilePath) {
 
		// Load a PNG or JPG file from disk to a Texture2D
		// Returns null if load fails
	
		//Texture2D Tex2D;
		//byte[] FileData;

		string id = database.GridFS.Files.FindOne(Query.EQ("filename",FilePath))["_id"].ToString();

		Debug.Log(id);
		Debug.Log(database.GridFS.Chunks.FindOne(Query.EQ("files_id",database.GridFS.Files.FindOne(Query.EQ("filename",FilePath))["_id"])).ToJson());

		BsonDocument temp = database.GridFS.Chunks.FindOne(Query.EQ("files_id",database.GridFS.Files.FindOne(Query.EQ("filename",FilePath))["_id"]));

		Texture2D tex = new Texture2D(2,2);
		tex.LoadImage(temp["data"].AsByteArray);

		imagen.sprite = Sprite.Create(tex, new Rect(0, 0, tex.width,tex.height), new Vector2(0, 0));

		return null;
	
		/* if (File.Exists(FilePath)){
		FileData = File.ReadAllBytes(FilePath);
		Tex2D = new Texture2D(2, 2);           // Create new "empty" texture
		if (Tex2D.LoadImage(FileData))           // Load the imagedata into the texture (size is set automatically)
			return Tex2D;                 // If data = readable -> return texture
		}  
		return null;                     // Return null if load failed
		*/
	}

	static void InsertarPokemon(){

	}

	static void EliminarPokemon(){

	}

	static void MostrarImagenesDelPokemon(){

	}

	//Extrae los datos de los pokemon de MongoDB y los pasa a la clase Pokemon, y devuelve la lista
	static List<Pokemon> SacarPokemons(MongoCursor<BsonDocument> datos){
		List<Pokemon> pokemons = new List<Pokemon>();
		foreach (BsonDocument document in datos){
			pokemons.Add(new Pokemon(
				document["pokedex_number"].AsInt32,
				document["name"].AsString,
				document["generation"].AsInt32,
				document["is_legendary"].AsInt32,
				document["type1"].AsString,
				document["type2"].AsString
			));
		}

		return pokemons;
	}

	//Cuando se añaden varias busquedas de pokemon, para que no se dupliquen si estan en ambas listas
	static void AddPokemons(List<Pokemon> pokemons, List<Pokemon> toAdd){
		foreach (Pokemon p in toAdd){
			if (!pokemons.Contains(p)){
				pokemons.Add(p);
			}
		}
	}

	public static List<Pokemon> BuscarPorTipos(string[] tipos){
		List<Pokemon> pokemons = new List<Pokemon>();
		string t = "Buscando por tipos primario o secundario:";
		foreach (string tipo in tipos){
			t+=" "+tipo;
			AddPokemons(pokemons, SacarPokemons(pokemoncollection.Find(Query.Or(Query.EQ("type1", tipo), Query.EQ("type2", tipo)))));
		}
		Debug.Log(t);
		return pokemons;
	}

	public static List<Pokemon> BuscarPorLegendario(bool legendario){
		Debug.Log("Buscando por legendarios '"+legendario.ToString()+"'");
		return SacarPokemons(pokemoncollection.Find(new QueryDocument("is_legendary",1)));
	}

	public static List<Pokemon> BuscarPorGeneracion(int[] gen){
		List<Pokemon> pokemons = new List<Pokemon>();
		string t = "Buscando por generaciones:";
		foreach (int n in gen){
			t+=" "+n;
			AddPokemons (pokemons, SacarPokemons(pokemoncollection.Find(new QueryDocument("generation",n))));
		}
		Debug.Log(t);
		return pokemons;
	}
}
