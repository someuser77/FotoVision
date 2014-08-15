using Microsoft.VisualBasic.CompilerServices;
using System;
using System.Globalization;
using System.IO;
using System.Windows.Forms;
namespace FotoVision
{
	public sealed class Global
	{
		private static Progress _progress = new Progress();
		private static bool _busy = false;
		private static bool _performingDrag;
		private static bool _publishingFiles = false;
		private static ActionList _actionList = new ActionList();
		private static SliderValues _sliderValues;
		private static Settings _settings = new Settings(SettingDefaults.Values);
		public static string DataLocation
		{
			get
			{
				return Path.Combine(Environment.GetFolderPath(5), Application.ProductName);
			}
		}
		public static Settings Settings
		{
			get
			{
				return Global._settings;
			}
		}
		public static Progress Progress
		{
			get
			{
				return Global._progress;
			}
		}
		public static bool Busy
		{
			get
			{
				return Global._busy;
			}
			set
			{
				Global._busy = value;
			}
		}
		public static bool PublishingFiles
		{
			get
			{
				return Global._publishingFiles;
			}
			set
			{
				Global._publishingFiles = value;
			}
		}
		public static bool PerformingDrag
		{
			get
			{
				return Global._performingDrag;
			}
			set
			{
				Global._performingDrag = value;
			}
		}
		public static ActionList ActionList
		{
			get
			{
				return Global._actionList;
			}
		}
		public static SliderValues SliderValues
		{
			get
			{
				return Global._sliderValues;
			}
		}
		private Global()
		{
		}
		public static void SetSliderValues(int contrast, int brightness, int gamma, int saturation)
		{
			Global._sliderValues.Contrast = contrast;
			Global._sliderValues.Brightness = brightness;
			Global._sliderValues.Gamma = gamma;
			Global._sliderValues.Saturation = saturation;
		}
		public static bool ValidateDate(string value)
		{
			bool result;
			try
			{
				DateTime.Parse(value, CultureInfo.CurrentCulture);
				result = true;
			}
			catch (Exception expr_10)
			{
				ProjectData.SetProjectError(expr_10);
				result = false;
				ProjectData.ClearProjectError();
			}
			return result;
		}
		public static string CombineUrl(string baseUrl, string relativeUrl)
		{
			if (baseUrl == null || baseUrl.Length == 0)
			{
				return "";
			}
			if (!baseUrl.EndsWith("/") && !baseUrl.EndsWith("\\"))
			{
				baseUrl += "/";
			}
			Uri uri = new Uri(baseUrl);
			Uri uri2 = new Uri(uri, relativeUrl);
			return uri2.AbsoluteUri;
		}
		public static void DisplayError(string message, Exception ex)
		{
			Cursor.set_Current(Cursors.Default);
			ErrorForm errorForm = new ErrorForm(message, ex);
			errorForm.ShowDialog();
		}
	}
}
