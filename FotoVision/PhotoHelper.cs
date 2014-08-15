using Microsoft.VisualBasic;
using Microsoft.VisualBasic.CompilerServices;
using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
namespace FotoVision
{
	public sealed class PhotoHelper
	{
		private class Consts
		{
			public const float GrayRed = 0.3086f;
			public const float GrayGreen = 0.6094f;
			public const float GrayBlue = 0.082f;
			public const float SepiaRed = 0.2f;
			public const float SepiaGreen = 0.14f;
			public const float SepiaBlue = 0.08f;
			public const InterpolationMode ResizeQuality = 7;
		}
		private PhotoHelper()
		{
		}
		public static void AdjustBrightness(Bitmap image, int percent)
		{
			PhotoHelper.DrawImage(image, PhotoHelper.GetBrightnessMatrix(percent));
		}
		public static void AdjustContrast(Bitmap image, int percent)
		{
			PhotoHelper.DrawImage(image, PhotoHelper.GetContrastMatrix(percent));
		}
		public static void AdjustSaturation(Bitmap image, int percent)
		{
			PhotoHelper.DrawImage(image, PhotoHelper.GetSaturationMatrix(percent));
		}
		public static void ConvertToGrayscale(Bitmap image)
		{
			PhotoHelper.DrawImage(image, PhotoHelper.GetGrayscaleMatrix());
		}
		public static void ConvertToSepia(Bitmap image)
		{
			PhotoHelper.DrawImage(image, PhotoHelper.GetSepiaMatrix());
		}
		public static void AdjustUsingCustomMatrix(Bitmap image, float[][] matrix)
		{
			PhotoHelper.DrawImage(image, matrix);
		}
		public static float[][] GetBrightnessMatrix(int percent)
		{
			float num = 0.006f * (float)percent;
			return new float[][]
			{
				new float[]
				{
					1f,
					0f,
					0f,
					0f,
					0f
				},
				new float[]
				{
					0f,
					1f,
					0f,
					0f,
					0f
				},
				new float[]
				{
					0f,
					0f,
					1f,
					0f,
					0f
				},
				new float[]
				{
					0f,
					0f,
					0f,
					1f,
					0f
				},
				new float[]
				{
					num,
					num,
					num,
					0f,
					1f
				}
			};
		}
		public static float[][] GetContrastMatrix(int percent)
		{
			float num;
			if (percent > 0)
			{
				num = 0.0195f * (float)percent;
				num *= num;
			}
			else
			{
				num = 0.009f * (float)percent;
			}
			float num2 = 1f + num;
			float num3 = num / 2f;
			return new float[][]
			{
				new float[]
				{
					num2,
					0f,
					0f,
					0f,
					0f
				},
				new float[]
				{
					0f,
					num2,
					0f,
					0f,
					0f
				},
				new float[]
				{
					0f,
					0f,
					num2,
					0f,
					0f
				},
				new float[]
				{
					0f,
					0f,
					0f,
					1f,
					0f
				},
				new float[]
				{
					-num3,
					-num3,
					-num3,
					0f,
					1f
				}
			};
		}
		public static float[][] GetSaturationMatrix(int percent)
		{
			float num;
			if (percent > 0)
			{
				num = 1f + 0.015f * (float)percent;
			}
			else
			{
				num = 1f - 0.0075f * (float)checked(0 - percent);
			}
			float num2 = (1f - num) * 0.3086f;
			float num3 = (1f - num) * 0.6094f;
			float num4 = (1f - num) * 0.082f;
			return new float[][]
			{
				new float[]
				{
					num2 + num,
					num2,
					num2,
					0f,
					0f
				},
				new float[]
				{
					num3,
					num3 + num,
					num3,
					0f,
					0f
				},
				new float[]
				{
					num4,
					num4,
					num4 + num,
					0f,
					0f
				},
				new float[]
				{
					0f,
					0f,
					0f,
					1f,
					0f
				},
				new float[]
				{
					0f,
					0f,
					0f,
					0f,
					1f
				}
			};
		}
		public static float[][] GetGrayscaleMatrix()
		{
			return new float[][]
			{
				new float[]
				{
					0.3086f,
					0.3086f,
					0.3086f,
					0f,
					0f
				},
				new float[]
				{
					0.6094f,
					0.6094f,
					0.6094f,
					0f,
					0f
				},
				new float[]
				{
					0.082f,
					0.082f,
					0.082f,
					0f,
					0f
				},
				new float[]
				{
					0f,
					0f,
					0f,
					1f,
					0f
				},
				new float[]
				{
					0f,
					0f,
					0f,
					0f,
					1f
				}
			};
		}
		public static float[][] GetSepiaMatrix()
		{
			float[][] brightnessMatrix = PhotoHelper.GetBrightnessMatrix(-25);
			float[][] m = new float[][]
			{
				new float[]
				{
					0.3086f,
					0.3086f,
					0.3086f,
					0f,
					0f
				},
				new float[]
				{
					0.6094f,
					0.6094f,
					0.6094f,
					0f,
					0f
				},
				new float[]
				{
					0.082f,
					0.082f,
					0.082f,
					0f,
					0f
				},
				new float[]
				{
					0f,
					0f,
					0f,
					1f,
					0f
				},
				new float[]
				{
					0.2f,
					0.14f,
					0.08f,
					0f,
					1f
				}
			};
			return PhotoHelper.CombineMatrix(brightnessMatrix, m);
		}
		public static float[][] CombineMatrix(float[][] m1, float[][] m2)
		{
			float[][] array = new float[][]
			{
				new float[]
				{
					0f,
					0f,
					0f,
					0f,
					0f
				},
				new float[]
				{
					0f,
					0f,
					0f,
					0f,
					0f
				},
				new float[]
				{
					0f,
					0f,
					0f,
					0f,
					0f
				},
				new float[]
				{
					0f,
					0f,
					0f,
					0f,
					0f
				},
				new float[]
				{
					0f,
					0f,
					0f,
					0f,
					0f
				}
			};
			int num = 0;
			checked
			{
				do
				{
					int num2 = 0;
					do
					{
						array[num2][num] = unchecked(m1[num2][0] * m2[0][num] + m1[num2][1] * m2[1][num] + m1[num2][2] * m2[2][num] + m1[num2][3] * m2[3][num] + m1[num2][4] * m2[4][num]);
						num2++;
					}
					while (num2 <= 4);
					num++;
				}
				while (num <= 4);
				return array;
			}
		}
		public static void AdjustGamma(Bitmap image, int percent)
		{
			ImageAttributes imageAttributes = new ImageAttributes();
			try
			{
				imageAttributes.SetGamma(PhotoHelper.GetGamma(percent));
				PhotoHelper.DrawImage(image, imageAttributes);
			}
			finally
			{
				imageAttributes.Dispose();
			}
		}
		public static float GetGamma(int percent)
		{
			float result;
			if (percent > 0)
			{
				result = 1f - 0.0085f * (float)percent;
			}
			else
			{
				result = 1f + 0.025f * (float)checked(0 - percent);
			}
			return result;
		}
		public static void Rotate(Bitmap image, RotateFlipType flipType)
		{
			image.RotateFlip(flipType);
		}
		public static Bitmap Crop(Bitmap image, Rectangle cropArea)
		{
			Bitmap bitmap = new Bitmap(cropArea.get_Width(), cropArea.get_Height());
			Graphics graphics = Graphics.FromImage(bitmap);
			try
			{
				Rectangle rectangle = new Rectangle(0, 0, bitmap.get_Width(), bitmap.get_Height());
				graphics.DrawImage(image, rectangle, cropArea, 2);
			}
			finally
			{
				graphics.Dispose();
			}
			return bitmap;
		}
		public static Bitmap Resize(string imagePath, int longestSide)
		{
			checked
			{
				Bitmap bitmap2;
				try
				{
					Bitmap bitmap = new Bitmap(imagePath);
					float num = SingleType.FromObject(Interaction.IIf(bitmap.get_Width() > bitmap.get_Height(), (double)longestSide / (double)bitmap.get_Width(), (double)longestSide / (double)bitmap.get_Height()));
					int num2 = (int)Math.Round((double)unchecked((float)bitmap.get_Width() * num));
					int num3 = (int)Math.Round((double)unchecked((float)bitmap.get_Height() * num));
					bitmap2 = new Bitmap(num2, num3);
					Graphics graphics = Graphics.FromImage(bitmap2);
					graphics.set_InterpolationMode(7);
					Rectangle rectangle = new Rectangle(0, 0, num2, num3);
					graphics.DrawImage(bitmap, rectangle, 0, 0, bitmap.get_Width(), bitmap.get_Height(), 2);
					graphics.Dispose();
				}
				catch (Exception expr_AC)
				{
					ProjectData.SetProjectError(expr_AC);
					ProjectData.ClearProjectError();
				}
				finally
				{
					Bitmap bitmap;
					if (bitmap != null)
					{
						bitmap.Dispose();
					}
				}
				return bitmap2;
			}
		}
		public static Image GetThumbnail(string imagePath, int longestSide)
		{
			Image thumbnail;
			try
			{
				Bitmap bitmap = new Bitmap(imagePath);
				thumbnail = PhotoHelper.GetThumbnail(bitmap, longestSide);
			}
			catch (Exception expr_11)
			{
				ProjectData.SetProjectError(expr_11);
				ProjectData.ClearProjectError();
			}
			finally
			{
				Bitmap bitmap;
				if (bitmap != null)
				{
					bitmap.Dispose();
				}
			}
			return thumbnail;
		}
		public static Image GetThumbnail(Bitmap image, int longestSide)
		{
			checked
			{
				Image thumbnailImage;
				try
				{
					float num = SingleType.FromObject(Interaction.IIf(image.get_Width() > image.get_Height(), (double)longestSide / (double)image.get_Width(), (double)longestSide / (double)image.get_Height()));
					int num2 = (int)Math.Round((double)unchecked((float)image.get_Width() * num));
					int num3 = (int)Math.Round((double)unchecked((float)image.get_Height() * num));
					thumbnailImage = image.GetThumbnailImage(num2, num3, null, IntPtr.Zero);
				}
				catch (Exception expr_6E)
				{
					ProjectData.SetProjectError(expr_6E);
					ProjectData.ClearProjectError();
				}
				return thumbnailImage;
			}
		}
		private static void DrawImage(Bitmap image, float[][] matrix)
		{
			ColorMatrix colorMatrix = new ColorMatrix(matrix);
			ImageAttributes imageAttributes = new ImageAttributes();
			try
			{
				imageAttributes.SetColorMatrix(colorMatrix);
				PhotoHelper.DrawImage(image, imageAttributes);
			}
			finally
			{
				imageAttributes.Dispose();
			}
		}
		private static void DrawImage(Bitmap image, ImageAttributes attr)
		{
			Graphics graphics = Graphics.FromImage(image);
			try
			{
				Rectangle rectangle = new Rectangle(0, 0, image.get_Width(), image.get_Height());
				graphics.DrawImage(image, rectangle, 0, 0, image.get_Width(), image.get_Height(), 2, attr);
			}
			finally
			{
				graphics.Dispose();
			}
		}
	}
}
