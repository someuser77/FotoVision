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
			this.buttonOK.Anchor = 10;
			this.buttonOK.DialogResult = 1;
			this.buttonOK.FlatStyle = 3;
			Control arg_193_0 = this.buttonOK;
			Point location = new Point(208, 296);
			arg_193_0.Location = location;
			this.buttonOK.Name = "buttonOK";
			Control arg_1BA_0 = this.buttonOK;
			Size size = new Size(56, 23);
			arg_1BA_0.Size = size;
			this.buttonOK.TabIndex = 1;
			this.buttonOK.Text = "OK";
			this.buttonCancel.Anchor = 10;
			this.buttonCancel.DialogResult = 2;
			this.buttonCancel.FlatStyle = 3;
			Control arg_219_0 = this.buttonCancel;
			location = new Point(272, 296);
			arg_219_0.Location = location;
			this.buttonCancel.Name = "buttonCancel";
			Control arg_240_0 = this.buttonCancel;
			size = new Size(56, 23);
			arg_240_0.Size = size;
			this.buttonCancel.TabIndex = 2;
			this.buttonCancel.Text = "Cancel";
			this.tabControl.Anchor = 15;
			this.tabControl.Controls.Add(this.pageWebHosting);
			this.tabControl.Controls.Add(this.pageApplication);
			Control arg_2AB_0 = this.tabControl;
			location = new Point(8, 8);
			arg_2AB_0.Location = location;
			this.tabControl.Name = "tabControl";
			this.tabControl.SelectedIndex = 0;
			Control arg_2E4_0 = this.tabControl;
			size = new Size(320, 280);
			arg_2E4_0.Size = size;
			this.tabControl.TabIndex = 0;
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
			arg_40F_0.Location = location;
			this.pageWebHosting.Name = "pageWebHosting";
			Control arg_43C_0 = this.pageWebHosting;
			size = new Size(312, 254);
			arg_43C_0.Size = size;
			this.pageWebHosting.TabIndex = 2;
			this.pageWebHosting.Text = "Website";
			this.radioLow.FlatStyle = 3;
			Control arg_47F_0 = this.radioLow;
			location = new Point(24, 188);
			arg_47F_0.Location = location;
			this.radioLow.Name = "radioLow";
			Control arg_4A6_0 = this.radioLow;
			size = new Size(48, 16);
			arg_4A6_0.Size = size;
			this.radioLow.TabIndex = 9;
			this.radioLow.Text = "L&ow";
			this.labelWebsite.FlatStyle = 3;
            this.labelWebsite.Font = new Font("Microsoft Sans Serif", 8.25f, FontStyle.Bold, GraphicsUnit.Point, 0);
			Control arg_502_0 = this.labelWebsite;
			location = new Point(8, 8);
			arg_502_0.Location = location;
			this.labelWebsite.Name = "labelWebsite";
			Control arg_529_0 = this.labelWebsite;
			size = new Size(104, 16);
			arg_529_0.Size = size;
			this.labelWebsite.TabIndex = 0;
			this.labelWebsite.Text = "Website settings";
			this.labelPixels.FlatStyle = 3;
			Control arg_56B_0 = this.labelPixels;
			location = new Point(8, 140);
			arg_56B_0.Location = location;
			this.labelPixels.Name = "labelPixels";
			Control arg_595_0 = this.labelPixels;
			size = new Size(192, 16);
			arg_595_0.Size = size;
			this.labelPixels.TabIndex = 6;
			this.labelPixels.Text = "&Maximum photo height or width in pixels:";
			NumericUpDown arg_5DD_0 = this.spinPixels;
			decimal num = new decimal(new int[]
			{
				20,
				0,
				0,
				0
			});
			arg_5DD_0.Increment = num;
			Control arg_5FB_0 = this.spinPixels;
			location = new Point(208, 136);
			arg_5FB_0.Location = location;
			NumericUpDown arg_62A_0 = this.spinPixels;
			num = new decimal(new int[]
			{
				10000,
				0,
				0,
				0
			});
			arg_62A_0.Maximum = num;
			NumericUpDown arg_656_0 = this.spinPixels;
			num = new decimal(new int[]
			{
				20,
				0,
				0,
				0
			});
			arg_656_0.Minimum = num;
			this.spinPixels.Name = "spinPixels";
			Control arg_67D_0 = this.spinPixels;
			size = new Size(56, 20);
			arg_67D_0.Size = size;
			this.spinPixels.TabIndex = 7;
			NumericUpDown arg_6B5_0 = this.spinPixels;
			num = new decimal(new int[]
			{
				20,
				0,
				0,
				0
			});
			arg_6B5_0.Value = num;
			this.labelPassword.FlatStyle = 3;
			Control arg_6D8_0 = this.labelPassword;
			location = new Point(8, 68);
			arg_6D8_0.Location = location;
			this.labelPassword.Name = "labelPassword";
			Control arg_6FF_0 = this.labelPassword;
			size = new Size(56, 16);
			arg_6FF_0.Size = size;
			this.labelPassword.TabIndex = 3;
			this.labelPassword.Text = "&Password:";
			this.textPassword.Anchor = 13;
			Control arg_740_0 = this.textPassword;
			location = new Point(72, 64);
			arg_740_0.Location = location;
			this.textPassword.Name = "textPassword";
			this.textPassword.PasswordChar = '●';
			Control arg_77A_0 = this.textPassword;
			size = new Size(232, 20);
			arg_77A_0.Size = size;
			this.textPassword.TabIndex = 4;
			this.textPassword.Text = "";
			this.textLocation.Anchor = 13;
			Control arg_7BB_0 = this.textLocation;
			location = new Point(72, 32);
			arg_7BB_0.Location = location;
			this.textLocation.Name = "textLocation";
			Control arg_7E5_0 = this.textLocation;
			size = new Size(232, 20);
			arg_7E5_0.Size = size;
			this.textLocation.TabIndex = 2;
			this.textLocation.Text = "";
			this.labelLocation.FlatStyle = 3;
			Control arg_824_0 = this.labelLocation;
			location = new Point(8, 36);
			arg_824_0.Location = location;
			this.labelLocation.Name = "labelLocation";
			Control arg_84B_0 = this.labelLocation;
			size = new Size(72, 16);
			arg_84B_0.Size = size;
			this.labelLocation.TabIndex = 1;
			this.labelLocation.Text = "&Location:";
			this.labelPublish.FlatStyle = 3;
            this.labelPublish.Font = new Font("Microsoft Sans Serif", 8.25f, FontStyle.Bold, GraphicsUnit.Point, 0);
			Control arg_8A7_0 = this.labelPublish;
			location = new Point(8, 112);
			arg_8A7_0.Location = location;
			this.labelPublish.Name = "labelPublish";
			Control arg_8CE_0 = this.labelPublish;
			size = new Size(112, 16);
			arg_8CE_0.Size = size;
			this.labelPublish.TabIndex = 5;
			this.labelPublish.Text = "Published photos";
			this.labelQuality.FlatStyle = 3;
			Control arg_910_0 = this.labelQuality;
			location = new Point(8, 168);
			arg_910_0.Location = location;
			this.labelQuality.Name = "labelQuality";
			Control arg_93A_0 = this.labelQuality;
			size = new Size(248, 16);
			arg_93A_0.Size = size;
			this.labelQuality.TabIndex = 8;
			this.labelQuality.Text = "Image quality (lower quality creates a smaller file):";
			this.radioMed.FlatStyle = 3;
			Control arg_97D_0 = this.radioMed;
			location = new Point(80, 188);
			arg_97D_0.Location = location;
			this.radioMed.Name = "radioMed";
			Control arg_9A4_0 = this.radioMed;
			size = new Size(64, 16);
			arg_9A4_0.Size = size;
			this.radioMed.TabIndex = 10;
			this.radioMed.Text = "M&edium";
			this.radioHigh.FlatStyle = 3;
			Control arg_9EB_0 = this.radioHigh;
			location = new Point(152, 188);
			arg_9EB_0.Location = location;
			this.radioHigh.Name = "radioHigh";
			Control arg_A12_0 = this.radioHigh;
			size = new Size(56, 16);
			arg_A12_0.Size = size;
			this.radioHigh.TabIndex = 11;
			this.radioHigh.Text = "&High";
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
			arg_AF6_0.Location = location;
			this.pageApplication.Name = "pageApplication";
			Control arg_B23_0 = this.pageApplication;
			size = new Size(312, 254);
			arg_B23_0.Size = size;
			this.pageApplication.TabIndex = 1;
			this.pageApplication.Text = "Application";
			this.checkConfirm.FlatStyle = 3;
			Control arg_B65_0 = this.checkConfirm;
			location = new Point(8, 136);
			arg_B65_0.Location = location;
			this.checkConfirm.Name = "checkConfirm";
			Control arg_B8C_0 = this.checkConfirm;
			size = new Size(120, 24);
			arg_B8C_0.Size = size;
			this.checkConfirm.TabIndex = 5;
			this.checkConfirm.Text = "&Confirm file deletions";
			this.textEmailSubject.Anchor = 13;
			Control arg_BCC_0 = this.textEmailSubject;
			location = new Point(8, 72);
			arg_BCC_0.Location = location;
			this.textEmailSubject.Name = "textEmailSubject";
			Control arg_BF6_0 = this.textEmailSubject;
			size = new Size(296, 20);
			arg_BF6_0.Size = size;
			this.textEmailSubject.TabIndex = 3;
			this.textEmailSubject.Text = "";
			this.labelEmailSubject.FlatStyle = 3;
			Control arg_C35_0 = this.labelEmailSubject;
			location = new Point(8, 56);
			arg_C35_0.Location = location;
			this.labelEmailSubject.Name = "labelEmailSubject";
			Control arg_C5F_0 = this.labelEmailSubject;
			size = new Size(280, 16);
			arg_C5F_0.Size = size;
			this.labelEmailSubject.TabIndex = 2;
			this.labelEmailSubject.Text = "&Default text for the subject line in your email:";
			this.checkUpload.FlatStyle = 3;
			Control arg_CA1_0 = this.checkUpload;
			location = new Point(8, 160);
			arg_CA1_0.Location = location;
			this.checkUpload.Name = "checkUpload";
			Control arg_CCB_0 = this.checkUpload;
			size = new Size(256, 24);
			arg_CCB_0.Size = size;
			this.checkUpload.TabIndex = 6;
			this.checkUpload.Text = "Close &upload window after uploading is complete";
			this.labelApplication.FlatStyle = 3;
            this.labelApplication.Font = new Font("Microsoft Sans Serif", 8.25f, FontStyle.Bold, GraphicsUnit.Point, 0);
			Control arg_D26_0 = this.labelApplication;
			location = new Point(8, 8);
			arg_D26_0.Location = location;
			this.labelApplication.Name = "labelApplication";
			Control arg_D50_0 = this.labelApplication;
			size = new Size(176, 16);
			arg_D50_0.Size = size;
			this.labelApplication.TabIndex = 0;
			this.labelApplication.Text = "Email notification settings";
			this.labelAppDesc.FlatStyle = 3;
			Control arg_D8F_0 = this.labelAppDesc;
			location = new Point(8, 32);
			arg_D8F_0.Location = location;
			this.labelAppDesc.Name = "labelAppDesc";
			Control arg_DB9_0 = this.labelAppDesc;
			size = new Size(256, 16);
			arg_DB9_0.Size = size;
			this.labelAppDesc.TabIndex = 1;
			this.labelAppDesc.Text = "You can notify your friends with your latest changes.";
			this.labelSettings.FlatStyle = 3;
            this.labelSettings.Font = new Font("Microsoft Sans Serif", 8.25f, FontStyle.Bold, GraphicsUnit.Point, 0);
			Control arg_E15_0 = this.labelSettings;
			location = new Point(8, 112);
			arg_E15_0.Location = location;
			this.labelSettings.Name = "labelSettings";
			Control arg_E3C_0 = this.labelSettings;
			size = new Size(104, 16);
			arg_E3C_0.Size = size;
			this.labelSettings.TabIndex = 4;
			this.labelSettings.Text = "General settings";
			this.checkExif.FlatStyle = 3;
			Control arg_E7E_0 = this.checkExif;
			location = new Point(8, 184);
			arg_E7E_0.Location = location;
			this.checkExif.Name = "checkExif";
			Control arg_EA8_0 = this.checkExif;
			size = new Size(216, 24);
			arg_EA8_0.Size = size;
			this.checkExif.TabIndex = 7;
			this.checkExif.Text = "Maintain &EXIF information in saved files";
			this.AcceptButton = this.buttonOK;
			size = new Size(5, 13);
			this.AutoScaleBaseSize = size;
			this.CancelButton = this.buttonCancel;
			size = new Size(338, 328);
			this.ClientSize = size;
			this.Controls.Add(this.tabControl);
			this.Controls.Add(this.buttonOK);
			this.Controls.Add(this.buttonCancel);
			this.Icon = (Icon)resourceManager.GetObject("$this.Icon");
			this.MaximizeBox = false;
			this.MinimizeBox = false;
			size = new Size(310, 320);
			this.MinimumSize = size;
			this.Name = "SettingsForm";
			this.ShowInTaskbar = false;
			this.SizeGripStyle = 1;
			this.StartPosition = 4;
			this.Text = "Options";
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
				this.Width = @int;
				this.Height = int2;
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
			this.textLocation.Text = Global.Settings.GetString(SettingKey.ServiceLocation);
			this.textPassword.Text = DataProtection.Decrypt(Global.Settings.GetString(SettingKey.ServicePassword), DataProtection.Store.User);
			this.spinPixels.Value = new decimal(Global.Settings.GetInt(SettingKey.PublishPhotoSize));
			this.textEmailSubject.Text = Global.Settings.GetString(SettingKey.EmailSubject);
			this.checkConfirm.Checked = Global.Settings.GetBool(SettingKey.PromptFileDelete);
			this.checkUpload.Checked = Global.Settings.GetBool(SettingKey.CloseAfterUpload);
			this.checkExif.Checked = Global.Settings.GetBool(SettingKey.MaintainExifInfo);
			int @int = Global.Settings.GetInt(SettingKey.PublishPhotoQuality);
			int num = @int;
			if (num == 45)
			{
				this.radioLow.Checked = true;
			}
			else
			{
				if (num == 70)
				{
					this.radioMed.Checked = true;
				}
				else
				{
					if (num == 85)
					{
						this.radioHigh.Checked = true;
					}
					else
					{
						this.radioHigh.Checked = true;
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
