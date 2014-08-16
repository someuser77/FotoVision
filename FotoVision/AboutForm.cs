using Microsoft.VisualBasic.CompilerServices;
using System;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;
using System.Reflection;
using System.Resources;
using System.Runtime.CompilerServices;
using System.Windows.Forms;
namespace FotoVision
{
	public class AboutForm : Form
	{
		private class Consts
		{
			public const string CompanyLink = "http://www.vertigosoftware.com?ref=fotovision1";
		}
		[AccessedThroughProperty("labelLocation")]
		private Label _labelLocation;
		[AccessedThroughProperty("listAssemblies")]
		private ListBox _listAssemblies;
		[AccessedThroughProperty("labelAssemblies")]
		private Label _labelAssemblies;
		[AccessedThroughProperty("textPhotoLocation")]
		private TextBox _textPhotoLocation;
		[AccessedThroughProperty("pictLogo")]
		private PictureBox _pictLogo;
		[AccessedThroughProperty("buttonOk")]
		private Button _buttonOk;
		[AccessedThroughProperty("linkCompany")]
		private LinkLabel _linkCompany;
		private Container components;
		private Label labelVersion;
		private PictureBox pictLogo
		{
			get
			{
				return this._pictLogo;
			}
			[MethodImpl(32)]
			set
			{
				if (this._pictLogo != null)
				{
				}
				this._pictLogo = value;
				if (this._pictLogo != null)
				{
				}
			}
		}
		private ListBox listAssemblies
		{
			get
			{
				return this._listAssemblies;
			}
			[MethodImpl(32)]
			set
			{
				if (this._listAssemblies != null)
				{
				}
				this._listAssemblies = value;
				if (this._listAssemblies != null)
				{
				}
			}
		}
		private LinkLabel linkCompany
		{
			get
			{
				return this._linkCompany;
			}
			[MethodImpl(32)]
			set
			{
				if (this._linkCompany != null)
				{
					this._linkCompany.LinkClicked -= new LinkLabelLinkClickedEventHandler(this.linkCompany_LinkClicked);
				}
				this._linkCompany = value;
				if (this._linkCompany != null)
				{
					this._linkCompany.LinkClicked += new LinkLabelLinkClickedEventHandler(this.linkCompany_LinkClicked);
				}
			}
		}
		private TextBox textPhotoLocation
		{
			get
			{
				return this._textPhotoLocation;
			}
			[MethodImpl(32)]
			set
			{
				if (this._textPhotoLocation != null)
				{
				}
				this._textPhotoLocation = value;
				if (this._textPhotoLocation != null)
				{
				}
			}
		}
		private Button buttonOk
		{
			get
			{
				return this._buttonOk;
			}
			[MethodImpl(32)]
			set
			{
				if (this._buttonOk != null)
				{
					this._buttonOk.Click -= new EventHandler(this.buttonOk_Click);
				}
				this._buttonOk = value;
				if (this._buttonOk != null)
				{
					this._buttonOk.Click += new EventHandler(this.buttonOk_Click);
				}
			}
		}
		private Label labelAssemblies
		{
			get
			{
				return this._labelAssemblies;
			}
			[MethodImpl(32)]
			set
			{
				if (this._labelAssemblies != null)
				{
				}
				this._labelAssemblies = value;
				if (this._labelAssemblies != null)
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
		public AboutForm()
		{
			this.components = null;
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
		private void InitializeComponent()
		{
			ResourceManager resourceManager = new ResourceManager(typeof(AboutForm));
			this.labelVersion = new Label();
			this.buttonOk = new Button();
			this.pictLogo = new PictureBox();
			this.labelAssemblies = new Label();
			this.labelLocation = new Label();
			this.textPhotoLocation = new TextBox();
			this.listAssemblies = new ListBox();
			this.linkCompany = new LinkLabel();
			this.SuspendLayout();
			this.labelVersion.FlatStyle = FlatStyle.System;
			Control arg_8B_0 = this.labelVersion;
			Point location = new Point(8, 72);
			arg_8B_0.Location = location;
			this.labelVersion.Name = "labelVersion";
			Control arg_B5_0 = this.labelVersion;
			Size size = new Size(182, 16);
			arg_B5_0.Size = size;
			this.labelVersion.TabIndex = 2;
			this.labelVersion.Text = "< version >";
			this.buttonOk.DialogResult = DialogResult.Cancel;
			this.buttonOk.FlatStyle = FlatStyle.System;
			Control arg_106_0 = this.buttonOk;
			location = new Point(240, 256);
			arg_106_0.Location = location;
			this.buttonOk.Name = "buttonOk";
			Control arg_12D_0 = this.buttonOk;
			size = new Size(56, 23);
			arg_12D_0.Size = size;
			this.buttonOk.TabIndex = 0;
			this.buttonOk.Text = "OK";
            this.pictLogo.BorderStyle = BorderStyle.FixedSingle;
			this.pictLogo.Image = (Image)resourceManager.GetObject("pictLogo.Image");
			Control arg_185_0 = this.pictLogo;
			location = new Point(8, 8);
			arg_185_0.Location = location;
			this.pictLogo.Name = "pictLogo";
			Control arg_1AF_0 = this.pictLogo;
			size = new Size(290, 41);
			arg_1AF_0.Size = size;
			this.pictLogo.SizeMode = 2;
			this.pictLogo.TabIndex = 3;
			this.pictLogo.TabStop = false;
			this.labelAssemblies.FlatStyle = FlatStyle.System;
			Control arg_1F8_0 = this.labelAssemblies;
			location = new Point(8, 168);
			arg_1F8_0.Location = location;
			this.labelAssemblies.Name = "labelAssemblies";
			Control arg_21F_0 = this.labelAssemblies;
			size = new Size(100, 16);
			arg_21F_0.Size = size;
			this.labelAssemblies.TabIndex = 5;
			this.labelAssemblies.Text = "Assemblies:";
			this.labelLocation.FlatStyle = FlatStyle.System;
			Control arg_25D_0 = this.labelLocation;
			location = new Point(8, 104);
			arg_25D_0.Location = location;
			this.labelLocation.Name = "labelLocation";
			Control arg_284_0 = this.labelLocation;
			size = new Size(104, 16);
			arg_284_0.Size = size;
			this.labelLocation.TabIndex = 3;
			this.labelLocation.Text = "Local photo location:";
			Control arg_2B6_0 = this.textPhotoLocation;
			location = new Point(8, 120);
			arg_2B6_0.Location = location;
			this.textPhotoLocation.Multiline = true;
			this.textPhotoLocation.Name = "textPhotoLocation";
			this.textPhotoLocation.ReadOnly = true;
			this.textPhotoLocation.ScrollBars = 2;
			Control arg_304_0 = this.textPhotoLocation;
			size = new Size(288, 32);
			arg_304_0.Size = size;
			this.textPhotoLocation.TabIndex = 4;
			this.textPhotoLocation.Text = "";
			this.listAssemblies.BackColor = SystemColors.Control;
			Control arg_349_0 = this.listAssemblies;
			location = new Point(8, 184);
			arg_349_0.Location = location;
			this.listAssemblies.Name = "listAssemblies";
			Control arg_373_0 = this.listAssemblies;
			size = new Size(288, 56);
			arg_373_0.Size = size;
			this.listAssemblies.TabIndex = 6;
			this.linkCompany.ActiveLinkColor = Color.RoyalBlue;
			this.linkCompany.FlatStyle = FlatStyle.System;
			this.linkCompany.LinkBehavior = 2;
			this.linkCompany.LinkColor = SystemColors.ControlText;
			Control arg_3CD_0 = this.linkCompany;
			location = new Point(6, 56);
			arg_3CD_0.Location = location;
			this.linkCompany.Name = "linkCompany";
			Control arg_3F7_0 = this.linkCompany;
			size = new Size(184, 16);
			arg_3F7_0.Size = size;
			this.linkCompany.TabIndex = 1;
			this.linkCompany.TabStop = true;
			this.linkCompany.Text = "Developed by Vertigo Software, Inc.";
			this.linkCompany.VisitedLinkColor = SystemColors.ControlText;
			this.AcceptButton = this.buttonOk;
			size = new Size(5, 13);
			this.AutoScaleBaseSize = size;
			this.CancelButton = this.buttonOk;
			size = new Size(306, 288);
			this.ClientSize = size;
			this.Controls.Add(this.linkCompany);
			this.Controls.Add(this.listAssemblies);
			this.Controls.Add(this.textPhotoLocation);
			this.Controls.Add(this.labelAssemblies);
			this.Controls.Add(this.pictLogo);
			this.Controls.Add(this.buttonOk);
			this.Controls.Add(this.labelVersion);
			this.Controls.Add(this.labelLocation);
			this.FormBorderStyle = FormBorderStyle.FixedDialog;
			this.MaximizeBox = false;
			this.MinimizeBox = false;
			this.Name = "AboutForm";
			this.ShowInTaskbar = false;
			this.StartPosition = FormStartPosition.CenterParent;
			this.Text = "About FotoVision";
			this.ResumeLayout(false);
		}
		protected override void OnLoad(EventArgs e)
		{
			base.OnLoad(e);
			string[] array = Application.ProductVersion.Split(new char[]
			{
				'.'
			});
			this.labelVersion.Text = string.Format("Version {0}.{1}.{2}", array[0], array[1], array[2]);
			AssemblyName[] referencedAssemblies = Assembly.GetExecutingAssembly().GetReferencedAssemblies();
			AssemblyName[] array2 = referencedAssemblies;
			checked
			{
				for (int i = 0; i < array2.Length; i++)
				{
					AssemblyName assemblyName = array2[i];
					this.listAssemblies.Items.Add(string.Format("{0} ({1})", assemblyName.Name, assemblyName.Version.ToString()));
				}
				this.textPhotoLocation.Text = Global.DataLocation;
			}
		}
		private void buttonOk_Click(object sender, EventArgs e)
		{
			this.Close();
		}
		private void linkCompany_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
		{
			try
			{
				Process.Start("http://www.vertigosoftware.com?ref=fotovision1");
			}
			catch (Exception expr_0D)
			{
				ProjectData.SetProjectError(expr_0D);
				ProjectData.ClearProjectError();
			}
		}
	}
}
