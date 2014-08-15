using Microsoft.VisualBasic;
using Microsoft.VisualBasic.CompilerServices;
using System;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;
using System.Runtime.CompilerServices;
using System.Windows.Forms;
namespace FotoVision
{
	public class DeletePhotoForm : Form
	{
		[AccessedThroughProperty("pictIcon")]
		private PictureBox _pictIcon;
		[AccessedThroughProperty("buttonNo")]
		private Button _buttonNo;
		[AccessedThroughProperty("checkDontShow")]
		private CheckBox _checkDontShow;
		[AccessedThroughProperty("labelMessage")]
		private Label _labelMessage;
		[AccessedThroughProperty("buttonYes")]
		private Button _buttonYes;
		private int _count;
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
		private Button buttonYes
		{
			get
			{
				return this._buttonYes;
			}
			[MethodImpl(32)]
			set
			{
				if (this._buttonYes != null)
				{
				}
				this._buttonYes = value;
				if (this._buttonYes != null)
				{
				}
			}
		}
		private Button buttonNo
		{
			get
			{
				return this._buttonNo;
			}
			[MethodImpl(32)]
			set
			{
				if (this._buttonNo != null)
				{
				}
				this._buttonNo = value;
				if (this._buttonNo != null)
				{
				}
			}
		}
		public int Count
		{
			get
			{
				return this._count;
			}
			set
			{
				this._count = value;
				this.labelMessage.Text = string.Concat(new string[]
				{
					StringType.FromObject(Interaction.IIf(this._count == 1, "Deleting this photo will only delete the copy FotoVision has made from your original photo", "Deleting these photos will only delete the copies FotoVision has made from your original photos")),
					" during the import.",
					"\r\n",
					"\r\n",
					"Do you want to continue?"
				});
			}
		}
		public DeletePhotoForm()
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
			this.labelMessage = new Label();
			this.buttonYes = new Button();
			this.buttonNo = new Button();
			this.checkDontShow = new CheckBox();
			this.SuspendLayout();
			Control arg_4D_0 = this.pictIcon;
			Point location = new Point(8, 8);
			arg_4D_0.Location = location;
			this.pictIcon.Name = "pictIcon";
			Control arg_74_0 = this.pictIcon;
			Size size = new Size(32, 32);
			arg_74_0.Size = size;
			this.pictIcon.TabIndex = 0;
			this.pictIcon.TabStop = false;
			this.labelMessage.FlatStyle = FlatStyle.System;
			Control arg_AE_0 = this.labelMessage;
			location = new Point(56, 8);
			arg_AE_0.Location = location;
			this.labelMessage.Name = "labelMessage";
			Control arg_D8_0 = this.labelMessage;
			size = new Size(280, 56);
			arg_D8_0.Size = size;
			this.labelMessage.TabIndex = 2;
			this.labelMessage.Text = "< message >";
			this.buttonYes.DialogResult = 1;
			this.buttonYes.FlatStyle = FlatStyle.System;
			Control arg_126_0 = this.buttonYes;
			location = new Point(216, 104);
			arg_126_0.Location = location;
			this.buttonYes.Name = "buttonYes";
			Control arg_14D_0 = this.buttonYes;
			size = new Size(56, 23);
			arg_14D_0.Size = size;
			this.buttonYes.TabIndex = 0;
			this.buttonYes.Text = "Yes";
			this.buttonNo.DialogResult = 2;
			this.buttonNo.FlatStyle = FlatStyle.System;
			Control arg_19B_0 = this.buttonNo;
			location = new Point(280, 104);
			arg_19B_0.Location = location;
			this.buttonNo.Name = "buttonNo";
			Control arg_1C2_0 = this.buttonNo;
			size = new Size(56, 23);
			arg_1C2_0.Size = size;
			this.buttonNo.TabIndex = 1;
			this.buttonNo.Text = "No";
			this.checkDontShow.FlatStyle = FlatStyle.System;
			Control arg_201_0 = this.checkDontShow;
			location = new Point(56, 72);
			arg_201_0.Location = location;
			this.checkDontShow.Name = "checkDontShow";
			Control arg_22B_0 = this.checkDontShow;
			size = new Size(184, 24);
			arg_22B_0.Size = size;
			this.checkDontShow.TabIndex = 3;
			this.checkDontShow.Text = "&Don't show this message again.";
			this.AcceptButton = this.buttonYes;
			size = new Size(5, 13);
			this.AutoScaleBaseSize = size;
			this.CancelButton = this.buttonNo;
			size = new Size(346, 136);
			this.ClientSize = size;
			this.Controls.Add(this.checkDontShow);
			this.Controls.Add(this.buttonYes);
			this.Controls.Add(this.labelMessage);
			this.Controls.Add(this.pictIcon);
			this.Controls.Add(this.buttonNo);
			this.FormBorderStyle = 3;
			this.MaximizeBox = false;
			this.MinimizeBox = false;
			this.Name = "DeletePhotoForm";
			this.ShowInTaskbar = false;
			this.StartPosition = 4;
			this.Text = "Confirm Photo Delete";
			this.ResumeLayout(false);
		}
		protected override void OnLoad(EventArgs e)
		{
			base.OnLoad(e);
			this.checkDontShow.Checked = !Global.Settings.GetBool(SettingKey.PromptFileDelete);
		}
		protected override void OnClosing(CancelEventArgs e)
		{
			base.OnClosing(e);
			Global.Settings.SetValue(SettingKey.PromptFileDelete, !this.checkDontShow.Checked);
		}
		private void pictIcon_Paint(object sender, PaintEventArgs e)
		{
			e.Graphics.DrawIcon(SystemIcons.Question, 0, 0);
		}
	}
}
