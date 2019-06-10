using System.Xml.Serialization;
using System.IO;
using UnityEngine;

public class XML {
	public static string pathXML = "user.xml";

	public static void CrearXML(){
		FileStream fs = new FileStream(pathXML, FileMode.Create);
    	fs.Dispose();
		fs.Close();
    	//string text = File.ReadAllText(myfile);
	}

	//Guarda los objetos en el XML en el path indicado (ya no se usa si se carga desde el xml directamente sin insertar usuario)
	/*public static void GuardarXML(object item, string path)
	{
		XmlSerializer serializer = new XmlSerializer(item.GetType());
		StreamWriter writer = new StreamWriter(path);
		serializer.Serialize(writer.BaseStream, item);
		writer.Close();
	}*/

	//Carga los datos del path indicado
	public static XMLPokedexDatos CargarXML()
	{
		XmlSerializer serializer = new XmlSerializer(typeof(XMLPokedexDatos));
		StreamReader reader = new StreamReader(pathXML);
		XMLPokedexDatos deserialized = (XMLPokedexDatos)serializer.Deserialize(reader.BaseStream);
		reader.Close();
		Debug.Log(deserialized);
		return deserialized;
	}
}