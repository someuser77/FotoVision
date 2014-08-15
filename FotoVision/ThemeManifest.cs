using Microsoft.VisualBasic.CompilerServices;
using System;
using System.IO;
using System.Reflection;
using System.Windows.Forms;
namespace FotoVision
{
	public sealed class ThemeManifest
	{
		private class Consts
		{
			public const string ResourceName = "FotoVision.ThemeManifest.xml";
		}
		private ThemeManifest()
		{
		}
		public static bool Create()
		{
			string text = Application.get_ExecutablePath() + ".manifest";
			if (File.Exists(text))
			{
				return false;
			}
			bool result;
			try
			{
				Assembly executingAssembly = Assembly.GetExecutingAssembly();
				TextReader textReader = new StreamReader(executingAssembly.GetManifestResourceStream("FotoVision.ThemeManifest.xml"));
				string text2 = textReader.ReadToEnd();
				textReader.Close();
				StreamWriter streamWriter = new StreamWriter(text);
				streamWriter.Write(text2);
				streamWriter.Close();
				result = true;
			}
			catch (Exception expr_5B)
			{
				ProjectData.SetProjectError(expr_5B);
				result = false;
				ProjectData.ClearProjectError();
			}
			return result;
		}
	}
}
