using System.Xml.Serialization;

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
