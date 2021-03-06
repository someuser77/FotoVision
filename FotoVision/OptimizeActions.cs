using Microsoft.VisualBasic.CompilerServices;
using System;
using System.Drawing;
using System.Windows.Forms;
namespace FotoVision
{
	public class OptimizeActions
	{
		private enum ActionType
		{
			None,
			Rotate,
			FlipHorizontal,
			FlipVertical,
			Crop
		}
		private enum ConvertColor
		{
			None,
			Grayscale,
			Sepia
		}
		private OptimizeActions.ActionType _curType;
		private int _rotate;
		private bool _flipHorz;
		private bool _flipVert;
		private Rectangle _crop;
		public OptimizeActions()
		{
			this._curType = OptimizeActions.ActionType.None;
			this.Clear();
		}
		public void Apply(ref Bitmap image, float scale)
		{
			if (Global.ActionList.Count == 0)
			{
				return;
			}
			Cursor.Current = Cursors.WaitCursor;
			this.AdjustDimensions(ref image, scale);
			this.AdjustColor(image);
			Global.Progress.Complete(this);
			Cursor.Current = Cursors.Default;
		}
		private void AdjustColor(Bitmap image)
		{
			int arg_0F_0 = 0;
			checked
			{
				int num = Global.ActionList.Count - 1;
                OptimizeActions.ConvertColor convertColor = OptimizeActions.ConvertColor.None;
				int percent = 0;
                int percent2 = 0;
                int percent3 = 0;
                int percent4 = 0;
				for (int i = arg_0F_0; i <= num; i++)
				{
					ActionItem at = Global.ActionList.GetAt(i);
					switch (at.Action)
					{
					case PhotoAction.ConvertSepia:
						convertColor = OptimizeActions.ConvertColor.Sepia;
						break;
					case PhotoAction.ConvertGrayscale:
						convertColor = OptimizeActions.ConvertColor.Grayscale;
						break;
					case PhotoAction.Brightness:
						percent = at.Percent;
						break;
					case PhotoAction.Contrast:
						percent2 = at.Percent;
						break;
					case PhotoAction.Gamma:
						percent3 = at.Percent;
						break;
					case PhotoAction.Saturation:
						percent4 = at.Percent;
						break;
					}
				}
				Global.Progress.Update(this, "Applying actions", 1, Global.ActionList.Count);
				if (convertColor != OptimizeActions.ConvertColor.None || percent2 != 0 || percent != 0 || percent4 != 0)
				{
					float[][] array = new float[][]
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
							0f,
							0f,
							0f,
							0f,
							1f
						}
					};
					if (convertColor == OptimizeActions.ConvertColor.Grayscale)
					{
						array = PhotoHelper.CombineMatrix(array, PhotoHelper.GetGrayscaleMatrix());
					}
					if (convertColor == OptimizeActions.ConvertColor.Sepia)
					{
						array = PhotoHelper.CombineMatrix(array, PhotoHelper.GetSepiaMatrix());
					}
					if (percent2 != 0)
					{
						array = PhotoHelper.CombineMatrix(array, PhotoHelper.GetContrastMatrix(percent2));
					}
					if (percent != 0)
					{
						array = PhotoHelper.CombineMatrix(array, PhotoHelper.GetBrightnessMatrix(percent));
					}
					if (percent4 != 0)
					{
						array = PhotoHelper.CombineMatrix(array, PhotoHelper.GetSaturationMatrix(percent4));
					}
					PhotoHelper.AdjustUsingCustomMatrix(image, array);
				}
				if (percent3 != 0)
				{
					PhotoHelper.AdjustGamma(image, percent3);
				}
			}
		}
		private void AdjustDimensions(ref Bitmap image, float scale)
		{
			int arg_0E_0 = 0;
			checked
			{
				int num = Global.ActionList.Count - 1;
				for (int i = arg_0E_0; i <= num; i++)
				{
					Global.Progress.Update(this, "Applying actions", i + 1, Global.ActionList.Count);
					ActionItem at = Global.ActionList.GetAt(i);
					switch (at.Action)
					{
					case PhotoAction.RotateLeft:
						this.CheckCurrentType(ref image, OptimizeActions.ActionType.Rotate);
						this._rotate--;
						break;
					case PhotoAction.RotateRight:
						this.CheckCurrentType(ref image, OptimizeActions.ActionType.Rotate);
						this._rotate++;
						break;
					case PhotoAction.FlipHorizontal:
						this.CheckCurrentType(ref image, OptimizeActions.ActionType.FlipHorizontal);
						this._flipHorz = !this._flipHorz;
						break;
					case PhotoAction.FlipVertical:
						this.CheckCurrentType(ref image, OptimizeActions.ActionType.FlipVertical);
						this._flipVert = !this._flipVert;
						break;
					case PhotoAction.Crop:
						this.ApplyType(ref image);
						this._curType = OptimizeActions.ActionType.Crop;
						this._crop = at.Bounds;
						if (scale > 0f)
						{
							this._crop.X = (int)Math.Round((double)((float)this._crop.X / scale));
							this._crop.Y = (int)Math.Round((double)((float)this._crop.Y / scale));
							this._crop.Width = (int)Math.Round((double)((float)this._crop.Width / scale));
							this._crop.Height = (int)Math.Round((double)((float)this._crop.Height / scale));
						}
						break;
					}
				}
				this.ApplyType(ref image);
			}
		}
		private void CheckCurrentType(ref Bitmap image, OptimizeActions.ActionType type)
		{
			if (this._curType != type)
			{
				this.ApplyType(ref image);
				this._curType = type;
			}
		}
		private void ApplyType(ref Bitmap image)
		{
            Bitmap bitmap = null;
			if (this._curType == OptimizeActions.ActionType.None)
			{
				return;
			}
			try
			{
				switch (this._curType)
				{
				case OptimizeActions.ActionType.Rotate:
				{
					RotateFlipType rotateFlipType = this.GetRotateFlipType();
					if (rotateFlipType != 0)
					{
						bitmap = new Bitmap(image);
						PhotoHelper.Rotate(bitmap, rotateFlipType);
						image.Dispose();
						image = new Bitmap(bitmap);
					}
					break;
				}
				case OptimizeActions.ActionType.FlipHorizontal:
					if (this._flipHorz)
					{
                        PhotoHelper.Rotate(image, System.Drawing.RotateFlipType.RotateNoneFlipX);
					}
					break;
				case OptimizeActions.ActionType.FlipVertical:
					if (this._flipVert)
					{
						PhotoHelper.Rotate(image, System.Drawing.RotateFlipType.RotateNoneFlipY);
					}
					break;
				case OptimizeActions.ActionType.Crop:
				{
					bitmap = PhotoHelper.Crop(image, this._crop);
					image.Dispose();
					image = new Bitmap(bitmap);
					break;
				}
				}
			}
			catch (Exception expr_9A)
			{
				ProjectData.SetProjectError(expr_9A);
				ProjectData.ClearProjectError();
			}
			finally
			{
				if (bitmap != null)
				{
					bitmap.Dispose();
				}
			}
			this.Clear();
		}
		private RotateFlipType GetRotateFlipType()
		{
			int num = this._rotate % 4;
			checked
			{
				if (num < 0)
				{
					num += 4;
				}
				switch (num)
				{
				case 0:
                    return RotateFlipType.RotateNoneFlipNone;
				case 1:
                    return RotateFlipType.Rotate90FlipNone;
				case 2:
                    return RotateFlipType.Rotate180FlipNone;
				case 3:
                    return RotateFlipType.Rotate270FlipNone;
				default:
                    return RotateFlipType.RotateNoneFlipNone;
				}
			}
		}
		private void Clear()
		{
			this._curType = OptimizeActions.ActionType.None;
			this._rotate = 0;
			this._flipHorz = false;
			this._flipVert = false;
			this._crop = (Rectangle)(null ?? Activator.CreateInstance(typeof(Rectangle)));
		}
	}
}
