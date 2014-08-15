using Microsoft.VisualBasic;
using Microsoft.VisualBasic.CompilerServices;
using System;
using System.Drawing;
using System.Windows.Forms;
namespace FotoVision
{
	public class CropHelper
	{
		private enum DragMode
		{
			None,
			Move,
			TopLeft,
			Top,
			TopRight,
			Right,
			BottomRight,
			Bottom,
			BottomLeft,
			Left
		}
		private static Cursor[] CursorLookup = new Cursor[]
		{
			Cursors.Cross,
			Cursors.SizeAll,
			Cursors.SizeNWSE,
			Cursors.SizeNS,
			Cursors.SizeNESW,
			Cursors.SizeWE,
			Cursors.SizeNWSE,
			Cursors.SizeNS,
			Cursors.SizeNESW,
			Cursors.SizeWE
		};
		private bool _mouseCaptured;
		private Point _originalPoint;
		private Point _lastPoint;
		private Rectangle _selArea;
		private Rectangle _photoBounds;
		private bool _empty;
		private Control _parent;
		private CropHelper.DragMode _dragMode;
		private bool _drewFrame;
		private Size _originalPhotoSize;
		public bool Empty
		{
			get
			{
				return this._empty;
			}
			set
			{
				this._empty = value;
			}
		}
		public Size OriginalPhotoSize
		{
			get
			{
				return this._originalPhotoSize;
			}
			set
			{
				this._originalPhotoSize = value;
			}
		}
		public Rectangle PhotoBounds
		{
			get
			{
				return this._photoBounds;
			}
			set
			{
				this._photoBounds = value;
				this._selArea.X = this._photoBounds.Left;
				this._selArea.Y = this._photoBounds.Top;
				this._selArea.Width = 0;
				this._selArea.Height = 0;
				this.Empty = true;
			}
		}
		public Rectangle SelectedArea
		{
			get
			{
				return this._selArea;
			}
		}
		public Rectangle OriginalPhotoSelectedArea
		{
			get
			{
				Rectangle result;
				if (this._photoBounds.Width == 0 | this._photoBounds.Height == 0)
				{
					result = new Rectangle(0, 0, 0, 0);
					return result;
				}
				Rectangle rectangle = this.NormalizeArea(this._selArea);
				float num = (float)this._originalPhotoSize.Width / (float)this._photoBounds.Width;
				float num2 = (float)this._originalPhotoSize.Height / (float)this._photoBounds.Height;
				result = checked(new Rectangle((int)Math.Round((double)unchecked((float)checked(rectangle.Left - this._photoBounds.Left) * num)), (int)Math.Round((double)unchecked((float)checked(rectangle.Top - this._photoBounds.Top) * num2)), (int)Math.Round((double)unchecked((float)rectangle.Width * num)), (int)Math.Round((double)unchecked((float)rectangle.Height * num2))));
				return result;
			}
		}
		private Rectangle SelectedAreaScreen
		{
			get
			{
				return this._parent.RectangleToScreen(this._selArea);
			}
		}
		public CropHelper(Control parent)
		{
			this._originalPoint = default(Point);
			this._lastPoint = default(Point);
			this._selArea = default(Rectangle);
			this._photoBounds = default(Rectangle);
			this._empty = true;
			this._dragMode = CropHelper.DragMode.None;
			this._drewFrame = false;
			this._originalPhotoSize = default(Size);
			this._parent = parent;
		}
		public void MouseDown(int x, int y)
		{
			if (!this._photoBounds.Contains(x, y))
			{
				return;
			}
			this._mouseCaptured = true;
			Cursor.Current = CropHelper.CursorLookup[(int)this._dragMode];
			if (this._dragMode == CropHelper.DragMode.None)
			{
				this._originalPoint.X = x;
				this._originalPoint.Y = y;
				this._drewFrame = false;
			}
			else
			{
				this._lastPoint.X = x;
				this._lastPoint.Y = y;
				this.DrawReversibleRectangle();
				this._drewFrame = true;
			}
		}
		public void MouseUp(int x, int y)
		{
			if (!this._mouseCaptured)
			{
				return;
			}
			this._mouseCaptured = false;
			if (this._dragMode == CropHelper.DragMode.None)
			{
				if (this._drewFrame)
				{
					this.DrawReversibleRectangle(this._originalPoint, this._lastPoint);
				}
			}
			else
			{
				if (this._drewFrame)
				{
					this.DrawReversibleRectangle();
				}
			}
			this._selArea = this.NormalizeArea(this._selArea);
			this.Empty = BooleanType.FromObject(Interaction.IIf(this._selArea.Width > 0 & this._selArea.Height > 0, false, true));
		}
		public void MouseMove(int x, int y)
		{
			if (!this._mouseCaptured)
			{
				return;
			}
			Cursor.Current = CropHelper.CursorLookup[(int)this._dragMode];
			if (this._dragMode == CropHelper.DragMode.None)
			{
				if (this._drewFrame)
				{
					this.DrawReversibleRectangle(this._originalPoint, this._lastPoint);
				}
				if (x >= this._photoBounds.Left & x <= this._photoBounds.Right)
				{
					this._lastPoint.X = x;
				}
				if (y >= this._photoBounds.Top & y <= this._photoBounds.Bottom)
				{
					this._lastPoint.Y = y;
				}
				this.DrawReversibleRectangle(this._originalPoint, this._lastPoint);
				this._drewFrame = true;
				return;
			}
			this.DrawReversibleRectangle();
			checked
			{
				int num = x - this._lastPoint.X;
				int num2 = y - this._lastPoint.Y;
				Rectangle selArea = this._selArea;
				switch (this._dragMode)
				{
				case CropHelper.DragMode.Move:
					selArea.Offset(num, num2);
					break;
				case CropHelper.DragMode.TopLeft:
					selArea.Y = selArea.Y + num2;
					selArea.Height = selArea.Height - num2;
					selArea.X = selArea.X + num;
					selArea.Width = selArea.Width - num;
					break;
				case CropHelper.DragMode.Top:
					selArea.Y = selArea.Y + num2;
					selArea.Height = selArea.Height - num2;
					break;
				case CropHelper.DragMode.TopRight:
					selArea.Y = selArea.Y + num2;
					selArea.Height = selArea.Height - num2;
					selArea.Width = selArea.Width + num;
					break;
				case CropHelper.DragMode.Right:
					selArea.Width = selArea.Width + num;
					break;
				case CropHelper.DragMode.BottomRight:
					selArea.Width = selArea.Width + num;
					selArea.Height = selArea.Height + num2;
					break;
				case CropHelper.DragMode.Bottom:
					selArea.Height = selArea.Height + num2;
					break;
				case CropHelper.DragMode.BottomLeft:
					selArea.X = selArea.X + num;
					selArea.Width = selArea.Width - num;
					selArea.Height = selArea.Height + num2;
					break;
				case CropHelper.DragMode.Left:
					selArea.X = selArea.X + num;
					selArea.Width = selArea.Width - num;
					break;
				}
				Rectangle rectangle = this.NormalizeArea(selArea);
				if (rectangle.Left >= this._photoBounds.Left & rectangle.Right <= this._photoBounds.Right)
				{
					this._selArea.X = selArea.X;
					this._selArea.Width = selArea.Width;
				}
				if (rectangle.Top >= this._photoBounds.Top & rectangle.Bottom <= this._photoBounds.Bottom)
				{
					this._selArea.Y = selArea.Y;
					this._selArea.Height = selArea.Height;
				}
				if (x >= this._photoBounds.Left & x <= this._photoBounds.Right)
				{
					this._lastPoint.X = x;
				}
				if (y >= this._photoBounds.Top & y <= this._photoBounds.Bottom)
				{
					this._lastPoint.Y = y;
				}
				this.DrawReversibleRectangle();
			}
		}
		public void SetCursor(int x, int y)
		{
			this._dragMode = this.DragModeHitTest(this.SelectedArea, x, y);
			Cursor.Current = CropHelper.CursorLookup[(int)this._dragMode];
		}
		public void OriginalPhotoRotated()
		{
			int width = this._originalPhotoSize.Width;
			this._originalPhotoSize.Width = this._originalPhotoSize.Height;
			this._originalPhotoSize.Height = width;
		}
		private CropHelper.DragMode DragModeHitTest(Rectangle area, int x, int y)
		{
			if (!area.Contains(x, y))
			{
				return CropHelper.DragMode.None;
			}
			checked
			{
				if (Math.Abs(x - area.Left) + Math.Abs(y - area.Top) < 10)
				{
					return CropHelper.DragMode.TopLeft;
				}
				if (Math.Abs(x - area.Right) + Math.Abs(y - area.Top) < 10)
				{
					return CropHelper.DragMode.TopRight;
				}
				if (Math.Abs(x - area.Left) + Math.Abs(y - area.Bottom) < 10)
				{
					return CropHelper.DragMode.BottomLeft;
				}
				if (Math.Abs(x - area.Right) + Math.Abs(y - area.Bottom) < 10)
				{
					return CropHelper.DragMode.BottomRight;
				}
				if (Math.Abs(y - area.Top) < 10)
				{
					return CropHelper.DragMode.Top;
				}
				if (Math.Abs(y - area.Bottom) < 10)
				{
					return CropHelper.DragMode.Bottom;
				}
				if (Math.Abs(x - area.Left) < 10)
				{
					return CropHelper.DragMode.Left;
				}
				if (Math.Abs(x - area.Right) < 10)
				{
					return CropHelper.DragMode.Right;
				}
				return CropHelper.DragMode.Move;
			}
		}
		private void DrawReversibleRectangle(Point p1, Point p2)
		{
			this._selArea.X = p1.X;
			this._selArea.Y = p1.Y;
			checked
			{
				this._selArea.Width = p2.X - p1.X;
				this._selArea.Height = p2.Y - p1.Y;
				this.DrawReversibleRectangle();
			}
		}
		private void DrawReversibleRectangle()
		{
			ControlPaint.DrawReversibleFrame(this.SelectedAreaScreen, Color.Gray, 0);
		}
		private Rectangle NormalizeArea(Rectangle area)
		{
			Rectangle result = area;
			checked
			{
				if (area.Width < 0)
				{
					result.X = area.Right;
					result.Width = 0 - area.Width;
				}
				if (area.Height < 0)
				{
					result.Y = area.Bottom;
					result.Height = 0 - area.Height;
				}
				return result;
			}
		}
	}
}
