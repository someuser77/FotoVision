using System;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;
using System.Resources;
using System.Runtime.CompilerServices;
using System.Windows.Forms;
namespace FotoVision
{
	public class ErrorForm : Form
	{
		[AccessedThroughProperty("labelHeader")]
		private Label _labelHeader;
		[AccessedThroughProperty("labelDetails")]
		private Label _labelDetails;
		[AccessedThroughProperty("buttonOK")]
		private Button _buttonOK;
		[AccessedThroughProperty("labelInfo")]
		private Label _labelInfo;
		[AccessedThroughProperty("labelType")]
		private Label _labelType;
		[AccessedThroughProperty("textType")]
		private TextBox _textType;
		[AccessedThroughProperty("pictIcon")]
		private PictureBox _pictIcon;
		[AccessedThroughProperty("textDetails")]
		private TextBox _textDetails;
		private string _message;
		private StringFormat _format;
		private IContainer components;
		private TextBox textDetails
		{
			get
			{
				return this._textDetails;
			}
			[MethodImpl(32)]
			set
			{
				if (this._textDetails != null)
				{
				}
				this._textDetails = value;
				if (this._textDetails != null)
				{
				}
			}
		}
		private TextBox textType
		{
			get
			{
				return this._textType;
			}
			[MethodImpl(32)]
			set
			{
				if (this._textType != null)
				{
				}
				this._textType = value;
				if (this._textType != null)
				{
				}
			}
		}
		private Label labelHeader
		{
			get
			{
				return this._labelHeader;
			}
			[MethodImpl(32)]
			set
			{
				if (this._labelHeader != null)
				{
				}
				this._labelHeader = value;
				if (this._labelHeader != null)
				{
				}
			}
		}
		private PictureBox pictIcon
		{
			get
			{
				return this._pictIcon;
			}
			[MethodImpl(32)]
			set
			{
				if (this._pictIcon != null)
				{
					this._pictIcon.remove_Paint(new PaintEventHandler(this.pictIcon_Paint));
				}
				this._pictIcon = value;
				if (this._pictIcon != null)
				{
					this._pictIcon.add_Paint(new PaintEventHandler(this.pictIcon_Paint));
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
					this._buttonOK.remove_Click(new EventHandler(this.buttonOK_Click));
				}
				this._buttonOK = value;
				if (this._buttonOK != null)
				{
					this._buttonOK.add_Click(new EventHandler(this.buttonOK_Click));
				}
			}
		}
		private Label labelDetails
		{
			get
			{
				return this._labelDetails;
			}
			[MethodImpl(32)]
			set
			{
				if (this._labelDetails != null)
				{
				}
				this._labelDetails = value;
				if (this._labelDetails != null)
				{
				}
			}
		}
		private Label labelType
		{
			get
			{
				return this._labelType;
			}
			[MethodImpl(32)]
			set
			{
				if (this._labelType != null)
				{
				}
				this._labelType = value;
				if (this._labelType != null)
				{
				}
			}
		}
		private Label labelInfo
		{
			get
			{
				return this._labelInfo;
			}
			[MethodImpl(32)]
			set
			{
				if (this._labelInfo != null)
				{
				}
				this._labelInfo = value;
				if (this._labelInfo != null)
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
			ResourceManager resourceManager = new ResourceManager(typeof(ErrorForm));
			this.pictIcon = new PictureBox();
			this.buttonOK = new Button();
			this.textDetails = new TextBox();
			this.labelDetails = new Label();
			this.labelType = new Label();
			this.textType = new TextBox();
			this.labelHeader = new Label();
			this.labelInfo = new Label();
			this.SuspendLayout();
			Control arg_7E_0 = this.pictIcon;
			Point location = new Point(8, 8);
			arg_7E_0.set_Location(location);
			this.pictIcon.set_Name("pictIcon");
			Control arg_A5_0 = this.pictIcon;
			Size size = new Size(32, 32);
			arg_A5_0.set_Size(size);
			this.pictIcon.set_TabIndex(0);
			this.pictIcon.set_TabStop(false);
			this.buttonOK.set_Anchor(10);
			this.buttonOK.set_DialogResult(2);
			this.buttonOK.set_FlatStyle(3);
			Control arg_FF_0 = this.buttonOK;
			location = new Point(270, 216);
			arg_FF_0.set_Location(location);
			this.buttonOK.set_Name("buttonOK");
			Control arg_126_0 = this.buttonOK;
			size = new Size(56, 23);
			arg_126_0.set_Size(size);
			this.buttonOK.set_TabIndex(0);
			this.buttonOK.set_Text("OK");
			this.textDetails.set_Anchor(15);
			this.textDetails.set_BackColor(SystemColors.Info);
			Control arg_178_0 = this.textDetails;
			location = new Point(8, 144);
			arg_178_0.set_Location(location);
			this.textDetails.set_Multiline(true);
			this.textDetails.set_Name("textDetails");
			this.textDetails.set_ReadOnly(true);
			this.textDetails.set_ScrollBars(2);
			Control arg_1C6_0 = this.textDetails;
			size = new Size(318, 64);
			arg_1C6_0.set_Size(size);
			this.textDetails.set_TabIndex(7);
			this.textDetails.set_Text("");
			this.labelDetails.set_FlatStyle(3);
			Control arg_207_0 = this.labelDetails;
			location = new Point(8, 128);
			arg_207_0.set_Location(location);
			this.labelDetails.set_Name("labelDetails");
			Control arg_22E_0 = this.labelDetails;
			size = new Size(56, 16);
			arg_22E_0.set_Size(size);
			this.labelDetails.set_TabIndex(6);
			this.labelDetails.set_Text("&Details:");
			this.labelType.set_FlatStyle(3);
			Control arg_26C_0 = this.labelType;
			location = new Point(8, 80);
			arg_26C_0.set_Location(location);
			this.labelType.set_Name("labelType");
			Control arg_293_0 = this.labelType;
			size = new Size(72, 16);
			arg_293_0.set_Size(size);
			this.labelType.set_TabIndex(4);
			this.labelType.set_Text("&Type of error:");
			this.textType.set_Anchor(13);
			this.textType.set_BackColor(SystemColors.Info);
			Control arg_2E2_0 = this.textType;
			location = new Point(8, 96);
			arg_2E2_0.set_Location(location);
			this.textType.set_Name("textType");
			this.textType.set_ReadOnly(true);
			Control arg_318_0 = this.textType;
			size = new Size(318, 20);
			arg_318_0.set_Size(size);
			this.textType.set_TabIndex(5);
			this.textType.set_Text("");
			this.labelHeader.set_Anchor(13);
			this.labelHeader.set_FlatStyle(3);
            this.labelHeader.set_Font(new Font("Microsoft Sans Serif", 8.25f, FontStyle.Bold, GraphicsUnit.Point, 0));
			Control arg_380_0 = this.labelHeader;
			location = new Point(56, 8);
			arg_380_0.set_Location(location);
			this.labelHeader.set_Name("labelHeader");
			Control arg_3AA_0 = this.labelHeader;
			size = new Size(270, 16);
			arg_3AA_0.set_Size(size);
			this.labelHeader.set_TabIndex(1);
			this.labelHeader.set_Text("An error occurred");
			this.labelInfo.set_FlatStyle(3);
            this.labelInfo.set_Font(new Font("Microsoft Sans Serif", 8.25f, FontStyle.Bold, GraphicsUnit.Point, 0));
			Control arg_405_0 = this.labelInfo;
			location = new Point(8, 56);
			arg_405_0.set_Location(location);
			this.labelInfo.set_Name("labelInfo");
			Control arg_42F_0 = this.labelInfo;
			size = new Size(136, 16);
			arg_42F_0.set_Size(size);
			this.labelInfo.set_TabIndex(3);
			this.labelInfo.set_Text("Additional information");
			this.set_AcceptButton(this.buttonOK);
			size = new Size(5, 13);
			this.set_AutoScaleBaseSize(size);
			this.set_CancelButton(this.buttonOK);
			size = new Size(336, 246);
			this.set_ClientSize(size);
			this.Controls.Add(this.labelDetails);
			this.Controls.Add(this.textDetails);
			this.Controls.Add(this.textType);
			this.Controls.Add(this.pictIcon);
			this.Controls.Add(this.buttonOK);
			this.Controls.Add(this.labelType);
			this.Controls.Add(this.labelHeader);
			this.Controls.Add(this.labelInfo);
			this.set_Icon((Icon)resourceManager.GetObject("$this.Icon"));
			this.set_MaximizeBox(false);
			this.set_MinimizeBox(false);
			size = new Size(260, 250);
			this.set_MinimumSize(size);
			this.set_Name("ErrorForm");
			this.set_ShowInTaskbar(false);
			this.set_SizeGripStyle(1);
			this.set_StartPosition(4);
			this.set_Text("Error");
			this.ResumeLayout(false);
		}
		public ErrorForm(string message, Exception ex)
		{
			this.InitializeComponent();
			int @int = Global.Settings.GetInt(SettingKey.ErrorDialogWidth);
			int int2 = Global.Settings.GetInt(SettingKey.ErrorDialogHeight);
			if (@int != 0 && int2 != 0)
			{
				this.set_Width(@int);
				this.set_Height(int2);
			}
			this._format = new StringFormat();
			this._format.set_FormatFlags(4096);
			this._format.set_Trimming(3);
			this._message = message;
			if (ex != null)
			{
				this.textType.set_Text(ex.GetType().FullName);
				this.textDetails.set_Text(ex.Message);
			}
		}
		protected override void OnClosing(CancelEventArgs e)
		{
			base.OnClosing(e);
			Global.Settings.SetValue(SettingKey.ErrorDialogWidth, this.Width);
			Global.Settings.SetValue(SettingKey.ErrorDialogHeight, this.Height);
		}
		private void buttonOK_Click(object sender, EventArgs e)
		{
			this.Close();
		}
		private void pictIcon_Paint(object sender, PaintEventArgs e)
		{
			e.Graphics.DrawIcon(SystemIcons.Exclamation, 0, 0);
		}
		protected override void OnPaint(PaintEventArgs e)
		{
			base.OnPaint(e);
			RectangleF rectangleF = new RectangleF((float)checked(this.labelHeader.Bounds.Left - 2), (float)this.labelHeader.Bottom, (float)this.labelHeader.Width, (float)this.labelHeader.Height);
			e.Graphics.DrawString(this._message, this.Font, SystemBrushes.ControlText, rectangleF, this._format);
		}
	}
}
