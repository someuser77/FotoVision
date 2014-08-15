using System;
using System.Web.Services.Protocols;
using System.Xml.Serialization;
namespace FotoVision.PhotoAdmin
{
	[XmlRoot(Namespace = "http://tempuri.org/", IsNullable = false), XmlType(Namespace = "http://tempuri.org/")]
	public class AdminHeader : SoapHeader
	{
		public string Password;
	}
}
