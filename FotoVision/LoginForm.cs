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
			arg_5F_0.Location = location;
			this.textPassword.Name = "textPassword";
			this.textPassword.PasswordChar = '●';
			Control arg_99_0 = this.textPassword;
			Size size = new Size(192, 20);
			arg_99_0.Size = size;
			this.textPassword.TabIndex = 1;
			this.textPassword.Text = "";
			this.buttonOK.DialogResult = 1;
			this.buttonOK.FlatStyle = 3;
			Control arg_E7_0 = this.buttonOK;
			location = new Point(128, 72);
			arg_E7_0.Location = location;
			this.buttonOK.Name = "buttonOK";
			Control arg_10E_0 = this.buttonOK;
			size = new Size(56, 23);
			arg_10E_0.Size = size;
			this.buttonOK.TabIndex = 2;
			this.buttonOK.Text = "OK";
			this.buttonCancel.DialogResult = 2;
			this.buttonCancel.FlatStyle = 3;
			Control arg_15C_0 = this.buttonCancel;
			location = new Point(192, 72);
			arg_15C_0.Location = location;
			this.buttonCancel.Name = "buttonCancel";
			Control arg_183_0 = this.buttonCancel;
			size = new Size(56, 23);
			arg_183_0.Size = size;
			this.buttonCancel.TabIndex = 3;
			this.buttonCancel.Text = "Cancel";
			this.labelPassword.FlatStyle = 3;
			Control arg_1C2_0 = this.labelPassword;
			location = new Point(56, 16);
			arg_1C2_0.Location = location;
			this.labelPassword.Name = "labelPassword";
			Control arg_1EC_0 = this.labelPassword;
			size = new Size(184, 16);
			arg_1EC_0.Size = size;
			this.labelPassword.TabIndex = 0;
			this.labelPassword.Text = "Please enter the website &password:";
			this.image.Image = (Image)resourceManager.GetObject("image.Image");
			Control arg_238_0 = this.image;
			location = new Point(8, 8);
			arg_238_0.Location = location;
			this.image.Name = "image";
			Control arg_25F_0 = this.image;
			size = new Size(32, 32);
			arg_25F_0.Size = size;
			this.image.SizeMode = 2;
			this.image.TabIndex = 5;
			this.image.TabStop = false;
			this.AcceptButton = this.buttonOK;
			size = new Size(5, 13);
			this.AutoScaleBaseSize = size;
			this.CancelButton = this.buttonCancel;
			size = new Size(258, 104);
			this.ClientSize = size;
			this.Controls.Add(this.image);
			this.Controls.Add(this.labelPassword);
			this.Controls.Add(this.buttonCancel);
			this.Controls.Add(this.buttonOK);
			this.Controls.Add(this.textPassword);
			this.FormBorderStyle = 3;
			this.MaximizeBox = false;
			this.MinimizeBox = false;
			this.Name = "LoginForm";
			this.ShowInTaskbar = false;
			this.StartPosition = 4;
			this.Text = "Login";
			this.ResumeLayout(false);
		}
		protected override void OnLoad(EventArgs e)
		{
			base.OnLoad(e);
			this.textPassword.Text = DataProtection.Decrypt(Global.Settings.GetString(SettingKey.ServicePassword), DataProtection.Store.User);
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
