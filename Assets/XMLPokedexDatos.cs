using System.Xml.Serialization;

//Clase que se usa para guardar el archivo XML de los datos
public class XMLPokedexDatos {
	[XmlElement("user")]
	public User user;

	override
	public string ToString(){
		return user.ToString()+"\n";
	}
}
