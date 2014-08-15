using Microsoft.VisualBasic;
using Microsoft.VisualBasic.CompilerServices;
using System;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;
using System.Globalization;
using System.IO;
using System.Runtime.CompilerServices;
using System.Security.Permissions;
using System.Text;
using System.Windows.Forms;
namespace FotoVision
{
	public class PropertiesForm : Form
	{
		private enum Win32
		{
			CS_DROPSHADOW = 131072,
			WM_NCACTIVATE = 134
		}
		[AccessedThroughProperty("pictThumbnail")]
		private PictureBox _pictThumbnail;
		[AccessedThroughProperty("labelProgress")]
		private Label _labelProgress;
		[AccessedThroughProperty("pageFile")]
		private TabPage _pageFile;
		[AccessedThroughProperty("timer")]
		private Timer _timer;
		[AccessedThroughProperty("pageExif")]
		private TabPage _pageExif;
		[AccessedThroughProperty("buttonClose")]
		private Button _buttonClose;
		[AccessedThroughProperty("panelProgress")]
		private Panel _panelProgress;
		[AccessedThroughProperty("labelSize")]
		private Label _labelSize;
		[AccessedThroughProperty("pageActions")]
		private TabPage _pageActions;
		[AccessedThroughProperty("textActions")]
		private TextBox _textActions;
		[AccessedThroughProperty("labelFileName")]
		private Label _labelFileName;
		[AccessedThroughProperty("tabControl")]
		private TabControl _tabControl;
		private string _path;
		private Exif _exif;
		private PhotoInfo _photoInfo;
		private Font _fontHeader;
		private StringFormat _format;
		private IContainer components;
		private PictureBox pictThumbnail
		{
			get
			{
				return this._pictThumbnail;
			}
			[MethodImpl(32)]
			set
			{
				if (this._pictThumbnail != null)
				{
				}
				this._pictThumbnail = value;
				if (this._pictThumbnail != null)
				{
				}
			}
		}
		private TabControl tabControl
		{
			get
			{
				return this._tabControl;
			}
			[MethodImpl(32)]
			set
			{
				if (this._tabControl != null)
				{
				}
				this._tabControl = value;
				if (this._tabControl != null)
				{
				}
			}
		}
		private TabPage pageFile
		{
			get
			{
				return this._pageFile;
			}
			[MethodImpl(32)]
			set
			{
				if (this._pageFile != null)
				{
					this._pageFile.remove_Paint(new PaintEventHandler(this.pageFile_Paint));
				}
				this._pageFile = value;
				if (this._pageFile != null)
				{
					this._pageFile.add_Paint(new PaintEventHandler(this.pageFile_Paint));
				}
			}
		}
		private TabPage pageExif
		{
			get
			{
				return this._pageExif;
			}
			[MethodImpl(32)]
			set
			{
				if (this._pageExif != null)
				{
					this._pageExif.remove_Paint(new PaintEventHandler(this.pageExif_Paint));
				}
				this._pageExif = value;
				if (this._pageExif != null)
				{
					this._pageExif.add_Paint(new PaintEventHandler(this.pageExif_Paint));
				}
			}
		}
		private Label labelSize
		{
			get
			{
				return this._labelSize;
			}
			[MethodImpl(32)]
			set
			{
				if (this._labelSize != null)
				{
				}
				this._labelSize = value;
				if (this._labelSize != null)
				{
				}
			}
		}
		private TabPage pageActions
		{
			get
			{
				return this._pageActions;
			}
			[MethodImpl(32)]
			set
			{
				if (this._pageActions != null)
				{
				}
				this._pageActions = value;
				if (this._pageActions != null)
				{
				}
			}
		}
		private TextBox textActions
		{
			get
			{
				return this._textActions;
			}
			[MethodImpl(32)]
			set
			{
				if (this._textActions != null)
				{
				}
				this._textActions = value;
				if (this._textActions != null)
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
		private Label labelFileName
		{
			get
			{
				return this._labelFileName;
			}
			[MethodImpl(32)]
			set
			{
				if (this._labelFileName != null)
				{
				}
				this._labelFileName = value;
				if (this._labelFileName != null)
				{
				}
			}
		}
		private Button buttonClose
		{
			get
			{
				return this._buttonClose;
			}
			[MethodImpl(32)]
			set
			{
				if (this._buttonClose != null)
				{
					this._buttonClose.remove_Click(new EventHandler(this.buttonClose_Click));
				}
				this._buttonClose = value;
				if (this._buttonClose != null)
				{
					this._buttonClose.add_Click(new EventHandler(this.buttonClose_Click));
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
		protected override CreateParams CreateParams
		{
			[PermissionSet(SecurityAction.LinkDemand, XML = "<PermissionSet class=\"System.Security.PermissionSet\"\r\n               version=\"1\">\r\n   <IPermission class=\"System.Security.Permissions.SecurityPermission, mscorlib, Version=1.0.5000.0, Culture=neutral, PublicKeyToken=b77a5c561934e089\"\r\n                version=\"1\"\r\n                Flags=\"UnmanagedCode\"/>\r\n</PermissionSet>\r\n")]
			get
			{
				CreateParams createParams = base.get_CreateParams();
				if (this.RunningOnXP())
				{
					createParams.set_ClassStyle(createParams.get_ClassStyle() | 131072);
				}
				return createParams;
			}
		}
		public PropertiesForm(string path)
		{
			this.InitializeComponent();
			this._path = path;
			this._fontHeader = this.labelFileName.get_Font();
			this._format = new StringFormat();
			this._format.set_Trimming(5);
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
			this.components = new Container();
			this.buttonClose = new Button();
			this.pictThumbnail = new PictureBox();
			this.tabControl = new TabControl();
			this.pageFile = new TabPage();
			this.pageExif = new TabPage();
			this.pageActions = new TabPage();
			this.textActions = new TextBox();
			this.labelFileName = new Label();
			this.labelSize = new Label();
			this.timer = new Timer(this.components);
			this.panelProgress = new Panel();
			this.labelProgress = new Label();
			this.tabControl.SuspendLayout();
			this.pageActions.SuspendLayout();
			this.panelProgress.SuspendLayout();
			this.SuspendLayout();
			this.buttonClose.set_DialogResult(1);
			this.buttonClose.set_FlatStyle(3);
			Control arg_EC_0 = this.buttonClose;
			Point location = new Point(232, 258);
			arg_EC_0.set_Location(location);
			this.buttonClose.set_Name("buttonClose");
			Control arg_113_0 = this.buttonClose;
			Size size = new Size(56, 23);
			arg_113_0.set_Size(size);
			this.buttonClose.set_TabIndex(0);
			this.buttonClose.set_Text("Close");
			Control arg_144_0 = this.pictThumbnail;
			location = new Point(8, 8);
			arg_144_0.set_Location(location);
			this.pictThumbnail.set_Name("pictThumbnail");
			Control arg_16B_0 = this.pictThumbnail;
			size = new Size(48, 48);
			arg_16B_0.set_Size(size);
			this.pictThumbnail.set_SizeMode(3);
			this.pictThumbnail.set_TabIndex(2);
			this.pictThumbnail.set_TabStop(false);
			this.tabControl.get_Controls().Add(this.pageFile);
			this.tabControl.get_Controls().Add(this.pageExif);
			this.tabControl.get_Controls().Add(this.pageActions);
			Control arg_1E7_0 = this.tabControl;
			location = new Point(8, 64);
			arg_1E7_0.set_Location(location);
			this.tabControl.set_Name("tabControl");
			this.tabControl.set_SelectedIndex(0);
			Control arg_220_0 = this.tabControl;
			size = new Size(280, 186);
			arg_220_0.set_Size(size);
			this.tabControl.set_TabIndex(3);
			this.pageFile.set_BackColor(SystemColors.get_Info());
			Control arg_252_0 = this.pageFile;
			location = new Point(4, 22);
			arg_252_0.set_Location(location);
			this.pageFile.set_Name("pageFile");
			Control arg_27F_0 = this.pageFile;
			size = new Size(272, 160);
			arg_27F_0.set_Size(size);
			this.pageFile.set_TabIndex(0);
			this.pageFile.set_Text("File");
			this.pageExif.set_BackColor(SystemColors.get_Info());
			Control arg_2C1_0 = this.pageExif;
			location = new Point(4, 22);
			arg_2C1_0.set_Location(location);
			this.pageExif.set_Name("pageExif");
			Control arg_2EE_0 = this.pageExif;
			size = new Size(272, 160);
			arg_2EE_0.set_Size(size);
			this.pageExif.set_TabIndex(1);
			this.pageExif.set_Text("Exif");
			this.pageActions.set_BackColor(SystemColors.get_Info());
			this.pageActions.get_Controls().Add(this.textActions);
			Control arg_346_0 = this.pageActions;
			location = new Point(4, 22);
			arg_346_0.set_Location(location);
			this.pageActions.set_Name("pageActions");
			Control arg_373_0 = this.pageActions;
			size = new Size(272, 160);
			arg_373_0.set_Size(size);
			this.pageActions.set_TabIndex(2);
			this.pageActions.set_Text("Actions");
			this.textActions.set_BackColor(SystemColors.get_Info());
			this.textActions.set_BorderStyle(0);
			this.textActions.set_Dock(5);
			Control arg_3CC_0 = this.textActions;
			location = new Point(0, 0);
			arg_3CC_0.set_Location(location);
			this.textActions.set_Multiline(true);
			this.textActions.set_Name("textActions");
			this.textActions.set_ReadOnly(true);
			this.textActions.set_ScrollBars(2);
			Control arg_41D_0 = this.textActions;
			size = new Size(272, 160);
			arg_41D_0.set_Size(size);
			this.textActions.set_TabIndex(2);
			this.textActions.set_Text("");
			this.textActions.set_WordWrap(false);
			this.labelFileName.set_FlatStyle(3);
            this.labelFileName.set_Font(new Font("Microsoft Sans Serif", 8.25f, FontStyle.Bold, GraphicsUnit.Point, 0));
			Control arg_485_0 = this.labelFileName;
			location = new Point(64, 16);
			arg_485_0.set_Location(location);
			this.labelFileName.set_Name("labelFileName");
			Control arg_4AF_0 = this.labelFileName;
			size = new Size(224, 16);
			arg_4AF_0.set_Size(size);
			this.labelFileName.set_TabIndex(4);
			this.labelFileName.set_Visible(false);
			this.labelSize.set_FlatStyle(3);
			Control arg_4EA_0 = this.labelSize;
			location = new Point(64, 32);
			arg_4EA_0.set_Location(location);
			this.labelSize.set_Name("labelSize");
			Control arg_514_0 = this.labelSize;
			size = new Size(224, 16);
			arg_514_0.set_Size(size);
			this.labelSize.set_TabIndex(4);
			this.timer.set_Interval(1);
			this.panelProgress.set_BackColor(SystemColors.get_Info());
			this.panelProgress.get_Controls().Add(this.labelProgress);
			Control arg_56C_0 = this.panelProgress;
			location = new Point(28, 140);
			arg_56C_0.set_Location(location);
			this.panelProgress.set_Name("panelProgress");
			Control arg_596_0 = this.panelProgress;
			size = new Size(240, 32);
			arg_596_0.set_Size(size);
			this.panelProgress.set_TabIndex(14);
			this.labelProgress.set_FlatStyle(3);
            this.labelProgress.set_Font(new Font("Microsoft Sans Serif", 8.25f, FontStyle.Regular, GraphicsUnit.Point, 0));
			this.labelProgress.set_ForeColor(SystemColors.get_ControlText());
			Control arg_5F2_0 = this.labelProgress;
			location = new Point(48, 8);
			arg_5F2_0.set_Location(location);
			this.labelProgress.set_Name("labelProgress");
			Control arg_61C_0 = this.labelProgress;
			size = new Size(152, 16);
			arg_61C_0.set_Size(size);
			this.labelProgress.set_TabIndex(0);
			this.labelProgress.set_Text("Collecting information…");
			this.set_AcceptButton(this.buttonClose);
			size = new Size(5, 13);
			this.set_AutoScaleBaseSize(size);
			this.set_BackColor(SystemColors.get_Info());
			this.set_CancelButton(this.buttonClose);
			size = new Size(296, 288);
			this.set_ClientSize(size);
			this.set_ControlBox(false);
			this.get_Controls().Add(this.panelProgress);
			this.get_Controls().Add(this.labelFileName);
			this.get_Controls().Add(this.tabControl);
			this.get_Controls().Add(this.pictThumbnail);
			this.get_Controls().Add(this.buttonClose);
			this.get_Controls().Add(this.labelSize);
			this.set_FormBorderStyle(0);
			this.set_MaximizeBox(false);
			this.set_MinimizeBox(false);
			this.set_Name("PropertiesForm");
			this.set_ShowInTaskbar(false);
			this.set_SizeGripStyle(1);
			this.set_StartPosition(4);
			this.set_Text("PropertiesForm");
			this.tabControl.ResumeLayout(false);
			this.pageActions.ResumeLayout(false);
			this.panelProgress.ResumeLayout(false);
			this.ResumeLayout(false);
		}
		protected override void OnLoad(EventArgs e)
		{
			base.OnLoad(e);
			this.Update();
			this._timer.set_Enabled(true);
		}
		protected override void OnClosed(EventArgs e)
		{
			this.pictThumbnail.get_Image().Dispose();
			base.OnClosed(e);
		}
		private void buttonClose_Click(object sender, EventArgs e)
		{
			this.Close();
		}
		private void timer_Tick(object sender, EventArgs e)
		{
			this._timer.set_Enabled(false);
			Cursor.set_Current(Cursors.get_WaitCursor());
			this.GetInfo();
			this.panelProgress.set_Visible(false);
			Cursor.set_Current(Cursors.get_Default());
		}
		private void GetInfo()
		{
			try
			{
				Bitmap bitmap = new Bitmap(this._path);
				this._photoInfo = default(PhotoInfo);
				this._photoInfo.Read(this._path, bitmap, bitmap.get_RawFormat());
				this._exif = new Exif();
				this._exif.Read(bitmap);
				this.labelSize.set_Text(string.Format("{0} x {1} pixels", this._photoInfo.ImageSize.get_Width(), this._photoInfo.ImageSize.get_Height()));
				this.pictThumbnail.set_Image(PhotoHelper.GetThumbnail(bitmap, this.pictThumbnail.get_Width()));
				this.textActions.set_Text(this.GetActionList());
				this.pageFile.Invalidate();
			}
			catch (Exception expr_CA)
			{
				ProjectData.SetProjectError(expr_CA);
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
		private string GetActionList()
		{
			int count = Global.ActionList.Count;
			if (count == 0)
			{
				return "No pending actions.";
			}
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.AppendFormat("Contains {0} pending action{1}.{2}{3}", new object[]
			{
				count,
				RuntimeHelpers.GetObjectValue(Interaction.IIf(count == 1, "", "s")),
				"\r\n",
				"\r\n"
			});
			int arg_78_0 = 0;
			checked
			{
				int num = Global.ActionList.Count - 1;
				for (int i = arg_78_0; i <= num; i++)
				{
					ActionItem at = Global.ActionList.GetAt(i);
					stringBuilder.AppendFormat("{0}{1}", at.ToString(true), "\r\n");
				}
				return stringBuilder.ToString();
			}
		}
		private bool RunningOnXP()
		{
			return Environment.get_OSVersion().get_Version().get_Major() >= 5 && (Environment.get_OSVersion().get_Version().get_Major() > 5 || Environment.get_OSVersion().get_Version().get_Minor() > 0);
		}
		[PermissionSet(SecurityAction.LinkDemand, XML = "<PermissionSet class=\"System.Security.PermissionSet\"\r\n               version=\"1\">\r\n   <IPermission class=\"System.Security.Permissions.SecurityPermission, mscorlib, Version=1.0.5000.0, Culture=neutral, PublicKeyToken=b77a5c561934e089\"\r\n                version=\"1\"\r\n                Flags=\"UnmanagedCode\"/>\r\n</PermissionSet>\r\n")]
		protected override void WndProc(ref Message m)
		{
			if (m.get_Msg() == 134 && m.get_WParam().ToInt32() == 0)
			{
				this.Close();
			}
			base.WndProc(ref m);
		}
		private void pageExif_Paint(object sender, PaintEventArgs e)
		{
			this.DrawExifInfo(e.get_Graphics());
		}
		private void pageFile_Paint(object sender, PaintEventArgs e)
		{
			this.DrawFileInfo(e.get_Graphics());
		}
		protected override void OnPaint(PaintEventArgs e)
		{
			RectangleF rectangleF = new RectangleF((float)this.labelFileName.get_Bounds().get_Left(), (float)this.labelFileName.get_Bounds().get_Top(), (float)this.labelFileName.get_Bounds().get_Width(), (float)this.get_Font().get_Height());
			RectangleF rectangleF2 = rectangleF;
			e.get_Graphics().DrawString(Path.GetFileName(this._path), this._fontHeader, SystemBrushes.get_ControlText(), rectangleF2, this._format);
			checked
			{
				e.get_Graphics().DrawRectangle(SystemPens.get_ControlDark(), 0, 0, this.get_Width() - 1, this.get_Height() - 1);
			}
		}
		private void DrawFileInfo(Graphics g)
		{
			if (this._exif == null)
			{
				return;
			}
			RectangleF rectangleF;
			int num;
			checked
			{
				rectangleF = new RectangleF(2f, 2f, (float)(this.pageExif.get_Width() - 4), (float)this.get_Font().get_Height());
				num = this.get_Font().get_Height() + 2;
				g.DrawString("Size", this._fontHeader, SystemBrushes.get_ControlText(), rectangleF);
			}
			rectangleF.set_Y(rectangleF.get_Y() + (float)num);
			g.DrawString(this._photoInfo.FileLengthString, this.get_Font(), SystemBrushes.get_ControlText(), rectangleF, this._format);
			rectangleF.set_Y(rectangleF.get_Y() + (float)num);
			rectangleF.set_Y(rectangleF.get_Y() + (float)(num / 2));
			g.DrawString("Created", this._fontHeader, SystemBrushes.get_ControlText(), rectangleF);
			rectangleF.set_Y(rectangleF.get_Y() + (float)num);
			g.DrawString(this._photoInfo.DateCreated.ToLongDateString() + " " + this._photoInfo.DateCreated.ToLongTimeString(), this.get_Font(), SystemBrushes.get_ControlText(), rectangleF, this._format);
			rectangleF.set_Y(rectangleF.get_Y() + (float)num);
			rectangleF.set_Y(rectangleF.get_Y() + (float)(num / 2));
			g.DrawString("Modified", this._fontHeader, SystemBrushes.get_ControlText(), rectangleF);
			rectangleF.set_Y(rectangleF.get_Y() + (float)num);
			g.DrawString(this._photoInfo.DateModified.ToLongDateString() + " " + this._photoInfo.DateModified.ToLongTimeString(), this.get_Font(), SystemBrushes.get_ControlText(), rectangleF, this._format);
			rectangleF.set_Y(rectangleF.get_Y() + (float)num);
			rectangleF.set_Y(rectangleF.get_Y() + (float)(num / 2));
			g.DrawString("Location", this._fontHeader, SystemBrushes.get_ControlText(), rectangleF);
			rectangleF.set_Y(rectangleF.get_Y() + (float)num);
			rectangleF.set_Height((float)checked(this.get_Font().get_Height() * 2));
			g.DrawString(Path.GetDirectoryName(this._path), this.get_Font(), SystemBrushes.get_ControlText(), rectangleF, this._format);
		}
		private void DrawExifInfo(Graphics g)
		{
			if (this._exif == null)
			{
				return;
			}
			RectangleF rectangleF;
			int num;
			checked
			{
				rectangleF = new RectangleF(2f, 2f, (float)(this.pageExif.get_Width() - 4), (float)this.get_Font().get_Height());
				num = this.get_Font().get_Height() + 2;
				g.DrawString("Camera", this._fontHeader, SystemBrushes.get_ControlText(), rectangleF);
			}
			rectangleF.set_Y(rectangleF.get_Y() + (float)num);
			if (this._exif.Make.get_Length() == 0 && this._exif.Model.get_Length() == 0)
			{
				g.DrawString("(none)", this.get_Font(), SystemBrushes.get_ControlText(), rectangleF, this._format);
				rectangleF.set_Y(rectangleF.get_Y() + (float)num);
			}
			else
			{
				if (this._exif.Make.get_Length() > 0)
				{
					g.DrawString(this._exif.Make, this.get_Font(), SystemBrushes.get_ControlText(), rectangleF, this._format);
					rectangleF.set_Y(rectangleF.get_Y() + (float)num);
				}
				if (this._exif.Model.get_Length() > 0)
				{
					g.DrawString(this._exif.Model, this.get_Font(), SystemBrushes.get_ControlText(), rectangleF, this._format);
					rectangleF.set_Y(rectangleF.get_Y() + (float)num);
				}
			}
			rectangleF.set_Y(rectangleF.get_Y() + (float)(num / 2));
			g.DrawString("Settings", this._fontHeader, SystemBrushes.get_ControlText(), rectangleF);
			rectangleF.set_Y(rectangleF.get_Y() + (float)num);
			if (this._exif.ExposureTime == 0f && this._exif.Aperture == 0f && this._exif.Iso == 0)
			{
				g.DrawString("(none)", this.get_Font(), SystemBrushes.get_ControlText(), rectangleF, this._format);
				rectangleF.set_Y(rectangleF.get_Y() + (float)num);
			}
			else
			{
				string text;
				if (this._exif.ExposureTime < 1f)
				{
					text = "1/" + StringType.FromInteger(checked((int)Math.Round((double)(1f / this._exif.ExposureTime))));
				}
				else
				{
					text = this._exif.ExposureTime.ToString("f1", CultureInfo.get_CurrentCulture());
				}
				g.DrawString(string.Format("{0} sec, F {1}", text, this._exif.Aperture.ToString("f1", CultureInfo.get_CurrentCulture())), this.get_Font(), SystemBrushes.get_ControlText(), rectangleF, this._format);
				rectangleF.set_Y(rectangleF.get_Y() + (float)num);
				if (this._exif.Iso == 0)
				{
					g.DrawString(StringType.FromObject(Interaction.IIf(this._exif.Flash, "Flash", "No flash")), this.get_Font(), SystemBrushes.get_ControlText(), rectangleF, this._format);
				}
				else
				{
					g.DrawString(string.Format("ISO {0}, {1}", this._exif.Iso, StringType.FromObject(Interaction.IIf(this._exif.Flash, "Flash", "No flash"))), this.get_Font(), SystemBrushes.get_ControlText(), rectangleF, this._format);
				}
				rectangleF.set_Y(rectangleF.get_Y() + (float)num);
			}
			rectangleF.set_Y(rectangleF.get_Y() + (float)(num / 2));
			g.DrawString("Comments", this._fontHeader, SystemBrushes.get_ControlText(), rectangleF);
			rectangleF.set_Y(rectangleF.get_Y() + (float)num);
			if (this._exif.UserComment.get_Length() == 0)
			{
				g.DrawString("(none)", this.get_Font(), SystemBrushes.get_ControlText(), rectangleF, this._format);
			}
			else
			{
				rectangleF.set_Height((float)checked(this.get_Font().get_Height() * 2));
				g.DrawString(this._exif.UserComment, this.get_Font(), SystemBrushes.get_ControlText(), rectangleF, this._format);
			}
		}
	}
}
