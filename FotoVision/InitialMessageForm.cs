using System;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;
using System.Runtime.CompilerServices;
using System.Windows.Forms;
namespace FotoVision
{
	public class InitialMessageForm : Form
	{
		[AccessedThroughProperty("labelMessage")]
		private Label _labelMessage;
		[AccessedThroughProperty("buttonOK")]
		private Button _buttonOK;
		[AccessedThroughProperty("pictIcon")]
		private PictureBox _pictIcon;
		[AccessedThroughProperty("checkDontShow")]
		private CheckBox _checkDontShow;
		private IContainer components;
		private CheckBox checkDontShow
		{
			get
			{
				return this._checkDontShow;
			}
			[MethodImpl(32)]
			set
			{
				if (this._checkDontShow != null)
				{
				}
				this._checkDontShow = value;
				if (this._checkDontShow != null)
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
				}
				this._buttonOK = value;
				if (this._buttonOK != null)
				{
				}
			}
		}
		private Label labelMessage
		{
			get
			{
				return this._labelMessage;
			}
			[MethodImpl(32)]
			set
			{
				if (this._labelMessage != null)
				{
				}
				this._labelMessage = value;
				if (this._labelMessage != null)
				{
				}
			}
		}
		public InitialMessageForm()
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
			this.pictIcon = new PictureBox();
			this.buttonOK = new Button();
			this.labelMessage = new Label();
			this.checkDontShow = new CheckBox();
			this.SuspendLayout();
			Control arg_42_0 = this.pictIcon;
			Point location = new Point(8, 8);
			arg_42_0.Location = location;
			this.pictIcon.Name = "pictIcon";
			Control arg_69_0 = this.pictIcon;
			Size size = new Size(32, 32);
			arg_69_0.Size = size;
			this.pictIcon.TabIndex = 1;
			this.pictIcon.TabStop = false;
			this.buttonOK.DialogResult = DialogResult.OK;
			this.buttonOK.FlatStyle = FlatStyle.System;
			Control arg_B3_0 = this.buttonOK;
			location = new Point(280, 88);
			arg_B3_0.Location = location;
			this.buttonOK.Name = "buttonOK";
			Control arg_DA_0 = this.buttonOK;
			size = new Size(56, 23);
			arg_DA_0.Size = size;
			this.buttonOK.TabIndex = 2;
			this.buttonOK.Text = "OK";
			this.labelMessage.FlatStyle = FlatStyle.System;
			Control arg_118_0 = this.labelMessage;
			location = new Point(56, 8);
			arg_118_0.Location = location;
			this.labelMessage.Name = "labelMessage";
			Control arg_142_0 = this.labelMessage;
			size = new Size(280, 48);
			arg_142_0.Size = size;
			this.labelMessage.TabIndex = 3;
			this.labelMessage.Text = "FotoVision makes a copy of your photos. The original photos are not deleted or modified when you delete albums, delete photos or modify photos in FotoVision.";
			this.checkDontShow.FlatStyle = FlatStyle.System;
			Control arg_181_0 = this.checkDontShow;
			location = new Point(56, 56);
			arg_181_0.Location = location;
			this.checkDontShow.Name = "checkDontShow";
			Control arg_1AB_0 = this.checkDontShow;
			size = new Size(224, 24);
			arg_1AB_0.Size = size;
			this.checkDontShow.TabIndex = 4;
			this.checkDontShow.Text = "&Don't show this message again.";
			this.AcceptButton = this.buttonOK;
			size = new Size(5, 13);
			this.AutoScaleBaseSize = size;
			this.CancelButton = this.buttonOK;
			size = new Size(346, 120);
			this.ClientSize = size;
			this.Controls.Add(this.checkDontShow);
			this.Controls.Add(this.labelMessage);
			this.Controls.Add(this.buttonOK);
			this.Controls.Add(this.pictIcon);
			this.FormBorderStyle = FormBorderStyle.FixedDialog;
			this.MaximizeBox = false;
			this.MinimizeBox = false;
			this.Name = "InitialMessageForm";
			this.ShowInTaskbar = false;
			this.StartPosition = FormStartPosition.CenterParent;
			this.Text = "FotoVision";
			this.ResumeLayout(false);
		}
		protected override void OnLoad(EventArgs e)
		{
			base.OnLoad(e);
			this.checkDontShow.Checked = true;
		}
		protected override void OnClosing(CancelEventArgs e)
		{
			base.OnClosing(e);
			Global.Settings.SetValue(SettingKey.PromptInitialMessage, !this.checkDontShow.Checked);
		}
		private void pictIcon_Paint(object sender, PaintEventArgs e)
		{
			e.Graphics.DrawIcon(SystemIcons.Information, 0, 0);
		}
	}
}
