using Microsoft.VisualBasic;
using Microsoft.VisualBasic.CompilerServices;
using System;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;
using System.Resources;
using System.Runtime.CompilerServices;
using System.Windows.Forms;
namespace FotoVision
{
	public class DetailsAlbum : UserControl
	{
		public delegate void AlbumMetadataChangedEventHandler(object sender, AlbumMetadataChangedEventArgs e);
		[AccessedThroughProperty("labelPublishHeader")]
		private Label _labelPublishHeader;
		[AccessedThroughProperty("radioPublish")]
		private RadioButton _radioPublish;
		[AccessedThroughProperty("radioDontPublish")]
		private RadioButton _radioDontPublish;
		[AccessedThroughProperty("textDate")]
		private TextBox _textDate;
		[AccessedThroughProperty("labelLocationHeader")]
		private Label _labelLocationHeader;
		[AccessedThroughProperty("textDesc")]
		private TextBox _textDesc;
		[AccessedThroughProperty("textLocation")]
		private TextBox _textLocation;
		[AccessedThroughProperty("textTitle")]
		private TextBox _textTitle;
		[AccessedThroughProperty("labelAlbumTitle")]
		private Label _labelAlbumTitle;
		[AccessedThroughProperty("labelWebsiteHeader")]
		private Label _labelWebsiteHeader;
		private DetailsAlbum.AlbumMetadataChangedEventHandler AlbumMetadataChangedEvent;
		[AccessedThroughProperty("imageList")]
		private ImageList _imageList;
		[AccessedThroughProperty("labelDate")]
		private Label _labelDate;
		private string _albumName;
		private Album _album;
		private bool _dirty;
		private IContainer components;
		public event DetailsAlbum.AlbumMetadataChangedEventHandler AlbumMetadataChanged
		{
			[MethodImpl(32)]
			add
			{
				this.AlbumMetadataChangedEvent = (DetailsAlbum.AlbumMetadataChangedEventHandler)Delegate.Combine(this.AlbumMetadataChangedEvent, value);
			}
			[MethodImpl(32)]
			remove
			{
				this.AlbumMetadataChangedEvent = (DetailsAlbum.AlbumMetadataChangedEventHandler)Delegate.Remove(this.AlbumMetadataChangedEvent, value);
			}
		}
		[Browsable(false)]
		public string AlbumName
		{
			get
			{
				return this._albumName;
			}
			set
			{
				this.CheckUpdate();
				this._albumName = value;
				if (StringType.StrCmp(this._albumName, "", false) == 0)
				{
					this.Enabled = false;
					this._album.Clear();
				}
				else
				{
					this.Enabled = true;
					this._album.ReadXml(this._albumName);
				}
				this.UpdateFields();
			}
		}
		private TextBox textTitle
		{
			get
			{
				return this._textTitle;
			}
			[MethodImpl(32)]
			set
			{
				if (this._textTitle != null)
				{
					this._textTitle.remove_TextChanged(new EventHandler(this.textControl_TextChanged));
					this._textTitle.remove_Leave(new EventHandler(this.textControl_Leave));
				}
				this._textTitle = value;
				if (this._textTitle != null)
				{
					this._textTitle.add_TextChanged(new EventHandler(this.textControl_TextChanged));
					this._textTitle.add_Leave(new EventHandler(this.textControl_Leave));
				}
			}
		}
		private TextBox textDesc
		{
			get
			{
				return this._textDesc;
			}
			[MethodImpl(32)]
			set
			{
				if (this._textDesc != null)
				{
					this._textDesc.remove_TextChanged(new EventHandler(this.textControl_TextChanged));
					this._textDesc.remove_Leave(new EventHandler(this.textControl_Leave));
				}
				this._textDesc = value;
				if (this._textDesc != null)
				{
					this._textDesc.add_TextChanged(new EventHandler(this.textControl_TextChanged));
					this._textDesc.add_Leave(new EventHandler(this.textControl_Leave));
				}
			}
		}
		private TextBox textDate
		{
			get
			{
				return this._textDate;
			}
			[MethodImpl(32)]
			set
			{
				if (this._textDate != null)
				{
					this._textDate.remove_TextChanged(new EventHandler(this.textControl_TextChanged));
					this._textDate.remove_Leave(new EventHandler(this.textControl_Leave));
				}
				this._textDate = value;
				if (this._textDate != null)
				{
					this._textDate.add_TextChanged(new EventHandler(this.textControl_TextChanged));
					this._textDate.add_Leave(new EventHandler(this.textControl_Leave));
				}
			}
		}
		private RadioButton radioPublish
		{
			get
			{
				return this._radioPublish;
			}
			[MethodImpl(32)]
			set
			{
				if (this._radioPublish != null)
				{
					this._radioPublish.remove_Click(new EventHandler(this.radioControl_Click));
				}
				this._radioPublish = value;
				if (this._radioPublish != null)
				{
					this._radioPublish.add_Click(new EventHandler(this.radioControl_Click));
				}
			}
		}
		private RadioButton radioDontPublish
		{
			get
			{
				return this._radioDontPublish;
			}
			[MethodImpl(32)]
			set
			{
				if (this._radioDontPublish != null)
				{
					this._radioDontPublish.remove_Click(new EventHandler(this.radioControl_Click));
				}
				this._radioDontPublish = value;
				if (this._radioDontPublish != null)
				{
					this._radioDontPublish.add_Click(new EventHandler(this.radioControl_Click));
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
		private TextBox textLocation
		{
			get
			{
				return this._textLocation;
			}
			[MethodImpl(32)]
			set
			{
				if (this._textLocation != null)
				{
				}
				this._textLocation = value;
				if (this._textLocation != null)
				{
				}
			}
		}
		private Label labelWebsiteHeader
		{
			get
			{
				return this._labelWebsiteHeader;
			}
			[MethodImpl(32)]
			set
			{
				if (this._labelWebsiteHeader != null)
				{
				}
				this._labelWebsiteHeader = value;
				if (this._labelWebsiteHeader != null)
				{
				}
			}
		}
		private Label labelPublishHeader
		{
			get
			{
				return this._labelPublishHeader;
			}
			[MethodImpl(32)]
			set
			{
				if (this._labelPublishHeader != null)
				{
				}
				this._labelPublishHeader = value;
				if (this._labelPublishHeader != null)
				{
				}
			}
		}
		private Label labelLocationHeader
		{
			get
			{
				return this._labelLocationHeader;
			}
			[MethodImpl(32)]
			set
			{
				if (this._labelLocationHeader != null)
				{
				}
				this._labelLocationHeader = value;
				if (this._labelLocationHeader != null)
				{
				}
			}
		}
		private Label labelAlbumTitle
		{
			get
			{
				return this._labelAlbumTitle;
			}
			[MethodImpl(32)]
			set
			{
				if (this._labelAlbumTitle != null)
				{
				}
				this._labelAlbumTitle = value;
				if (this._labelAlbumTitle != null)
				{
				}
			}
		}
		private Label labelDate
		{
			get
			{
				return this._labelDate;
			}
			[MethodImpl(32)]
			set
			{
				if (this._labelDate != null)
				{
				}
				this._labelDate = value;
				if (this._labelDate != null)
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
			this.components = new Container();
			ResourceManager resourceManager = new ResourceManager(typeof(DetailsAlbum));
			this.labelWebsiteHeader = new Label();
			this.labelAlbumTitle = new Label();
			this.textTitle = new TextBox();
			this.textDesc = new TextBox();
			this.labelDate = new Label();
			this.textDate = new TextBox();
			this.radioPublish = new RadioButton();
			this.imageList = new ImageList(this.components);
			this.radioDontPublish = new RadioButton();
			this.labelPublishHeader = new Label();
			this.labelLocationHeader = new Label();
			this.textLocation = new TextBox();
			this.SuspendLayout();
			this.labelWebsiteHeader.FlatStyle = 3;
            this.labelWebsiteHeader.Font = new Font("Microsoft Sans Serif", 8.25f, FontStyle.Bold, GraphicsUnit.Point, 0);
			Control arg_E4_0 = this.labelWebsiteHeader;
			Point location = new Point(8, 8);
			arg_E4_0.Location = location;
			this.labelWebsiteHeader.Name = "labelWebsiteHeader";
			Control arg_10E_0 = this.labelWebsiteHeader;
			Size size = new Size(232, 16);
			arg_10E_0.Size = size;
			this.labelWebsiteHeader.TabIndex = 0;
			this.labelWebsiteHeader.Text = "Text appearing on website (optional)";
			this.labelAlbumTitle.FlatStyle = 3;
			Control arg_14C_0 = this.labelAlbumTitle;
			location = new Point(8, 32);
			arg_14C_0.Location = location;
			this.labelAlbumTitle.Name = "labelAlbumTitle";
			Control arg_176_0 = this.labelAlbumTitle;
			size = new Size(136, 16);
			arg_176_0.Size = size;
			this.labelAlbumTitle.TabIndex = 1;
			this.labelAlbumTitle.Text = "Album Title && Description:";
			Control arg_1A8_0 = this.textTitle;
			location = new Point(8, 48);
			arg_1A8_0.Location = location;
			this.textTitle.MaxLength = 100;
			this.textTitle.Name = "textTitle";
			Control arg_1DF_0 = this.textTitle;
			size = new Size(288, 20);
			arg_1DF_0.Size = size;
			this.textTitle.TabIndex = 2;
			this.textTitle.Text = "";
			Control arg_211_0 = this.textDesc;
			location = new Point(8, 80);
			arg_211_0.Location = location;
			this.textDesc.Multiline = true;
			this.textDesc.Name = "textDesc";
			Control arg_247_0 = this.textDesc;
			size = new Size(288, 56);
			arg_247_0.Size = size;
			this.textDesc.TabIndex = 3;
			this.textDesc.Text = "";
			this.labelDate.FlatStyle = 3;
			Control arg_288_0 = this.labelDate;
			location = new Point(8, 152);
			arg_288_0.Location = location;
			this.labelDate.Name = "labelDate";
			Control arg_2B2_0 = this.labelDate;
			size = new Size(152, 16);
			arg_2B2_0.Size = size;
			this.labelDate.TabIndex = 4;
			this.labelDate.Text = "Date  (default: date created):";
			Control arg_2E7_0 = this.textDate;
			location = new Point(8, 168);
			arg_2E7_0.Location = location;
			this.textDate.Name = "textDate";
			Control arg_311_0 = this.textDate;
			size = new Size(288, 20);
			arg_311_0.Size = size;
			this.textDate.TabIndex = 5;
			this.textDate.Text = "";
			this.radioPublish.FlatStyle = 3;
			Control arg_353_0 = this.radioPublish;
			location = new Point(16, 232);
			arg_353_0.Location = location;
			this.radioPublish.Name = "radioPublish";
			Control arg_37D_0 = this.radioPublish;
			size = new Size(168, 24);
			arg_37D_0.Size = size;
			this.radioPublish.TabIndex = 7;
			this.radioPublish.Text = "         Upload album to my site";
			ImageList arg_3B0_0 = this.imageList;
			size = new Size(16, 16);
			arg_3B0_0.ImageSize = size;
			this.imageList.ImageStream = (ImageListStreamer)resourceManager.GetObject("imageList.ImageStream");
			this.imageList.TransparentColor = Color.Lime;
			this.radioDontPublish.FlatStyle = 3;
			Control arg_401_0 = this.radioDontPublish;
			location = new Point(16, 256);
			arg_401_0.Location = location;
			this.radioDontPublish.Name = "radioDontPublish";
			Control arg_42B_0 = this.radioDontPublish;
			size = new Size(192, 24);
			arg_42B_0.Size = size;
			this.radioDontPublish.TabIndex = 8;
			this.radioDontPublish.Text = "         View album in FotoVision only";
			this.labelPublishHeader.FlatStyle = 3;
            this.labelPublishHeader.Font = new Font("Microsoft Sans Serif", 8.25f, FontStyle.Bold, GraphicsUnit.Point, 0);
			Control arg_489_0 = this.labelPublishHeader;
			location = new Point(8, 216);
			arg_489_0.Location = location;
			this.labelPublishHeader.Name = "labelPublishHeader";
			Control arg_4B0_0 = this.labelPublishHeader;
			size = new Size(112, 16);
			arg_4B0_0.Size = size;
			this.labelPublishHeader.TabIndex = 6;
			this.labelPublishHeader.Text = "Publishing settings";
			this.labelLocationHeader.FlatStyle = 3;
            this.labelLocationHeader.Font = new Font("Microsoft Sans Serif", 8.25f, FontStyle.Bold, GraphicsUnit.Point, 0);
			Control arg_50E_0 = this.labelLocationHeader;
			location = new Point(8, 304);
			arg_50E_0.Location = location;
			this.labelLocationHeader.Name = "labelLocationHeader";
			Control arg_538_0 = this.labelLocationHeader;
			size = new Size(200, 16);
			arg_538_0.Size = size;
			this.labelLocationHeader.TabIndex = 9;
			this.labelLocationHeader.Text = "Album location";
			this.textLocation.BorderStyle = 0;
			Control arg_57A_0 = this.textLocation;
			location = new Point(8, 320);
			arg_57A_0.Location = location;
			this.textLocation.Multiline = true;
			this.textLocation.Name = "textLocation";
			this.textLocation.ReadOnly = true;
			Control arg_5BC_0 = this.textLocation;
			size = new Size(288, 40);
			arg_5BC_0.Size = size;
			this.textLocation.TabIndex = 10;
			this.textLocation.Text = "";
			this.Controls.Add(this.radioPublish);
			this.Controls.Add(this.textTitle);
			this.Controls.Add(this.labelAlbumTitle);
			this.Controls.Add(this.labelWebsiteHeader);
			this.Controls.Add(this.textDesc);
			this.Controls.Add(this.labelDate);
			this.Controls.Add(this.textDate);
			this.Controls.Add(this.radioDontPublish);
			this.Controls.Add(this.labelPublishHeader);
			this.Controls.Add(this.labelLocationHeader);
			this.Controls.Add(this.textLocation);
			this.Name = "DetailsAlbum";
			size = new Size(304, 368);
			this.Size = size;
			this.ResumeLayout(false);
		}
		public DetailsAlbum()
		{
			base.add_Paint(new PaintEventHandler(this.DetailsAlbum_Paint));
			this._album = new Album();
			this.InitializeComponent();
			this.Enabled = false;
		}
		public void PublishAlbum(string albumName, bool publish)
		{
			this.radioPublish.Checked = publish;
			this.radioDontPublish.Checked = !publish;
			this.radioControl_Click(this, EventArgs.Empty);
		}
		public void Save()
		{
			this.CheckUpdate();
		}
		private void textControl_TextChanged(object sender, EventArgs e)
		{
			this._dirty = true;
		}
		private void textControl_Leave(object sender, EventArgs e)
		{
			this.CheckUpdate();
		}
		private void radioControl_Click(object sender, EventArgs e)
		{
			this._dirty = true;
			this.CheckUpdate();
		}
		private void DetailsAlbum_Paint(object sender, PaintEventArgs e)
		{
			checked
			{
				this.imageList.Draw(e.Graphics, this.radioPublish.Left + 20, this.radioPublish.Top + (this.radioPublish.Height - this.imageList.ImageSize.Height) / 2, IntegerType.FromObject(Interaction.IIf(this.radioPublish.Enabled, 0, 2)));
				this.imageList.Draw(e.Graphics, this.radioDontPublish.Left + 20, this.radioDontPublish.Top + (this.radioDontPublish.Height - this.imageList.ImageSize.Height) / 2, IntegerType.FromObject(Interaction.IIf(this.radioDontPublish.Enabled, 1, 3)));
				int left = this.textTitle.Left;
				int width = this.textTitle.Width;
				e.Graphics.DrawLine(SystemPens.ControlDark, left, this.labelPublishHeader.Top - 14, width, this.labelPublishHeader.Top - 14);
				e.Graphics.DrawLine(SystemPens.ControlDark, left, this.labelLocationHeader.Top - 14, width, this.labelLocationHeader.Top - 14);
			}
		}
		private void UpdateFields()
		{
			this.textTitle.Text = this._album.Name;
			this.textDesc.Text = this._album.Description;
			this.textDate.Text = this._album.DateCreated;
			this.radioPublish.Checked = this._album.Publish;
			this.radioDontPublish.Checked = !this._album.Publish;
			this.textLocation.Text = this._album.Path;
			this._dirty = false;
		}
		private void UpdateXmlFile()
		{
			this._album.Name = this.textTitle.Text.Trim();
			this._album.Description = this.textDesc.Text;
			this._album.DateCreated = this.textDate.Text;
			this._album.Publish = this.radioPublish.Checked;
			this._album.WriteXml();
		}
		private void CheckUpdate()
		{
			if (!this._dirty)
			{
				return;
			}
			this._dirty = false;
			this.ValidateFields();
			this.UpdateXmlFile();
			if (this.AlbumMetadataChangedEvent != null)
			{
				this.AlbumMetadataChangedEvent(this, new AlbumMetadataChangedEventArgs(this.AlbumName, this._album));
			}
		}
		private void ValidateFields()
		{
			if (!Global.ValidateDate(this.textDate.Text.Trim()))
			{
				this.textDate.Text = this._album.DateCreated;
			}
			if (!FileManager.IsValidAlbumName(this.textTitle.Text))
			{
				this.textTitle.Text = this._album.Name;
			}
			string text = this.textTitle.Text.Trim();
			if (StringType.StrCmp(text, this._album.Name, false) != 0 && FileManager.AlbumExists(this.textTitle.Text.Trim()))
			{
				MessageBox.Show(this.TopLevelControl, string.Format("The album '{0}' already exist. Please use a different album name.", text), "Cannot Rename Album", 0, 48);
				this.textTitle.Text = this._album.Name;
			}
		}
	}
}
