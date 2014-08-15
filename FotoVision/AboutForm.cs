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
					this._linkCompany.remove_LinkClicked(new LinkLabelLinkClickedEventHandler(this.linkCompany_LinkClicked));
				}
				this._linkCompany = value;
				if (this._linkCompany != null)
				{
					this._linkCompany.add_LinkClicked(new LinkLabelLinkClickedEventHandler(this.linkCompany_LinkClicked));
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
					this._buttonOk.remove_Click(new EventHandler(this.buttonOk_Click));
				}
				this._buttonOk = value;
				if (this._buttonOk != null)
				{
					this._buttonOk.add_Click(new EventHandler(this.buttonOk_Click));
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
			this.labelVersion.set_FlatStyle(3);
			Control arg_8B_0 = this.labelVersion;
			Point location = new Point(8, 72);
			arg_8B_0.set_Location(location);
			this.labelVersion.set_Name("labelVersion");
			Control arg_B5_0 = this.labelVersion;
			Size size = new Size(182, 16);
			arg_B5_0.set_Size(size);
			this.labelVersion.set_TabIndex(2);
			this.labelVersion.set_Text("< version >");
			this.buttonOk.set_DialogResult(2);
			this.buttonOk.set_FlatStyle(3);
			Control arg_106_0 = this.buttonOk;
			location = new Point(240, 256);
			arg_106_0.set_Location(location);
			this.buttonOk.set_Name("buttonOk");
			Control arg_12D_0 = this.buttonOk;
			size = new Size(56, 23);
			arg_12D_0.set_Size(size);
			this.buttonOk.set_TabIndex(0);
			this.buttonOk.set_Text("OK");
			this.pictLogo.set_BorderStyle(1);
			this.pictLogo.set_Image((Image)resourceManager.GetObject("pictLogo.Image"));
			Control arg_185_0 = this.pictLogo;
			location = new Point(8, 8);
			arg_185_0.set_Location(location);
			this.pictLogo.set_Name("pictLogo");
			Control arg_1AF_0 = this.pictLogo;
			size = new Size(290, 41);
			arg_1AF_0.set_Size(size);
			this.pictLogo.set_SizeMode(2);
			this.pictLogo.set_TabIndex(3);
			this.pictLogo.set_TabStop(false);
			this.labelAssemblies.set_FlatStyle(3);
			Control arg_1F8_0 = this.labelAssemblies;
			location = new Point(8, 168);
			arg_1F8_0.set_Location(location);
			this.labelAssemblies.set_Name("labelAssemblies");
			Control arg_21F_0 = this.labelAssemblies;
			size = new Size(100, 16);
			arg_21F_0.set_Size(size);
			this.labelAssemblies.set_TabIndex(5);
			this.labelAssemblies.set_Text("Assemblies:");
			this.labelLocation.set_FlatStyle(3);
			Control arg_25D_0 = this.labelLocation;
			location = new Point(8, 104);
			arg_25D_0.set_Location(location);
			this.labelLocation.set_Name("labelLocation");
			Control arg_284_0 = this.labelLocation;
			size = new Size(104, 16);
			arg_284_0.set_Size(size);
			this.labelLocation.set_TabIndex(3);
			this.labelLocation.set_Text("Local photo location:");
			Control arg_2B6_0 = this.textPhotoLocation;
			location = new Point(8, 120);
			arg_2B6_0.set_Location(location);
			this.textPhotoLocation.set_Multiline(true);
			this.textPhotoLocation.set_Name("textPhotoLocation");
			this.textPhotoLocation.set_ReadOnly(true);
			this.textPhotoLocation.set_ScrollBars(2);
			Control arg_304_0 = this.textPhotoLocation;
			size = new Size(288, 32);
			arg_304_0.set_Size(size);
			this.textPhotoLocation.set_TabIndex(4);
			this.textPhotoLocation.set_Text("");
			this.listAssemblies.set_BackColor(SystemColors.get_Control());
			Control arg_349_0 = this.listAssemblies;
			location = new Point(8, 184);
			arg_349_0.set_Location(location);
			this.listAssemblies.set_Name("listAssemblies");
			Control arg_373_0 = this.listAssemblies;
			size = new Size(288, 56);
			arg_373_0.set_Size(size);
			this.listAssemblies.set_TabIndex(6);
			this.linkCompany.set_ActiveLinkColor(Color.get_RoyalBlue());
			this.linkCompany.set_FlatStyle(3);
			this.linkCompany.set_LinkBehavior(2);
			this.linkCompany.set_LinkColor(SystemColors.get_ControlText());
			Control arg_3CD_0 = this.linkCompany;
			location = new Point(6, 56);
			arg_3CD_0.set_Location(location);
			this.linkCompany.set_Name("linkCompany");
			Control arg_3F7_0 = this.linkCompany;
			size = new Size(184, 16);
			arg_3F7_0.set_Size(size);
			this.linkCompany.set_TabIndex(1);
			this.linkCompany.set_TabStop(true);
			this.linkCompany.set_Text("Developed by Vertigo Software, Inc.");
			this.linkCompany.set_VisitedLinkColor(SystemColors.get_ControlText());
			this.set_AcceptButton(this.buttonOk);
			size = new Size(5, 13);
			this.set_AutoScaleBaseSize(size);
			this.set_CancelButton(this.buttonOk);
			size = new Size(306, 288);
			this.set_ClientSize(size);
			this.get_Controls().Add(this.linkCompany);
			this.get_Controls().Add(this.listAssemblies);
			this.get_Controls().Add(this.textPhotoLocation);
			this.get_Controls().Add(this.labelAssemblies);
			this.get_Controls().Add(this.pictLogo);
			this.get_Controls().Add(this.buttonOk);
			this.get_Controls().Add(this.labelVersion);
			this.get_Controls().Add(this.labelLocation);
			this.set_FormBorderStyle(3);
			this.set_MaximizeBox(false);
			this.set_MinimizeBox(false);
			this.set_Name("AboutForm");
			this.set_ShowInTaskbar(false);
			this.set_StartPosition(4);
			this.set_Text("About FotoVision");
			this.ResumeLayout(false);
		}
		protected override void OnLoad(EventArgs e)
		{
			base.OnLoad(e);
			string[] array = Application.get_ProductVersion().Split(new char[]
			{
				'.'
			});
			this.labelVersion.set_Text(string.Format("Version {0}.{1}.{2}", array[0], array[1], array[2]));
			AssemblyName[] referencedAssemblies = Assembly.GetExecutingAssembly().GetReferencedAssemblies();
			AssemblyName[] array2 = referencedAssemblies;
			checked
			{
				for (int i = 0; i < array2.Length; i++)
				{
					AssemblyName assemblyName = array2[i];
					this.listAssemblies.get_Items().Add(string.Format("{0} ({1})", assemblyName.get_Name(), assemblyName.get_Version().ToString()));
				}
				this.textPhotoLocation.set_Text(Global.DataLocation);
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
