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
					this._pictIcon.remove_Paint(new PaintEventHandler(this.pictIcon_Paint));
				}
				this._pictIcon = value;
				if (this._pictIcon != null)
				{
					this._pictIcon.add_Paint(new PaintEventHandler(this.pictIcon_Paint));
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
					this._timer.remove_Tick(new EventHandler(this.timer_Tick));
				}
				this._timer = value;
				if (this._timer != null)
				{
					this._timer.add_Tick(new EventHandler(this.timer_Tick));
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
			this.labelHeader.set_FlatStyle(3);
			Control arg_E4_0 = this.labelHeader;
			Point location = new Point(56, 8);
			arg_E4_0.set_Location(location);
			this.labelHeader.set_Name("labelHeader");
			Control arg_10E_0 = this.labelHeader;
			Size size = new Size(296, 32);
			arg_10E_0.set_Size(size);
			this.labelHeader.set_TabIndex(4);
			this.labelHeader.set_Text("The album contains a photo with the same name. Do you want to replace the existing photo with the new one?");
			Control arg_141_0 = this.pictCurrent;
			location = new Point(56, 72);
			arg_141_0.set_Location(location);
			this.pictCurrent.set_Name("pictCurrent");
			Control arg_168_0 = this.pictCurrent;
			size = new Size(60, 60);
			arg_168_0.set_Size(size);
			this.pictCurrent.set_SizeMode(3);
			this.pictCurrent.set_TabIndex(1);
			this.pictCurrent.set_TabStop(false);
			this.labelExisting.set_FlatStyle(3);
			this.labelExisting.set_Font(new Font("Microsoft Sans Serif", 8.25f, FontStyle.Bold, GraphicsUnit.Point, 0));
			Control arg_1CC_0 = this.labelExisting;
			location = new Point(56, 56);
			arg_1CC_0.set_Location(location);
			this.labelExisting.set_Name("labelExisting");
			Control arg_1F3_0 = this.labelExisting;
			size = new Size(120, 16);
			arg_1F3_0.set_Size(size);
			this.labelExisting.set_TabIndex(5);
			this.labelExisting.set_Text("Existing photo");
			this.labelNew.set_FlatStyle(3);
            this.labelNew.set_Font(new Font("Microsoft Sans Serif", 8.25f, FontStyle.Bold, GraphicsUnit.Point, 0));
			Control arg_252_0 = this.labelNew;
			location = new Point(56, 152);
			arg_252_0.set_Location(location);
			this.labelNew.set_Name("labelNew");
			Control arg_279_0 = this.labelNew;
			size = new Size(120, 16);
			arg_279_0.set_Size(size);
			this.labelNew.set_TabIndex(7);
			this.labelNew.set_Text("New photo");
			Control arg_2AF_0 = this.pictNew;
			location = new Point(56, 168);
			arg_2AF_0.set_Location(location);
			this.pictNew.set_Name("pictNew");
			Control arg_2D6_0 = this.pictNew;
			size = new Size(60, 60);
			arg_2D6_0.set_Size(size);
			this.pictNew.set_SizeMode(3);
			this.pictNew.set_TabIndex(1);
			this.pictNew.set_TabStop(false);
			this.buttonOK.set_DialogResult(1);
			this.buttonOK.set_FlatStyle(3);
			Control arg_32C_0 = this.buttonOK;
			location = new Point(16, 248);
			arg_32C_0.set_Location(location);
			this.buttonOK.set_Name("buttonOK");
			Control arg_353_0 = this.buttonOK;
			size = new Size(72, 23);
			arg_353_0.set_Size(size);
			this.buttonOK.set_TabIndex(0);
			this.buttonOK.set_Text("Yes");
			this.buttonYesToAll.set_DialogResult(6);
			this.buttonYesToAll.set_FlatStyle(3);
			Control arg_3A1_0 = this.buttonYesToAll;
			location = new Point(104, 248);
			arg_3A1_0.set_Location(location);
			this.buttonYesToAll.set_Name("buttonYesToAll");
			Control arg_3C8_0 = this.buttonYesToAll;
			size = new Size(72, 23);
			arg_3C8_0.set_Size(size);
			this.buttonYesToAll.set_TabIndex(1);
			this.buttonYesToAll.set_Text("Yes to All");
			this.buttonNo.set_DialogResult(7);
			this.buttonNo.set_FlatStyle(3);
			Control arg_419_0 = this.buttonNo;
			location = new Point(192, 248);
			arg_419_0.set_Location(location);
			this.buttonNo.set_Name("buttonNo");
			Control arg_440_0 = this.buttonNo;
			size = new Size(72, 23);
			arg_440_0.set_Size(size);
			this.buttonNo.set_TabIndex(2);
			this.buttonNo.set_Text("No");
			this.buttonCancel.set_DialogResult(2);
			this.buttonCancel.set_FlatStyle(3);
			Control arg_491_0 = this.buttonCancel;
			location = new Point(280, 248);
			arg_491_0.set_Location(location);
			this.buttonCancel.set_Name("buttonCancel");
			Control arg_4B8_0 = this.buttonCancel;
			size = new Size(72, 23);
			arg_4B8_0.set_Size(size);
			this.buttonCancel.set_TabIndex(3);
			this.buttonCancel.set_Text("Cancel");
			Control arg_4E9_0 = this.pictIcon;
			location = new Point(8, 8);
			arg_4E9_0.set_Location(location);
			this.pictIcon.set_Name("pictIcon");
			Control arg_510_0 = this.pictIcon;
			size = new Size(32, 32);
			arg_510_0.set_Size(size);
			this.pictIcon.set_TabIndex(11);
			this.pictIcon.set_TabStop(false);
			this.textCurrent.set_BackColor(SystemColors.get_Info());
			Control arg_553_0 = this.textCurrent;
			location = new Point(128, 72);
			arg_553_0.set_Location(location);
			this.textCurrent.set_Multiline(true);
			this.textCurrent.set_Name("textCurrent");
			this.textCurrent.set_ReadOnly(true);
			this.textCurrent.set_ScrollBars(2);
			Control arg_5A1_0 = this.textCurrent;
			size = new Size(224, 60);
			arg_5A1_0.set_Size(size);
			this.textCurrent.set_TabIndex(6);
			this.textCurrent.set_Text("");
			this.textNew.set_BackColor(SystemColors.get_Info());
			Control arg_5EA_0 = this.textNew;
			location = new Point(128, 168);
			arg_5EA_0.set_Location(location);
			this.textNew.set_Multiline(true);
			this.textNew.set_Name("textNew");
			this.textNew.set_ReadOnly(true);
			this.textNew.set_ScrollBars(2);
			Control arg_638_0 = this.textNew;
			size = new Size(224, 60);
			arg_638_0.set_Size(size);
			this.textNew.set_TabIndex(8);
			this.textNew.set_Text("");
			this.timer.set_Interval(1);
			this.panelProgress.set_BackColor(SystemColors.get_Info());
			this.panelProgress.set_BorderStyle(1);
			this.panelProgress.get_Controls().Add(this.labelProgress);
			Control arg_6A9_0 = this.panelProgress;
			location = new Point(72, 120);
			arg_6A9_0.set_Location(location);
			this.panelProgress.set_Name("panelProgress");
			Control arg_6D3_0 = this.panelProgress;
			size = new Size(240, 32);
			arg_6D3_0.set_Size(size);
			this.panelProgress.set_TabIndex(13);
			this.labelProgress.set_FlatStyle(3);
			this.labelProgress.set_Font(new Font("Microsoft Sans Serif", 8.25f, FontStyle.Regular, GraphicsUnit.Point, 0));
			this.labelProgress.set_ForeColor(SystemColors.get_ControlText());
			Control arg_72F_0 = this.labelProgress;
			location = new Point(48, 8);
			arg_72F_0.set_Location(location);
			this.labelProgress.set_Name("labelProgress");
			Control arg_759_0 = this.labelProgress;
			size = new Size(152, 16);
			arg_759_0.set_Size(size);
			this.labelProgress.set_TabIndex(0);
			this.labelProgress.set_Text("Collecting information…");
			this.set_AcceptButton(this.buttonOK);
			size = new Size(5, 13);
			this.set_AutoScaleBaseSize(size);
			this.set_CancelButton(this.buttonCancel);
			size = new Size(362, 280);
			this.set_ClientSize(size);
			this.get_Controls().Add(this.panelProgress);
			this.get_Controls().Add(this.textCurrent);
			this.get_Controls().Add(this.pictIcon);
			this.get_Controls().Add(this.buttonOK);
			this.get_Controls().Add(this.labelExisting);
			this.get_Controls().Add(this.labelNew);
			this.get_Controls().Add(this.labelHeader);
			this.get_Controls().Add(this.buttonYesToAll);
			this.get_Controls().Add(this.buttonNo);
			this.get_Controls().Add(this.buttonCancel);
			this.get_Controls().Add(this.pictCurrent);
			this.get_Controls().Add(this.pictNew);
			this.get_Controls().Add(this.textNew);
			this.set_FormBorderStyle(3);
			this.set_MaximizeBox(false);
			this.set_MinimizeBox(false);
			this.set_Name("ConfirmPhotoForm");
			this.set_ShowInTaskbar(false);
			this.set_StartPosition(4);
			this.set_Text("Confirm Photo Replace");
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
			this.timer.set_Enabled(true);
		}
		protected override void OnClosed(EventArgs e)
		{
			if (this.pictCurrent.get_Image() != null)
			{
				this.pictCurrent.get_Image().Dispose();
			}
			if (this.pictNew.get_Image() != null)
			{
				this.pictNew.get_Image().Dispose();
			}
			base.OnClosed(e);
		}
		private void pictIcon_Paint(object sender, PaintEventArgs e)
		{
			e.get_Graphics().DrawIcon(SystemIcons.get_Question(), 0, 0);
		}
		private void timer_Tick(object sender, EventArgs e)
		{
			this.timer.set_Enabled(false);
			Cursor.set_Current(Cursors.get_WaitCursor());
			this.GetInfo();
			this.panelProgress.set_Visible(false);
			Cursor.set_Current(Cursors.get_Default());
		}
		private void GetInfo()
		{
			try
			{
				Bitmap bitmap = new Bitmap(this._pathCur);
				this.textCurrent.set_Text(this.GetPhotoInfo(this._pathCur, bitmap));
				this.pictCurrent.set_Image(PhotoHelper.GetThumbnail(bitmap, this.pictCurrent.get_Width()));
				bitmap.Dispose();
				bitmap = new Bitmap(this._pathNew);
				this.textNew.set_Text(this.GetPhotoInfo(this._pathNew, bitmap));
				this.pictNew.set_Image(PhotoHelper.GetThumbnail(bitmap, this.pictNew.get_Width()));
			}
			catch (Exception expr_88)
			{
				ProjectData.SetProjectError(expr_88);
				ProjectData.ClearProjectError();
			}
			finally
			{
				Bitmap bitmap;
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
				photoInfo.Read(path, image, image.get_RawFormat());
				result = string.Format("{0}{1}{2} x {3} pixels{4}{5}{6}{7}", new object[]
				{
					photoInfo.FileName,
					"\r\n",
					photoInfo.ImageSize.get_Width(),
					photoInfo.ImageSize.get_Height(),
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
