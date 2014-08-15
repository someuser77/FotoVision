using Microsoft.VisualBasic;
using Microsoft.VisualBasic.CompilerServices;
using System;
using System.ComponentModel;
using System.Drawing;
using System.Globalization;
using System.Resources;
using System.Runtime.CompilerServices;
using System.Windows.Forms;
namespace FotoVision
{
	public class DetailsActions : UserControl
	{
		private class SliderData
		{
			public NumericUpDown NumericUpDown;
			public PhotoAction Action;
			public int ImageIndexLow;
			public int ImageIndexHigh;
			public bool ValueChanged;
			public SliderData(NumericUpDown numericUpDown, PhotoAction action, int imageIndexLow, int imageIndexHigh)
			{
				this.NumericUpDown = numericUpDown;
				this.Action = action;
				this.ImageIndexLow = imageIndexLow;
				this.ImageIndexHigh = imageIndexHigh;
			}
		}
		public delegate void CropModeChangedEventHandler(object sender, CropModeChangedEventArgs e);
		public delegate void ActionEventHandler(object sender, ActionEventArgs e);
		public delegate void CommandButtonClickedEventHandler(object sender, CommandButtonClickedEventArgs e);
		[AccessedThroughProperty("buttonRedo")]
		private Button _buttonRedo;
		[AccessedThroughProperty("numBright")]
		private NumericUpDown _numBright;
		[AccessedThroughProperty("buttonUndo")]
		private Button _buttonUndo;
		[AccessedThroughProperty("buttonReset")]
		private Button _buttonReset;
		[AccessedThroughProperty("buttonSave")]
		private Button _buttonSave;
		[AccessedThroughProperty("buttonClearCrop")]
		private Button _buttonClearCrop;
		[AccessedThroughProperty("labelSat")]
		private Label _labelSat;
		[AccessedThroughProperty("panelPictureSep")]
		private Panel _panelPictureSep;
		[AccessedThroughProperty("imageList")]
		private ImageList _imageList;
		[AccessedThroughProperty("tabControl")]
		private TabControl _tabControl;
		[AccessedThroughProperty("labelPicture")]
		private Label _labelPicture;
		private DetailsActions.CommandButtonClickedEventHandler CommandButtonClickedEvent;
		[AccessedThroughProperty("labelCrop")]
		private Label _labelCrop;
		[AccessedThroughProperty("numGamma")]
		private NumericUpDown _numGamma;
		[AccessedThroughProperty("panelCropSep")]
		private Panel _panelCropSep;
		[AccessedThroughProperty("numContrast")]
		private NumericUpDown _numContrast;
		[AccessedThroughProperty("buttonCrop")]
		private Button _buttonCrop;
		private DetailsActions.ActionEventHandler ActionEvent;
		[AccessedThroughProperty("sliderBright")]
		private TrackBar _sliderBright;
		[AccessedThroughProperty("labelColors")]
		private Label _labelColors;
		[AccessedThroughProperty("labelBright")]
		private Label _labelBright;
		[AccessedThroughProperty("sliderContrast")]
		private TrackBar _sliderContrast;
		[AccessedThroughProperty("labelGamma")]
		private Label _labelGamma;
		private DetailsActions.CropModeChangedEventHandler CropModeChangedEvent;
		[AccessedThroughProperty("panelAdjust")]
		private Panel _panelAdjust;
		[AccessedThroughProperty("labelContrast")]
		private Label _labelContrast;
		[AccessedThroughProperty("sliderSat")]
		private TrackBar _sliderSat;
		[AccessedThroughProperty("buttonSepia")]
		private Button _buttonSepia;
		[AccessedThroughProperty("sliderGamma")]
		private TrackBar _sliderGamma;
		[AccessedThroughProperty("buttonGrayscale")]
		private Button _buttonGrayscale;
		[AccessedThroughProperty("numSat")]
		private NumericUpDown _numSat;
		private Rectangle _cropBounds;
		private bool _updatingSlider;
		private IContainer components;
		private TabPage pageCrop;
		private TabPage pageAdjust;
		private PictureBox pictCropCoord;
		private PictureBox pictCropDim;
		private Panel panelCrop;
		private Panel panelSave;
		public event DetailsActions.CropModeChangedEventHandler CropModeChanged
		{
			[MethodImpl(32)]
			add
			{
				this.CropModeChangedEvent = (DetailsActions.CropModeChangedEventHandler)Delegate.Combine(this.CropModeChangedEvent, value);
			}
			[MethodImpl(32)]
			remove
			{
				this.CropModeChangedEvent = (DetailsActions.CropModeChangedEventHandler)Delegate.Remove(this.CropModeChangedEvent, value);
			}
		}
		public event DetailsActions.ActionEventHandler Action
		{
			[MethodImpl(32)]
			add
			{
				this.ActionEvent = (DetailsActions.ActionEventHandler)Delegate.Combine(this.ActionEvent, value);
			}
			[MethodImpl(32)]
			remove
			{
				this.ActionEvent = (DetailsActions.ActionEventHandler)Delegate.Remove(this.ActionEvent, value);
			}
		}
		public event DetailsActions.CommandButtonClickedEventHandler CommandButtonClicked
		{
			[MethodImpl(32)]
			add
			{
				this.CommandButtonClickedEvent = (DetailsActions.CommandButtonClickedEventHandler)Delegate.Combine(this.CommandButtonClickedEvent, value);
			}
			[MethodImpl(32)]
			remove
			{
				this.CommandButtonClickedEvent = (DetailsActions.CommandButtonClickedEventHandler)Delegate.Remove(this.CommandButtonClickedEvent, value);
			}
		}
		private ImageList imageList
		{
			get
			{
				return this._imageList;
			}
			[MethodImpl(32)]
			set
			{
				if (this._imageList != null)
				{
				}
				this._imageList = value;
				if (this._imageList != null)
				{
				}
			}
		}
		private TabControl tabControl
		{
			get
			{
				return this._tabControl;
			}
			[MethodImpl(32)]
			set
			{
				if (this._tabControl != null)
				{
					this._tabControl.remove_SelectedIndexChanged(new EventHandler(this.tabControl_SelectedIndexChanged));
				}
				this._tabControl = value;
				if (this._tabControl != null)
				{
					this._tabControl.add_SelectedIndexChanged(new EventHandler(this.tabControl_SelectedIndexChanged));
				}
			}
		}
		private NumericUpDown numContrast
		{
			get
			{
				return this._numContrast;
			}
			[MethodImpl(32)]
			set
			{
				if (this._numContrast != null)
				{
					this._numContrast.remove_MouseUp(new MouseEventHandler(this.spinner_MouseUp));
					this._numContrast.remove_KeyUp(new KeyEventHandler(this.spinner_KeyUp));
					this._numContrast.remove_ValueChanged(new EventHandler(this.spinner_ValueChanged));
					this._numContrast.remove_Enter(new EventHandler(this.spinner_Enter));
				}
				this._numContrast = value;
				if (this._numContrast != null)
				{
					this._numContrast.add_MouseUp(new MouseEventHandler(this.spinner_MouseUp));
					this._numContrast.add_KeyUp(new KeyEventHandler(this.spinner_KeyUp));
					this._numContrast.add_ValueChanged(new EventHandler(this.spinner_ValueChanged));
					this._numContrast.add_Enter(new EventHandler(this.spinner_Enter));
				}
			}
		}
		private TrackBar sliderBright
		{
			get
			{
				return this._sliderBright;
			}
			[MethodImpl(32)]
			set
			{
				if (this._sliderBright != null)
				{
					this._sliderBright.remove_MouseUp(new MouseEventHandler(this.slider_MouseUp));
					this._sliderBright.remove_KeyUp(new KeyEventHandler(this.slider_KeyUp));
					this._sliderBright.remove_ValueChanged(new EventHandler(this.slider_ValueChanged));
				}
				this._sliderBright = value;
				if (this._sliderBright != null)
				{
					this._sliderBright.add_MouseUp(new MouseEventHandler(this.slider_MouseUp));
					this._sliderBright.add_KeyUp(new KeyEventHandler(this.slider_KeyUp));
					this._sliderBright.add_ValueChanged(new EventHandler(this.slider_ValueChanged));
				}
			}
		}
		private TrackBar sliderContrast
		{
			get
			{
				return this._sliderContrast;
			}
			[MethodImpl(32)]
			set
			{
				if (this._sliderContrast != null)
				{
					this._sliderContrast.remove_MouseUp(new MouseEventHandler(this.slider_MouseUp));
					this._sliderContrast.remove_KeyUp(new KeyEventHandler(this.slider_KeyUp));
					this._sliderContrast.remove_ValueChanged(new EventHandler(this.slider_ValueChanged));
				}
				this._sliderContrast = value;
				if (this._sliderContrast != null)
				{
					this._sliderContrast.add_MouseUp(new MouseEventHandler(this.slider_MouseUp));
					this._sliderContrast.add_KeyUp(new KeyEventHandler(this.slider_KeyUp));
					this._sliderContrast.add_ValueChanged(new EventHandler(this.slider_ValueChanged));
				}
			}
		}
		private TrackBar sliderSat
		{
			get
			{
				return this._sliderSat;
			}
			[MethodImpl(32)]
			set
			{
				if (this._sliderSat != null)
				{
					this._sliderSat.remove_MouseUp(new MouseEventHandler(this.slider_MouseUp));
					this._sliderSat.remove_KeyUp(new KeyEventHandler(this.slider_KeyUp));
					this._sliderSat.remove_ValueChanged(new EventHandler(this.slider_ValueChanged));
				}
				this._sliderSat = value;
				if (this._sliderSat != null)
				{
					this._sliderSat.add_MouseUp(new MouseEventHandler(this.slider_MouseUp));
					this._sliderSat.add_KeyUp(new KeyEventHandler(this.slider_KeyUp));
					this._sliderSat.add_ValueChanged(new EventHandler(this.slider_ValueChanged));
				}
			}
		}
		private TrackBar sliderGamma
		{
			get
			{
				return this._sliderGamma;
			}
			[MethodImpl(32)]
			set
			{
				if (this._sliderGamma != null)
				{
					this._sliderGamma.remove_MouseUp(new MouseEventHandler(this.slider_MouseUp));
					this._sliderGamma.remove_KeyUp(new KeyEventHandler(this.slider_KeyUp));
					this._sliderGamma.remove_ValueChanged(new EventHandler(this.slider_ValueChanged));
				}
				this._sliderGamma = value;
				if (this._sliderGamma != null)
				{
					this._sliderGamma.add_MouseUp(new MouseEventHandler(this.slider_MouseUp));
					this._sliderGamma.add_KeyUp(new KeyEventHandler(this.slider_KeyUp));
					this._sliderGamma.add_ValueChanged(new EventHandler(this.slider_ValueChanged));
				}
			}
		}
		private Panel panelAdjust
		{
			get
			{
				return this._panelAdjust;
			}
			[MethodImpl(32)]
			set
			{
				if (this._panelAdjust != null)
				{
					this._panelAdjust.remove_Paint(new PaintEventHandler(this.panelAdjust_Paint));
				}
				this._panelAdjust = value;
				if (this._panelAdjust != null)
				{
					this._panelAdjust.add_Paint(new PaintEventHandler(this.panelAdjust_Paint));
				}
			}
		}
		private NumericUpDown numBright
		{
			get
			{
				return this._numBright;
			}
			[MethodImpl(32)]
			set
			{
				if (this._numBright != null)
				{
					this._numBright.remove_MouseUp(new MouseEventHandler(this.spinner_MouseUp));
					this._numBright.remove_KeyUp(new KeyEventHandler(this.spinner_KeyUp));
					this._numBright.remove_ValueChanged(new EventHandler(this.spinner_ValueChanged));
					this._numBright.remove_Enter(new EventHandler(this.spinner_Enter));
				}
				this._numBright = value;
				if (this._numBright != null)
				{
					this._numBright.add_MouseUp(new MouseEventHandler(this.spinner_MouseUp));
					this._numBright.add_KeyUp(new KeyEventHandler(this.spinner_KeyUp));
					this._numBright.add_ValueChanged(new EventHandler(this.spinner_ValueChanged));
					this._numBright.add_Enter(new EventHandler(this.spinner_Enter));
				}
			}
		}
		private NumericUpDown numGamma
		{
			get
			{
				return this._numGamma;
			}
			[MethodImpl(32)]
			set
			{
				if (this._numGamma != null)
				{
					this._numGamma.remove_MouseUp(new MouseEventHandler(this.spinner_MouseUp));
					this._numGamma.remove_KeyUp(new KeyEventHandler(this.spinner_KeyUp));
					this._numGamma.remove_ValueChanged(new EventHandler(this.spinner_ValueChanged));
					this._numGamma.remove_Enter(new EventHandler(this.spinner_Enter));
				}
				this._numGamma = value;
				if (this._numGamma != null)
				{
					this._numGamma.add_MouseUp(new MouseEventHandler(this.spinner_MouseUp));
					this._numGamma.add_KeyUp(new KeyEventHandler(this.spinner_KeyUp));
					this._numGamma.add_ValueChanged(new EventHandler(this.spinner_ValueChanged));
					this._numGamma.add_Enter(new EventHandler(this.spinner_Enter));
				}
			}
		}
		private NumericUpDown numSat
		{
			get
			{
				return this._numSat;
			}
			[MethodImpl(32)]
			set
			{
				if (this._numSat != null)
				{
					this._numSat.remove_MouseUp(new MouseEventHandler(this.spinner_MouseUp));
					this._numSat.remove_KeyUp(new KeyEventHandler(this.spinner_KeyUp));
					this._numSat.remove_ValueChanged(new EventHandler(this.spinner_ValueChanged));
					this._numSat.remove_Enter(new EventHandler(this.spinner_Enter));
				}
				this._numSat = value;
				if (this._numSat != null)
				{
					this._numSat.add_MouseUp(new MouseEventHandler(this.spinner_MouseUp));
					this._numSat.add_KeyUp(new KeyEventHandler(this.spinner_KeyUp));
					this._numSat.add_ValueChanged(new EventHandler(this.spinner_ValueChanged));
					this._numSat.add_Enter(new EventHandler(this.spinner_Enter));
				}
			}
		}
		private Label labelSat
		{
			get
			{
				return this._labelSat;
			}
			[MethodImpl(32)]
			set
			{
				if (this._labelSat != null)
				{
				}
				this._labelSat = value;
				if (this._labelSat != null)
				{
				}
			}
		}
		private Label labelBright
		{
			get
			{
				return this._labelBright;
			}
			[MethodImpl(32)]
			set
			{
				if (this._labelBright != null)
				{
				}
				this._labelBright = value;
				if (this._labelBright != null)
				{
				}
			}
		}
		private Button buttonGrayscale
		{
			get
			{
				return this._buttonGrayscale;
			}
			[MethodImpl(32)]
			set
			{
				if (this._buttonGrayscale != null)
				{
					this._buttonGrayscale.remove_Click(new EventHandler(this.buttonGrayscale_Click));
				}
				this._buttonGrayscale = value;
				if (this._buttonGrayscale != null)
				{
					this._buttonGrayscale.add_Click(new EventHandler(this.buttonGrayscale_Click));
				}
			}
		}
		private Button buttonSepia
		{
			get
			{
				return this._buttonSepia;
			}
			[MethodImpl(32)]
			set
			{
				if (this._buttonSepia != null)
				{
					this._buttonSepia.remove_Click(new EventHandler(this.buttonSepia_Click));
				}
				this._buttonSepia = value;
				if (this._buttonSepia != null)
				{
					this._buttonSepia.add_Click(new EventHandler(this.buttonSepia_Click));
				}
			}
		}
		private Label labelContrast
		{
			get
			{
				return this._labelContrast;
			}
			[MethodImpl(32)]
			set
			{
				if (this._labelContrast != null)
				{
				}
				this._labelContrast = value;
				if (this._labelContrast != null)
				{
				}
			}
		}
		private Label labelGamma
		{
			get
			{
				return this._labelGamma;
			}
			[MethodImpl(32)]
			set
			{
				if (this._labelGamma != null)
				{
				}
				this._labelGamma = value;
				if (this._labelGamma != null)
				{
				}
			}
		}
		private Label labelColors
		{
			get
			{
				return this._labelColors;
			}
			[MethodImpl(32)]
			set
			{
				if (this._labelColors != null)
				{
				}
				this._labelColors = value;
				if (this._labelColors != null)
				{
				}
			}
		}
		private Button buttonCrop
		{
			get
			{
				return this._buttonCrop;
			}
			[MethodImpl(32)]
			set
			{
				if (this._buttonCrop != null)
				{
					this._buttonCrop.remove_Click(new EventHandler(this.buttonCrop_Click));
				}
				this._buttonCrop = value;
				if (this._buttonCrop != null)
				{
					this._buttonCrop.add_Click(new EventHandler(this.buttonCrop_Click));
				}
			}
		}
		private Panel panelCropSep
		{
			get
			{
				return this._panelCropSep;
			}
			[MethodImpl(32)]
			set
			{
				if (this._panelCropSep != null)
				{
				}
				this._panelCropSep = value;
				if (this._panelCropSep != null)
				{
				}
			}
		}
		private Label labelCrop
		{
			get
			{
				return this._labelCrop;
			}
			[MethodImpl(32)]
			set
			{
				if (this._labelCrop != null)
				{
				}
				this._labelCrop = value;
				if (this._labelCrop != null)
				{
				}
			}
		}
		private Label labelPicture
		{
			get
			{
				return this._labelPicture;
			}
			[MethodImpl(32)]
			set
			{
				if (this._labelPicture != null)
				{
				}
				this._labelPicture = value;
				if (this._labelPicture != null)
				{
				}
			}
		}
		private Panel panelPictureSep
		{
			get
			{
				return this._panelPictureSep;
			}
			[MethodImpl(32)]
			set
			{
				if (this._panelPictureSep != null)
				{
				}
				this._panelPictureSep = value;
				if (this._panelPictureSep != null)
				{
				}
			}
		}
		private Button buttonClearCrop
		{
			get
			{
				return this._buttonClearCrop;
			}
			[MethodImpl(32)]
			set
			{
				if (this._buttonClearCrop != null)
				{
					this._buttonClearCrop.remove_Click(new EventHandler(this.buttonClearCrop_Click));
				}
				this._buttonClearCrop = value;
				if (this._buttonClearCrop != null)
				{
					this._buttonClearCrop.add_Click(new EventHandler(this.buttonClearCrop_Click));
				}
			}
		}
		private Button buttonSave
		{
			get
			{
				return this._buttonSave;
			}
			[MethodImpl(32)]
			set
			{
				if (this._buttonSave != null)
				{
					this._buttonSave.remove_Click(new EventHandler(this.commandButton_Clicked));
				}
				this._buttonSave = value;
				if (this._buttonSave != null)
				{
					this._buttonSave.add_Click(new EventHandler(this.commandButton_Clicked));
				}
			}
		}
		private Button buttonReset
		{
			get
			{
				return this._buttonReset;
			}
			[MethodImpl(32)]
			set
			{
				if (this._buttonReset != null)
				{
					this._buttonReset.remove_Click(new EventHandler(this.commandButton_Clicked));
				}
				this._buttonReset = value;
				if (this._buttonReset != null)
				{
					this._buttonReset.add_Click(new EventHandler(this.commandButton_Clicked));
				}
			}
		}
		private Button buttonUndo
		{
			get
			{
				return this._buttonUndo;
			}
			[MethodImpl(32)]
			set
			{
				if (this._buttonUndo != null)
				{
					this._buttonUndo.remove_Click(new EventHandler(this.commandButton_Clicked));
				}
				this._buttonUndo = value;
				if (this._buttonUndo != null)
				{
					this._buttonUndo.add_Click(new EventHandler(this.commandButton_Clicked));
				}
			}
		}
		private Button buttonRedo
		{
			get
			{
				return this._buttonRedo;
			}
			[MethodImpl(32)]
			set
			{
				if (this._buttonRedo != null)
				{
					this._buttonRedo.remove_Click(new EventHandler(this.commandButton_Clicked));
				}
				this._buttonRedo = value;
				if (this._buttonRedo != null)
				{
					this._buttonRedo.add_Click(new EventHandler(this.commandButton_Clicked));
				}
			}
		}
		private void InitializeComponent()
		{
			this.components = new Container();
			ResourceManager resourceManager = new ResourceManager(typeof(DetailsActions));
			this.tabControl = new TabControl();
			this.pageAdjust = new TabPage();
			this.panelAdjust = new Panel();
			this.numContrast = new NumericUpDown();
			this.labelSat = new Label();
			this.labelBright = new Label();
			this.sliderBright = new TrackBar();
			this.buttonGrayscale = new Button();
			this.buttonSepia = new Button();
			this.labelContrast = new Label();
			this.sliderContrast = new TrackBar();
			this.sliderSat = new TrackBar();
			this.labelGamma = new Label();
			this.sliderGamma = new TrackBar();
			this.labelColors = new Label();
			this.numBright = new NumericUpDown();
			this.numGamma = new NumericUpDown();
			this.numSat = new NumericUpDown();
			this.pageCrop = new TabPage();
			this.panelCrop = new Panel();
			this.buttonCrop = new Button();
			this.pictCropCoord = new PictureBox();
			this.panelCropSep = new Panel();
			this.labelCrop = new Label();
			this.labelPicture = new Label();
			this.panelPictureSep = new Panel();
			this.pictCropDim = new PictureBox();
			this.buttonClearCrop = new Button();
			this.panelSave = new Panel();
			this.buttonSave = new Button();
			this.buttonReset = new Button();
			this.buttonUndo = new Button();
			this.buttonRedo = new Button();
			this.imageList = new ImageList(this.components);
			this.tabControl.SuspendLayout();
			this.pageAdjust.SuspendLayout();
			this.panelAdjust.SuspendLayout();
			this.numContrast.BeginInit();
			this.sliderBright.BeginInit();
			this.sliderContrast.BeginInit();
			this.sliderSat.BeginInit();
			this.sliderGamma.BeginInit();
			this.numBright.BeginInit();
			this.numGamma.BeginInit();
			this.numSat.BeginInit();
			this.pageCrop.SuspendLayout();
			this.panelCrop.SuspendLayout();
			this.panelSave.SuspendLayout();
			this.SuspendLayout();
			this.tabControl.get_Controls().Add(this.pageAdjust);
			this.tabControl.get_Controls().Add(this.pageCrop);
			this.tabControl.set_Dock(5);
			Control arg_280_0 = this.tabControl;
			Point location = new Point(2, 2);
			arg_280_0.set_Location(location);
			this.tabControl.set_Name("tabControl");
			this.tabControl.set_SelectedIndex(0);
			Control arg_2B9_0 = this.tabControl;
			Size size = new Size(308, 388);
			arg_2B9_0.set_Size(size);
			this.tabControl.set_TabIndex(0);
			this.pageAdjust.get_Controls().Add(this.panelAdjust);
			Control arg_2F2_0 = this.pageAdjust;
			location = new Point(4, 22);
			arg_2F2_0.set_Location(location);
			this.pageAdjust.set_Name("pageAdjust");
			Control arg_31F_0 = this.pageAdjust;
			size = new Size(300, 362);
			arg_31F_0.set_Size(size);
			this.pageAdjust.set_TabIndex(1);
			this.pageAdjust.set_Text("Adjust");
			this.panelAdjust.get_Controls().Add(this.numContrast);
			this.panelAdjust.get_Controls().Add(this.labelSat);
			this.panelAdjust.get_Controls().Add(this.labelBright);
			this.panelAdjust.get_Controls().Add(this.sliderBright);
			this.panelAdjust.get_Controls().Add(this.buttonGrayscale);
			this.panelAdjust.get_Controls().Add(this.buttonSepia);
			this.panelAdjust.get_Controls().Add(this.labelContrast);
			this.panelAdjust.get_Controls().Add(this.sliderContrast);
			this.panelAdjust.get_Controls().Add(this.sliderSat);
			this.panelAdjust.get_Controls().Add(this.labelGamma);
			this.panelAdjust.get_Controls().Add(this.sliderGamma);
			this.panelAdjust.get_Controls().Add(this.labelColors);
			this.panelAdjust.get_Controls().Add(this.numBright);
			this.panelAdjust.get_Controls().Add(this.numGamma);
			this.panelAdjust.get_Controls().Add(this.numSat);
			this.panelAdjust.set_Dock(5);
			Control arg_4A7_0 = this.panelAdjust;
			location = new Point(0, 0);
			arg_4A7_0.set_Location(location);
			this.panelAdjust.set_Name("panelAdjust");
			Control arg_4D4_0 = this.panelAdjust;
			size = new Size(300, 362);
			arg_4D4_0.set_Size(size);
			this.panelAdjust.set_TabIndex(0);
			Control arg_4FB_0 = this.numContrast;
			location = new Point(240, 40);
			arg_4FB_0.set_Location(location);
			NumericUpDown arg_52B_0 = this.numContrast;
			decimal minimum = new decimal(new int[]
			{
				100,
				0,
				0,
				-2147483648
			});
			arg_52B_0.set_Minimum(minimum);
			this.numContrast.set_Name("numContrast");
			Control arg_552_0 = this.numContrast;
			size = new Size(48, 20);
			arg_552_0.set_Size(size);
			this.numContrast.set_TabIndex(2);
			this.labelSat.set_FlatStyle(3);
			Control arg_584_0 = this.labelSat;
			location = new Point(8, 208);
			arg_584_0.set_Location(location);
			this.labelSat.set_Name("labelSat");
			Control arg_5AB_0 = this.labelSat;
			size = new Size(64, 16);
			arg_5AB_0.set_Size(size);
			this.labelSat.set_TabIndex(9);
			this.labelSat.set_Text("&Saturation");
			this.labelBright.set_FlatStyle(3);
			Control arg_5EB_0 = this.labelBright;
			location = new Point(8, 80);
			arg_5EB_0.set_Location(location);
			this.labelBright.set_Name("labelBright");
			Control arg_612_0 = this.labelBright;
			size = new Size(64, 16);
			arg_612_0.set_Size(size);
			this.labelBright.set_TabIndex(3);
			this.labelBright.set_Text("&Brightness");
			this.sliderBright.set_AutoSize(false);
			this.sliderBright.set_LargeChange(10);
			Control arg_65F_0 = this.sliderBright;
			location = new Point(32, 104);
			arg_65F_0.set_Location(location);
			this.sliderBright.set_Maximum(100);
			this.sliderBright.set_Minimum(-100);
			this.sliderBright.set_Name("sliderBright");
			Control arg_6A3_0 = this.sliderBright;
			size = new Size(160, 20);
			arg_6A3_0.set_Size(size);
			this.sliderBright.set_TabIndex(4);
			this.sliderBright.set_TickFrequency(25);
			this.buttonGrayscale.set_FlatStyle(3);
			Control arg_6E3_0 = this.buttonGrayscale;
			location = new Point(16, 304);
			arg_6E3_0.set_Location(location);
			this.buttonGrayscale.set_Name("buttonGrayscale");
			this.buttonGrayscale.set_TabIndex(13);
			this.buttonGrayscale.set_Text("&Grayscale");
			this.buttonSepia.set_FlatStyle(3);
			Control arg_737_0 = this.buttonSepia;
			location = new Point(112, 304);
			arg_737_0.set_Location(location);
			this.buttonSepia.set_Name("buttonSepia");
			this.buttonSepia.set_TabIndex(14);
			this.buttonSepia.set_Text("Se&pia");
			this.labelContrast.set_FlatStyle(3);
			Control arg_787_0 = this.labelContrast;
			location = new Point(8, 16);
			arg_787_0.set_Location(location);
			this.labelContrast.set_Name("labelContrast");
			Control arg_7AE_0 = this.labelContrast;
			size = new Size(64, 16);
			arg_7AE_0.set_Size(size);
			this.labelContrast.set_TabIndex(0);
			this.labelContrast.set_Text("&Contrast");
			this.sliderContrast.set_AutoSize(false);
			this.sliderContrast.set_LargeChange(10);
			Control arg_7FB_0 = this.sliderContrast;
			location = new Point(32, 40);
			arg_7FB_0.set_Location(location);
			this.sliderContrast.set_Maximum(100);
			this.sliderContrast.set_Minimum(-100);
			this.sliderContrast.set_Name("sliderContrast");
			Control arg_83F_0 = this.sliderContrast;
			size = new Size(160, 20);
			arg_83F_0.set_Size(size);
			this.sliderContrast.set_TabIndex(1);
			this.sliderContrast.set_TickFrequency(25);
			this.sliderSat.set_AutoSize(false);
			this.sliderSat.set_LargeChange(10);
			Control arg_88C_0 = this.sliderSat;
			location = new Point(32, 232);
			arg_88C_0.set_Location(location);
			this.sliderSat.set_Maximum(100);
			this.sliderSat.set_Minimum(-100);
			this.sliderSat.set_Name("sliderSat");
			Control arg_8D0_0 = this.sliderSat;
			size = new Size(160, 20);
			arg_8D0_0.set_Size(size);
			this.sliderSat.set_TabIndex(10);
			this.sliderSat.set_TickFrequency(25);
			this.labelGamma.set_FlatStyle(3);
			Control arg_910_0 = this.labelGamma;
			location = new Point(8, 144);
			arg_910_0.set_Location(location);
			this.labelGamma.set_Name("labelGamma");
			Control arg_937_0 = this.labelGamma;
			size = new Size(112, 16);
			arg_937_0.set_Size(size);
			this.labelGamma.set_TabIndex(6);
			this.labelGamma.set_Text("&Midtones (gamma)");
			this.sliderGamma.set_AutoSize(false);
			this.sliderGamma.set_LargeChange(10);
			Control arg_987_0 = this.sliderGamma;
			location = new Point(32, 168);
			arg_987_0.set_Location(location);
			this.sliderGamma.set_Maximum(100);
			this.sliderGamma.set_Minimum(-100);
			this.sliderGamma.set_Name("sliderGamma");
			Control arg_9CB_0 = this.sliderGamma;
			size = new Size(160, 20);
			arg_9CB_0.set_Size(size);
			this.sliderGamma.set_TabIndex(7);
			this.sliderGamma.set_TickFrequency(25);
			this.labelColors.set_FlatStyle(3);
			Control arg_A0A_0 = this.labelColors;
			location = new Point(8, 280);
			arg_A0A_0.set_Location(location);
			this.labelColors.set_Name("labelColors");
			Control arg_A31_0 = this.labelColors;
			size = new Size(96, 16);
			arg_A31_0.set_Size(size);
			this.labelColors.set_TabIndex(12);
			this.labelColors.set_Text("Convert Colors");
			Control arg_A69_0 = this.numBright;
			location = new Point(240, 104);
			arg_A69_0.set_Location(location);
			NumericUpDown arg_A99_0 = this.numBright;
			minimum = new decimal(new int[]
			{
				100,
				0,
				0,
				-2147483648
			});
			arg_A99_0.set_Minimum(minimum);
			this.numBright.set_Name("numBright");
			Control arg_AC0_0 = this.numBright;
			size = new Size(48, 20);
			arg_AC0_0.set_Size(size);
			this.numBright.set_TabIndex(5);
			Control arg_AEA_0 = this.numGamma;
			location = new Point(240, 168);
			arg_AEA_0.set_Location(location);
			NumericUpDown arg_B1A_0 = this.numGamma;
			minimum = new decimal(new int[]
			{
				100,
				0,
				0,
				-2147483648
			});
			arg_B1A_0.set_Minimum(minimum);
			this.numGamma.set_Name("numGamma");
			Control arg_B41_0 = this.numGamma;
			size = new Size(48, 20);
			arg_B41_0.set_Size(size);
			this.numGamma.set_TabIndex(8);
			Control arg_B6B_0 = this.numSat;
			location = new Point(240, 232);
			arg_B6B_0.set_Location(location);
			NumericUpDown arg_B9B_0 = this.numSat;
			minimum = new decimal(new int[]
			{
				100,
				0,
				0,
				-2147483648
			});
			arg_B9B_0.set_Minimum(minimum);
			this.numSat.set_Name("numSat");
			Control arg_BC2_0 = this.numSat;
			size = new Size(48, 20);
			arg_BC2_0.set_Size(size);
			this.numSat.set_TabIndex(11);
			this.pageCrop.get_Controls().Add(this.panelCrop);
			Control arg_BFC_0 = this.pageCrop;
			location = new Point(4, 22);
			arg_BFC_0.set_Location(location);
			this.pageCrop.set_Name("pageCrop");
			Control arg_C29_0 = this.pageCrop;
			size = new Size(300, 362);
			arg_C29_0.set_Size(size);
			this.pageCrop.set_TabIndex(0);
			this.pageCrop.set_Text("Crop");
			this.panelCrop.get_Controls().Add(this.buttonCrop);
			this.panelCrop.get_Controls().Add(this.pictCropCoord);
			this.panelCrop.get_Controls().Add(this.panelCropSep);
			this.panelCrop.get_Controls().Add(this.labelCrop);
			this.panelCrop.get_Controls().Add(this.labelPicture);
			this.panelCrop.get_Controls().Add(this.panelPictureSep);
			this.panelCrop.get_Controls().Add(this.pictCropDim);
			this.panelCrop.get_Controls().Add(this.buttonClearCrop);
			this.panelCrop.set_Dock(5);
			Control arg_D17_0 = this.panelCrop;
			location = new Point(0, 0);
			arg_D17_0.set_Location(location);
			this.panelCrop.set_Name("panelCrop");
			Control arg_D44_0 = this.panelCrop;
			size = new Size(300, 362);
			arg_D44_0.set_Size(size);
			this.panelCrop.set_TabIndex(0);
			this.buttonCrop.set_Enabled(false);
			this.buttonCrop.set_FlatStyle(3);
			Control arg_D83_0 = this.buttonCrop;
			location = new Point(16, 200);
			arg_D83_0.set_Location(location);
			this.buttonCrop.set_Name("buttonCrop");
			this.buttonCrop.set_TabIndex(4);
			this.buttonCrop.set_Text("Cr&op");
			Control arg_DC7_0 = this.pictCropCoord;
			location = new Point(16, 40);
			arg_DC7_0.set_Location(location);
			this.pictCropCoord.set_Name("pictCropCoord");
			Control arg_DF1_0 = this.pictCropCoord;
			size = new Size(176, 64);
			arg_DF1_0.set_Size(size);
			this.pictCropCoord.set_TabIndex(2);
			this.pictCropCoord.set_TabStop(false);
			this.panelCropSep.set_BackColor(SystemColors.get_ControlDark());
			Control arg_E30_0 = this.panelCropSep;
			location = new Point(8, 32);
			arg_E30_0.set_Location(location);
			this.panelCropSep.set_Name("panelCropSep");
			Control arg_E59_0 = this.panelCropSep;
			size = new Size(276, 1);
			arg_E59_0.set_Size(size);
			this.panelCropSep.set_TabIndex(1);
			this.labelCrop.set_FlatStyle(3);
			Control arg_E88_0 = this.labelCrop;
			location = new Point(8, 16);
			arg_E88_0.set_Location(location);
			this.labelCrop.set_Name("labelCrop");
			Control arg_EB2_0 = this.labelCrop;
			size = new Size(136, 16);
			arg_EB2_0.set_Size(size);
			this.labelCrop.set_TabIndex(0);
			this.labelCrop.set_Text("Crop Coordinates");
			this.labelPicture.set_FlatStyle(3);
			Control arg_EF1_0 = this.labelPicture;
			location = new Point(8, 120);
			arg_EF1_0.set_Location(location);
			this.labelPicture.set_Name("labelPicture");
			Control arg_F1B_0 = this.labelPicture;
			size = new Size(136, 16);
			arg_F1B_0.set_Size(size);
			this.labelPicture.set_TabIndex(2);
			this.labelPicture.set_Text("Picture Dimensions");
			this.panelPictureSep.set_BackColor(SystemColors.get_ControlDark());
			Control arg_F61_0 = this.panelPictureSep;
			location = new Point(8, 136);
			arg_F61_0.set_Location(location);
			this.panelPictureSep.set_Name("panelPictureSep");
			Control arg_F8A_0 = this.panelPictureSep;
			size = new Size(276, 1);
			arg_F8A_0.set_Size(size);
			this.panelPictureSep.set_TabIndex(3);
			Control arg_FB1_0 = this.pictCropDim;
			location = new Point(16, 144);
			arg_FB1_0.set_Location(location);
			this.pictCropDim.set_Name("pictCropDim");
			Control arg_FDB_0 = this.pictCropDim;
			size = new Size(176, 40);
			arg_FDB_0.set_Size(size);
			this.pictCropDim.set_TabIndex(2);
			this.pictCropDim.set_TabStop(false);
			this.buttonClearCrop.set_Enabled(false);
			this.buttonClearCrop.set_FlatStyle(3);
			Control arg_1026_0 = this.buttonClearCrop;
			location = new Point(104, 200);
			arg_1026_0.set_Location(location);
			this.buttonClearCrop.set_Name("buttonClearCrop");
			this.buttonClearCrop.set_TabIndex(5);
			this.buttonClearCrop.set_Text("C&lear Crop");
			this.panelSave.get_Controls().Add(this.buttonSave);
			this.panelSave.get_Controls().Add(this.buttonReset);
			this.panelSave.get_Controls().Add(this.buttonUndo);
			this.panelSave.get_Controls().Add(this.buttonRedo);
			this.panelSave.set_Dock(2);
			Control arg_10D0_0 = this.panelSave;
			location = new Point(2, 390);
			arg_10D0_0.set_Location(location);
			this.panelSave.set_Name("panelSave");
			Control arg_10FA_0 = this.panelSave;
			size = new Size(308, 40);
			arg_10FA_0.set_Size(size);
			this.panelSave.set_TabIndex(1);
			this.buttonSave.set_FlatStyle(3);
			Control arg_1129_0 = this.buttonSave;
			location = new Point(16, 8);
			arg_1129_0.set_Location(location);
			this.buttonSave.set_Name("buttonSave");
			Control arg_1150_0 = this.buttonSave;
			size = new Size(56, 23);
			arg_1150_0.set_Size(size);
			this.buttonSave.set_TabIndex(0);
			this.buttonSave.set_Text("S&ave");
			this.buttonReset.set_FlatStyle(3);
			Control arg_118F_0 = this.buttonReset;
			location = new Point(88, 8);
			arg_118F_0.set_Location(location);
			this.buttonReset.set_Name("buttonReset");
			Control arg_11B6_0 = this.buttonReset;
			size = new Size(56, 23);
			arg_11B6_0.set_Size(size);
			this.buttonReset.set_TabIndex(1);
			this.buttonReset.set_Text("&Reset");
			this.buttonUndo.set_FlatStyle(3);
			Control arg_11F8_0 = this.buttonUndo;
			location = new Point(160, 8);
			arg_11F8_0.set_Location(location);
			this.buttonUndo.set_Name("buttonUndo");
			Control arg_121F_0 = this.buttonUndo;
			size = new Size(56, 23);
			arg_121F_0.set_Size(size);
			this.buttonUndo.set_TabIndex(2);
			this.buttonUndo.set_Text("&Undo");
			this.buttonRedo.set_FlatStyle(3);
			Control arg_1261_0 = this.buttonRedo;
			location = new Point(232, 8);
			arg_1261_0.set_Location(location);
			this.buttonRedo.set_Name("buttonRedo");
			Control arg_1288_0 = this.buttonRedo;
			size = new Size(56, 23);
			arg_1288_0.set_Size(size);
			this.buttonRedo.set_TabIndex(3);
			this.buttonRedo.set_Text("R&edo");
			ImageList arg_12BB_0 = this.imageList;
			size = new Size(23, 23);
			arg_12BB_0.set_ImageSize(size);
			this.imageList.set_ImageStream((ImageListStreamer)resourceManager.GetObject("imageList.ImageStream"));
			this.imageList.set_TransparentColor(Color.get_Lime());
			this.get_Controls().Add(this.tabControl);
			this.get_Controls().Add(this.panelSave);
			this.get_DockPadding().set_All(2);
			this.set_Name("DetailsActions");
			size = new Size(312, 432);
			this.set_Size(size);
			this.tabControl.ResumeLayout(false);
			this.pageAdjust.ResumeLayout(false);
			this.panelAdjust.ResumeLayout(false);
			this.numContrast.EndInit();
			this.sliderBright.EndInit();
			this.sliderContrast.EndInit();
			this.sliderSat.EndInit();
			this.sliderGamma.EndInit();
			this.numBright.EndInit();
			this.numGamma.EndInit();
			this.numSat.EndInit();
			this.pageCrop.ResumeLayout(false);
			this.panelCrop.ResumeLayout(false);
			this.panelSave.ResumeLayout(false);
			this.ResumeLayout(false);
		}
		public DetailsActions()
		{
			this._updatingSlider = false;
			this.InitializeComponent();
			this.sliderContrast.set_Tag(new DetailsActions.SliderData(this.numContrast, PhotoAction.Contrast, 0, 1));
			this.sliderBright.set_Tag(new DetailsActions.SliderData(this.numBright, PhotoAction.Brightness, 2, 3));
			this.sliderGamma.set_Tag(new DetailsActions.SliderData(this.numGamma, PhotoAction.Gamma, 4, 5));
			this.sliderSat.set_Tag(new DetailsActions.SliderData(this.numSat, PhotoAction.Saturation, 6, 7));
			this.numContrast.set_Tag(this.sliderContrast);
			this.numBright.set_Tag(this.sliderBright);
			this.numGamma.set_Tag(this.sliderGamma);
			this.numSat.set_Tag(this.sliderSat);
			this.InitCropDrawing();
			this.panelSave.set_BackColor(SystemColors.get_Control());
			this.buttonSave.set_Tag(DetailsCommandButton.Save);
			this.buttonReset.set_Tag(DetailsCommandButton.Reset);
			this.buttonUndo.set_Tag(DetailsCommandButton.Undo);
			this.buttonRedo.set_Tag(DetailsCommandButton.Redo);
		}
		public void SetActionValues(int contrast, int brightness, int gamma, int saturation)
		{
			this.sliderContrast.set_Value(contrast);
			this.sliderBright.set_Value(brightness);
			this.sliderGamma.set_Value(gamma);
			this.sliderSat.set_Value(saturation);
		}
		public void EnableCommandButtons(bool photoDirty)
		{
			this.buttonSave.set_Enabled(photoDirty);
			this.buttonReset.set_Enabled(photoDirty);
			this.buttonUndo.set_Enabled(photoDirty);
			this.buttonRedo.set_Enabled(Global.ActionList.ContainsRedo);
		}
		public void CropDataChanged(Size orgSize, Size newSize, Rectangle cropBounds)
		{
			this._cropBounds = cropBounds;
			this.DrawCropCoords(cropBounds);
			this.DrawCropDim(orgSize, newSize);
			this.buttonCrop.set_Enabled(BooleanType.FromObject(Interaction.IIf(this._cropBounds.get_Width() > 0 & this._cropBounds.get_Height() > 0, true, false)));
			this.buttonClearCrop.set_Enabled(this.buttonCrop.get_Enabled());
		}
		private void buttonGrayscale_Click(object sender, EventArgs e)
		{
			this.OnNewAction(new ActionItem(PhotoAction.ConvertGrayscale));
		}
		private void buttonSepia_Click(object sender, EventArgs e)
		{
			this.OnNewAction(new ActionItem(PhotoAction.ConvertSepia));
		}
		private void tabControl_SelectedIndexChanged(object sender, EventArgs e)
		{
			if (this.CropModeChangedEvent != null)
			{
				this.CropModeChangedEvent(this, new CropModeChangedEventArgs(BooleanType.FromObject(Interaction.IIf(this.tabControl.get_SelectedIndex() == 1, true, false))));
			}
		}
		private void buttonCrop_Click(object sender, EventArgs e)
		{
			this.OnNewAction(new ActionItem(PhotoAction.Crop, this._cropBounds));
		}
		private void buttonClearCrop_Click(object sender, EventArgs e)
		{
			if (this.CommandButtonClickedEvent != null)
			{
				this.CommandButtonClickedEvent(this, new CommandButtonClickedEventArgs(DetailsCommandButton.ClearCrop));
			}
		}
		private void commandButton_Clicked(object sender, EventArgs e)
		{
			Button button = (Button)sender;
			if (this.CommandButtonClickedEvent != null)
			{
				this.CommandButtonClickedEvent(this, new CommandButtonClickedEventArgs((DetailsCommandButton)button.get_Tag()));
			}
		}
		private void panelAdjust_Paint(object sender, PaintEventArgs e)
		{
			this.DrawSliderIcons(e.get_Graphics(), this.sliderContrast);
			this.DrawSliderIcons(e.get_Graphics(), this.sliderBright);
			this.DrawSliderIcons(e.get_Graphics(), this.sliderGamma);
			this.DrawSliderIcons(e.get_Graphics(), this.sliderSat);
		}
		private void slider_ValueChanged(object sender, EventArgs e)
		{
			TrackBar trackBar = (TrackBar)sender;
			DetailsActions.SliderData sliderData = (DetailsActions.SliderData)trackBar.get_Tag();
			this._updatingSlider = true;
			sliderData.NumericUpDown.set_Value(new decimal(trackBar.get_Value()));
			this._updatingSlider = false;
			sliderData.ValueChanged = true;
		}
		private void slider_KeyUp(object sender, KeyEventArgs e)
		{
			TrackBar trackBar = (TrackBar)sender;
			DetailsActions.SliderData sliderData = (DetailsActions.SliderData)trackBar.get_Tag();
			if (sliderData.ValueChanged)
			{
				this.OnNewAction(new ActionItem(sliderData.Action, trackBar.get_Value()));
				sliderData.ValueChanged = false;
			}
		}
		private void slider_MouseUp(object sender, MouseEventArgs e)
		{
			TrackBar trackBar = (TrackBar)sender;
			DetailsActions.SliderData sliderData = (DetailsActions.SliderData)trackBar.get_Tag();
			if (sliderData.ValueChanged)
			{
				this.OnNewAction(new ActionItem(sliderData.Action, trackBar.get_Value()));
				sliderData.ValueChanged = false;
			}
		}
		private void spinner_Enter(object sender, EventArgs e)
		{
			NumericUpDown numericUpDown = (NumericUpDown)sender;
			numericUpDown.Select(0, numericUpDown.get_Text().get_Length());
		}
		private void spinner_ValueChanged(object sender, EventArgs e)
		{
			if (!this._updatingSlider)
			{
				NumericUpDown numericUpDown = (NumericUpDown)sender;
				TrackBar trackBar = (TrackBar)numericUpDown.get_Tag();
				trackBar.set_Value(Convert.ToInt32(numericUpDown.get_Value(), CultureInfo.get_InvariantCulture()));
			}
		}
		private void spinner_KeyUp(object sender, KeyEventArgs e)
		{
			this.spinner_ValueChanged(RuntimeHelpers.GetObjectValue(sender), EventArgs.Empty);
			NumericUpDown numericUpDown = (NumericUpDown)sender;
			TrackBar trackBar = (TrackBar)numericUpDown.get_Tag();
			DetailsActions.SliderData sliderData = (DetailsActions.SliderData)trackBar.get_Tag();
			if (sliderData.ValueChanged)
			{
				this.OnNewAction(new ActionItem(sliderData.Action, trackBar.get_Value()));
				sliderData.ValueChanged = false;
			}
		}
		private void spinner_MouseUp(object sender, MouseEventArgs e)
		{
			NumericUpDown numericUpDown = (NumericUpDown)sender;
			TrackBar trackBar = (TrackBar)numericUpDown.get_Tag();
			DetailsActions.SliderData sliderData = (DetailsActions.SliderData)trackBar.get_Tag();
			if (sliderData.ValueChanged)
			{
				this.OnNewAction(new ActionItem(sliderData.Action, trackBar.get_Value()));
				sliderData.ValueChanged = false;
			}
		}
		private void OnNewAction(ActionItem actionItem)
		{
			Global.SetSliderValues(this.sliderContrast.get_Value(), this.sliderBright.get_Value(), this.sliderGamma.get_Value(), this.sliderSat.get_Value());
			if (this.ActionEvent != null)
			{
				this.ActionEvent(this, new ActionEventArgs(actionItem));
			}
		}
		private void DrawCropCoords(Rectangle bounds)
		{
			Brush brush = (Brush)Interaction.IIf(this.pictCropCoord.get_Enabled(), SystemBrushes.get_ControlText(), SystemBrushes.FromSystemColor(SystemColors.get_GrayText()));
			Graphics graphics = Graphics.FromImage(this.pictCropCoord.get_Image());
			checked
			{
				try
				{
					graphics.Clear(SystemColors.get_Control());
					graphics.DrawString("Left:", this.get_Font(), brush, 0f, 0f);
					graphics.DrawString("Right:", this.get_Font(), brush, 0f, (float)(this.get_Font().get_Height() + 3));
					graphics.DrawString("Top:", this.get_Font(), brush, 0f, (float)((this.get_Font().get_Height() + 3) * 2));
					graphics.DrawString("Bottom:", this.get_Font(), brush, 0f, (float)((this.get_Font().get_Height() + 3) * 3));
					graphics.DrawString(StringType.FromInteger(bounds.get_Left()), this.get_Font(), brush, 48f, 0f);
					graphics.DrawString(StringType.FromInteger(bounds.get_Right()), this.get_Font(), brush, 48f, (float)(this.get_Font().get_Height() + 3));
					graphics.DrawString(StringType.FromInteger(bounds.get_Top()), this.get_Font(), brush, 48f, (float)((this.get_Font().get_Height() + 3) * 2));
					graphics.DrawString(StringType.FromInteger(bounds.get_Bottom()), this.get_Font(), brush, 48f, (float)((this.get_Font().get_Height() + 3) * 3));
					int width = graphics.MeasureString(StringType.FromInteger(Math.Max(bounds.get_Right(), bounds.get_Bottom())), this.get_Font()).ToSize().get_Width();
					graphics.DrawString("pixels", this.get_Font(), brush, (float)(48 + width + 5), 0f);
					graphics.DrawString("pixels", this.get_Font(), brush, (float)(48 + width + 5), (float)(this.get_Font().get_Height() + 3));
					graphics.DrawString("pixels", this.get_Font(), brush, (float)(48 + width + 5), (float)((this.get_Font().get_Height() + 3) * 2));
					graphics.DrawString("pixels", this.get_Font(), brush, (float)(48 + width + 5), (float)((this.get_Font().get_Height() + 3) * 3));
				}
				finally
				{
					graphics.Dispose();
				}
				this.pictCropCoord.Invalidate();
			}
		}
		private void DrawCropDim(Size orgSize, Size newSize)
		{
			Brush brush = (Brush)Interaction.IIf(this.pictCropDim.get_Enabled(), SystemBrushes.get_ControlText(), SystemBrushes.FromSystemColor(SystemColors.get_GrayText()));
			Graphics graphics = Graphics.FromImage(this.pictCropDim.get_Image());
			checked
			{
				try
				{
					graphics.Clear(SystemColors.get_Control());
					graphics.DrawString("Original:", this.get_Font(), brush, 0f, 0f);
					graphics.DrawString("Selection:", this.get_Font(), brush, 0f, (float)(this.get_Font().get_Height() + 3));
					graphics.DrawString(string.Format("{0} x {1} pixels", orgSize.get_Width(), orgSize.get_Height()), this.get_Font(), brush, 55f, 0f);
					graphics.DrawString(string.Format("{0} x {1} pixels", newSize.get_Width(), newSize.get_Height()), this.get_Font(), (Brush)Interaction.IIf(this.pictCropDim.get_Enabled(), Brushes.get_Firebrick(), brush), 55f, (float)(this.get_Font().get_Height() + 3));
				}
				finally
				{
					graphics.Dispose();
				}
				this.pictCropDim.Invalidate();
			}
		}
		private void InitCropDrawing()
		{
			this.pictCropCoord.set_Image(new Bitmap(this.pictCropCoord.get_Width(), this.pictCropCoord.get_Height()));
			this.pictCropDim.set_Image(new Bitmap(this.pictCropDim.get_Width(), this.pictCropDim.get_Height()));
		}
		private void DrawSliderIcons(Graphics g, TrackBar slider)
		{
			DetailsActions.SliderData sliderData = (DetailsActions.SliderData)slider.get_Tag();
			checked
			{
				this.imageList.Draw(g, slider.get_Left() - this.imageList.get_ImageSize().get_Width() - 3, slider.get_Top() - 3, sliderData.ImageIndexLow);
				this.imageList.Draw(g, slider.get_Right() + 3, slider.get_Top() - 3, sliderData.ImageIndexHigh);
			}
		}
	}
}
