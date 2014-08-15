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
		private Publish _publish
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
		private Upload _upload
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
		private StatusBar statusBar
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
		private StatusBarPanel statusTasks
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
		private StatusBarPanel statusErrors
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
		private StatusBarPanel statusMessage
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
		private ImageList imageList
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
		private ProgressBar progressBar
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
		private TabPage pageTasks
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
		private ListView listTasks
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
		private TabPage pageErrors
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
		private ListView listErrors
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
		private ColumnHeader colMessage
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
		private TextBox textError
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
		private CheckBox checkCloseComplete
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
		private ColumnHeader colErrors
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
		private Button buttonDetails
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
					this._buttonCancel.remove_Click(new EventHandler(this.buttonCancel_Click));
				}
				this._buttonCancel = value;
				if (this._buttonCancel != null)
				{
					this._buttonCancel.add_Click(new EventHandler(this.buttonCancel_Click));
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
			arg_14F_0.Location = location;
			this.statusBar.Name = "statusBar";
			this.statusBar.Panels.AddRange(new StatusBarPanel[]
			{
				this.statusTasks,
				this.statusErrors,
				this.statusMessage
			});
			this.statusBar.ShowPanels = true;
			Control arg_1B8_0 = this.statusBar;
			Size size = new Size(472, 22);
			arg_1B8_0.Size = size;
			this.statusBar.TabIndex = 6;
			this.statusTasks.Width = 75;
			this.statusErrors.Width = 75;
			this.statusMessage.AutoSize = 2;
			this.statusMessage.Width = 306;
			this.colErrors.Width = 404;
			ImageList arg_221_0 = this.imageList;
			size = new Size(22, 18);
			arg_221_0.ImageSize = size;
			this.imageList.ImageStream = (ImageListStreamer)resourceManager.GetObject("imageList.ImageStream");
			this.imageList.TransparentColor = Color.Lime;
			this.buttonDetails.Anchor = 9;
			this.buttonDetails.FlatStyle = 3;
			Control arg_280_0 = this.buttonDetails;
			location = new Point(388, 40);
			arg_280_0.Location = location;
			this.buttonDetails.Name = "buttonDetails";
			this.buttonDetails.TabIndex = 4;
			this.buttonDetails.Text = "<< Details";
			this.buttonCancel.Anchor = 9;
			this.buttonCancel.FlatStyle = 3;
			Control arg_2DF_0 = this.buttonCancel;
			location = new Point(388, 8);
			arg_2DF_0.Location = location;
			this.buttonCancel.Name = "buttonCancel";
			this.buttonCancel.TabIndex = 3;
			this.buttonCancel.Text = "Cancel";
			this.progressBar.Anchor = 13;
			Control arg_32F_0 = this.progressBar;
			location = new Point(8, 28);
			arg_32F_0.Location = location;
			this.progressBar.Name = "progressBar";
			Control arg_359_0 = this.progressBar;
			size = new Size(362, 14);
			arg_359_0.Size = size;
			this.progressBar.TabIndex = 1;
			this.tabControl.Anchor = 15;
			this.tabControl.Controls.Add(this.pageTasks);
			this.tabControl.Controls.Add(this.pageErrors);
			Control arg_3B5_0 = this.tabControl;
			location = new Point(0, 80);
			arg_3B5_0.Location = location;
			this.tabControl.Name = "tabControl";
			this.tabControl.SelectedIndex = 0;
			Control arg_3EE_0 = this.tabControl;
			size = new Size(472, 168);
			arg_3EE_0.Size = size;
			this.tabControl.TabIndex = 5;
			this.pageTasks.Controls.Add(this.listTasks);
			Control arg_427_0 = this.pageTasks;
			location = new Point(4, 22);
			arg_427_0.Location = location;
			this.pageTasks.Name = "pageTasks";
			Control arg_454_0 = this.pageTasks;
			size = new Size(464, 142);
			arg_454_0.Size = size;
			this.pageTasks.TabIndex = 0;
			this.pageTasks.Text = "Tasks";
			this.listTasks.Columns.AddRange(new ColumnHeader[]
			{
				this.colMessage
			});
			this.listTasks.Dock = 5;
			this.listTasks.HeaderStyle = 0;
			Control arg_4BF_0 = this.listTasks;
			location = new Point(0, 0);
			arg_4BF_0.Location = location;
			this.listTasks.Name = "listTasks";
			Control arg_4EC_0 = this.listTasks;
			size = new Size(464, 142);
			arg_4EC_0.Size = size;
			this.listTasks.SmallImageList = this.imageList;
			this.listTasks.TabIndex = 0;
			this.listTasks.View = 1;
			this.colMessage.Width = 404;
			this.pageErrors.Controls.Add(this.textError);
			this.pageErrors.Controls.Add(this.listErrors);
			Control arg_568_0 = this.pageErrors;
			location = new Point(4, 22);
			arg_568_0.Location = location;
			this.pageErrors.Name = "pageErrors";
			Control arg_595_0 = this.pageErrors;
			size = new Size(464, 142);
			arg_595_0.Size = size;
			this.pageErrors.TabIndex = 1;
			this.pageErrors.Text = "Errors";
			this.pageErrors.Visible = false;
			this.textError.AcceptsReturn = true;
			this.textError.BackColor = SystemColors.Info;
			this.textError.Dock = 2;
			Control arg_5FC_0 = this.textError;
			location = new Point(0, 110);
			arg_5FC_0.Location = location;
			this.textError.Multiline = true;
			this.textError.Name = "textError";
			this.textError.ReadOnly = true;
			this.textError.ScrollBars = 2;
			Control arg_64A_0 = this.textError;
			size = new Size(464, 32);
			arg_64A_0.Size = size;
			this.textError.TabIndex = 2;
			this.textError.Text = "";
			this.listErrors.Columns.AddRange(new ColumnHeader[]
			{
				this.colErrors
			});
			this.listErrors.Dock = 5;
			this.listErrors.HeaderStyle = 0;
			Control arg_6B5_0 = this.listErrors;
			location = new Point(0, 0);
			arg_6B5_0.Location = location;
			this.listErrors.Name = "listErrors";
			Control arg_6E2_0 = this.listErrors;
			size = new Size(464, 142);
			arg_6E2_0.Size = size;
			this.listErrors.SmallImageList = this.imageList;
			this.listErrors.TabIndex = 1;
			this.listErrors.View = 1;
			this.labelMessage.FlatStyle = 3;
			Control arg_72D_0 = this.labelMessage;
			location = new Point(8, 8);
			arg_72D_0.Location = location;
			this.labelMessage.Name = "labelMessage";
			Control arg_757_0 = this.labelMessage;
			size = new Size(352, 16);
			arg_757_0.Size = size;
			this.labelMessage.TabIndex = 0;
			this.labelMessage.Text = "< header message >";
			this.checkCloseComplete.FlatStyle = 3;
			Control arg_796_0 = this.checkCloseComplete;
			location = new Point(8, 44);
			arg_796_0.Location = location;
			this.checkCloseComplete.Name = "checkCloseComplete";
			Control arg_7C0_0 = this.checkCloseComplete;
			size = new Size(240, 24);
			arg_7C0_0.Size = size;
			this.checkCloseComplete.TabIndex = 2;
			this.checkCloseComplete.Text = "&Close window after uploading is complete";
			this.buttonClose.Anchor = 9;
			this.buttonClose.FlatStyle = 3;
			Control arg_80F_0 = this.buttonClose;
			location = new Point(388, 8);
			arg_80F_0.Location = location;
			this.buttonClose.Name = "buttonClose";
			this.buttonClose.TabIndex = 3;
			this.buttonClose.Text = "Close";
			size = new Size(5, 13);
			this.AutoScaleBaseSize = size;
			size = new Size(472, 270);
			this.ClientSize = size;
			this.Controls.Add(this.buttonClose);
			this.Controls.Add(this.checkCloseComplete);
			this.Controls.Add(this.buttonDetails);
			this.Controls.Add(this.buttonCancel);
			this.Controls.Add(this.progressBar);
			this.Controls.Add(this.tabControl);
			this.Controls.Add(this.labelMessage);
			this.Controls.Add(this.statusBar);
			this.FormBorderStyle = 6;
			this.Icon = (Icon)resourceManager.GetObject("$this.Icon");
			this.MaximizeBox = false;
			this.MinimizeBox = false;
			this.Name = "UploadForm";
			this.Text = "Upload Changes";
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
			this.statusTasks.Text = "0 tasks";
			this.statusErrors.Text = "0 errors";
			this._progressBar.Minimum = 0;
			this._progressBar.Maximum = 0;
			this.tabControl.SelectedIndex = 0;
			this.listTasks.Items.Clear();
			this.listErrors.Items.Clear();
			this.textError.Visible = false;
			this.textError.Text = "";
			this.buttonCancel.Enabled = true;
			this.buttonClose.Visible = false;
			this.checkCloseComplete.Checked = Global.Settings.GetBool(SettingKey.CloseAfterUpload);
			this.labelMessage.Text = "Preparing local files to be published";
			this.Show();
			this.BringToFront();
			this.Update();
			this._thread = new Thread(new ThreadStart(this._publish.CreatePublishFiles));
			this._thread.Start();
		}
		public void Upload()
		{
			this._upload.Parent = this;
			this._progressBar.Minimum = 0;
			this._progressBar.Maximum = 0;
			this.buttonCancel.Enabled = true;
			this.buttonClose.Visible = false;
			this.labelMessage.Text = "Uploading files to the website";
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
			this._heightDetails = this.Height;
			this._heightNoDetails = checked(this.Height - this.tabControl.Height);
			Size minimumSize = new Size(350, this._heightNoDetails);
			this.MinimumSize = minimumSize;
			this.Location = this._initialLocation;
			this.checkCloseComplete.Checked = Global.Settings.GetBool(SettingKey.CloseAfterUpload);
			this.ExpandChanged(Global.Settings.GetBool(SettingKey.ShowStatusDetails));
		}
		protected override void OnClosing(CancelEventArgs e)
		{
			base.OnClosing(e);
			e.Cancel = true;
			if (this.buttonCancel.Enabled)
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
			this.buttonCancel.Enabled = false;
			this.Cursor = Cursors.WaitCursor;
			this._thread.Abort();
			this._thread.Join();
			this.Cursor = Cursors.Default;
			this.UpdateMessage("Operation was canceled", false, true);
			this.statusMessage.Text = "Operation was canceled";
			this.labelMessage.Text = "Uploading was canceled";
			this.buttonClose.Visible = true;
			this.tabControl.SelectedIndex = 1;
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
				this.buttonDetails.Text = "<< Details";
				this.Height = this._heightDetails;
				this.tabControl.Visible = true;
			}
			else
			{
				this._heightDetails = this.Height;
				this.buttonDetails.Text = "Details >>";
				this.Height = this._heightNoDetails;
				this.tabControl.Visible = false;
			}
		}
		private void listTasks_Resize(object sender, EventArgs e)
		{
			int num = this.listTasks.DisplayRectangle.Width;
			checked
			{
				if (this.listTasks.Width > this.listTasks.DisplayRectangle.Width + SystemInformation.Border3DSize.Width * 2)
				{
					num -= SystemInformation.VerticalScrollBarWidth;
				}
				this.listTasks.Columns.get_Item(0).Width = num;
			}
		}
		private void listErrors_Resize(object sender, EventArgs e)
		{
			int num = this.listErrors.DisplayRectangle.Width;
			checked
			{
				if (this.listErrors.Width > this.listErrors.DisplayRectangle.Width + SystemInformation.Border3DSize.Width * 2)
				{
					num -= SystemInformation.VerticalScrollBarWidth;
				}
				this.listErrors.Columns.get_Item(0).Width = num;
			}
		}
		private void listErrors_SelectedIndexChanged(object sender, EventArgs e)
		{
			if (this.listErrors.SelectedItems != null && this.listErrors.SelectedItems.Count > 0)
			{
				this.textError.Text = this.listErrors.SelectedItems.Item(0).get_Text;
			}
			else
			{
				this.textError.Text = "";
			}
		}
		private void checkCloseComplete_CheckedChanged(object sender, EventArgs e)
		{
			Global.Settings.SetValue(SettingKey.CloseAfterUpload, this.checkCloseComplete.Checked);
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
			this.progressBar.Maximum = total;
			this.progressBar.Value = pos;
		}
		private void UpdateMessage(string message, bool success, bool log)
		{
			this.statusMessage.Text = message.Trim() + "...";
			if (!log)
			{
				return;
			}
			checked
			{
				if (success)
				{
					ListViewItem listViewItem = this.listTasks.Items.Add(message, 1);
					listViewItem.EnsureVisible();
					this.listTasks.Update();
					this._taskCount++;
					this.statusTasks.Text = string.Format("{0} task{1}", this._taskCount, RuntimeHelpers.GetObjectValue(Interaction.IIf(this._taskCount == 1, "", "s")));
				}
				else
				{
					string text = message.Replace('\r', ' ');
					text = text.Replace('\n', ' ');
					ListViewItem listViewItem = this.listErrors.Items.Add(text, 0);
					listViewItem.EnsureVisible();
					this.listErrors.Update();
					this.textError.Visible = true;
					this._errorCount++;
					this.statusErrors.Text = string.Format("{0} error{1}", this._errorCount, RuntimeHelpers.GetObjectValue(Interaction.IIf(this._errorCount == 1, "", "s")));
				}
			}
		}
		private void Complete(bool errorOccurred, bool publishOperation)
		{
			this.buttonCancel.Enabled = false;
			this.buttonClose.Visible = true;
			this.statusMessage.Text = Interaction.IIf(errorOccurred, "Operation was canceled", "Complete").ToString();
			if (this._errorCount > 0)
			{
				this.tabControl.SelectedIndex = 1;
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
				this.labelMessage.Text = "Uploading is complete";
				if (!errorOccurred & this.checkCloseComplete.Checked)
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
