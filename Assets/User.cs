using System.Xml.Serialization;

//Clase base para identificar a los usuarios y sus datos
public class User {
	[XmlElement("nombre")]
 	public string nombre;

	override
	public string ToString(){
		string texto = "";
		texto+="Nombre: "+nombre;
		return texto;
	}
}