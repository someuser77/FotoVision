using Microsoft.VisualBasic;
using Microsoft.VisualBasic.CompilerServices;
using System;
using System.Drawing;
using System.IO;
using System.Windows.Forms;
namespace FotoVision
{
	public class DropData
	{
		private bool _containsPhotos;
		private bool _inDragDrop;
		private ListView _listView;
		private ListViewItem _curItem;
		private MouseButtons _lastMouseButtons;
		public MouseButtons MouseButtons
		{
			get
			{
				return this._lastMouseButtons;
			}
		}
		public bool ContainsPhotos
		{
			get
			{
				return this._containsPhotos;
			}
		}
		public bool IsInDragDrop
		{
			get
			{
				return this._inDragDrop;
			}
			set
			{
				this._inDragDrop = value;
			}
		}
		public ListViewItem TargetItem
		{
			get
			{
				return this._curItem;
			}
		}
		public DropData(ListView listView)
		{
			this._containsPhotos = false;
			this._inDragDrop = false;
			this._listView = listView;
		}
		public void Enter(DragEventArgs e)
		{
			this.IsInDragDrop = true;
			if (!e.Data.GetDataPresent(DataFormats.FileDrop))
			{
				e.Effect = 0;
				return;
			}
			string[] files = (string[])e.Data.GetData(DataFormats.FileDrop);
			this._containsPhotos = this.PhotosInList(files);
			this.FindTargetItem(e.X, e.Y);
			e.Effect = this.GetDropEffect(e);
		}
		public void Leave()
		{
			this.IsInDragDrop = false;
		}
		public void Over(DragEventArgs e)
		{
			this._lastMouseButtons = Control.MouseButtons;
			this.FindTargetItem(e.X, e.Y);
			e.Effect = this.GetDropEffect(e);
		}
		public string[] Drop(DragEventArgs e)
		{
			this.FindTargetItem(e.X, e.Y);
			this.IsInDragDrop = false;
			if (e.Data.GetDataPresent(DataFormats.FileDrop))
			{
				return (string[])e.Data.GetData(DataFormats.FileDrop);
			}
			return null;
		}
		private void FindTargetItem(int X, int Y)
		{
			if (this._listView == null)
			{
				return;
			}
			if (!Global.Busy)
			{
				if (Global.PublishingFiles)
				{
					return;
				}
				Control arg_28_0 = this._listView;
				Point point = new Point(X, Y);
				Point point2 = arg_28_0.PointToClient(point);
				ListViewItem itemAt = this._listView.GetItemAt(point2.X, point2.Y);
				if (itemAt == this._curItem)
				{
					return;
				}
				if (this._curItem != null)
				{
					this._curItem.Selected = false;
				}
				if (itemAt != null)
				{
					itemAt.Selected = true;
				}
				this._curItem = itemAt;
			}
		}
		private DragDropEffects GetDropEffect(DragEventArgs e)
		{
			if (Global.Busy || Global.PublishingFiles)
			{
				return 0;
			}
			if (!e.Data.GetDataPresent(DataFormats.FileDrop))
			{
				return 0;
			}
			if (!this._containsPhotos)
			{
				return 0;
			}
			if (!Global.PerformingDrag)
			{
				return 1;
			}
			return IntegerType.FromObject(Interaction.IIf((e.KeyState & 8) == 8, 1, 2));
		}
		private bool PhotosInList(string[] files)
		{
			checked
			{
				for (int i = 0; i < files.Length; i++)
				{
					string text = files[i];
					if (Directory.Exists(text))
					{
						string[] files2 = Directory.GetFiles(text);
						string[] array = files2;
						for (int j = 0; j < array.Length; j++)
						{
							string file = array[j];
							if (FileManager.IsSupportedFile(file))
							{
								return true;
							}
						}
					}
					else
					{
						if (FileManager.IsSupportedFile(text))
						{
							return true;
						}
					}
				}
				return false;
			}
		}
	}
}
