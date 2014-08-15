using Microsoft.VisualBasic;
using System;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;
using System.Resources;
using System.Runtime.CompilerServices;
using System.Threading;
using System.Windows.Forms;
namespace FotoVision
{
	public class UploadForm : Form
	{
		public delegate void PublishCompleteEventHandler(object sender, UploadCompleteEventArgs e);
		public delegate void UploadCompleteEventHandler(object sender, UploadCompleteEventArgs e);
		private delegate void LocalUpdateMessage(string message, bool success, bool add);
		private delegate void LocalUpdateProgress(int pos, int total);
		private delegate void LocalComplete(bool errorOccurred, bool publishOperation);
		[AccessedThroughProperty("_publish")]
		private Publish __publish;
		[AccessedThroughProperty("_upload")]
		private Upload __upload;
		[AccessedThroughProperty("statusBar")]
		private StatusBar _statusBar;
		[AccessedThroughProperty("imageList")]
		private ImageList _imageList;
		[AccessedThroughProperty("statusTasks")]
		private StatusBarPanel _statusTasks;
		[AccessedThroughProperty("tabControl")]
		private TabControl _tabControl;
		[AccessedThroughProperty("statusErrors")]
		private StatusBarPanel _statusErrors;
		[AccessedThroughProperty("statusMessage")]
		private StatusBarPanel _statusMessage;
		[AccessedThroughProperty("pageTasks")]
		private TabPage _pageTasks;
		[AccessedThroughProperty("progressBar")]
		private ProgressBar _progressBar;
		[AccessedThroughProperty("listTasks")]
		private ListView _listTasks;
		[AccessedThroughProperty("pageErrors")]
		private TabPage _pageErrors;
		[AccessedThroughProperty("listErrors")]
		private ListView _listErrors;
		[AccessedThroughProperty("buttonCancel")]
		private Button _buttonCancel;
		[AccessedThroughProperty("colMessage")]
		private ColumnHeader _colMessage;
		[AccessedThroughProperty("labelMessage")]
		private Label _labelMessage;
		[AccessedThroughProperty("textError")]
		private TextBox _textError;
		private UploadForm.PublishCompleteEventHandler PublishCompleteEvent;
		private UploadForm.UploadCompleteEventHandler UploadCompleteEvent;
		[AccessedThroughProperty("checkCloseComplete")]
		private CheckBox _checkCloseComplete;
		[AccessedThroughProperty("colErrors")]
		private ColumnHeader _colErrors;
		[AccessedThroughProperty("buttonDetails")]
		private Button _buttonDetails;
		[AccessedThroughProperty("buttonClose")]
		private Button _buttonClose;
		private Point _initialLocation;
		private bool _expand;
		private int _heightDetails;
		private int _heightNoDetails;
		private int _taskCount;
		private int _errorCount;
		private Thread _thread;
		private IContainer components;
		public event UploadForm.PublishCompleteEventHandler PublishComplete
		{
			[MethodImpl(32)]
			add
			{
				this.PublishCompleteEvent = (UploadForm.PublishCompleteEventHandler)Delegate.Combine(this.PublishCompleteEvent, value);
			}
			[MethodImpl(32)]
			remove
			{
				this.PublishCompleteEvent = (UploadForm.PublishCompleteEventHandler)Delegate.Remove(this.PublishCompleteEvent, value);
			}
		}
		public event UploadForm.UploadCompleteEventHandler UploadComplete
		{
			[MethodImpl(32)]
			add
			{
				this.UploadCompleteEvent = (UploadForm.UploadCompleteEventHandler)Delegate.Combine(this.UploadCompleteEvent, value);
			}
			[MethodImpl(32)]
			remove
			{
				this.UploadCompleteEvent = (UploadForm.UploadCompleteEventHandler)Delegate.Remove(this.UploadCompleteEvent, value);
			}
		}
		private virtual Publish _publish
		{
			get
			{
				return this.__publish;
			}
			[MethodImpl(32)]
			set
			{
				if (this.__publish != null)
				{
					this.__publish.Complete -= new Publish.CompleteEventHandler(this.common_Complete);
					this.__publish.UpdateMessage -= new Publish.UpdateMessageEventHandler(this.common_UpdateMessage);
					this.__publish.UpdateProgress -= new Publish.UpdateProgressEventHandler(this.common_UpdateProgress);
				}
				this.__publish = value;
				if (this.__publish != null)
				{
					this.__publish.Complete += new Publish.CompleteEventHandler(this.common_Complete);
					this.__publish.UpdateMessage += new Publish.UpdateMessageEventHandler(this.common_UpdateMessage);
					this.__publish.UpdateProgress += new Publish.UpdateProgressEventHandler(this.common_UpdateProgress);
				}
			}
		}
		private virtual Upload _upload
		{
			get
			{
				return this.__upload;
			}
			[MethodImpl(32)]
			set
			{
				if (this.__upload != null)
				{
					this.__upload.Complete -= new Upload.CompleteEventHandler(this.common_Complete);
					this.__upload.UpdateMessage -= new Upload.UpdateMessageEventHandler(this.common_UpdateMessage);
					this.__upload.UpdateProgress -= new Upload.UpdateProgressEventHandler(this.common_UpdateProgress);
				}
				this.__upload = value;
				if (this.__upload != null)
				{
					this.__upload.Complete += new Upload.CompleteEventHandler(this.common_Complete);
					this.__upload.UpdateMessage += new Upload.UpdateMessageEventHandler(this.common_UpdateMessage);
					this.__upload.UpdateProgress += new Upload.UpdateProgressEventHandler(this.common_UpdateProgress);
				}
			}
		}
		public Point InitialLocation
		{
			get
			{
				return this._initialLocation;
			}
			set
			{
				this._initialLocation = value;
			}
		}
		private virtual StatusBar statusBar
		{
			get
			{
				return this._statusBar;
			}
			[MethodImpl(32)]
			set
			{
				if (this._statusBar != null)
				{
				}
				this._statusBar = value;
				if (this._statusBar != null)
				{
				}
			}
		}
		private virtual StatusBarPanel statusTasks
		{
			get
			{
				return this._statusTasks;
			}
			[MethodImpl(32)]
			set
			{
				if (this._statusTasks != null)
				{
				}
				this._statusTasks = value;
				if (this._statusTasks != null)
				{
				}
			}
		}
		private virtual StatusBarPanel statusErrors
		{
			get
			{
				return this._statusErrors;
			}
			[MethodImpl(32)]
			set
			{
				if (this._statusErrors != null)
				{
				}
				this._statusErrors = value;
				if (this._statusErrors != null)
				{
				}
			}
		}
		private virtual StatusBarPanel statusMessage
		{
			get
			{
				return this._statusMessage;
			}
			[MethodImpl(32)]
			set
			{
				if (this._statusMessage != null)
				{
				}
				this._statusMessage = value;
				if (this._statusMessage != null)
				{
				}
			}
		}
		private virtual ImageList imageList
		{
			get
			{
				return this._imageList;
			}
			[MethodImpl(32)]
			set
			{
				if (this._imageList != null)
				{
				}
				this._imageList = value;
				if (this._imageList != null)
				{
				}
			}
		}
		private virtual ProgressBar progressBar
		{
			get
			{
				return this._progressBar;
			}
			[MethodImpl(32)]
			set
			{
				if (this._progressBar != null)
				{
				}
				this._progressBar = value;
				if (this._progressBar != null)
				{
				}
			}
		}
		private virtual TabControl tabControl
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
		private virtual TabPage pageTasks
		{
			get
			{
				return this._pageTasks;
			}
			[MethodImpl(32)]
			set
			{
				if (this._pageTasks != null)
				{
				}
				this._pageTasks = value;
				if (this._pageTasks != null)
				{
				}
			}
		}
		private virtual ListView listTasks
		{
			get
			{
				return this._listTasks;
			}
			[MethodImpl(32)]
			set
			{
				if (this._listTasks != null)
				{
					this._listTasks.remove_Resize(new EventHandler(this.listTasks_Resize));
				}
				this._listTasks = value;
				if (this._listTasks != null)
				{
					this._listTasks.add_Resize(new EventHandler(this.listTasks_Resize));
				}
			}
		}
		private virtual TabPage pageErrors
		{
			get
			{
				return this._pageErrors;
			}
			[MethodImpl(32)]
			set
			{
				if (this._pageErrors != null)
				{
				}
				this._pageErrors = value;
				if (this._pageErrors != null)
				{
				}
			}
		}
		private virtual ListView listErrors
		{
			get
			{
				return this._listErrors;
			}
			[MethodImpl(32)]
			set
			{
				if (this._listErrors != null)
				{
					this._listErrors.remove_SelectedIndexChanged(new EventHandler(this.listErrors_SelectedIndexChanged));
					this._listErrors.remove_Resize(new EventHandler(this.listErrors_Resize));
				}
				this._listErrors = value;
				if (this._listErrors != null)
				{
					this._listErrors.add_SelectedIndexChanged(new EventHandler(this.listErrors_SelectedIndexChanged));
					this._listErrors.add_Resize(new EventHandler(this.listErrors_Resize));
				}
			}
		}
		private virtual ColumnHeader colMessage
		{
			get
			{
				return this._colMessage;
			}
			[MethodImpl(32)]
			set
			{
				if (this._colMessage != null)
				{
				}
				this._colMessage = value;
				if (this._colMessage != null)
				{
				}
			}
		}
		private virtual Label labelMessage
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
		private virtual TextBox textError
		{
			get
			{
				return this._textError;
			}
			[MethodImpl(32)]
			set
			{
				if (this._textError != null)
				{
				}
				this._textError = value;
				if (this._textError != null)
				{
				}
			}
		}
		private virtual CheckBox checkCloseComplete
		{
			get
			{
				return this._checkCloseComplete;
			}
			[MethodImpl(32)]
			set
			{
				if (this._checkCloseComplete != null)
				{
					this._checkCloseComplete.remove_CheckedChanged(new EventHandler(this.checkCloseComplete_CheckedChanged));
				}
				this._checkCloseComplete = value;
				if (this._checkCloseComplete != null)
				{
					this._checkCloseComplete.add_CheckedChanged(new EventHandler(this.checkCloseComplete_CheckedChanged));
				}
			}
		}
		private virtual ColumnHeader colErrors
		{
			get
			{
				return this._colErrors;
			}
			[MethodImpl(32)]
			set
			{
				if (this._colErrors != null)
				{
				}
				this._colErrors = value;
				if (this._colErrors != null)
				{
				}
			}
		}
		private virtual Button buttonDetails
		{
			get
			{
				return this._buttonDetails;
			}
			[MethodImpl(32)]
			set
			{
				if (this._buttonDetails != null)
				{
					this._buttonDetails.remove_Click(new EventHandler(this.buttonDetails_Click));
				}
				this._buttonDetails = value;
				if (this._buttonDetails != null)
				{
					this._buttonDetails.add_Click(new EventHandler(this.buttonDetails_Click));
				}
			}
		}
		private virtual Button buttonCancel
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
					this._buttonCancel.remove_Click(new EventHandler(this.buttonCancel_Click));
				}
				this._buttonCancel = value;
				if (this._buttonCancel != null)
				{
					this._buttonCancel.add_Click(new EventHandler(this.buttonCancel_Click));
				}
			}
		}
		private virtual Button buttonClose
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
		public UploadForm()
		{
			this._expand = true;
			this._publish = new Publish();
			this._upload = new Upload();
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
			this.components = new Container();
			ResourceManager resourceManager = new ResourceManager(typeof(UploadForm));
			this.statusBar = new StatusBar();
			this.statusTasks = new StatusBarPanel();
			this.statusErrors = new StatusBarPanel();
			this.statusMessage = new StatusBarPanel();
			this.colErrors = new ColumnHeader();
			this.imageList = new ImageList(this.components);
			this.buttonDetails = new Button();
			this.buttonCancel = new Button();
			this.progressBar = new ProgressBar();
			this.tabControl = new TabControl();
			this.pageTasks = new TabPage();
			this.listTasks = new ListView();
			this.colMessage = new ColumnHeader();
			this.pageErrors = new TabPage();
			this.textError = new TextBox();
			this.listErrors = new ListView();
			this.labelMessage = new Label();
			this.checkCloseComplete = new CheckBox();
			this.buttonClose = new Button();
			this.statusTasks.BeginInit();
			this.statusErrors.BeginInit();
			this.statusMessage.BeginInit();
			this.tabControl.SuspendLayout();
			this.pageTasks.SuspendLayout();
			this.pageErrors.SuspendLayout();
			this.SuspendLayout();
			Control arg_14F_0 = this.statusBar;
			Point location = new Point(0, 248);
			arg_14F_0.set_Location(location);
			this.statusBar.set_Name("statusBar");
			this.statusBar.get_Panels().AddRange(new StatusBarPanel[]
			{
				this.statusTasks,
				this.statusErrors,
				this.statusMessage
			});
			this.statusBar.set_ShowPanels(true);
			Control arg_1B8_0 = this.statusBar;
			Size size = new Size(472, 22);
			arg_1B8_0.set_Size(size);
			this.statusBar.set_TabIndex(6);
			this.statusTasks.set_Width(75);
			this.statusErrors.set_Width(75);
			this.statusMessage.set_AutoSize(2);
			this.statusMessage.set_Width(306);
			this.colErrors.set_Width(404);
			ImageList arg_221_0 = this.imageList;
			size = new Size(22, 18);
			arg_221_0.set_ImageSize(size);
			this.imageList.set_ImageStream((ImageListStreamer)resourceManager.GetObject("imageList.ImageStream"));
			this.imageList.set_TransparentColor(Color.get_Lime());
			this.buttonDetails.set_Anchor(9);
			this.buttonDetails.set_FlatStyle(3);
			Control arg_280_0 = this.buttonDetails;
			location = new Point(388, 40);
			arg_280_0.set_Location(location);
			this.buttonDetails.set_Name("buttonDetails");
			this.buttonDetails.set_TabIndex(4);
			this.buttonDetails.set_Text("<< Details");
			this.buttonCancel.set_Anchor(9);
			this.buttonCancel.set_FlatStyle(3);
			Control arg_2DF_0 = this.buttonCancel;
			location = new Point(388, 8);
			arg_2DF_0.set_Location(location);
			this.buttonCancel.set_Name("buttonCancel");
			this.buttonCancel.set_TabIndex(3);
			this.buttonCancel.set_Text("Cancel");
			this.progressBar.set_Anchor(13);
			Control arg_32F_0 = this.progressBar;
			location = new Point(8, 28);
			arg_32F_0.set_Location(location);
			this.progressBar.set_Name("progressBar");
			Control arg_359_0 = this.progressBar;
			size = new Size(362, 14);
			arg_359_0.set_Size(size);
			this.progressBar.set_TabIndex(1);
			this.tabControl.set_Anchor(15);
			this.tabControl.get_Controls().Add(this.pageTasks);
			this.tabControl.get_Controls().Add(this.pageErrors);
			Control arg_3B5_0 = this.tabControl;
			location = new Point(0, 80);
			arg_3B5_0.set_Location(location);
			this.tabControl.set_Name("tabControl");
			this.tabControl.set_SelectedIndex(0);
			Control arg_3EE_0 = this.tabControl;
			size = new Size(472, 168);
			arg_3EE_0.set_Size(size);
			this.tabControl.set_TabIndex(5);
			this.pageTasks.get_Controls().Add(this.listTasks);
			Control arg_427_0 = this.pageTasks;
			location = new Point(4, 22);
			arg_427_0.set_Location(location);
			this.pageTasks.set_Name("pageTasks");
			Control arg_454_0 = this.pageTasks;
			size = new Size(464, 142);
			arg_454_0.set_Size(size);
			this.pageTasks.set_TabIndex(0);
			this.pageTasks.set_Text("Tasks");
			this.listTasks.get_Columns().AddRange(new ColumnHeader[]
			{
				this.colMessage
			});
			this.listTasks.set_Dock(5);
			this.listTasks.set_HeaderStyle(0);
			Control arg_4BF_0 = this.listTasks;
			location = new Point(0, 0);
			arg_4BF_0.set_Location(location);
			this.listTasks.set_Name("listTasks");
			Control arg_4EC_0 = this.listTasks;
			size = new Size(464, 142);
			arg_4EC_0.set_Size(size);
			this.listTasks.set_SmallImageList(this.imageList);
			this.listTasks.set_TabIndex(0);
			this.listTasks.set_View(1);
			this.colMessage.set_Width(404);
			this.pageErrors.get_Controls().Add(this.textError);
			this.pageErrors.get_Controls().Add(this.listErrors);
			Control arg_568_0 = this.pageErrors;
			location = new Point(4, 22);
			arg_568_0.set_Location(location);
			this.pageErrors.set_Name("pageErrors");
			Control arg_595_0 = this.pageErrors;
			size = new Size(464, 142);
			arg_595_0.set_Size(size);
			this.pageErrors.set_TabIndex(1);
			this.pageErrors.set_Text("Errors");
			this.pageErrors.set_Visible(false);
			this.textError.set_AcceptsReturn(true);
			this.textError.set_BackColor(SystemColors.get_Info());
			this.textError.set_Dock(2);
			Control arg_5FC_0 = this.textError;
			location = new Point(0, 110);
			arg_5FC_0.set_Location(location);
			this.textError.set_Multiline(true);
			this.textError.set_Name("textError");
			this.textError.set_ReadOnly(true);
			this.textError.set_ScrollBars(2);
			Control arg_64A_0 = this.textError;
			size = new Size(464, 32);
			arg_64A_0.set_Size(size);
			this.textError.set_TabIndex(2);
			this.textError.set_Text("");
			this.listErrors.get_Columns().AddRange(new ColumnHeader[]
			{
				this.colErrors
			});
			this.listErrors.set_Dock(5);
			this.listErrors.set_HeaderStyle(0);
			Control arg_6B5_0 = this.listErrors;
			location = new Point(0, 0);
			arg_6B5_0.set_Location(location);
			this.listErrors.set_Name("listErrors");
			Control arg_6E2_0 = this.listErrors;
			size = new Size(464, 142);
			arg_6E2_0.set_Size(size);
			this.listErrors.set_SmallImageList(this.imageList);
			this.listErrors.set_TabIndex(1);
			this.listErrors.set_View(1);
			this.labelMessage.set_FlatStyle(3);
			Control arg_72D_0 = this.labelMessage;
			location = new Point(8, 8);
			arg_72D_0.set_Location(location);
			this.labelMessage.set_Name("labelMessage");
			Control arg_757_0 = this.labelMessage;
			size = new Size(352, 16);
			arg_757_0.set_Size(size);
			this.labelMessage.set_TabIndex(0);
			this.labelMessage.set_Text("< header message >");
			this.checkCloseComplete.set_FlatStyle(3);
			Control arg_796_0 = this.checkCloseComplete;
			location = new Point(8, 44);
			arg_796_0.set_Location(location);
			this.checkCloseComplete.set_Name("checkCloseComplete");
			Control arg_7C0_0 = this.checkCloseComplete;
			size = new Size(240, 24);
			arg_7C0_0.set_Size(size);
			this.checkCloseComplete.set_TabIndex(2);
			this.checkCloseComplete.set_Text("&Close window after uploading is complete");
			this.buttonClose.set_Anchor(9);
			this.buttonClose.set_FlatStyle(3);
			Control arg_80F_0 = this.buttonClose;
			location = new Point(388, 8);
			arg_80F_0.set_Location(location);
			this.buttonClose.set_Name("buttonClose");
			this.buttonClose.set_TabIndex(3);
			this.buttonClose.set_Text("Close");
			size = new Size(5, 13);
			this.set_AutoScaleBaseSize(size);
			size = new Size(472, 270);
			this.set_ClientSize(size);
			this.get_Controls().Add(this.buttonClose);
			this.get_Controls().Add(this.checkCloseComplete);
			this.get_Controls().Add(this.buttonDetails);
			this.get_Controls().Add(this.buttonCancel);
			this.get_Controls().Add(this.progressBar);
			this.get_Controls().Add(this.tabControl);
			this.get_Controls().Add(this.labelMessage);
			this.get_Controls().Add(this.statusBar);
			this.set_FormBorderStyle(6);
			this.set_Icon((Icon)resourceManager.GetObject("$this.Icon"));
			this.set_MaximizeBox(false);
			this.set_MinimizeBox(false);
			this.set_Name("UploadForm");
			this.set_Text("Upload Changes");
			this.statusTasks.EndInit();
			this.statusErrors.EndInit();
			this.statusMessage.EndInit();
			this.tabControl.ResumeLayout(false);
			this.pageTasks.ResumeLayout(false);
			this.pageErrors.ResumeLayout(false);
			this.ResumeLayout(false);
		}
		public void Publish(string[] albumList)
		{
			this._publish.SetAlbumList(albumList);
			this._taskCount = 0;
			this._errorCount = 0;
			this.statusTasks.set_Text("0 tasks");
			this.statusErrors.set_Text("0 errors");
			this._progressBar.set_Minimum(0);
			this._progressBar.set_Maximum(0);
			this.tabControl.set_SelectedIndex(0);
			this.listTasks.get_Items().Clear();
			this.listErrors.get_Items().Clear();
			this.textError.set_Visible(false);
			this.textError.set_Text("");
			this.buttonCancel.set_Enabled(true);
			this.buttonClose.set_Visible(false);
			this.checkCloseComplete.set_Checked(Global.Settings.GetBool(SettingKey.CloseAfterUpload));
			this.labelMessage.set_Text("Preparing local files to be published");
			this.Show();
			this.BringToFront();
			this.Update();
			this._thread = new Thread(new ThreadStart(this._publish.CreatePublishFiles));
			this._thread.Start();
		}
		public void Upload()
		{
			this._upload.Parent = this;
			this._progressBar.set_Minimum(0);
			this._progressBar.set_Maximum(0);
			this.buttonCancel.set_Enabled(true);
			this.buttonClose.set_Visible(false);
			this.labelMessage.set_Text("Uploading files to the website");
			this.Update();
			this._thread = new Thread(new ThreadStart(this._upload.Synchronize));
			this._thread.Start();
		}
		public void Abort()
		{
			if (this._thread != null)
			{
				this._thread.Abort();
			}
		}
		protected override void OnLoad(EventArgs e)
		{
			base.OnLoad(e);
			this._heightDetails = this.get_Height();
			this._heightNoDetails = checked(this.get_Height() - this.tabControl.get_Height());
			Size minimumSize = new Size(350, this._heightNoDetails);
			this.set_MinimumSize(minimumSize);
			this.set_Location(this._initialLocation);
			this.checkCloseComplete.set_Checked(Global.Settings.GetBool(SettingKey.CloseAfterUpload));
			this.ExpandChanged(Global.Settings.GetBool(SettingKey.ShowStatusDetails));
		}
		protected override void OnClosing(CancelEventArgs e)
		{
			base.OnClosing(e);
			e.set_Cancel(true);
			if (this.buttonCancel.get_Enabled())
			{
				this.buttonCancel_Click(this, EventArgs.Empty);
			}
			else
			{
				this.Hide();
			}
		}
		private void buttonCancel_Click(object sender, EventArgs e)
		{
			this.buttonCancel.set_Enabled(false);
			this.set_Cursor(Cursors.get_WaitCursor());
			this._thread.Abort();
			this._thread.Join();
			this.set_Cursor(Cursors.get_Default());
			this.UpdateMessage("Operation was canceled", false, true);
			this.statusMessage.set_Text("Operation was canceled");
			this.labelMessage.set_Text("Uploading was canceled");
			this.buttonClose.set_Visible(true);
			this.tabControl.set_SelectedIndex(1);
			if (this.PublishCompleteEvent != null)
			{
				this.PublishCompleteEvent(this, new UploadCompleteEventArgs(true));
			}
			if (this.UploadCompleteEvent != null)
			{
				this.UploadCompleteEvent(this, new UploadCompleteEventArgs(true));
			}
		}
		private void buttonClose_Click(object sender, EventArgs e)
		{
			this.Close();
		}
		private void buttonDetails_Click(object sender, EventArgs e)
		{
			Global.Settings.SetValue(SettingKey.ShowStatusDetails, !this._expand);
			this.ExpandChanged(!this._expand);
		}
		private void ExpandChanged(bool expand)
		{
			this._expand = expand;
			if (this._expand)
			{
				this.buttonDetails.set_Text("<< Details");
				this.set_Height(this._heightDetails);
				this.tabControl.set_Visible(true);
			}
			else
			{
				this._heightDetails = this.get_Height();
				this.buttonDetails.set_Text("Details >>");
				this.set_Height(this._heightNoDetails);
				this.tabControl.set_Visible(false);
			}
		}
		private void listTasks_Resize(object sender, EventArgs e)
		{
			int num = this.listTasks.get_DisplayRectangle().get_Width();
			checked
			{
				if (this.listTasks.get_Width() > this.listTasks.get_DisplayRectangle().get_Width() + SystemInformation.get_Border3DSize().get_Width() * 2)
				{
					num -= SystemInformation.get_VerticalScrollBarWidth();
				}
				this.listTasks.get_Columns().get_Item(0).set_Width(num);
			}
		}
		private void listErrors_Resize(object sender, EventArgs e)
		{
			int num = this.listErrors.get_DisplayRectangle().get_Width();
			checked
			{
				if (this.listErrors.get_Width() > this.listErrors.get_DisplayRectangle().get_Width() + SystemInformation.get_Border3DSize().get_Width() * 2)
				{
					num -= SystemInformation.get_VerticalScrollBarWidth();
				}
				this.listErrors.get_Columns().get_Item(0).set_Width(num);
			}
		}
		private void listErrors_SelectedIndexChanged(object sender, EventArgs e)
		{
			if (this.listErrors.get_SelectedItems() != null && this.listErrors.get_SelectedItems().get_Count() > 0)
			{
				this.textError.set_Text(this.listErrors.get_SelectedItems().get_Item(0).get_Text());
			}
			else
			{
				this.textError.set_Text("");
			}
		}
		private void checkCloseComplete_CheckedChanged(object sender, EventArgs e)
		{
			Global.Settings.SetValue(SettingKey.CloseAfterUpload, this.checkCloseComplete.get_Checked());
		}
		private void common_UpdateProgress(object sender, UploadProgressEventArgs e)
		{
			this.BeginInvoke(new UploadForm.LocalUpdateProgress(this.UpdateProgress), new object[]
			{
				e.Position,
				e.Total
			});
		}
		private void common_UpdateMessage(object sender, UploadMessageEventArgs e)
		{
			this.BeginInvoke(new UploadForm.LocalUpdateMessage(this.UpdateMessage), new object[]
			{
				e.Message,
				e.Success,
				e.Log
			});
		}
		private void common_Complete(object sender, UploadCompleteEventArgs e)
		{
			this.BeginInvoke(new UploadForm.LocalComplete(this.Complete), new object[]
			{
				e.ErrorOccurred,
				RuntimeHelpers.GetObjectValue(Interaction.IIf(sender == this._publish, true, false))
			});
		}
		private void UpdateProgress(int pos, int total)
		{
			this.progressBar.set_Maximum(total);
			this.progressBar.set_Value(pos);
		}
		private void UpdateMessage(string message, bool success, bool log)
		{
			this.statusMessage.set_Text(message.Trim() + "...");
			if (!log)
			{
				return;
			}
			checked
			{
				if (success)
				{
					ListViewItem listViewItem = this.listTasks.get_Items().Add(message, 1);
					listViewItem.EnsureVisible();
					this.listTasks.Update();
					this._taskCount++;
					this.statusTasks.set_Text(string.Format("{0} task{1}", this._taskCount, RuntimeHelpers.GetObjectValue(Interaction.IIf(this._taskCount == 1, "", "s"))));
				}
				else
				{
					string text = message.Replace('\r', ' ');
					text = text.Replace('\n', ' ');
					ListViewItem listViewItem = this.listErrors.get_Items().Add(text, 0);
					listViewItem.EnsureVisible();
					this.listErrors.Update();
					this.textError.set_Visible(true);
					this._errorCount++;
					this.statusErrors.set_Text(string.Format("{0} error{1}", this._errorCount, RuntimeHelpers.GetObjectValue(Interaction.IIf(this._errorCount == 1, "", "s"))));
				}
			}
		}
		private void Complete(bool errorOccurred, bool publishOperation)
		{
			this.buttonCancel.set_Enabled(false);
			this.buttonClose.set_Visible(true);
			this.statusMessage.set_Text(Interaction.IIf(errorOccurred, "Operation was canceled", "Complete").ToString());
			if (this._errorCount > 0)
			{
				this.tabControl.set_SelectedIndex(1);
			}
			if (publishOperation)
			{
				if (this.PublishCompleteEvent != null)
				{
					this.PublishCompleteEvent(this, new UploadCompleteEventArgs(errorOccurred));
				}
			}
			else
			{
				this.labelMessage.set_Text("Uploading is complete");
				if (!errorOccurred & this.checkCloseComplete.get_Checked())
				{
					this.Hide();
				}
				if (this.UploadCompleteEvent != null)
				{
					this.UploadCompleteEvent(this, new UploadCompleteEventArgs(errorOccurred));
				}
			}
		}
	}
}
