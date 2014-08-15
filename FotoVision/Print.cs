using Microsoft.VisualBasic;
using Microsoft.VisualBasic.CompilerServices;
using System;
using System.Runtime.CompilerServices;
namespace FotoVision
{
	public sealed class Print
	{
		private class Consts
		{
			public const string DialogProgId = "WIA.CommonDialog";
			public const string VectorProgId = "WIA.Vector";
		}
		private Print()
		{
		}
		public static void PrintFile(string file)
		{
			Print.PrintFiles(new string[]
			{
				file
			});
		}
		public static void PrintFiles(Photo[] photos)
		{
			checked
			{
				string[] array = new string[photos.Length - 1 + 1];
				int arg_1A_0 = 0;
				int num = array.Length - 1;
				for (int i = arg_1A_0; i <= num; i++)
				{
					array[i] = photos[i].PhotoPath;
				}
				Print.PrintFiles(array);
			}
		}
		public static void PrintFiles(string[] files)
		{
			checked
			{
				try
				{
					object objectValue = RuntimeHelpers.GetObjectValue(Interaction.CreateObject("WIA.Vector", ""));
					bool[] array2;
					for (int i = 0; i < files.Length; i++)
					{
						string text = files[i];
						object arg_4D_0 = objectValue;
						Type arg_4D_1 = null;
						string arg_4D_2 = "Add";
						object[] array = new object[]
						{
							text
						};
						object[] arg_4D_3 = array;
						string[] arg_4D_4 = null;
						array2 = new bool[]
						{
							true
						};
						LateBinding.LateCall(arg_4D_0, arg_4D_1, arg_4D_2, arg_4D_3, arg_4D_4, array2);
						if (array2[0])
						{
							text = StringType.FromObject(array[0]);
						}
					}
					object objectValue2 = RuntimeHelpers.GetObjectValue(Interaction.CreateObject("WIA.CommonDialog", ""));
					object arg_B4_0 = objectValue2;
					Type arg_B4_1 = null;
					string arg_B4_2 = "ShowPhotoPrintingWizard";
					object[] array3 = new object[]
					{
						RuntimeHelpers.GetObjectValue(objectValue)
					};
					object[] arg_B4_3 = array3;
					string[] arg_B4_4 = null;
					array2 = new bool[]
					{
						true
					};
					LateBinding.LateCall(arg_B4_0, arg_B4_1, arg_B4_2, arg_B4_3, arg_B4_4, array2);
					if (array2[0])
					{
						objectValue = RuntimeHelpers.GetObjectValue(array3[0]);
					}
				}
				catch (Exception expr_CF)
				{
					ProjectData.SetProjectError(expr_CF);
					Exception ex = expr_CF;
					Global.DisplayError("The photo could not be printed.", ex);
					ProjectData.ClearProjectError();
				}
			}
		}
	}
}
