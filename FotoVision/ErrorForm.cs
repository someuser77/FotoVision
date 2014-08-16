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
					this._pictIcon.Paint -= new PaintEventHandler(this.pictIcon_Paint);
				}
				this._pictIcon = value;
				if (this._pictIcon != null)
				{
					this._pictIcon.Paint += new PaintEventHandler(this.pictIcon_Paint);
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
					this._buttonOK.Click -= new EventHandler(this.buttonOK_Click);
				}
				this._buttonOK = value;
				if (this._buttonOK != null)
				{
					this._buttonOK.Click += new EventHandler(this.buttonOK_Click);
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
			arg_7E_0.Location = location;
			this.pictIcon.Name = "pictIcon";
			Control arg_A5_0 = this.pictIcon;
			Size size = new Size(32, 32);
			arg_A5_0.Size = size;
			this.pictIcon.TabIndex = 0;
			this.pictIcon.TabStop = false;
			this.buttonOK.Anchor = AnchorStyles.Right | AnchorStyles.Bottom;
			this.buttonOK.DialogResult = DialogResult.Cancel;
			this.buttonOK.FlatStyle = FlatStyle.System;
			Control arg_FF_0 = this.buttonOK;
			location = new Point(270, 216);
			arg_FF_0.Location = location;
			this.buttonOK.Name = "buttonOK";
			Control arg_126_0 = this.buttonOK;
			size = new Size(56, 23);
			arg_126_0.Size = size;
			this.buttonOK.TabIndex = 0;
			this.buttonOK.Text = "OK";
			this.textDetails.Anchor = AnchorStyles.Right | AnchorStyles.Left | AnchorStyles.Bottom | AnchorStyles.Top;
			this.textDetails.BackColor = SystemColors.Info;
			Control arg_178_0 = this.textDetails;
			location = new Point(8, 144);
			arg_178_0.Location = location;
			this.textDetails.Multiline = true;
			this.textDetails.Name = "textDetails";
			this.textDetails.ReadOnly = true;
			this.textDetails.ScrollBars = 2;
			Control arg_1C6_0 = this.textDetails;
			size = new Size(318, 64);
			arg_1C6_0.Size = size;
			this.textDetails.TabIndex = 7;
			this.textDetails.Text = "";
			this.labelDetails.FlatStyle = FlatStyle.System;
			Control arg_207_0 = this.labelDetails;
			location = new Point(8, 128);
			arg_207_0.Location = location;
			this.labelDetails.Name = "labelDetails";
			Control arg_22E_0 = this.labelDetails;
			size = new Size(56, 16);
			arg_22E_0.Size = size;
			this.labelDetails.TabIndex = 6;
			this.labelDetails.Text = "&Details:";
			this.labelType.FlatStyle = FlatStyle.System;
			Control arg_26C_0 = this.labelType;
			location = new Point(8, 80);
			arg_26C_0.Location = location;
			this.labelType.Name = "labelType";
			Control arg_293_0 = this.labelType;
			size = new Size(72, 16);
			arg_293_0.Size = size;
			this.labelType.TabIndex = 4;
			this.labelType.Text = "&Type of error:";
			this.textType.Anchor = AnchorStyles.Right | AnchorStyles.Left | AnchorStyles.Top;
			this.textType.BackColor = SystemColors.Info;
			Control arg_2E2_0 = this.textType;
			location = new Point(8, 96);
			arg_2E2_0.Location = location;
			this.textType.Name = "textType";
			this.textType.ReadOnly = true;
			Control arg_318_0 = this.textType;
			size = new Size(318, 20);
			arg_318_0.Size = size;
			this.textType.TabIndex = 5;
			this.textType.Text = "";
			this.labelHeader.Anchor = AnchorStyles.Right | AnchorStyles.Left | AnchorStyles.Top;
			this.labelHeader.FlatStyle = FlatStyle.System;
            this.labelHeader.Font = new Font("Microsoft Sans Serif", 8.25f, FontStyle.Bold, GraphicsUnit.Point, 0);
			Control arg_380_0 = this.labelHeader;
			location = new Point(56, 8);
			arg_380_0.Location = location;
			this.labelHeader.Name = "labelHeader";
			Control arg_3AA_0 = this.labelHeader;
			size = new Size(270, 16);
			arg_3AA_0.Size = size;
			this.labelHeader.TabIndex = 1;
			this.labelHeader.Text = "An error occurred";
			this.labelInfo.FlatStyle = FlatStyle.System;
            this.labelInfo.Font = new Font("Microsoft Sans Serif", 8.25f, FontStyle.Bold, GraphicsUnit.Point, 0);
			Control arg_405_0 = this.labelInfo;
			location = new Point(8, 56);
			arg_405_0.Location = location;
			this.labelInfo.Name = "labelInfo";
			Control arg_42F_0 = this.labelInfo;
			size = new Size(136, 16);
			arg_42F_0.Size = size;
			this.labelInfo.TabIndex = 3;
			this.labelInfo.Text = "Additional information";
			this.AcceptButton = this.buttonOK;
			size = new Size(5, 13);
			this.AutoScaleBaseSize = size;
			this.CancelButton = this.buttonOK;
			size = new Size(336, 246);
			this.ClientSize = size;
			this.Controls.Add(this.labelDetails);
			this.Controls.Add(this.textDetails);
			this.Controls.Add(this.textType);
			this.Controls.Add(this.pictIcon);
			this.Controls.Add(this.buttonOK);
			this.Controls.Add(this.labelType);
			this.Controls.Add(this.labelHeader);
			this.Controls.Add(this.labelInfo);
			this.Icon = (Icon)resourceManager.GetObject("$this.Icon");
			this.MaximizeBox = false;
			this.MinimizeBox = false;
			size = new Size(260, 250);
			this.MinimumSize = size;
			this.Name = "ErrorForm";
			this.ShowInTaskbar = false;
			this.SizeGripStyle = 1;
			this.StartPosition = FormStartPosition.CenterParent;
			this.Text = "Error";
			this.ResumeLayout(false);
		}
		public ErrorForm(string message, Exception ex)
		{
			this.InitializeComponent();
			int @int = Global.Settings.GetInt(SettingKey.ErrorDialogWidth);
			int int2 = Global.Settings.GetInt(SettingKey.ErrorDialogHeight);
			if (@int != 0 && int2 != 0)
			{
				this.Width = @int;
				this.Height = int2;
			}
			this._format = new StringFormat();
			this._format.FormatFlags = StringFormatFlags.NoWrap;
            this._format.Trimming = System.Drawing.StringTrimming.EllipsisCharacter;
			this._message = message;
			if (ex != null)
			{
				this.textType.Text = ex.GetType().FullName;
				this.textDetails.Text = ex.Message;
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
