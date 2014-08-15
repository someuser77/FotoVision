using Microsoft.VisualBasic;
using Microsoft.VisualBasic.CompilerServices;
using System;
using System.Collections;
namespace FotoVision
{
	public class ActionList
	{
		private ArrayList _list;
		private ActionItem _redoItem;
		public bool ContainsRedo
		{
			get
			{
				return BooleanType.FromObject(Interaction.IIf(this._redoItem == null, false, true));
			}
		}
		public ActionItem RedoItem
		{
			get
			{
				return this._redoItem;
			}
			set
			{
				this._redoItem = value;
			}
		}
		public ActionItem LastItem
		{
			get
			{
				if (this.Count == 0)
				{
					return null;
				}
				return this.GetAt(checked(this.Count - 1));
			}
		}
		public int Count
		{
			get
			{
				return this._list.Count;
			}
		}
		public bool OnlyRotates
		{
			get
			{
				try
				{
					IEnumerator enumerator = this._list.GetEnumerator();
					while (enumerator.MoveNext())
					{
						ActionItem actionItem = (ActionItem)enumerator.Current;
						if (actionItem.Action != PhotoAction.RotateLeft && actionItem.Action != PhotoAction.RotateRight && actionItem.Action != PhotoAction.FlipHorizontal && actionItem.Action != PhotoAction.FlipVertical)
						{
							return false;
						}
					}
				}
				finally
				{
					IEnumerator enumerator;
					if (enumerator is IDisposable)
					{
						((IDisposable)enumerator).Dispose();
					}
				}
				return true;
			}
		}
		public ActionList()
		{
			this._list = new ArrayList();
			this._redoItem = null;
			this.Clear();
		}
		public void Clear()
		{
			this._list.Clear();
			this._redoItem = null;
		}
		public bool Add(ActionItem actionItem)
		{
			this._redoItem = null;
			ActionItem lastItem = this.LastItem;
			if (lastItem != null && lastItem.Action == actionItem.Action)
			{
				if (lastItem.Action == PhotoAction.ConvertSepia || lastItem.Action == PhotoAction.ConvertGrayscale)
				{
					return false;
				}
				if (lastItem.Action == PhotoAction.Brightness || lastItem.Action == PhotoAction.Contrast || lastItem.Action == PhotoAction.Saturation || lastItem.Action == PhotoAction.Gamma)
				{
					lastItem.Percent = actionItem.Percent;
					lastItem.SetSliderValues(Global.SliderValues.Contrast, Global.SliderValues.Brightness, Global.SliderValues.Gamma, Global.SliderValues.Saturation);
					return false;
				}
			}
			this._list.Add(new ActionItem(actionItem));
			return true;
		}
		public ActionItem GetAt(int index)
		{
			return (ActionItem)this._list.get_Item(index);
		}
		public void RemoveLast()
		{
			checked
			{
				if (this.Count > 0)
				{
					this._redoItem = this.GetAt(this.Count - 1);
					this._list.RemoveAt(this.Count - 1);
				}
			}
		}
	}
}
