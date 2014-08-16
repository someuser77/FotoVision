using Microsoft.VisualBasic;
using Microsoft.VisualBasic.CompilerServices;
using System;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.Runtime.CompilerServices;
using System.Windows.Forms;
namespace FotoVision
{
	public class PhotoViewer : Panel
	{
		private class Consts
		{
			public const int FrameSize = 2;
			public const int BorderSpace = 10;
			public static Color FrameColor = Color.FromArgb(240, 237, 219);
			public static Color CropDimColor = Color.FromArgb(100, 128, 128, 128);
            public const InterpolationMode WorkingInterpolationMode = InterpolationMode.Bilinear;
            public const InterpolationMode ViewingInterpolationMode = InterpolationMode.Bilinear;
			public const float WorkingScale = 0.65f;
		}
		public delegate void CropDataChangedEventHandler(object sender, CropDataChangedEventArgs e);
		private PhotoViewer.CropDataChangedEventHandler CropDataChangedEvent;
		private Photo _photo;
		private PhotoInfo _photoInfo;
		private float _scale;
		private Bitmap _startingImage;
		private Bitmap _workingImage;
		private Size _orgPhotoSize;
		private Pen _penFrame;
		private SolidBrush _brushDimCrop;
		private bool _cropMode;
		private bool _editMode;
		private CropHelper _cropHelper;
		private Rectangle _photoBounds;
		public event PhotoViewer.CropDataChangedEventHandler CropDataChanged
		{
			[MethodImpl(32)]
			add
			{
				this.CropDataChangedEvent = (PhotoViewer.CropDataChangedEventHandler)Delegate.Combine(this.CropDataChangedEvent, value);
			}
			[MethodImpl(32)]
			remove
			{
				this.CropDataChangedEvent = (PhotoViewer.CropDataChangedEventHandler)Delegate.Remove(this.CropDataChangedEvent, value);
			}
		}
		[Browsable(false)]
		public PhotoInfo PhotoInfo
		{
			get
			{
				return this._photoInfo;
			}
		}
		[Browsable(false)]
		public Photo Photo
		{
			get
			{
				return this._photo;
			}
			set
			{
				this._photo = value;
				this.ClearActions();
				if (this._photo != null)
				{
					this.CreateImage();
				}
			}
		}
		[Browsable(false)]
		public bool CropMode
		{
			get
			{
				return this._cropMode;
			}
			set
			{
				this._cropMode = value;
				this.Invalidate();
				this.OnCropDataChanged();
			}
		}
		[Browsable(false)]
		public bool EditMode
		{
			get
			{
				return this._editMode;
			}
			set
			{
				this._editMode = value;
				this.Invalidate();
			}
		}
		[Browsable(false)]
		public bool PhotoDirty
		{
			get
			{
				return this.Visible && BooleanType.FromObject(Interaction.IIf(Global.ActionList.Count > 0, true, false));
			}
		}
		[Browsable(false)]
		public Bitmap ImageWithActions
		{
			get
			{
				Bitmap result;
				try
				{
					Cursor.Current = Cursors.WaitCursor;
					Global.Progress.Update(this, "Processing photo", 1, 2);
					Bitmap bitmap = new Bitmap(this._photo.PhotoPath);
					Global.Progress.Complete(this);
					Cursor.Current = Cursors.Default;
					OptimizeActions optimizeActions = new OptimizeActions();
					optimizeActions.Apply(ref bitmap, 0f);
					result = new Bitmap(bitmap);
					bitmap.Dispose();
					bitmap = null;
					Global.Progress.Complete(this);
					Cursor.Current = Cursors.Default;
				}
				catch (Exception expr_7B)
				{
					ProjectData.SetProjectError(expr_7B);
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
				return result;
			}
		}
		public PhotoViewer()
		{
			this._scale = 0f;
			this._cropMode = false;
			this._editMode = false;
            this.SetStyle(ControlStyles.DoubleBuffer | ControlStyles.AllPaintingInWmPaint | ControlStyles.ResizeRedraw | ControlStyles.UserPaint, true);
			this._penFrame = new Pen(PhotoViewer.Consts.FrameColor, 2f);
			this._brushDimCrop = new SolidBrush(PhotoViewer.Consts.CropDimColor);
			this._cropHelper = new CropHelper(this);
		}
		public void ApplyPhotoAction(ActionItem actionItem)
		{
			Global.ActionList.Add(actionItem);
			this.ApplyActionsToWorking();
			this.Invalidate();
		}
		public void SavePhoto()
		{
			try
			{
				Cursor.Current = Cursors.WaitCursor;
				Global.Progress.Update(this, "Processing photo", 1, 2);
				if (FileManager.IsFileReadOnly(this.Photo.PhotoPath))
				{
					throw new ApplicationException(string.Format("The photo '{0}' is read-only.", this._photo.PhotoPath));
				}
				Bitmap bitmap = new Bitmap(this._photo.PhotoPath);
				Bitmap bitmap2 = new Bitmap(bitmap);
				ImageFormat rawFormat = bitmap.RawFormat;
				Global.Progress.Complete(this);
				Cursor.Current = Cursors.Default;
				OptimizeActions optimizeActions = new OptimizeActions();
				optimizeActions.Apply(ref bitmap2, 0f);
				if (Global.Settings.GetBool(SettingKey.MaintainExifInfo))
				{
					Exif.Copy(bitmap, bitmap2);
				}
				bitmap.Dispose();
				bitmap = null;
				Global.Progress.Update(this, "Saving photo", 2, 2);
				bitmap2.Save(this._photo.PhotoPath, rawFormat);
				this.OnNewPhoto(this._photo.PhotoPath, bitmap2, rawFormat);
			}
			catch (Exception expr_E6)
			{
				ProjectData.SetProjectError(expr_E6);
				Exception ex = expr_E6;
				Global.DisplayError(string.Format("The file '{0}' could not be saved.", this._photo.PhotoName), ex);
				ProjectData.ClearProjectError();
			}
			finally
			{
				Bitmap bitmap;
				if (bitmap != null)
				{
					bitmap.Dispose();
				}
				Bitmap bitmap2;
				if (bitmap2 != null)
				{
					bitmap2.Dispose();
				}
			}
			Global.Progress.Complete(this);
			Cursor.Current = Cursors.Default;
		}
		public void ClearCrop()
		{
			this._cropHelper.PhotoBounds = this._photoBounds;
			this.OnCropDataChanged();
			this.Invalidate();
		}
		public void ClearActions()
		{
			Global.ActionList.Clear();
		}
		public void UpdateLocation(string location)
		{
			this.Photo.UpdateLocation(location);
		}
		public void Undo()
		{
			Global.ActionList.RemoveLast();
			this.ApplyActionsToWorking();
			this.Invalidate();
		}
		public void DiscardChanges()
		{
			this.Photo = this._photo;
			this.Invalidate();
		}
		protected override void OnPaint(PaintEventArgs e)
		{
			base.OnPaint(e);
			if (this.DesignMode || this._workingImage == null)
			{
				e.Graphics.Clear(this.BackColor);
				return;
			}
			this.DrawPhoto(e.Graphics);
			this.DrawCrop(e.Graphics);
		}
		protected override void OnPaintBackground(PaintEventArgs pevent)
		{
		}
		private void DrawPhoto(Graphics g)
		{
			g.Clear(this.BackColor);
			Rectangle displayRectangle = this.DisplayRectangle;
			displayRectangle.Inflate(-10, -10);
			int num = Math.Max(displayRectangle.Width, 0);
			int num2 = Math.Max(displayRectangle.Height, 0);
			float num3 = Math.Max((float)((double)this._workingImage.Width / (double)num), (float)((double)this._workingImage.Height / (double)num2));
			checked
			{
				int num4 = (int)Math.Round((double)((float)this._workingImage.Width / num3));
				int num5 = (int)Math.Round((double)((float)this._workingImage.Height / num3));
				int num6 = (this.Width - num4) / 2;
				int num7 = (this.Height - num5) / 2;
				g.InterpolationMode = 3;
				Rectangle rectangle = new Rectangle(num6, num7, num4, num5);
				g.DrawImage(this._workingImage, rectangle, 0, 0, this._workingImage.Width, this._workingImage.Height, 2);
				g.DrawRectangle(this._penFrame, rectangle);
				if (!this._photoBounds.Equals(rectangle))
				{
					this._photoBounds = rectangle;
					this._cropHelper.PhotoBounds = rectangle;
					this.OnCropDataChanged();
				}
			}
		}
		private void CreateImage()
		{
			try
			{
				Cursor.Current = Cursors.WaitCursor;
				Global.Progress.Update(this, "Loading photo", 1, 2);
				Bitmap bitmap = new Bitmap(this._photo.PhotoPath);
				Global.Progress.Update(this, "Loading photo", 2, 2);
				this.OnNewPhoto(this._photo.PhotoPath, bitmap, bitmap.RawFormat);
			}
			catch (Exception expr_59)
			{
				ProjectData.SetProjectError(expr_59);
				Exception ex = expr_59;
				Global.DisplayError(string.Format("The photo '{0}' could not be opened.", this._photo.PhotoName), ex);
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
			Global.Progress.Complete(this);
			Cursor.Current = Cursors.Default;
		}
		private void OnNewPhoto(string path, Bitmap srcImage, ImageFormat format)
		{
			this.CreateWorkingImage(srcImage);
			this.ClearActions();
			this._cropHelper.PhotoBounds = this._photoBounds;
			this._orgPhotoSize = srcImage.Size;
			this._cropHelper.OriginalPhotoSize = this._orgPhotoSize;
			this._photoInfo.Read(path, srcImage, format);
			this.OnCropDataChanged();
		}
		private void CreateWorkingImage(Image srcImage)
		{
			if (this._startingImage != null)
			{
				this._startingImage.Dispose();
				this._startingImage = null;
			}
			if (this._workingImage != null)
			{
				this._workingImage.Dispose();
				this._workingImage = null;
			}
			checked
			{
				int num = (int)Math.Round((double)unchecked((float)Math.Max(Screen.PrimaryScreen.WorkingArea.Width, Screen.PrimaryScreen.WorkingArea.Height) * 0.65f));
				if (srcImage.Width <= num & srcImage.Height <= num)
				{
					this._scale = 0f;
					this._startingImage = new Bitmap(srcImage);
				}
				else
				{
					this._scale = (float)Math.Max(srcImage.Width, srcImage.Height) / (float)num;
					int num2 = (int)Math.Round((double)((float)srcImage.Width / this._scale));
					int num3 = (int)Math.Round((double)((float)srcImage.Height / this._scale));
					this._startingImage = new Bitmap(num2, num3);
					Graphics graphics = Graphics.FromImage(this._startingImage);
					try
					{
						graphics.InterpolationMode = 3;
						Rectangle rectangle = new Rectangle(0, 0, num2, num3);
						graphics.DrawImage(srcImage, rectangle, 0, 0, srcImage.Width, srcImage.Height, 2);
					}
					finally
					{
						graphics.Dispose();
					}
				}
				this._workingImage = new Bitmap(this._startingImage);
			}
		}
		private void ApplyActionsToWorking()
		{
			if (this._workingImage != null)
			{
				this._workingImage.Dispose();
			}
			this._workingImage = new Bitmap(this._startingImage);
			OptimizeActions optimizeActions = new OptimizeActions();
			optimizeActions.Apply(ref this._workingImage, this._scale);
			this._cropHelper.PhotoBounds = this._photoBounds;
			this._cropHelper.OriginalPhotoSize = this._orgPhotoSize;
			int arg_6D_0 = 0;
			checked
			{
				int num = Global.ActionList.Count - 1;
				for (int i = arg_6D_0; i <= num; i++)
				{
					ActionItem at = Global.ActionList.GetAt(i);
					switch (at.Action)
					{
					case PhotoAction.RotateLeft:
						this._cropHelper.OriginalPhotoRotated();
						break;
					case PhotoAction.RotateRight:
						this._cropHelper.OriginalPhotoRotated();
						break;
					case PhotoAction.Crop:
						this._cropHelper.OriginalPhotoSize = at.Bounds.Size;
						break;
					}
				}
				this.OnCropDataChanged();
			}
		}
		protected override void OnMouseDown(MouseEventArgs e)
		{
			if (!this._cropMode | !this._editMode)
			{
				this.Focus();
				base.OnMouseDown(e);
				return;
			}
			base.OnMouseDown(e);
			if (Control.MouseButtons != 1048576)
			{
				return;
			}
			this.Capture = true;
			this.Invalidate();
			this.Update();
			this._cropHelper.MouseDown(e.X, e.Y);
		}
		protected override void OnMouseMove(MouseEventArgs e)
		{
			base.OnMouseMove(e);
			if (!this._cropMode | !this._editMode)
			{
				return;
			}
			if (this.Capture)
			{
				this._cropHelper.MouseMove(e.X, e.Y);
				this.OnCropDataChanged();
			}
			else
			{
				this._cropHelper.SetCursor(e.X, e.Y);
			}
		}
		protected override void OnMouseUp(MouseEventArgs e)
		{
			base.OnMouseUp(e);
			if (!this._cropMode | !this._editMode)
			{
				return;
			}
			this.Capture = false;
			this._cropHelper.MouseUp(e.X, e.Y);
			this.Invalidate();
		}
		private void OnCropDataChanged()
		{
			Rectangle originalPhotoSelectedArea = this._cropHelper.OriginalPhotoSelectedArea;
			if (this.CropDataChangedEvent != null)
			{
				this.CropDataChangedEvent(this, new CropDataChangedEventArgs(this._cropHelper.OriginalPhotoSize, originalPhotoSelectedArea.Size, originalPhotoSelectedArea));
			}
		}
		private void DrawCrop(Graphics g)
		{
			if (this.Capture | !this._cropMode | !this._editMode)
			{
				return;
			}
			if (this._cropHelper.Empty)
			{
				return;
			}
			Rectangle selectedArea = this._cropHelper.SelectedArea;
			Region region = new Region(this._photoBounds);
			region.Exclude(selectedArea);
			g.FillRegion(this._brushDimCrop, region);
			region.Dispose();
			ControlPaint.DrawFocusRectangle(g, this._cropHelper.SelectedArea, Color.White, Color.Black);
		}
	}
}
