using System;
using System.Runtime.CompilerServices;
using System.Windows.Forms;
namespace FotoVision
{
	public class DropContextMenu
	{
		[AccessedThroughProperty("menuCopy")]
		private MenuItem _menuCopy;
		[AccessedThroughProperty("menuMove")]
		private MenuItem _menuMove;
		[AccessedThroughProperty("menuCancel")]
		private MenuItem _menuCancel;
		private DragDropEffects _effect;
		private ContextMenu contextMenu;
		private MenuItem menuSep;
		private MenuItem menuCopy
		{
			get
			{
				return this._menuCopy;
			}
			[MethodImpl(32)]
			set
			{
				if (this._menuCopy != null)
				{
					this._menuCopy.Click -= new EventHandler(this.menuCopy_Click);
				}
				this._menuCopy = value;
				if (this._menuCopy != null)
				{
					this._menuCopy.Click += new EventHandler(this.menuCopy_Click);
				}
			}
		}
		private MenuItem menuMove
		{
			get
			{
				return this._menuMove;
			}
			[MethodImpl(32)]
			set
			{
				if (this._menuMove != null)
				{
					this._menuMove.Click -= new EventHandler(this.menuMove_Click);
				}
				this._menuMove = value;
				if (this._menuMove != null)
				{
					this._menuMove.Click += new EventHandler(this.menuMove_Click);
				}
			}
		}
		private MenuItem menuCancel
		{
			get
			{
				return this._menuCancel;
			}
			[MethodImpl(32)]
			set
			{
				if (this._menuCancel != null)
				{
					this._menuCancel.Click -= new EventHandler(this.menuCancel_Click);
				}
				this._menuCancel = value;
				if (this._menuCancel != null)
				{
					this._menuCancel.Click += new EventHandler(this.menuCancel_Click);
				}
			}
		}
		public bool EnableCopy
		{
			get
			{
				return this.menuCopy.Enabled;
			}
			set
			{
				this.menuCopy.Enabled = value;
			}
		}
		public bool EnableMove
		{
			get
			{
				return this.menuMove.Enabled;
			}
			set
			{
				this.menuMove.Enabled = value;
			}
		}
		public DropContextMenu()
		{
			this._effect = 0;
			this.contextMenu = new ContextMenu();
			this.menuSep = new MenuItem("-");
			this.menuCopy = new MenuItem("Copy here");
			this.menuMove = new MenuItem("Move here");
			this.menuCancel = new MenuItem("Cancel");
			this.contextMenu.MenuItems.AddRange(new MenuItem[]
			{
				this.menuCopy,
				this.menuMove,
				this.menuSep,
				this.menuCancel
			});
		}
		public DragDropEffects Display(Control parent)
		{
			this.contextMenu.Show(parent, parent.PointToClient(Control.MousePosition));
			Application.DoEvents();
			return this._effect;
		}
		private void menuCopy_Click(object sender, EventArgs e)
		{
			this._effect = 1;
		}
		private void menuMove_Click(object sender, EventArgs e)
		{
			this._effect = 2;
		}
		private void menuCancel_Click(object sender, EventArgs e)
		{
			this._effect = 0;
		}
	}
}
