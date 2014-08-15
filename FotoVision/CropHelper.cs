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
			Cursors.get_Cross(),
			Cursors.get_SizeAll(),
			Cursors.get_SizeNWSE(),
			Cursors.get_SizeNS(),
			Cursors.get_SizeNESW(),
			Cursors.get_SizeWE(),
			Cursors.get_SizeNWSE(),
			Cursors.get_SizeNS(),
			Cursors.get_SizeNESW(),
			Cursors.get_SizeWE()
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
				this._selArea.set_X(this._photoBounds.get_Left());
				this._selArea.set_Y(this._photoBounds.get_Top());
				this._selArea.set_Width(0);
				this._selArea.set_Height(0);
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
				if (this._photoBounds.get_Width() == 0 | this._photoBounds.get_Height() == 0)
				{
					result = new Rectangle(0, 0, 0, 0);
					return result;
				}
				Rectangle rectangle = this.NormalizeArea(this._selArea);
				float num = (float)this._originalPhotoSize.get_Width() / (float)this._photoBounds.get_Width();
				float num2 = (float)this._originalPhotoSize.get_Height() / (float)this._photoBounds.get_Height();
				result = checked(new Rectangle((int)Math.Round((double)unchecked((float)checked(rectangle.get_Left() - this._photoBounds.get_Left()) * num)), (int)Math.Round((double)unchecked((float)checked(rectangle.get_Top() - this._photoBounds.get_Top()) * num2)), (int)Math.Round((double)unchecked((float)rectangle.get_Width() * num)), (int)Math.Round((double)unchecked((float)rectangle.get_Height() * num2))));
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
			Cursor.set_Current(CropHelper.CursorLookup[(int)this._dragMode]);
			if (this._dragMode == CropHelper.DragMode.None)
			{
				this._originalPoint.set_X(x);
				this._originalPoint.set_Y(y);
				this._drewFrame = false;
			}
			else
			{
				this._lastPoint.set_X(x);
				this._lastPoint.set_Y(y);
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
			this.Empty = BooleanType.FromObject(Interaction.IIf(this._selArea.get_Width() > 0 & this._selArea.get_Height() > 0, false, true));
		}
		public void MouseMove(int x, int y)
		{
			if (!this._mouseCaptured)
			{
				return;
			}
			Cursor.set_Current(CropHelper.CursorLookup[(int)this._dragMode]);
			if (this._dragMode == CropHelper.DragMode.None)
			{
				if (this._drewFrame)
				{
					this.DrawReversibleRectangle(this._originalPoint, this._lastPoint);
				}
				if (x >= this._photoBounds.get_Left() & x <= this._photoBounds.get_Right())
				{
					this._lastPoint.set_X(x);
				}
				if (y >= this._photoBounds.get_Top() & y <= this._photoBounds.get_Bottom())
				{
					this._lastPoint.set_Y(y);
				}
				this.DrawReversibleRectangle(this._originalPoint, this._lastPoint);
				this._drewFrame = true;
				return;
			}
			this.DrawReversibleRectangle();
			checked
			{
				int num = x - this._lastPoint.get_X();
				int num2 = y - this._lastPoint.get_Y();
				Rectangle selArea = this._selArea;
				switch (this._dragMode)
				{
				case CropHelper.DragMode.Move:
					selArea.Offset(num, num2);
					break;
				case CropHelper.DragMode.TopLeft:
					selArea.set_Y(selArea.get_Y() + num2);
					selArea.set_Height(selArea.get_Height() - num2);
					selArea.set_X(selArea.get_X() + num);
					selArea.set_Width(selArea.get_Width() - num);
					break;
				case CropHelper.DragMode.Top:
					selArea.set_Y(selArea.get_Y() + num2);
					selArea.set_Height(selArea.get_Height() - num2);
					break;
				case CropHelper.DragMode.TopRight:
					selArea.set_Y(selArea.get_Y() + num2);
					selArea.set_Height(selArea.get_Height() - num2);
					selArea.set_Width(selArea.get_Width() + num);
					break;
				case CropHelper.DragMode.Right:
					selArea.set_Width(selArea.get_Width() + num);
					break;
				case CropHelper.DragMode.BottomRight:
					selArea.set_Width(selArea.get_Width() + num);
					selArea.set_Height(selArea.get_Height() + num2);
					break;
				case CropHelper.DragMode.Bottom:
					selArea.set_Height(selArea.get_Height() + num2);
					break;
				case CropHelper.DragMode.BottomLeft:
					selArea.set_X(selArea.get_X() + num);
					selArea.set_Width(selArea.get_Width() - num);
					selArea.set_Height(selArea.get_Height() + num2);
					break;
				case CropHelper.DragMode.Left:
					selArea.set_X(selArea.get_X() + num);
					selArea.set_Width(selArea.get_Width() - num);
					break;
				}
				Rectangle rectangle = this.NormalizeArea(selArea);
				if (rectangle.get_Left() >= this._photoBounds.get_Left() & rectangle.get_Right() <= this._photoBounds.get_Right())
				{
					this._selArea.set_X(selArea.get_X());
					this._selArea.set_Width(selArea.get_Width());
				}
				if (rectangle.get_Top() >= this._photoBounds.get_Top() & rectangle.get_Bottom() <= this._photoBounds.get_Bottom())
				{
					this._selArea.set_Y(selArea.get_Y());
					this._selArea.set_Height(selArea.get_Height());
				}
				if (x >= this._photoBounds.get_Left() & x <= this._photoBounds.get_Right())
				{
					this._lastPoint.set_X(x);
				}
				if (y >= this._photoBounds.get_Top() & y <= this._photoBounds.get_Bottom())
				{
					this._lastPoint.set_Y(y);
				}
				this.DrawReversibleRectangle();
			}
		}
		public void SetCursor(int x, int y)
		{
			this._dragMode = this.DragModeHitTest(this.SelectedArea, x, y);
			Cursor.set_Current(CropHelper.CursorLookup[(int)this._dragMode]);
		}
		public void OriginalPhotoRotated()
		{
			int width = this._originalPhotoSize.get_Width();
			this._originalPhotoSize.set_Width(this._originalPhotoSize.get_Height());
			this._originalPhotoSize.set_Height(width);
		}
		private CropHelper.DragMode DragModeHitTest(Rectangle area, int x, int y)
		{
			if (!area.Contains(x, y))
			{
				return CropHelper.DragMode.None;
			}
			checked
			{
				if (Math.Abs(x - area.get_Left()) + Math.Abs(y - area.get_Top()) < 10)
				{
					return CropHelper.DragMode.TopLeft;
				}
				if (Math.Abs(x - area.get_Right()) + Math.Abs(y - area.get_Top()) < 10)
				{
					return CropHelper.DragMode.TopRight;
				}
				if (Math.Abs(x - area.get_Left()) + Math.Abs(y - area.get_Bottom()) < 10)
				{
					return CropHelper.DragMode.BottomLeft;
				}
				if (Math.Abs(x - area.get_Right()) + Math.Abs(y - area.get_Bottom()) < 10)
				{
					return CropHelper.DragMode.BottomRight;
				}
				if (Math.Abs(y - area.get_Top()) < 10)
				{
					return CropHelper.DragMode.Top;
				}
				if (Math.Abs(y - area.get_Bottom()) < 10)
				{
					return CropHelper.DragMode.Bottom;
				}
				if (Math.Abs(x - area.get_Left()) < 10)
				{
					return CropHelper.DragMode.Left;
				}
				if (Math.Abs(x - area.get_Right()) < 10)
				{
					return CropHelper.DragMode.Right;
				}
				return CropHelper.DragMode.Move;
			}
		}
		private void DrawReversibleRectangle(Point p1, Point p2)
		{
			this._selArea.set_X(p1.get_X());
			this._selArea.set_Y(p1.get_Y());
			checked
			{
				this._selArea.set_Width(p2.get_X() - p1.get_X());
				this._selArea.set_Height(p2.get_Y() - p1.get_Y());
				this.DrawReversibleRectangle();
			}
		}
		private void DrawReversibleRectangle()
		{
			ControlPaint.DrawReversibleFrame(this.SelectedAreaScreen, Color.get_Gray(), 0);
		}
		private Rectangle NormalizeArea(Rectangle area)
		{
			Rectangle result = area;
			checked
			{
				if (area.get_Width() < 0)
				{
					result.set_X(area.get_Right());
					result.set_Width(0 - area.get_Width());
				}
				if (area.get_Height() < 0)
				{
					result.set_Y(area.get_Bottom());
					result.set_Height(0 - area.get_Height());
				}
				return result;
			}
		}
	}
}
