using Microsoft.VisualBasic.CompilerServices;
using System;
using System.Drawing;
using System.Text.RegularExpressions;
namespace FotoVision
{
	public class ActionItem
	{
		private PhotoAction _action;
		private int _percent;
		private Rectangle _bounds;
		private SliderValues _sliderValues;
		public PhotoAction Action
		{
			get
			{
				return this._action;
			}
		}
		public int Percent
		{
			get
			{
				return this._percent;
			}
			set
			{
				this._percent = value;
			}
		}
		public Rectangle Bounds
		{
			get
			{
				return this._bounds;
			}
			set
			{
				this._bounds = value;
			}
		}
		public SliderValues SliderValues
		{
			get
			{
				return this._sliderValues;
			}
		}
		public ActionItem(PhotoAction action)
		{
			this._action = action;
			this.SetSliderValues(Global.SliderValues.Contrast, Global.SliderValues.Brightness, Global.SliderValues.Gamma, Global.SliderValues.Saturation);
		}
		public ActionItem(PhotoAction action, int percent)
		{
			this._action = action;
			this._percent = percent;
			this.SetSliderValues(Global.SliderValues.Contrast, Global.SliderValues.Brightness, Global.SliderValues.Gamma, Global.SliderValues.Saturation);
		}
		public ActionItem(PhotoAction action, Rectangle bounds)
		{
			this._action = action;
			this._bounds = bounds;
			this.SetSliderValues(Global.SliderValues.Contrast, Global.SliderValues.Brightness, Global.SliderValues.Gamma, Global.SliderValues.Saturation);
		}
		public ActionItem(ActionItem actionItem)
		{
			this._action = actionItem.Action;
			this._percent = actionItem.Percent;
			this._bounds = actionItem.Bounds;
			this.SetSliderValues(Global.SliderValues.Contrast, Global.SliderValues.Brightness, Global.SliderValues.Gamma, Global.SliderValues.Saturation);
		}
		public void SetSliderValues(int contrast, int brightness, int gamma, int saturation)
		{
			this._sliderValues.Contrast = contrast;
			this._sliderValues.Brightness = brightness;
			this._sliderValues.Gamma = gamma;
			this._sliderValues.Saturation = saturation;
		}
		public string ToString()
		{
			return this.ToString(false);
		}
		public string ToString(bool includeValue)
		{
			string text = Regex.Replace(this.Action.ToString(), "([a-z])([A-Z])", "$1 $2");
			if (!includeValue)
			{
				return text;
			}
			if (this._action == PhotoAction.Crop)
			{
				return string.Format("{0} ({1} x {2} pixels)", text, this._bounds.Width, this._bounds.Height);
			}
			if (this._action == PhotoAction.Brightness || this._action == PhotoAction.Contrast || this._action == PhotoAction.Gamma || this._action == PhotoAction.Saturation)
			{
				return string.Format("{0} ({1}%)", text, StringType.FromInteger(this._percent));
			}
			return text;
		}
	}
}
