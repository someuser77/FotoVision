using System;
using System.Xml.Serialization;
namespace FotoVision.PhotoAdmin
{
	[XmlType(Namespace = "http://tempuri.org/")]
	public class PhotoHashCode
	{
		public string ImageFileName;
		public string MetaDataFileName;
		public string ImageHash;
		public string MetaDataHash;
	}
}
