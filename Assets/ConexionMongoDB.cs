﻿using System.Collections;
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

	static void InsertarPokemon(){

	}

	static void EliminarPokemon(){

	}

	static void MostrarImagenesDelPokemon(){

	}

	public static List<Pokemon> CogerPokemons(){
		List<Pokemon> pokemons = new List<Pokemon>();
		foreach (BsonDocument document in pokemoncollection.FindAll()){
			pokemons.Add(FormarPokemon(document));
			
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

	public static Pokemon BuscarPokemon(int pokedex_number){
		Pokemon p = null;
		foreach (BsonDocument document in pokemoncollection.Find(new QueryDocument("pokedex_number", pokedex_number))){
			p = FormarPokemon(document);
		}
		return p;
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

	public static List<Pokemon> BuscarPorLegendario(){
		Debug.Log("Buscando por legendarios");
		return SacarPokemons(pokemoncollection.Find(new QueryDocument("is_legendary",1)));
	}

	public static List<Pokemon> BuscarPorLegendarioTipos(string[] tipos){
		List<Pokemon> pokemons = new List<Pokemon>();
		string t = "Buscando por Legendarios y tipos primario o secundario:";
		foreach (string tipo in tipos){
			t+=" "+tipo;
			AddPokemons(pokemons, SacarPokemons(pokemoncollection.Find(Query.And(
					Query.Or(Query.EQ("type1", tipo), Query.EQ("type2", tipo)),
					Query.EQ("is_legendary",1)
				)
			)));
		}
		Debug.Log(t);
		return pokemons;
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
