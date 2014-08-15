using Microsoft.VisualBasic.CompilerServices;
using System;
using System.ComponentModel;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using System.Web.Services;
using System.Web.Services.Protocols;
using System.Xml.Serialization;
namespace FotoVision.PhotoAdmin
{
	[DesignerCategory("code"), DebuggerStepThrough, WebServiceBinding(Name = "PhotoAdminSoap", Namespace = "http://tempuri.org/")]
	public class PhotoAdmin : SoapHttpClientProtocol
	{
		public AdminHeader AdminHeaderValue;
		public PhotoAdmin()
		{
			this.Url = "http://localhost/FotoVisionVB/PhotoAdmin.asmx";
		}
		[SoapDocumentMethod, SoapHeader("AdminHeaderValue")]
		public bool Login()
		{
			object[] array = this.Invoke("Login", new object[0]);
			return BooleanType.FromObject(array[0]);
		}
		public IAsyncResult BeginLogin(AsyncCallback callback, object asyncState)
		{
			return this.BeginInvoke("Login", new object[0], callback, RuntimeHelpers.GetObjectValue(asyncState));
		}
		public bool EndLogin(IAsyncResult asyncResult)
		{
			object[] array = this.EndInvoke(asyncResult);
			return BooleanType.FromObject(array[0]);
		}
		[SoapDocumentMethod, SoapHeader("AdminHeaderValue")]
		public void UpdateAlbum(string albumName, [XmlElement(DataType = "base64Binary")] byte[] metaData)
		{
			this.Invoke("UpdateAlbum", new object[]
			{
				albumName,
				metaData
			});
		}
		public IAsyncResult BeginUpdateAlbum(string albumName, byte[] metaData, AsyncCallback callback, object asyncState)
		{
			return this.BeginInvoke("UpdateAlbum", new object[]
			{
				albumName,
				metaData
			}, callback, RuntimeHelpers.GetObjectValue(asyncState));
		}
		public void EndUpdateAlbum(IAsyncResult asyncResult)
		{
			this.EndInvoke(asyncResult);
		}
		[SoapDocumentMethod, SoapHeader("AdminHeaderValue")]
		public void DeleteAlbum(string albumName)
		{
			this.Invoke("DeleteAlbum", new object[]
			{
				albumName
			});
		}
		public IAsyncResult BeginDeleteAlbum(string albumName, AsyncCallback callback, object asyncState)
		{
			return this.BeginInvoke("DeleteAlbum", new object[]
			{
				albumName
			}, callback, RuntimeHelpers.GetObjectValue(asyncState));
		}
		public void EndDeleteAlbum(IAsyncResult asyncResult)
		{
			this.EndInvoke(asyncResult);
		}
		[SoapDocumentMethod, SoapHeader("AdminHeaderValue")]
		public void UpdatePhoto(string albumName, string photoName, [XmlElement(DataType = "base64Binary")] byte[] image, [XmlElement(DataType = "base64Binary")] byte[] metaData)
		{
			this.Invoke("UpdatePhoto", new object[]
			{
				albumName,
				photoName,
				image,
				metaData
			});
		}
		public IAsyncResult BeginUpdatePhoto(string albumName, string photoName, byte[] image, byte[] metaData, AsyncCallback callback, object asyncState)
		{
			return this.BeginInvoke("UpdatePhoto", new object[]
			{
				albumName,
				photoName,
				image,
				metaData
			}, callback, RuntimeHelpers.GetObjectValue(asyncState));
		}
		public void EndUpdatePhoto(IAsyncResult asyncResult)
		{
			this.EndInvoke(asyncResult);
		}
		[SoapDocumentMethod, SoapHeader("AdminHeaderValue")]
		public void DeletePhoto(string albumName, string photoName)
		{
			this.Invoke("DeletePhoto", new object[]
			{
				albumName,
				photoName
			});
		}
		public IAsyncResult BeginDeletePhoto(string albumName, string photoName, AsyncCallback callback, object asyncState)
		{
			return this.BeginInvoke("DeletePhoto", new object[]
			{
				albumName,
				photoName
			}, callback, RuntimeHelpers.GetObjectValue(asyncState));
		}
		public void EndDeletePhoto(IAsyncResult asyncResult)
		{
			this.EndInvoke(asyncResult);
		}
		[SoapDocumentMethod, SoapHeader("AdminHeaderValue")]
		public AlbumHashCode[] GetAlbumHashCodes()
		{
			object[] array = this.Invoke("GetAlbumHashCodes", new object[0]);
			return (AlbumHashCode[])array[0];
		}
		public IAsyncResult BeginGetAlbumHashCodes(AsyncCallback callback, object asyncState)
		{
			return this.BeginInvoke("GetAlbumHashCodes", new object[0], callback, RuntimeHelpers.GetObjectValue(asyncState));
		}
		public AlbumHashCode[] EndGetAlbumHashCodes(IAsyncResult asyncResult)
		{
			object[] array = this.EndInvoke(asyncResult);
			return (AlbumHashCode[])array[0];
		}
		[SoapDocumentMethod, SoapHeader("AdminHeaderValue")]
		public PhotoHashCode[] GetPhotoHashCodes(string albumName)
		{
			object[] array = this.Invoke("GetPhotoHashCodes", new object[]
			{
				albumName
			});
			return (PhotoHashCode[])array[0];
		}
		public IAsyncResult BeginGetPhotoHashCodes(string albumName, AsyncCallback callback, object asyncState)
		{
			return this.BeginInvoke("GetPhotoHashCodes", new object[]
			{
				albumName
			}, callback, RuntimeHelpers.GetObjectValue(asyncState));
		}
		public PhotoHashCode[] EndGetPhotoHashCodes(IAsyncResult asyncResult)
		{
			object[] array = this.EndInvoke(asyncResult);
			return (PhotoHashCode[])array[0];
		}
	}
}
