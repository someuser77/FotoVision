using System;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;
using System.Resources;
using System.Runtime.CompilerServices;
using System.Windows.Forms;
namespace FotoVision
{
	public class LoginForm : Form
	{
		[AccessedThroughProperty("buttonOK")]
		private Button _buttonOK;
		[AccessedThroughProperty("image")]
		private PictureBox _image;
		[AccessedThroughProperty("labelPassword")]
		private Label _labelPassword;
		[AccessedThroughProperty("buttonCancel")]
		private Button _buttonCancel;
		[AccessedThroughProperty("textPassword")]
		private TextBox _textPassword;
		private IContainer components;
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
		private PictureBox image
		{
			get
			{
				return this._image;
			}
			[MethodImpl(32)]
			set
			{
				if (this._image != null)
				{
				}
				this._image = value;
				if (this._image != null)
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
		public LoginForm()
		{
			this.InitializeComponent();
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
			ResourceManager resourceManager = new ResourceManager(typeof(LoginForm));
			this.textPassword = new TextBox();
			this.buttonOK = new Button();
			this.buttonCancel = new Button();
			this.labelPassword = new Label();
			this.image = new PictureBox();
			this.SuspendLayout();
			Control arg_5F_0 = this.textPassword;
			Point location = new Point(56, 40);
			arg_5F_0.set_Location(location);
			this.textPassword.set_Name("textPassword");
			this.textPassword.set_PasswordChar('●');
			Control arg_99_0 = this.textPassword;
			Size size = new Size(192, 20);
			arg_99_0.set_Size(size);
			this.textPassword.set_TabIndex(1);
			this.textPassword.set_Text("");
			this.buttonOK.set_DialogResult(1);
			this.buttonOK.set_FlatStyle(3);
			Control arg_E7_0 = this.buttonOK;
			location = new Point(128, 72);
			arg_E7_0.set_Location(location);
			this.buttonOK.set_Name("buttonOK");
			Control arg_10E_0 = this.buttonOK;
			size = new Size(56, 23);
			arg_10E_0.set_Size(size);
			this.buttonOK.set_TabIndex(2);
			this.buttonOK.set_Text("OK");
			this.buttonCancel.set_DialogResult(2);
			this.buttonCancel.set_FlatStyle(3);
			Control arg_15C_0 = this.buttonCancel;
			location = new Point(192, 72);
			arg_15C_0.set_Location(location);
			this.buttonCancel.set_Name("buttonCancel");
			Control arg_183_0 = this.buttonCancel;
			size = new Size(56, 23);
			arg_183_0.set_Size(size);
			this.buttonCancel.set_TabIndex(3);
			this.buttonCancel.set_Text("Cancel");
			this.labelPassword.set_FlatStyle(3);
			Control arg_1C2_0 = this.labelPassword;
			location = new Point(56, 16);
			arg_1C2_0.set_Location(location);
			this.labelPassword.set_Name("labelPassword");
			Control arg_1EC_0 = this.labelPassword;
			size = new Size(184, 16);
			arg_1EC_0.set_Size(size);
			this.labelPassword.set_TabIndex(0);
			this.labelPassword.set_Text("Please enter the website &password:");
			this.image.set_Image((Image)resourceManager.GetObject("image.Image"));
			Control arg_238_0 = this.image;
			location = new Point(8, 8);
			arg_238_0.set_Location(location);
			this.image.set_Name("image");
			Control arg_25F_0 = this.image;
			size = new Size(32, 32);
			arg_25F_0.set_Size(size);
			this.image.set_SizeMode(2);
			this.image.set_TabIndex(5);
			this.image.set_TabStop(false);
			this.set_AcceptButton(this.buttonOK);
			size = new Size(5, 13);
			this.set_AutoScaleBaseSize(size);
			this.set_CancelButton(this.buttonCancel);
			size = new Size(258, 104);
			this.set_ClientSize(size);
			this.Controls.Add(this.image);
			this.Controls.Add(this.labelPassword);
			this.Controls.Add(this.buttonCancel);
			this.Controls.Add(this.buttonOK);
			this.Controls.Add(this.textPassword);
			this.set_FormBorderStyle(3);
			this.set_MaximizeBox(false);
			this.set_MinimizeBox(false);
			this.set_Name("LoginForm");
			this.set_ShowInTaskbar(false);
			this.set_StartPosition(4);
			this.set_Text("Login");
			this.ResumeLayout(false);
		}
		protected override void OnLoad(EventArgs e)
		{
			base.OnLoad(e);
			this.textPassword.set_Text(DataProtection.Decrypt(Global.Settings.GetString(SettingKey.ServicePassword), DataProtection.Store.User));
		}
		protected override void OnClosing(CancelEventArgs e)
		{
			if (this.DialogResult == 1)
			{
				Global.Settings.SetValue(SettingKey.ServicePassword, DataProtection.Encrypt(this.textPassword.Text, DataProtection.Store.User));
			}
			base.OnClosing(e);
		}
	}
}
