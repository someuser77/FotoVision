using System;
namespace FotoVision
{
	public struct SliderValues
	{
		private int _contrast;
		private int _brightness;
		private int _gamma;
		private int _saturation;
		public int Contrast
		{
			get
			{
				return this._contrast;
			}
			set
			{
				this._contrast = value;
			}
		}
		public int Brightness
		{
			get
			{
				return this._brightness;
			}
			set
			{
				this._brightness = value;
			}
		}
		public int Gamma
		{
			get
			{
				return this._gamma;
			}
			set
			{
				this._gamma = value;
			}
		}
		public int Saturation
		{
			get
			{
				return this._saturation;
			}
			set
			{
				this._saturation = value;
			}
		}
	}
}
