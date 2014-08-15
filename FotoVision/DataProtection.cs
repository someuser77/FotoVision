using Microsoft.VisualBasic;
using Microsoft.VisualBasic.CompilerServices;
using System;
using System.Runtime.InteropServices;
using System.Text;
namespace FotoVision
{
	public sealed class DataProtection
	{
		public enum Store
		{
			Machine,
			User
		}
		private class Consts
		{
			public static byte[] EntropyData = Encoding.ASCII.GetBytes("9EA4882F-F8D8-4f24-9402-FF951E5B4929");
		}
		private class Win32
		{
			public struct DATA_BLOB
			{
				public int cbData;
				public IntPtr pbData;
			}
			public const int CRYPTPROTECT_UI_FORBIDDEN = 1;
			public const int CRYPTPROTECT_LOCAL_MACHINE = 4;
			[DllImport("crypt32", CharSet = CharSet.Auto)]
			public static extern bool CryptProtectData(ref DataProtection.Win32.DATA_BLOB pDataIn, string szDataDescr, ref DataProtection.Win32.DATA_BLOB pOptionalEntropy, IntPtr pvReserved, IntPtr pPromptStruct, int dwFlags, ref DataProtection.Win32.DATA_BLOB pDataOut);
			[DllImport("crypt32", CharSet = CharSet.Auto)]
			public static extern bool CryptUnprotectData(ref DataProtection.Win32.DATA_BLOB pDataIn, StringBuilder szDataDescr, ref DataProtection.Win32.DATA_BLOB pOptionalEntropy, IntPtr pvReserved, IntPtr pPromptStruct, int dwFlags, ref DataProtection.Win32.DATA_BLOB pDataOut);
			[DllImport("kernel32")]
			public static extern IntPtr LocalFree(IntPtr hMem);
		}
		private DataProtection()
		{
		}
		public static string Encrypt(string data, DataProtection.Store store)
		{
			string result = "";
			DataProtection.Win32.DATA_BLOB dATA_BLOB = default(DataProtection.Win32.DATA_BLOB);
			DataProtection.Win32.DATA_BLOB dATA_BLOB2 = default(DataProtection.Win32.DATA_BLOB);
			DataProtection.Win32.DATA_BLOB dATA_BLOB3 = default(DataProtection.Win32.DATA_BLOB);
			try
			{
				int dwFlags = 1 | IntegerType.FromObject(Interaction.IIf(store == DataProtection.Store.Machine, 4, 0));
				DataProtection.SetBlobData(ref dATA_BLOB, Encoding.ASCII.GetBytes(data));
				DataProtection.SetBlobData(ref dATA_BLOB2, DataProtection.Consts.EntropyData);
				if (DataProtection.Win32.CryptProtectData(ref dATA_BLOB, "", ref dATA_BLOB2, IntPtr.Zero, IntPtr.Zero, dwFlags, ref dATA_BLOB3))
				{
					byte[] blobData = DataProtection.GetBlobData(ref dATA_BLOB3);
					if (blobData != null)
					{
						result = Convert.ToBase64String(blobData);
					}
				}
			}
			catch (Exception expr_91)
			{
				ProjectData.SetProjectError(expr_91);
				ProjectData.ClearProjectError();
			}
			finally
			{
				if (dATA_BLOB.pbData.ToInt32() != 0)
				{
					Marshal.FreeHGlobal(dATA_BLOB.pbData);
				}
				if (dATA_BLOB2.pbData.ToInt32() != 0)
				{
					Marshal.FreeHGlobal(dATA_BLOB2.pbData);
				}
			}
			return result;
		}
		public static string Decrypt(string data, DataProtection.Store store)
		{
			string result = "";
			DataProtection.Win32.DATA_BLOB dATA_BLOB = default(DataProtection.Win32.DATA_BLOB);
			DataProtection.Win32.DATA_BLOB dATA_BLOB2 = default(DataProtection.Win32.DATA_BLOB);
			DataProtection.Win32.DATA_BLOB dATA_BLOB3 = default(DataProtection.Win32.DATA_BLOB);
			try
			{
				int dwFlags = 1 | IntegerType.FromObject(Interaction.IIf(store == DataProtection.Store.Machine, 4, 0));
				byte[] bits = Convert.FromBase64String(data);
				DataProtection.SetBlobData(ref dATA_BLOB, bits);
				DataProtection.SetBlobData(ref dATA_BLOB2, DataProtection.Consts.EntropyData);
				if (DataProtection.Win32.CryptUnprotectData(ref dATA_BLOB, null, ref dATA_BLOB2, IntPtr.Zero, IntPtr.Zero, dwFlags, ref dATA_BLOB3))
				{
					byte[] blobData = DataProtection.GetBlobData(ref dATA_BLOB3);
					if (blobData != null)
					{
						result = Encoding.ASCII.GetString(blobData);
					}
				}
			}
			catch (Exception expr_91)
			{
				ProjectData.SetProjectError(expr_91);
				ProjectData.ClearProjectError();
			}
			finally
			{
				if (dATA_BLOB.pbData.ToInt32() != 0)
				{
					Marshal.FreeHGlobal(dATA_BLOB.pbData);
				}
				if (dATA_BLOB2.pbData.ToInt32() != 0)
				{
					Marshal.FreeHGlobal(dATA_BLOB2.pbData);
				}
			}
			return result;
		}
		private static void SetBlobData(ref DataProtection.Win32.DATA_BLOB blob, byte[] bits)
		{
			blob.cbData = bits.Length;
			blob.pbData = Marshal.AllocHGlobal(bits.Length);
			Marshal.Copy(bits, 0, blob.pbData, bits.Length);
		}
		private static byte[] GetBlobData(ref DataProtection.Win32.DATA_BLOB blob)
		{
			if (blob.pbData.ToInt32() == 0)
			{
				return null;
			}
			byte[] array = new byte[checked(blob.cbData - 1 + 1)];
			Marshal.Copy(blob.pbData, array, 0, blob.cbData);
			DataProtection.Win32.LocalFree(blob.pbData);
			return array;
		}
	}
}
