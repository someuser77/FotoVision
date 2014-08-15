using System;
using System.Xml.Serialization;
namespace FotoVision.PhotoAdmin
{
	[XmlType(Namespace = "http://tempuri.org/")]
	public class AlbumHashCode
	{
		public string AlbumName;
		public string MetaDataFileName;
		public string MetaDataHash;
	}
}
