using Microsoft.VisualBasic.CompilerServices;
using System;
using System.IO;
using System.Security.Cryptography;
using System.Text;
namespace FotoVision
{
	public sealed class Hash
	{
		private Hash()
		{
		}
		public static string ComputeFileHash(string filePath)
		{
			string result;
            FileStream fileStream = null;
            MD5CryptoServiceProvider mD5CryptoServiceProvider = null;

			try
			{
				if (!File.Exists(filePath))
				{
					result = "";
				}
				else
				{
                    fileStream = new FileStream(filePath, FileMode.Open);
                    mD5CryptoServiceProvider = new MD5CryptoServiceProvider();
					byte[] array = mD5CryptoServiceProvider.ComputeHash(fileStream);
					result = Convert.ToBase64String(array);
				}
			}
			catch (Exception expr_2F)
			{
				ProjectData.SetProjectError(expr_2F);
				result = "";
				ProjectData.ClearProjectError();
			}
			finally
			{
				if (fileStream != null)
				{
					fileStream.Close();
				}
				if (mD5CryptoServiceProvider != null)
				{
					mD5CryptoServiceProvider.Clear();
				}
			}
			return result;
		}
		public static string ComputeHash(string s)
		{
			SHA1CryptoServiceProvider sHA1CryptoServiceProvider = new SHA1CryptoServiceProvider();
			string result = Convert.ToBase64String(sHA1CryptoServiceProvider.ComputeHash(Encoding.ASCII.GetBytes(s)));
			sHA1CryptoServiceProvider.Clear();
			return result;
		}
	}
}
