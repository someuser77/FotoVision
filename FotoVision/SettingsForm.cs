using System;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;
using System.Resources;
using System.Runtime.CompilerServices;
using System.Windows.Forms;
namespace FotoVision
{
	public class SettingsForm : Form
	{
		private enum PhotoQuality
		{
			Low = 45,
			Medium = 70,
			High = 85
		}
		[AccessedThroughProperty("checkUpload")]
		private CheckBox _checkUpload;
		[AccessedThroughProperty("checkConfirm")]
		private CheckBox _checkConfirm;
		[AccessedThroughProperty("pageApplication")]
		private TabPage _pageApplication;
		[AccessedThroughProperty("radioLow")]
		private RadioButton _radioLow;
		[AccessedThroughProperty("textEmailSubject")]
		private TextBox _textEmailSubject;
		[AccessedThroughProperty("labelPassword")]
		private Label _labelPassword;
		[AccessedThroughProperty("spinPixels")]
		private NumericUpDown _spinPixels;
		[AccessedThroughProperty("radioMed")]
		private RadioButton _radioMed;
		[AccessedThroughProperty("buttonCancel")]
		private Button _buttonCancel;
		[AccessedThroughProperty("pageWebHosting")]
		private TabPage _pageWebHosting;
		[AccessedThroughProperty("buttonOK")]
		private Button _buttonOK;
		[AccessedThroughProperty("radioHigh")]
		private RadioButton _radioHigh;
		[AccessedThroughProperty("textPassword")]
		private TextBox _textPassword;
		[AccessedThroughProperty("labelWebsite")]
		private Label _labelWebsite;
		[AccessedThroughProperty("labelPixels")]
		private Label _labelPixels;
		[AccessedThroughProperty("labelLocation")]
		private Label _labelLocation;
		[AccessedThroughProperty("labelPublish")]
		private Label _labelPublish;
		[AccessedThroughProperty("labelQuality")]
		private Label _labelQuality;
		[AccessedThroughProperty("textLocation")]
		private TextBox _textLocation;
		[AccessedThroughProperty("labelEmailSubject")]
		private Label _labelEmailSubject;
		[AccessedThroughProperty("labelApplication")]
		private Label _labelApplication;
		[AccessedThroughProperty("labelAppDesc")]
		private Label _labelAppDesc;
		[AccessedThroughProperty("tabControl")]
		private TabControl _tabControl;
		[AccessedThroughProperty("labelSettings")]
		private Label _labelSettings;
		[AccessedThroughProperty("checkExif")]
		private CheckBox _checkExif;
		private IContainer components;
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
				}
				this._tabControl = value;
				if (this._tabControl != null)
				{
				}
			}
		}
		private TextBox textPassword
		{
			get
			{
				return this._textPassword;
			}
			[MethodImpl(32)]
			set
			{
				if (this._textPassword != null)
				{
				}
				this._textPassword = value;
				if (this._textPassword != null)
				{
				}
			}
		}
		private TabPage pageWebHosting
		{
			get
			{
				return this._pageWebHosting;
			}
			[MethodImpl(32)]
			set
			{
				if (this._pageWebHosting != null)
				{
				}
				this._pageWebHosting = value;
				if (this._pageWebHosting != null)
				{
				}
			}
		}
		private NumericUpDown spinPixels
		{
			get
			{
				return this._spinPixels;
			}
			[MethodImpl(32)]
			set
			{
				if (this._spinPixels != null)
				{
				}
				this._spinPixels = value;
				if (this._spinPixels != null)
				{
				}
			}
		}
		private TextBox textLocation
		{
			get
			{
				return this._textLocation;
			}
			[MethodImpl(32)]
			set
			{
				if (this._textLocation != null)
				{
				}
				this._textLocation = value;
				if (this._textLocation != null)
				{
				}
			}
		}
		private TextBox textEmailSubject
		{
			get
			{
				return this._textEmailSubject;
			}
			[MethodImpl(32)]
			set
			{
				if (this._textEmailSubject != null)
				{
				}
				this._textEmailSubject = value;
				if (this._textEmailSubject != null)
				{
				}
			}
		}
		private TabPage pageApplication
		{
			get
			{
				return this._pageApplication;
			}
			[MethodImpl(32)]
			set
			{
				if (this._pageApplication != null)
				{
				}
				this._pageApplication = value;
				if (this._pageApplication != null)
				{
				}
			}
		}
		private CheckBox checkConfirm
		{
			get
			{
				return this._checkConfirm;
			}
			[MethodImpl(32)]
			set
			{
				if (this._checkConfirm != null)
				{
				}
				this._checkConfirm = value;
				if (this._checkConfirm != null)
				{
				}
			}
		}
		private CheckBox checkUpload
		{
			get
			{
				return this._checkUpload;
			}
			[MethodImpl(32)]
			set
			{
				if (this._checkUpload != null)
				{
				}
				this._checkUpload = value;
				if (this._checkUpload != null)
				{
				}
			}
		}
		private CheckBox checkExif
		{
			get
			{
				return this._checkExif;
			}
			[MethodImpl(32)]
			set
			{
				if (this._checkExif != null)
				{
				}
				this._checkExif = value;
				if (this._checkExif != null)
				{
				}
			}
		}
		private RadioButton radioLow
		{
			get
			{
				return this._radioLow;
			}
			[MethodImpl(32)]
			set
			{
				if (this._radioLow != null)
				{
				}
				this._radioLow = value;
				if (this._radioLow != null)
				{
				}
			}
		}
		private RadioButton radioMed
		{
			get
			{
				return this._radioMed;
			}
			[MethodImpl(32)]
			set
			{
				if (this._radioMed != null)
				{
				}
				this._radioMed = value;
				if (this._radioMed != null)
				{
				}
			}
		}
		private RadioButton radioHigh
		{
			get
			{
				return this._radioHigh;
			}
			[MethodImpl(32)]
			set
			{
				if (this._radioHigh != null)
				{
				}
				this._radioHigh = value;
				if (this._radioHigh != null)
				{
				}
			}
		}
		private Button buttonOK
		{
			get
			{
				return this._buttonOK;
			}
			[MethodImpl(32)]
			set
			{
				if (this._buttonOK != null)
				{
				}
				this._buttonOK = value;
				if (this._buttonOK != null)
				{
				}
			}
		}
		private Button buttonCancel
		{
			get
			{
				return this._buttonCancel;
			}
			[MethodImpl(32)]
			set
			{
				if (this._buttonCancel != null)
				{
				}
				this._buttonCancel = value;
				if (this._buttonCancel != null)
				{
				}
			}
		}
		private Label labelWebsite
		{
			get
			{
				return this._labelWebsite;
			}
			[MethodImpl(32)]
			set
			{
				if (this._labelWebsite != null)
				{
				}
				this._labelWebsite = value;
				if (this._labelWebsite != null)
				{
				}
			}
		}
		private Label labelPixels
		{
			get
			{
				return this._labelPixels;
			}
			[MethodImpl(32)]
			set
			{
				if (this._labelPixels != null)
				{
				}
				this._labelPixels = value;
				if (this._labelPixels != null)
				{
				}
			}
		}
		private Label labelPassword
		{
			get
			{
				return this._labelPassword;
			}
			[MethodImpl(32)]
			set
			{
				if (this._labelPassword != null)
				{
				}
				this._labelPassword = value;
				if (this._labelPassword != null)
				{
				}
			}
		}
		private Label labelLocation
		{
			get
			{
				return this._labelLocation;
			}
			[MethodImpl(32)]
			set
			{
				if (this._labelLocation != null)
				{
				}
				this._labelLocation = value;
				if (this._labelLocation != null)
				{
				}
			}
		}
		private Label labelPublish
		{
			get
			{
				return this._labelPublish;
			}
			[MethodImpl(32)]
			set
			{
				if (this._labelPublish != null)
				{
				}
				this._labelPublish = value;
				if (this._labelPublish != null)
				{
				}
			}
		}
		private Label labelQuality
		{
			get
			{
				return this._labelQuality;
			}
			[MethodImpl(32)]
			set
			{
				if (this._labelQuality != null)
				{
				}
				this._labelQuality = value;
				if (this._labelQuality != null)
				{
				}
			}
		}
		private Label labelEmailSubject
		{
			get
			{
				return this._labelEmailSubject;
			}
			[MethodImpl(32)]
			set
			{
				if (this._labelEmailSubject != null)
				{
				}
				this._labelEmailSubject = value;
				if (this._labelEmailSubject != null)
				{
				}
			}
		}
		private Label labelApplication
		{
			get
			{
				return this._labelApplication;
			}
			[MethodImpl(32)]
			set
			{
				if (this._labelApplication != null)
				{
				}
				this._labelApplication = value;
				if (this._labelApplication != null)
				{
				}
			}
		}
		private Label labelAppDesc
		{
			get
			{
				return this._labelAppDesc;
			}
			[MethodImpl(32)]
			set
			{
				if (this._labelAppDesc != null)
				{
				}
				this._labelAppDesc = value;
				if (this._labelAppDesc != null)
				{
				}
			}
		}
		private Label labelSettings
		{
			get
			{
				return this._labelSettings;
			}
			[MethodImpl(32)]
			set
			{
				if (this._labelSettings != null)
				{
				}
				this._labelSettings = value;
				if (this._labelSettings != null)
				{
				}
			}
		}
		protected override void Dispose(bool disposing)
		{
			if (disposing && this.components != null)
			{
				this.components.Dispose();
			}
			base.Dispose(disposing);
		}
		[DebuggerStepThrough]
		private void InitializeComponent()
		{
			ResourceManager resourceManager = new ResourceManager(typeof(SettingsForm));
			this.buttonOK = new Button();
			this.buttonCancel = new Button();
			this.tabControl = new TabControl();
			this.pageWebHosting = new TabPage();
			this.radioLow = new RadioButton();
			this.labelWebsite = new Label();
			this.labelPixels = new Label();
			this.spinPixels = new NumericUpDown();
			this.labelPassword = new Label();
			this.textPassword = new TextBox();
			this.textLocation = new TextBox();
			this.labelLocation = new Label();
			this.labelPublish = new Label();
			this.labelQuality = new Label();
			this.radioMed = new RadioButton();
			this.radioHigh = new RadioButton();
			this.pageApplication = new TabPage();
			this.checkConfirm = new CheckBox();
			this.textEmailSubject = new TextBox();
			this.labelEmailSubject = new Label();
			this.checkUpload = new CheckBox();
			this.labelApplication = new Label();
			this.labelAppDesc = new Label();
			this.labelSettings = new Label();
			this.checkExif = new CheckBox();
			this.tabControl.SuspendLayout();
			this.pageWebHosting.SuspendLayout();
			this.spinPixels.BeginInit();
			this.pageApplication.SuspendLayout();
			this.SuspendLayout();
			this.buttonOK.set_Anchor(10);
			this.buttonOK.set_DialogResult(1);
			this.buttonOK.set_FlatStyle(3);
			Control arg_193_0 = this.buttonOK;
			Point location = new Point(208, 296);
			arg_193_0.set_Location(location);
			this.buttonOK.set_Name("buttonOK");
			Control arg_1BA_0 = this.buttonOK;
			Size size = new Size(56, 23);
			arg_1BA_0.set_Size(size);
			this.buttonOK.set_TabIndex(1);
			this.buttonOK.set_Text("OK");
			this.buttonCancel.set_Anchor(10);
			this.buttonCancel.set_DialogResult(2);
			this.buttonCancel.set_FlatStyle(3);
			Control arg_219_0 = this.buttonCancel;
			location = new Point(272, 296);
			arg_219_0.set_Location(location);
			this.buttonCancel.set_Name("buttonCancel");
			Control arg_240_0 = this.buttonCancel;
			size = new Size(56, 23);
			arg_240_0.set_Size(size);
			this.buttonCancel.set_TabIndex(2);
			this.buttonCancel.set_Text("Cancel");
			this.tabControl.set_Anchor(15);
			this.tabControl.Controls.Add(this.pageWebHosting);
			this.tabControl.Controls.Add(this.pageApplication);
			Control arg_2AB_0 = this.tabControl;
			location = new Point(8, 8);
			arg_2AB_0.set_Location(location);
			this.tabControl.set_Name("tabControl");
			this.tabControl.set_SelectedIndex(0);
			Control arg_2E4_0 = this.tabControl;
			size = new Size(320, 280);
			arg_2E4_0.set_Size(size);
			this.tabControl.set_TabIndex(0);
			this.pageWebHosting.Controls.Add(this.radioLow);
			this.pageWebHosting.Controls.Add(this.labelWebsite);
			this.pageWebHosting.Controls.Add(this.labelPixels);
			this.pageWebHosting.Controls.Add(this.spinPixels);
			this.pageWebHosting.Controls.Add(this.labelPassword);
			this.pageWebHosting.Controls.Add(this.textPassword);
			this.pageWebHosting.Controls.Add(this.textLocation);
			this.pageWebHosting.Controls.Add(this.labelLocation);
			this.pageWebHosting.Controls.Add(this.labelPublish);
			this.pageWebHosting.Controls.Add(this.labelQuality);
			this.pageWebHosting.Controls.Add(this.radioMed);
			this.pageWebHosting.Controls.Add(this.radioHigh);
			Control arg_40F_0 = this.pageWebHosting;
			location = new Point(4, 22);
			arg_40F_0.set_Location(location);
			this.pageWebHosting.set_Name("pageWebHosting");
			Control arg_43C_0 = this.pageWebHosting;
			size = new Size(312, 254);
			arg_43C_0.set_Size(size);
			this.pageWebHosting.set_TabIndex(2);
			this.pageWebHosting.set_Text("Website");
			this.radioLow.set_FlatStyle(3);
			Control arg_47F_0 = this.radioLow;
			location = new Point(24, 188);
			arg_47F_0.set_Location(location);
			this.radioLow.set_Name("radioLow");
			Control arg_4A6_0 = this.radioLow;
			size = new Size(48, 16);
			arg_4A6_0.set_Size(size);
			this.radioLow.set_TabIndex(9);
			this.radioLow.set_Text("L&ow");
			this.labelWebsite.set_FlatStyle(3);
            this.labelWebsite.set_Font(new Font("Microsoft Sans Serif", 8.25f, FontStyle.Bold, GraphicsUnit.Point, 0));
			Control arg_502_0 = this.labelWebsite;
			location = new Point(8, 8);
			arg_502_0.set_Location(location);
			this.labelWebsite.set_Name("labelWebsite");
			Control arg_529_0 = this.labelWebsite;
			size = new Size(104, 16);
			arg_529_0.set_Size(size);
			this.labelWebsite.set_TabIndex(0);
			this.labelWebsite.set_Text("Website settings");
			this.labelPixels.set_FlatStyle(3);
			Control arg_56B_0 = this.labelPixels;
			location = new Point(8, 140);
			arg_56B_0.set_Location(location);
			this.labelPixels.set_Name("labelPixels");
			Control arg_595_0 = this.labelPixels;
			size = new Size(192, 16);
			arg_595_0.set_Size(size);
			this.labelPixels.set_TabIndex(6);
			this.labelPixels.set_Text("&Maximum photo height or width in pixels:");
			NumericUpDown arg_5DD_0 = this.spinPixels;
			decimal num = new decimal(new int[]
			{
				20,
				0,
				0,
				0
			});
			arg_5DD_0.set_Increment(num);
			Control arg_5FB_0 = this.spinPixels;
			location = new Point(208, 136);
			arg_5FB_0.set_Location(location);
			NumericUpDown arg_62A_0 = this.spinPixels;
			num = new decimal(new int[]
			{
				10000,
				0,
				0,
				0
			});
			arg_62A_0.set_Maximum(num);
			NumericUpDown arg_656_0 = this.spinPixels;
			num = new decimal(new int[]
			{
				20,
				0,
				0,
				0
			});
			arg_656_0.set_Minimum(num);
			this.spinPixels.set_Name("spinPixels");
			Control arg_67D_0 = this.spinPixels;
			size = new Size(56, 20);
			arg_67D_0.set_Size(size);
			this.spinPixels.set_TabIndex(7);
			NumericUpDown arg_6B5_0 = this.spinPixels;
			num = new decimal(new int[]
			{
				20,
				0,
				0,
				0
			});
			arg_6B5_0.set_Value(num);
			this.labelPassword.set_FlatStyle(3);
			Control arg_6D8_0 = this.labelPassword;
			location = new Point(8, 68);
			arg_6D8_0.set_Location(location);
			this.labelPassword.set_Name("labelPassword");
			Control arg_6FF_0 = this.labelPassword;
			size = new Size(56, 16);
			arg_6FF_0.set_Size(size);
			this.labelPassword.set_TabIndex(3);
			this.labelPassword.set_Text("&Password:");
			this.textPassword.set_Anchor(13);
			Control arg_740_0 = this.textPassword;
			location = new Point(72, 64);
			arg_740_0.set_Location(location);
			this.textPassword.set_Name("textPassword");
			this.textPassword.set_PasswordChar('●');
			Control arg_77A_0 = this.textPassword;
			size = new Size(232, 20);
			arg_77A_0.set_Size(size);
			this.textPassword.set_TabIndex(4);
			this.textPassword.set_Text("");
			this.textLocation.set_Anchor(13);
			Control arg_7BB_0 = this.textLocation;
			location = new Point(72, 32);
			arg_7BB_0.set_Location(location);
			this.textLocation.set_Name("textLocation");
			Control arg_7E5_0 = this.textLocation;
			size = new Size(232, 20);
			arg_7E5_0.set_Size(size);
			this.textLocation.set_TabIndex(2);
			this.textLocation.set_Text("");
			this.labelLocation.set_FlatStyle(3);
			Control arg_824_0 = this.labelLocation;
			location = new Point(8, 36);
			arg_824_0.set_Location(location);
			this.labelLocation.set_Name("labelLocation");
			Control arg_84B_0 = this.labelLocation;
			size = new Size(72, 16);
			arg_84B_0.set_Size(size);
			this.labelLocation.set_TabIndex(1);
			this.labelLocation.set_Text("&Location:");
			this.labelPublish.set_FlatStyle(3);
            this.labelPublish.set_Font(new Font("Microsoft Sans Serif", 8.25f, FontStyle.Bold, GraphicsUnit.Point, 0));
			Control arg_8A7_0 = this.labelPublish;
			location = new Point(8, 112);
			arg_8A7_0.set_Location(location);
			this.labelPublish.set_Name("labelPublish");
			Control arg_8CE_0 = this.labelPublish;
			size = new Size(112, 16);
			arg_8CE_0.set_Size(size);
			this.labelPublish.set_TabIndex(5);
			this.labelPublish.set_Text("Published photos");
			this.labelQuality.set_FlatStyle(3);
			Control arg_910_0 = this.labelQuality;
			location = new Point(8, 168);
			arg_910_0.set_Location(location);
			this.labelQuality.set_Name("labelQuality");
			Control arg_93A_0 = this.labelQuality;
			size = new Size(248, 16);
			arg_93A_0.set_Size(size);
			this.labelQuality.set_TabIndex(8);
			this.labelQuality.set_Text("Image quality (lower quality creates a smaller file):");
			this.radioMed.set_FlatStyle(3);
			Control arg_97D_0 = this.radioMed;
			location = new Point(80, 188);
			arg_97D_0.set_Location(location);
			this.radioMed.set_Name("radioMed");
			Control arg_9A4_0 = this.radioMed;
			size = new Size(64, 16);
			arg_9A4_0.set_Size(size);
			this.radioMed.set_TabIndex(10);
			this.radioMed.set_Text("M&edium");
			this.radioHigh.set_FlatStyle(3);
			Control arg_9EB_0 = this.radioHigh;
			location = new Point(152, 188);
			arg_9EB_0.set_Location(location);
			this.radioHigh.set_Name("radioHigh");
			Control arg_A12_0 = this.radioHigh;
			size = new Size(56, 16);
			arg_A12_0.set_Size(size);
			this.radioHigh.set_TabIndex(11);
			this.radioHigh.set_Text("&High");
			this.pageApplication.Controls.Add(this.checkConfirm);
			this.pageApplication.Controls.Add(this.textEmailSubject);
			this.pageApplication.Controls.Add(this.labelEmailSubject);
			this.pageApplication.Controls.Add(this.checkUpload);
			this.pageApplication.Controls.Add(this.labelApplication);
			this.pageApplication.Controls.Add(this.labelAppDesc);
			this.pageApplication.Controls.Add(this.labelSettings);
			this.pageApplication.Controls.Add(this.checkExif);
			Control arg_AF6_0 = this.pageApplication;
			location = new Point(4, 22);
			arg_AF6_0.set_Location(location);
			this.pageApplication.set_Name("pageApplication");
			Control arg_B23_0 = this.pageApplication;
			size = new Size(312, 254);
			arg_B23_0.set_Size(size);
			this.pageApplication.set_TabIndex(1);
			this.pageApplication.set_Text("Application");
			this.checkConfirm.set_FlatStyle(3);
			Control arg_B65_0 = this.checkConfirm;
			location = new Point(8, 136);
			arg_B65_0.set_Location(location);
			this.checkConfirm.set_Name("checkConfirm");
			Control arg_B8C_0 = this.checkConfirm;
			size = new Size(120, 24);
			arg_B8C_0.set_Size(size);
			this.checkConfirm.set_TabIndex(5);
			this.checkConfirm.set_Text("&Confirm file deletions");
			this.textEmailSubject.set_Anchor(13);
			Control arg_BCC_0 = this.textEmailSubject;
			location = new Point(8, 72);
			arg_BCC_0.set_Location(location);
			this.textEmailSubject.set_Name("textEmailSubject");
			Control arg_BF6_0 = this.textEmailSubject;
			size = new Size(296, 20);
			arg_BF6_0.set_Size(size);
			this.textEmailSubject.set_TabIndex(3);
			this.textEmailSubject.set_Text("");
			this.labelEmailSubject.set_FlatStyle(3);
			Control arg_C35_0 = this.labelEmailSubject;
			location = new Point(8, 56);
			arg_C35_0.set_Location(location);
			this.labelEmailSubject.set_Name("labelEmailSubject");
			Control arg_C5F_0 = this.labelEmailSubject;
			size = new Size(280, 16);
			arg_C5F_0.set_Size(size);
			this.labelEmailSubject.set_TabIndex(2);
			this.labelEmailSubject.set_Text("&Default text for the subject line in your email:");
			this.checkUpload.set_FlatStyle(3);
			Control arg_CA1_0 = this.checkUpload;
			location = new Point(8, 160);
			arg_CA1_0.set_Location(location);
			this.checkUpload.set_Name("checkUpload");
			Control arg_CCB_0 = this.checkUpload;
			size = new Size(256, 24);
			arg_CCB_0.set_Size(size);
			this.checkUpload.set_TabIndex(6);
			this.checkUpload.set_Text("Close &upload window after uploading is complete");
			this.labelApplication.set_FlatStyle(3);
            this.labelApplication.set_Font(new Font("Microsoft Sans Serif", 8.25f, FontStyle.Bold, GraphicsUnit.Point, 0));
			Control arg_D26_0 = this.labelApplication;
			location = new Point(8, 8);
			arg_D26_0.set_Location(location);
			this.labelApplication.set_Name("labelApplication");
			Control arg_D50_0 = this.labelApplication;
			size = new Size(176, 16);
			arg_D50_0.set_Size(size);
			this.labelApplication.set_TabIndex(0);
			this.labelApplication.set_Text("Email notification settings");
			this.labelAppDesc.set_FlatStyle(3);
			Control arg_D8F_0 = this.labelAppDesc;
			location = new Point(8, 32);
			arg_D8F_0.set_Location(location);
			this.labelAppDesc.set_Name("labelAppDesc");
			Control arg_DB9_0 = this.labelAppDesc;
			size = new Size(256, 16);
			arg_DB9_0.set_Size(size);
			this.labelAppDesc.set_TabIndex(1);
			this.labelAppDesc.set_Text("You can notify your friends with your latest changes.");
			this.labelSettings.set_FlatStyle(3);
            this.labelSettings.set_Font(new Font("Microsoft Sans Serif", 8.25f, FontStyle.Bold, GraphicsUnit.Point, 0));
			Control arg_E15_0 = this.labelSettings;
			location = new Point(8, 112);
			arg_E15_0.set_Location(location);
			this.labelSettings.set_Name("labelSettings");
			Control arg_E3C_0 = this.labelSettings;
			size = new Size(104, 16);
			arg_E3C_0.set_Size(size);
			this.labelSettings.set_TabIndex(4);
			this.labelSettings.set_Text("General settings");
			this.checkExif.set_FlatStyle(3);
			Control arg_E7E_0 = this.checkExif;
			location = new Point(8, 184);
			arg_E7E_0.set_Location(location);
			this.checkExif.set_Name("checkExif");
			Control arg_EA8_0 = this.checkExif;
			size = new Size(216, 24);
			arg_EA8_0.set_Size(size);
			this.checkExif.set_TabIndex(7);
			this.checkExif.set_Text("Maintain &EXIF information in saved files");
			this.set_AcceptButton(this.buttonOK);
			size = new Size(5, 13);
			this.set_AutoScaleBaseSize(size);
			this.set_CancelButton(this.buttonCancel);
			size = new Size(338, 328);
			this.set_ClientSize(size);
			this.Controls.Add(this.tabControl);
			this.Controls.Add(this.buttonOK);
			this.Controls.Add(this.buttonCancel);
			this.set_Icon((Icon)resourceManager.GetObject("$this.Icon"));
			this.set_MaximizeBox(false);
			this.set_MinimizeBox(false);
			size = new Size(310, 320);
			this.set_MinimumSize(size);
			this.set_Name("SettingsForm");
			this.set_ShowInTaskbar(false);
			this.set_SizeGripStyle(1);
			this.set_StartPosition(4);
			this.set_Text("Options");
			this.tabControl.ResumeLayout(false);
			this.pageWebHosting.ResumeLayout(false);
			this.spinPixels.EndInit();
			this.pageApplication.ResumeLayout(false);
			this.ResumeLayout(false);
		}
		public SettingsForm()
		{
			this.InitializeComponent();
			int @int = Global.Settings.GetInt(SettingKey.OptionsDialogWidth);
			int int2 = Global.Settings.GetInt(SettingKey.OptionsDialogHeight);
			if (@int != 0 && int2 != 0)
			{
				this.set_Width(@int);
				this.set_Height(int2);
			}
		}
		protected override void OnLoad(EventArgs e)
		{
			base.OnLoad(e);
			this.RestoreSettings();
		}
		protected override void OnClosing(CancelEventArgs e)
		{
			base.OnClosing(e);
			if (this.DialogResult == 1)
			{
				this.SaveSettings();
			}
			Global.Settings.SetValue(SettingKey.OptionsDialogWidth, this.Width);
			Global.Settings.SetValue(SettingKey.OptionsDialogHeight, this.Height);
		}
		private void RestoreSettings()
		{
			this.textLocation.set_Text(Global.Settings.GetString(SettingKey.ServiceLocation));
			this.textPassword.set_Text(DataProtection.Decrypt(Global.Settings.GetString(SettingKey.ServicePassword), DataProtection.Store.User));
			this.spinPixels.set_Value(new decimal(Global.Settings.GetInt(SettingKey.PublishPhotoSize)));
			this.textEmailSubject.set_Text(Global.Settings.GetString(SettingKey.EmailSubject));
			this.checkConfirm.set_Checked(Global.Settings.GetBool(SettingKey.PromptFileDelete));
			this.checkUpload.set_Checked(Global.Settings.GetBool(SettingKey.CloseAfterUpload));
			this.checkExif.set_Checked(Global.Settings.GetBool(SettingKey.MaintainExifInfo));
			int @int = Global.Settings.GetInt(SettingKey.PublishPhotoQuality);
			int num = @int;
			if (num == 45)
			{
				this.radioLow.set_Checked(true);
			}
			else
			{
				if (num == 70)
				{
					this.radioMed.set_Checked(true);
				}
				else
				{
					if (num == 85)
					{
						this.radioHigh.set_Checked(true);
					}
					else
					{
						this.radioHigh.set_Checked(true);
						Global.Settings.SetValue(SettingKey.PublishPhotoQuality, SettingsForm.PhotoQuality.High);
					}
				}
			}
		}
		private void SaveSettings()
		{
			Global.Settings.AutoWrite = false;
			Global.Settings.SetValue(SettingKey.ServiceLocation, this.textLocation.Text);
			Global.Settings.SetValue(SettingKey.ServicePassword, DataProtection.Encrypt(this.textPassword.Text, DataProtection.Store.User));
			Global.Settings.SetValue(SettingKey.PublishPhotoSize, this.spinPixels.Value);
			Global.Settings.SetValue(SettingKey.EmailSubject, this.textEmailSubject.Text);
			Global.Settings.SetValue(SettingKey.PromptFileDelete, this.checkConfirm.Checked);
			Global.Settings.SetValue(SettingKey.CloseAfterUpload, this.checkUpload.Checked);
			Global.Settings.SetValue(SettingKey.MaintainExifInfo, this.checkExif.Checked);
			int num = 85;
			if (this.radioLow.Checked)
			{
				num = 45;
			}
			if (this.radioMed.Checked)
			{
				num = 70;
			}
			Global.Settings.SetValue(SettingKey.PublishPhotoQuality, num);
			Global.Settings.AutoWrite = true;
			Global.Settings.Write();
		}
	}
}
