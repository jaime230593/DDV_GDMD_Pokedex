using System.Xml.Serialization;
using System.IO;
using UnityEngine;

public class XML {

	//public static string pathXML = Path.Combine(Application.persistentDataPath, "users.xml");
	public static string pathXML = "Assets/users.xml";

	public static void CrearXML(){
		FileStream fs = new FileStream(pathXML, FileMode.Create);
    	fs.Dispose();
		fs.Close();
    	//string text = File.ReadAllText(myfile);
	}

	//Guarda los objetos en el XML en el path indicado
	public static void GuardarXML(object item, string path)
	{
		XmlSerializer serializer = new XmlSerializer(item.GetType());
		StreamWriter writer = new StreamWriter(path);
		serializer.Serialize(writer.BaseStream, item);
		writer.Close();
	}

	//Carga los datos del path indicado
	public static XMLPokedexDatos CargarXML()
	{
		//Debug.Log(pathXML);
		if (!File.Exists(pathXML)){
			Debug.Log("No hay archivo xml, creando uno vacio");
			CrearXML();
			return null;
		}

		XmlSerializer serializer = new XmlSerializer(typeof(XMLPokedexDatos));
		StreamReader reader = new StreamReader(pathXML);
		XMLPokedexDatos deserialized = (XMLPokedexDatos)serializer.Deserialize(reader.BaseStream);
		reader.Close();
		Debug.Log(deserialized);
		return deserialized;
	}
}