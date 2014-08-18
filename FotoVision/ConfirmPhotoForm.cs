using Microsoft.VisualBasic.CompilerServices;
using System;
using System.ComponentModel;
using System.Drawing;
using System.Runtime.CompilerServices;
using System.Windows.Forms;
namespace FotoVision
{
	public class ConfirmPhotoForm : Form
	{
		[AccessedThroughProperty("pictIcon")]
		private PictureBox _pictIcon;
		[AccessedThroughProperty("labelProgress")]
		private Label _labelProgress;
		[AccessedThroughProperty("buttonNo")]
		private Button _buttonNo;
		[AccessedThroughProperty("buttonYesToAll")]
		private Button _buttonYesToAll;
		[AccessedThroughProperty("textCurrent")]
		private TextBox _textCurrent;
		[AccessedThroughProperty("buttonOK")]
		private Button _buttonOK;
		[AccessedThroughProperty("buttonCancel")]
		private Button _buttonCancel;
		[AccessedThroughProperty("labelNew")]
		private Label _labelNew;
		[AccessedThroughProperty("labelExisting")]
		private Label _labelExisting;
		[AccessedThroughProperty("timer")]
		private Timer _timer;
		[AccessedThroughProperty("labelHeader")]
		private Label _labelHeader;
		[AccessedThroughProperty("panelProgress")]
		private Panel _panelProgress;
		[AccessedThroughProperty("textNew")]
		private TextBox _textNew;
		private string _pathCur;
		private string _pathNew;
		private IContainer components;
		private PictureBox pictCurrent;
		private PictureBox pictNew;
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
		private TextBox textCurrent
		{
			get
			{
				return this._textCurrent;
			}
			[MethodImpl(32)]
			set
			{
				if (this._textCurrent != null)
				{
				}
				this._textCurrent = value;
				if (this._textCurrent != null)
				{
				}
			}
		}
		private TextBox textNew
		{
			get
			{
				return this._textNew;
			}
			[MethodImpl(32)]
			set
			{
				if (this._textNew != null)
				{
				}
				this._textNew = value;
				if (this._textNew != null)
				{
				}
			}
		}
		private Timer timer
		{
			get
			{
				return this._timer;
			}
			[MethodImpl(32)]
			set
			{
				if (this._timer != null)
				{
					this._timer.Tick -= new EventHandler(this.timer_Tick);
				}
				this._timer = value;
				if (this._timer != null)
				{
					this._timer.Tick += new EventHandler(this.timer_Tick);
				}
			}
		}
		private Panel panelProgress
		{
			get
			{
				return this._panelProgress;
			}
			[MethodImpl(32)]
			set
			{
				if (this._panelProgress != null)
				{
				}
				this._panelProgress = value;
				if (this._panelProgress != null)
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
		private Label labelExisting
		{
			get
			{
				return this._labelExisting;
			}
			[MethodImpl(32)]
			set
			{
				if (this._labelExisting != null)
				{
				}
				this._labelExisting = value;
				if (this._labelExisting != null)
				{
				}
			}
		}
		private Label labelNew
		{
			get
			{
				return this._labelNew;
			}
			[MethodImpl(32)]
			set
			{
				if (this._labelNew != null)
				{
				}
				this._labelNew = value;
				if (this._labelNew != null)
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
		private Button buttonYesToAll
		{
			get
			{
				return this._buttonYesToAll;
			}
			[MethodImpl(32)]
			set
			{
				if (this._buttonYesToAll != null)
				{
				}
				this._buttonYesToAll = value;
				if (this._buttonYesToAll != null)
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
		private Label labelProgress
		{
			get
			{
				return this._labelProgress;
			}
			[MethodImpl(32)]
			set
			{
				if (this._labelProgress != null)
				{
				}
				this._labelProgress = value;
				if (this._labelProgress != null)
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
		private void InitializeComponent()
		{
			this.components = new Container();
			this.labelHeader = new Label();
			this.pictCurrent = new PictureBox();
			this.labelExisting = new Label();
			this.labelNew = new Label();
			this.pictNew = new PictureBox();
			this.buttonOK = new Button();
			this.buttonYesToAll = new Button();
			this.buttonNo = new Button();
			this.buttonCancel = new Button();
			this.pictIcon = new PictureBox();
			this.textCurrent = new TextBox();
			this.textNew = new TextBox();
			this.timer = new Timer(this.components);
			this.panelProgress = new Panel();
			this.labelProgress = new Label();
			this.panelProgress.SuspendLayout();
			this.SuspendLayout();
			this.labelHeader.FlatStyle = FlatStyle.System;
			Control arg_E4_0 = this.labelHeader;
			Point location = new Point(56, 8);
			arg_E4_0.Location = location;
			this.labelHeader.Name = "labelHeader";
			Control arg_10E_0 = this.labelHeader;
			Size size = new Size(296, 32);
			arg_10E_0.Size = size;
			this.labelHeader.TabIndex = 4;
			this.labelHeader.Text = "The album contains a photo with the same name. Do you want to replace the existing photo with the new one?";
			Control arg_141_0 = this.pictCurrent;
			location = new Point(56, 72);
			arg_141_0.Location = location;
			this.pictCurrent.Name = "pictCurrent";
			Control arg_168_0 = this.pictCurrent;
			size = new Size(60, 60);
			arg_168_0.Size = size;
            this.pictCurrent.SizeMode = PictureBoxSizeMode.CenterImage;
			this.pictCurrent.TabIndex = 1;
			this.pictCurrent.TabStop = false;
			this.labelExisting.FlatStyle = FlatStyle.System;
			this.labelExisting.Font = new Font("Microsoft Sans Serif", 8.25f, FontStyle.Bold, GraphicsUnit.Point, 0);
			Control arg_1CC_0 = this.labelExisting;
			location = new Point(56, 56);
			arg_1CC_0.Location = location;
			this.labelExisting.Name = "labelExisting";
			Control arg_1F3_0 = this.labelExisting;
			size = new Size(120, 16);
			arg_1F3_0.Size = size;
			this.labelExisting.TabIndex = 5;
			this.labelExisting.Text = "Existing photo";
			this.labelNew.FlatStyle = FlatStyle.System;
            this.labelNew.Font = new Font("Microsoft Sans Serif", 8.25f, FontStyle.Bold, GraphicsUnit.Point, 0);
			Control arg_252_0 = this.labelNew;
			location = new Point(56, 152);
			arg_252_0.Location = location;
			this.labelNew.Name = "labelNew";
			Control arg_279_0 = this.labelNew;
			size = new Size(120, 16);
			arg_279_0.Size = size;
			this.labelNew.TabIndex = 7;
			this.labelNew.Text = "New photo";
			Control arg_2AF_0 = this.pictNew;
			location = new Point(56, 168);
			arg_2AF_0.Location = location;
			this.pictNew.Name = "pictNew";
			Control arg_2D6_0 = this.pictNew;
			size = new Size(60, 60);
			arg_2D6_0.Size = size;
            this.pictNew.SizeMode = PictureBoxSizeMode.CenterImage;
			this.pictNew.TabIndex = 1;
			this.pictNew.TabStop = false;
			this.buttonOK.DialogResult = DialogResult.OK;
			this.buttonOK.FlatStyle = FlatStyle.System;
			Control arg_32C_0 = this.buttonOK;
			location = new Point(16, 248);
			arg_32C_0.Location = location;
			this.buttonOK.Name = "buttonOK";
			Control arg_353_0 = this.buttonOK;
			size = new Size(72, 23);
			arg_353_0.Size = size;
			this.buttonOK.TabIndex = 0;
			this.buttonOK.Text = "Yes";
			this.buttonYesToAll.DialogResult = DialogResult.Yes;
			this.buttonYesToAll.FlatStyle = FlatStyle.System;
			Control arg_3A1_0 = this.buttonYesToAll;
			location = new Point(104, 248);
			arg_3A1_0.Location = location;
			this.buttonYesToAll.Name = "buttonYesToAll";
			Control arg_3C8_0 = this.buttonYesToAll;
			size = new Size(72, 23);
			arg_3C8_0.Size = size;
			this.buttonYesToAll.TabIndex = 1;
			this.buttonYesToAll.Text = "Yes to All";
			this.buttonNo.DialogResult = DialogResult.No;
			this.buttonNo.FlatStyle = FlatStyle.System;
			Control arg_419_0 = this.buttonNo;
			location = new Point(192, 248);
			arg_419_0.Location = location;
			this.buttonNo.Name = "buttonNo";
			Control arg_440_0 = this.buttonNo;
			size = new Size(72, 23);
			arg_440_0.Size = size;
			this.buttonNo.TabIndex = 2;
			this.buttonNo.Text = "No";
			this.buttonCancel.DialogResult = DialogResult.Cancel;
			this.buttonCancel.FlatStyle = FlatStyle.System;
			Control arg_491_0 = this.buttonCancel;
			location = new Point(280, 248);
			arg_491_0.Location = location;
			this.buttonCancel.Name = "buttonCancel";
			Control arg_4B8_0 = this.buttonCancel;
			size = new Size(72, 23);
			arg_4B8_0.Size = size;
			this.buttonCancel.TabIndex = 3;
			this.buttonCancel.Text = "Cancel";
			Control arg_4E9_0 = this.pictIcon;
			location = new Point(8, 8);
			arg_4E9_0.Location = location;
			this.pictIcon.Name = "pictIcon";
			Control arg_510_0 = this.pictIcon;
			size = new Size(32, 32);
			arg_510_0.Size = size;
			this.pictIcon.TabIndex = 11;
			this.pictIcon.TabStop = false;
			this.textCurrent.BackColor = SystemColors.Info;
			Control arg_553_0 = this.textCurrent;
			location = new Point(128, 72);
			arg_553_0.Location = location;
			this.textCurrent.Multiline = true;
			this.textCurrent.Name = "textCurrent";
			this.textCurrent.ReadOnly = true;
			this.textCurrent.ScrollBars = ScrollBars.Vertical;
			Control arg_5A1_0 = this.textCurrent;
			size = new Size(224, 60);
			arg_5A1_0.Size = size;
			this.textCurrent.TabIndex = 6;
			this.textCurrent.Text = "";
			this.textNew.BackColor = SystemColors.Info;
			Control arg_5EA_0 = this.textNew;
			location = new Point(128, 168);
			arg_5EA_0.Location = location;
			this.textNew.Multiline = true;
			this.textNew.Name = "textNew";
			this.textNew.ReadOnly = true;
            this.textNew.ScrollBars = ScrollBars.Vertical;
			Control arg_638_0 = this.textNew;
			size = new Size(224, 60);
			arg_638_0.Size = size;
			this.textNew.TabIndex = 8;
			this.textNew.Text = "";
			this.timer.Interval = 1;
			this.panelProgress.BackColor = SystemColors.Info;
            this.panelProgress.BorderStyle = BorderStyle.FixedSingle;
			this.panelProgress.Controls.Add(this.labelProgress);
			Control arg_6A9_0 = this.panelProgress;
			location = new Point(72, 120);
			arg_6A9_0.Location = location;
			this.panelProgress.Name = "panelProgress";
			Control arg_6D3_0 = this.panelProgress;
			size = new Size(240, 32);
			arg_6D3_0.Size = size;
			this.panelProgress.TabIndex = 13;
			this.labelProgress.FlatStyle = FlatStyle.System;
			this.labelProgress.Font = new Font("Microsoft Sans Serif", 8.25f, FontStyle.Regular, GraphicsUnit.Point, 0);
			this.labelProgress.ForeColor = SystemColors.ControlText;
			Control arg_72F_0 = this.labelProgress;
			location = new Point(48, 8);
			arg_72F_0.Location = location;
			this.labelProgress.Name = "labelProgress";
			Control arg_759_0 = this.labelProgress;
			size = new Size(152, 16);
			arg_759_0.Size = size;
			this.labelProgress.TabIndex = 0;
			this.labelProgress.Text = "Collecting information…";
			this.AcceptButton = this.buttonOK;
			size = new Size(5, 13);
			this.AutoScaleBaseSize = size;
			this.CancelButton = this.buttonCancel;
			size = new Size(362, 280);
			this.ClientSize = size;
			this.Controls.Add(this.panelProgress);
			this.Controls.Add(this.textCurrent);
			this.Controls.Add(this.pictIcon);
			this.Controls.Add(this.buttonOK);
			this.Controls.Add(this.labelExisting);
			this.Controls.Add(this.labelNew);
			this.Controls.Add(this.labelHeader);
			this.Controls.Add(this.buttonYesToAll);
			this.Controls.Add(this.buttonNo);
			this.Controls.Add(this.buttonCancel);
			this.Controls.Add(this.pictCurrent);
			this.Controls.Add(this.pictNew);
			this.Controls.Add(this.textNew);
			this.FormBorderStyle = FormBorderStyle.FixedDialog;
			this.MaximizeBox = false;
			this.MinimizeBox = false;
			this.Name = "ConfirmPhotoForm";
			this.ShowInTaskbar = false;
			this.StartPosition = FormStartPosition.CenterParent;
			this.Text = "Confirm Photo Replace";
			this.panelProgress.ResumeLayout(false);
			this.ResumeLayout(false);
		}
		public ConfirmPhotoForm(string pathCur, string pathNew)
		{
			this.InitializeComponent();
			this._pathCur = pathCur;
			this._pathNew = pathNew;
		}
		protected override void OnLoad(EventArgs e)
		{
			base.OnLoad(e);
			this.Show();
			this.timer.Enabled = true;
		}
		protected override void OnClosed(EventArgs e)
		{
			if (this.pictCurrent.Image != null)
			{
				this.pictCurrent.Image.Dispose();
			}
			if (this.pictNew.Image != null)
			{
				this.pictNew.Image.Dispose();
			}
			base.OnClosed(e);
		}
		private void pictIcon_Paint(object sender, PaintEventArgs e)
		{
			e.Graphics.DrawIcon(SystemIcons.Question, 0, 0);
		}
		private void timer_Tick(object sender, EventArgs e)
		{
			this.timer.Enabled = false;
			Cursor.Current = Cursors.WaitCursor;
			this.GetInfo();
			this.panelProgress.Visible = false;
			Cursor.Current = Cursors.Default;
		}
		private void GetInfo()
		{
            Bitmap bitmap = null;
			try
			{
				bitmap = new Bitmap(this._pathCur);
				this.textCurrent.Text = this.GetPhotoInfo(this._pathCur, bitmap);
				this.pictCurrent.Image = PhotoHelper.GetThumbnail(bitmap, this.pictCurrent.Width);
				bitmap.Dispose();
				bitmap = new Bitmap(this._pathNew);
				this.textNew.Text = this.GetPhotoInfo(this._pathNew, bitmap);
				this.pictNew.Image = PhotoHelper.GetThumbnail(bitmap, this.pictNew.Width);
			}
			catch (Exception expr_88)
			{
				ProjectData.SetProjectError(expr_88);
				ProjectData.ClearProjectError();
			}
			finally
			{
				if (bitmap != null)
				{
					bitmap.Dispose();
				}
			}
		}
		private string GetPhotoInfo(string path, Bitmap image)
		{
			string result;
			try
			{
				PhotoInfo photoInfo = default(PhotoInfo);
				photoInfo.Read(path, image, image.RawFormat);
				result = string.Format("{0}{1}{2} x {3} pixels{4}{5}{6}{7}", new object[]
				{
					photoInfo.FileName,
					"\r\n",
					photoInfo.ImageSize.Width,
					photoInfo.ImageSize.Height,
					"\r\n",
					photoInfo.DateModified.ToLongDateString(),
					"\r\n",
					photoInfo.DateCreated.ToLongTimeString()
				});
			}
			catch (Exception expr_A4)
			{
				ProjectData.SetProjectError(expr_A4);
				result = "";
				ProjectData.ClearProjectError();
			}
			return result;
		}
	}
}
