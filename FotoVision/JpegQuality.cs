using Microsoft.VisualBasic.CompilerServices;
using System;
using System.Drawing;
using System.Drawing.Imaging;
namespace FotoVision
{
	public sealed class JpegQuality
	{
		private static ImageCodecInfo _codec;
		private static ImageCodecInfo Codec
		{
			get
			{
				if (JpegQuality._codec != null)
				{
					return JpegQuality._codec;
				}
				ImageCodecInfo[] imageDecoders = ImageCodecInfo.GetImageDecoders();
				ImageCodecInfo[] array = imageDecoders;
				checked
				{
					for (int i = 0; i < array.Length; i++)
					{
						ImageCodecInfo imageCodecInfo = array[i];
						if (StringType.StrCmp(imageCodecInfo.MimeType, "image/jpeg", false) == 0)
						{
							JpegQuality._codec = imageCodecInfo;
							return JpegQuality._codec;
						}
					}
					return null;
				}
			}
		}
		private JpegQuality()
		{
		}
		public static void Save(string imagePath, Bitmap image, int quality)
		{
			if (JpegQuality.Codec == null)
			{
				image.Save(imagePath, ImageFormat.Jpeg);
			}
			else
			{
				EncoderParameters encoderParameters = new EncoderParameters();
				encoderParameters.Param[0] = new EncoderParameter(Encoder.Quality, (long)quality);
				image.Save(imagePath, JpegQuality.Codec, encoderParameters);
				encoderParameters.Dispose();
			}
		}
	}
}
