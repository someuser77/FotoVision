using Microsoft.VisualBasic.CompilerServices;
using System;
using System.Drawing;
using System.Drawing.Imaging;
using System.Text;
using System.Text.RegularExpressions;
namespace FotoVision
{
	public class Exif
	{
		private enum ExifTag
		{
			Make = 271,
			Model,
			UserComment = 37510,
			ExposureTime = 33434,
			Aperture = 33437,
			ExposureTimeApex = 37377,
			ApertureApex,
			Iso = 34855,
			Flash = 37385,
			ThumbnailFormat = 513,
			ThumbnailLength
		}
		private enum ExifCategory
		{
			Thumbnail = 1
		}
		private string _make;
		private string _model;
		private string _userComment;
		private float _exposureTime;
		private float _aperture;
		private int _iso;
		private bool _flash;
		public string Make
		{
			get
			{
				return this._make;
			}
		}
		public string Model
		{
			get
			{
				return this._model;
			}
		}
		public string UserComment
		{
			get
			{
				return this._userComment;
			}
		}
		public float ExposureTime
		{
			get
			{
				return this._exposureTime;
			}
		}
		public float Aperture
		{
			get
			{
				return this._aperture;
			}
		}
		public int Iso
		{
			get
			{
				return this._iso;
			}
		}
		public bool Flash
		{
			get
			{
				return this._flash;
			}
		}
		public Exif()
		{
			this.Clear();
		}
		public static void Copy(Bitmap sourceImage, Bitmap targetImage)
		{
			if (!Exif.ContainsProperties(sourceImage))
			{
				return;
			}
			checked
			{
				try
				{
					PropertyItem[] propertyItems = sourceImage.PropertyItems;
					for (int i = 0; i < propertyItems.Length; i++)
					{
						PropertyItem propertyItem = propertyItems[i];
						if (propertyItem.Type != 1 && propertyItem.Id != 513 && propertyItem.Id != 514)
						{
							targetImage.SetPropertyItem(propertyItem);
						}
					}
				}
				catch (Exception expr_4E)
				{
					ProjectData.SetProjectError(expr_4E);
					ProjectData.ClearProjectError();
				}
			}
		}
		public void Read(Bitmap image)
		{
			this.Clear();
			if (!Exif.ContainsProperties(image))
			{
				return;
			}
			PropertyItem[] propertyItems = image.PropertyItems;
			checked
			{
				for (int i = 0; i < propertyItems.Length; i++)
				{
					PropertyItem propertyItem = propertyItems[i];
					int id = propertyItem.Id;
					if (id == 271)
					{
						this._make = this.GetAscii(propertyItem.Value);
					}
					else
					{
						if (id == 272)
						{
							this._model = this.GetAscii(propertyItem.Value);
						}
						else
						{
							if (id == 37510)
							{
								this._userComment = this.GetAscii(propertyItem.Value);
							}
							else
							{
								if (id == 33434)
								{
									this._exposureTime = this.GetRational(propertyItem.Value);
								}
								else
								{
									if (id == 37377)
									{
										float rational = this.GetRational(propertyItem.Value);
										this._exposureTime = (float)(1.0 / Math.Pow(2.0, (double)rational));
									}
									else
									{
										if (id == 33437)
										{
											this._aperture = this.GetRational(propertyItem.Value);
										}
										else
										{
											if (id == 37378)
											{
												float rational2 = this.GetRational(propertyItem.Value);
												this._aperture = (float)Math.Pow(2.0, (double)(rational2 / 2f));
											}
											else
											{
												if (id == 34855)
												{
													this._iso = this.GetShort(propertyItem.Value);
												}
												else
												{
													if (id == 37385)
													{
														this._flash = this.GetBoolean(propertyItem.Value);
													}
												}
											}
										}
									}
								}
							}
						}
					}
				}
			}
		}
		private static bool ContainsProperties(Bitmap image)
		{
			bool result;
			try
			{
				if (image.PropertyItems == null)
				{
					result = false;
				}
				else
				{
					result = true;
				}
			}
			catch (Exception expr_10)
			{
				ProjectData.SetProjectError(expr_10);
				result = false;
				ProjectData.ClearProjectError();
			}
			return result;
		}
		private void Clear()
		{
			this._make = "";
			this._model = "";
			this._userComment = "";
			this._exposureTime = 0f;
			this._aperture = 0f;
			this._iso = 0;
			this._flash = false;
		}
		private string GetAscii(byte[] bits)
		{
			string result;
			try
			{
				string text = Encoding.ASCII.GetString(bits, 0, checked(bits.Length - 1)).Trim();
				result = Regex.Replace(text, "[^\\w\\s\\p{P}]", "");
			}
			catch (Exception expr_2D)
			{
				ProjectData.SetProjectError(expr_2D);
				result = "";
				ProjectData.ClearProjectError();
			}
			return result;
		}
		private float GetRational(byte[] bits)
		{
			float result;
			try
			{
				result = (float)((double)BitConverter.ToInt32(bits, 0) / (double)BitConverter.ToInt32(bits, 4));
			}
			catch (Exception expr_15)
			{
				ProjectData.SetProjectError(expr_15);
				result = 0f;
				ProjectData.ClearProjectError();
			}
			return result;
		}
		private int GetShort(byte[] bits)
		{
			int result;
			try
			{
				result = (int)BitConverter.ToInt16(bits, 0);
			}
			catch (Exception expr_0A)
			{
				ProjectData.SetProjectError(expr_0A);
				result = 0;
				ProjectData.ClearProjectError();
			}
			return result;
		}
		private bool GetBoolean(byte[] bits)
		{
			bool result;
			try
			{
				result = (BitConverter.ToInt16(bits, 0) > 0);
			}
			catch (Exception expr_0D)
			{
				ProjectData.SetProjectError(expr_0D);
				result = false;
				ProjectData.ClearProjectError();
			}
			return result;
		}
	}
}
