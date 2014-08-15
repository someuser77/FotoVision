using Microsoft.VisualBasic.CompilerServices;
using System;
using System.Collections;
using System.IO;
using System.Runtime.CompilerServices;
using System.Windows.Forms;
using System.Xml;
namespace FotoVision
{
	public class Settings
	{
		private Hashtable _list;
		private string _filePath;
		private bool _autoWrite;
		private string[][] _defaultValues;
		public bool AutoWrite
		{
			get
			{
				return this._autoWrite;
			}
			set
			{
				this._autoWrite = value;
			}
		}
		public string FilePath
		{
			get
			{
				string directoryName = Path.GetDirectoryName(this._filePath);
				if (!Directory.Exists(directoryName))
				{
					Directory.CreateDirectory(directoryName);
				}
				return this._filePath;
			}
			set
			{
				this._filePath = value;
			}
		}
		public Settings()
		{
			this._list = new Hashtable();
			this._filePath = "";
			this._autoWrite = true;
			this.InitFilePath();
			this.Read();
		}
		public Settings(string[][] defaultValues)
		{
			this._list = new Hashtable();
			this._filePath = "";
			this._autoWrite = true;
			this._defaultValues = defaultValues;
			this.InitFilePath();
			this.Read();
		}
		public void SetValue(SettingKey key, object value)
		{
			this._list.Item = key.ToString(), RuntimeHelpers.GetObjectValue(value);
			if (this._autoWrite)
			{
				this.Write();
			}
		}
		public string GetString(SettingKey key)
		{
			object objectValue = RuntimeHelpers.GetObjectValue(this._list.Item(key.ToString));
			if (objectValue == null)
			{
				return "";
			}
			return objectValue.ToString();
		}
		public int GetInt(SettingKey key)
		{
			string @string = this.GetString(key);
			if (StringType.StrCmp(@string, "", false) == 0)
			{
				return 0;
			}
			return IntegerType.FromString(@string);
		}
		public bool GetBool(SettingKey key)
		{
			string @string = this.GetString(key);
			return StringType.StrCmp(@string, "", false) != 0 && BooleanType.FromString(@string);
		}
		public void Read()
		{
			this._list.Clear();
			int arg_1B_0 = 0;
			checked
			{
				int num = this._defaultValues.GetLength(0) - 1;
				for (int i = arg_1B_0; i <= num; i++)
				{
					this._list.Item = this._defaultValues[i][0], this._defaultValues[i][1];
				}
				if (File.Exists(this.FilePath))
				{
					XmlTextReader xmlTextReader = new XmlTextReader(this.FilePath);
					while (xmlTextReader.Read())
					{
						if (xmlTextReader.NodeType == 1 & StringType.StrCmp(xmlTextReader.Name, "add", false) == 0)
						{
							this._list.Item = xmlTextReader.GetAttribute("key"), xmlTextReader.GetAttribute("value");
						}
					}
					xmlTextReader.Close();
				}
			}
		}
		public void Write()
		{
			XmlTextWriter xmlTextWriter = new XmlTextWriter(this.FilePath, null);
			xmlTextWriter.Formatting = 1;
			xmlTextWriter.WriteStartElement("configuration");
			xmlTextWriter.WriteStartElement("appSettings");
			IDictionaryEnumerator enumerator = this._list.GetEnumerator();
			while (enumerator.MoveNext())
			{
				xmlTextWriter.WriteStartElement("add");
				xmlTextWriter.WriteStartAttribute("key", null);
				xmlTextWriter.WriteString(enumerator.Key.ToString());
				xmlTextWriter.WriteEndAttribute();
				xmlTextWriter.WriteStartAttribute("value", null);
				xmlTextWriter.WriteString(enumerator.Value.ToString());
				xmlTextWriter.WriteEndAttribute();
				xmlTextWriter.WriteEndElement();
			}
			xmlTextWriter.WriteEndElement();
			xmlTextWriter.WriteEndElement();
			xmlTextWriter.Flush();
			xmlTextWriter.Close();
		}
		public void RestoreDefaults()
		{
			try
			{
				File.Delete(this.FilePath);
				this.Read();
			}
			catch (Exception expr_13)
			{
				ProjectData.SetProjectError(expr_13);
				ProjectData.ClearProjectError();
			}
		}
		private void InitFilePath()
		{
			string[] array = Application.ProductVersion.Split(new char[]
			{
				'.'
			});
			string text = string.Format("{0}\\{1}.{2}", Application.ProductName, array[0], array[1]);
			string text2 = Path.Combine(Environment.GetFolderPath(26), text);
			if (!Directory.Exists(text2))
			{
				Directory.CreateDirectory(text2);
			}
			this.FilePath = Path.Combine(text2, Path.GetFileName(Application.ExecutablePath) + ".config");
		}
	}
}
