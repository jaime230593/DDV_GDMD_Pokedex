using System.Xml.Serialization;
using System.IO;

public class XML {
	//Guarda los objetos en el XML en el path indicado
	public static void GuardarXML(object item, string path)
	{
		XmlSerializer serializer = new XmlSerializer(item.GetType());
		StreamWriter writer = new StreamWriter(path);
		serializer.Serialize(writer.BaseStream, item);
		writer.Close();
	}

	//Carga los datos del path indicado
	public static T CargarXML<T>(string path)
	{
		XmlSerializer serializer = new XmlSerializer(typeof(T));
		StreamReader reader = new StreamReader(path);
		T deserialized = (T)serializer.Deserialize(reader.BaseStream);
		reader.Close();
		return deserialized;
	}
}

//Clase que se usa para guardar el archivo XML de los datos
public class XMLPokedexDatos {
	[XmlArray("Users"), XmlArrayItem("user")]
	public User[] users;

	override
	public string ToString(){
		string texto = "Usuarios:\n";
		foreach (User u in users){
			texto+="\t"+u.ToString()+"\n";
		}
		return texto;
	}
}

//Clase base para identificar a los usuarios y sus datos
public class User {
 	//[XmlAttribute("name")]
	[XmlElement("nombre")]
 	public string nombre;
 	[XmlArray("opciones"), XmlArrayItem("opcion")]
    public string[] opciones;

	override
	public string ToString(){
		string texto = "";
		texto+="Nombre: "+nombre;
		if (opciones.Length > 0){
			texto+=", opciones:";
			foreach (string t in opciones){
				texto+=" "+t;
			}
		}
		return texto;
	}
}