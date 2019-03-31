using System.Xml.Serialization;

//Clase base para identificar a los usuarios y sus datos
public class User {
 	//[XmlAttribute("name")]
	[XmlElement("nombre")]
 	public string nombre;
 	//[XmlArray("opciones"), XmlArrayItem("opcion")]
    //public string[] opciones;
	[XmlElement(ElementName = "pokemonacargar", IsNullable = true)]
	public string pokemonACargar;

	override
	public string ToString(){
		string texto = "";
		texto+="Nombre: "+nombre;
		/*if (opciones != null && opciones.Length > 0){
			texto+=", opciones:";
			foreach (string t in opciones){
				texto+=" "+t;
			}
		}*/
		texto+="\nUltimo pokemon visto: "+pokemonACargar;
		return texto;
	}
}
