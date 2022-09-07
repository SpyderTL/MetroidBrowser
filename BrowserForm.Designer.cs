namespace MetroidBrowser
{
	partial class BrowserForm
	{
		/// <summary>
		///  Required designer variable.
		/// </summary>
		private System.ComponentModel.IContainer components = null;

		/// <summary>
		///  Clean up any resources being used.
		/// </summary>
		/// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
		protected override void Dispose(bool disposing)
		{
			if (disposing && (components != null))
			{
				components.Dispose();
			}
			base.Dispose(disposing);
		}

		#region Windows Form Designer generated code

		/// <summary>
		///  Required method for Designer support - do not modify
		///  the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
			this.splitContainer1 = new System.Windows.Forms.SplitContainer();
			this.TreeView = new System.Windows.Forms.TreeView();
			this.TabControl = new System.Windows.Forms.TabControl();
			this.PropertyTabPage = new System.Windows.Forms.TabPage();
			this.PropertyGrid = new System.Windows.Forms.PropertyGrid();
			this.PreviewTabPage = new System.Windows.Forms.TabPage();
			this.SongPanel = new System.Windows.Forms.Panel();
			this.NoiseLabel = new System.Windows.Forms.Label();
			this.TriangleLabel = new System.Windows.Forms.Label();
			this.Square2Label = new System.Windows.Forms.Label();
			this.SquareLabel = new System.Windows.Forms.Label();
			this.label4 = new System.Windows.Forms.Label();
			this.label3 = new System.Windows.Forms.Label();
			this.label2 = new System.Windows.Forms.Label();
			this.label1 = new System.Windows.Forms.Label();
			this.SongNameLabel = new System.Windows.Forms.Label();
			this.PlayButton = new System.Windows.Forms.Button();
			this.ImagePanel = new System.Windows.Forms.Panel();
			this.PictureBox = new System.Windows.Forms.PictureBox();
			((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
			this.splitContainer1.Panel1.SuspendLayout();
			this.splitContainer1.Panel2.SuspendLayout();
			this.splitContainer1.SuspendLayout();
			this.TabControl.SuspendLayout();
			this.PropertyTabPage.SuspendLayout();
			this.PreviewTabPage.SuspendLayout();
			this.SongPanel.SuspendLayout();
			this.ImagePanel.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.PictureBox)).BeginInit();
			this.SuspendLayout();
			// 
			// splitContainer1
			// 
			this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
			this.splitContainer1.Location = new System.Drawing.Point(0, 0);
			this.splitContainer1.Name = "splitContainer1";
			// 
			// splitContainer1.Panel1
			// 
			this.splitContainer1.Panel1.Controls.Add(this.TreeView);
			// 
			// splitContainer1.Panel2
			// 
			this.splitContainer1.Panel2.Controls.Add(this.TabControl);
			this.splitContainer1.Size = new System.Drawing.Size(1006, 721);
			this.splitContainer1.SplitterDistance = 335;
			this.splitContainer1.TabIndex = 0;
			// 
			// TreeView
			// 
			this.TreeView.Dock = System.Windows.Forms.DockStyle.Fill;
			this.TreeView.Location = new System.Drawing.Point(0, 0);
			this.TreeView.Name = "TreeView";
			this.TreeView.Size = new System.Drawing.Size(335, 721);
			this.TreeView.TabIndex = 0;
			// 
			// TabControl
			// 
			this.TabControl.Alignment = System.Windows.Forms.TabAlignment.Bottom;
			this.TabControl.Controls.Add(this.PropertyTabPage);
			this.TabControl.Controls.Add(this.PreviewTabPage);
			this.TabControl.Dock = System.Windows.Forms.DockStyle.Fill;
			this.TabControl.Location = new System.Drawing.Point(0, 0);
			this.TabControl.Name = "TabControl";
			this.TabControl.SelectedIndex = 0;
			this.TabControl.Size = new System.Drawing.Size(667, 721);
			this.TabControl.TabIndex = 0;
			// 
			// PropertyTabPage
			// 
			this.PropertyTabPage.Controls.Add(this.PropertyGrid);
			this.PropertyTabPage.Location = new System.Drawing.Point(4, 4);
			this.PropertyTabPage.Name = "PropertyTabPage";
			this.PropertyTabPage.Padding = new System.Windows.Forms.Padding(3);
			this.PropertyTabPage.Size = new System.Drawing.Size(659, 688);
			this.PropertyTabPage.TabIndex = 0;
			this.PropertyTabPage.Text = "Properties";
			this.PropertyTabPage.UseVisualStyleBackColor = true;
			// 
			// PropertyGrid
			// 
			this.PropertyGrid.Dock = System.Windows.Forms.DockStyle.Fill;
			this.PropertyGrid.Location = new System.Drawing.Point(3, 3);
			this.PropertyGrid.Name = "PropertyGrid";
			this.PropertyGrid.Size = new System.Drawing.Size(653, 682);
			this.PropertyGrid.TabIndex = 0;
			// 
			// PreviewTabPage
			// 
			this.PreviewTabPage.Controls.Add(this.ImagePanel);
			this.PreviewTabPage.Controls.Add(this.SongPanel);
			this.PreviewTabPage.Location = new System.Drawing.Point(4, 4);
			this.PreviewTabPage.Name = "PreviewTabPage";
			this.PreviewTabPage.Padding = new System.Windows.Forms.Padding(3);
			this.PreviewTabPage.Size = new System.Drawing.Size(659, 688);
			this.PreviewTabPage.TabIndex = 1;
			this.PreviewTabPage.Text = "Preview";
			this.PreviewTabPage.UseVisualStyleBackColor = true;
			// 
			// SongPanel
			// 
			this.SongPanel.Controls.Add(this.NoiseLabel);
			this.SongPanel.Controls.Add(this.TriangleLabel);
			this.SongPanel.Controls.Add(this.Square2Label);
			this.SongPanel.Controls.Add(this.SquareLabel);
			this.SongPanel.Controls.Add(this.label4);
			this.SongPanel.Controls.Add(this.label3);
			this.SongPanel.Controls.Add(this.label2);
			this.SongPanel.Controls.Add(this.label1);
			this.SongPanel.Controls.Add(this.SongNameLabel);
			this.SongPanel.Controls.Add(this.PlayButton);
			this.SongPanel.Dock = System.Windows.Forms.DockStyle.Fill;
			this.SongPanel.Location = new System.Drawing.Point(3, 3);
			this.SongPanel.Name = "SongPanel";
			this.SongPanel.Size = new System.Drawing.Size(653, 682);
			this.SongPanel.TabIndex = 0;
			this.SongPanel.Visible = false;
			// 
			// NoiseLabel
			// 
			this.NoiseLabel.Location = new System.Drawing.Point(321, 39);
			this.NoiseLabel.Name = "NoiseLabel";
			this.NoiseLabel.Size = new System.Drawing.Size(100, 25);
			this.NoiseLabel.TabIndex = 9;
			this.NoiseLabel.Text = "[Note]";
			// 
			// TriangleLabel
			// 
			this.TriangleLabel.Location = new System.Drawing.Point(215, 39);
			this.TriangleLabel.Name = "TriangleLabel";
			this.TriangleLabel.Size = new System.Drawing.Size(100, 25);
			this.TriangleLabel.TabIndex = 8;
			this.TriangleLabel.Text = "[Note]";
			// 
			// Square2Label
			// 
			this.Square2Label.Location = new System.Drawing.Point(109, 39);
			this.Square2Label.Name = "Square2Label";
			this.Square2Label.Size = new System.Drawing.Size(100, 25);
			this.Square2Label.TabIndex = 7;
			this.Square2Label.Text = "[Note]";
			// 
			// SquareLabel
			// 
			this.SquareLabel.Location = new System.Drawing.Point(3, 39);
			this.SquareLabel.Name = "SquareLabel";
			this.SquareLabel.Size = new System.Drawing.Size(100, 25);
			this.SquareLabel.TabIndex = 6;
			this.SquareLabel.Text = "[Note]";
			// 
			// label4
			// 
			this.label4.Location = new System.Drawing.Point(321, 2);
			this.label4.Name = "label4";
			this.label4.Size = new System.Drawing.Size(100, 25);
			this.label4.TabIndex = 5;
			this.label4.Text = "Noise";
			// 
			// label3
			// 
			this.label3.Location = new System.Drawing.Point(215, 2);
			this.label3.Name = "label3";
			this.label3.Size = new System.Drawing.Size(100, 25);
			this.label3.TabIndex = 4;
			this.label3.Text = "Triangle";
			// 
			// label2
			// 
			this.label2.Location = new System.Drawing.Point(109, 2);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(100, 25);
			this.label2.TabIndex = 3;
			this.label2.Text = "Square 2";
			// 
			// label1
			// 
			this.label1.Location = new System.Drawing.Point(3, 2);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(100, 25);
			this.label1.TabIndex = 2;
			this.label1.Text = "Square 1";
			// 
			// SongNameLabel
			// 
			this.SongNameLabel.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.SongNameLabel.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.SongNameLabel.Location = new System.Drawing.Point(3, 650);
			this.SongNameLabel.Name = "SongNameLabel";
			this.SongNameLabel.Size = new System.Drawing.Size(608, 29);
			this.SongNameLabel.TabIndex = 1;
			this.SongNameLabel.Text = "[Song Name]";
			this.SongNameLabel.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// PlayButton
			// 
			this.PlayButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.PlayButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			this.PlayButton.Font = new System.Drawing.Font("Webdings", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
			this.PlayButton.Location = new System.Drawing.Point(615, 650);
			this.PlayButton.Name = "PlayButton";
			this.PlayButton.Size = new System.Drawing.Size(33, 29);
			this.PlayButton.TabIndex = 0;
			this.PlayButton.Text = "4";
			this.PlayButton.UseVisualStyleBackColor = true;
			// 
			// ImagePanel
			// 
			this.ImagePanel.AutoScroll = true;
			this.ImagePanel.Controls.Add(this.PictureBox);
			this.ImagePanel.Dock = System.Windows.Forms.DockStyle.Fill;
			this.ImagePanel.Location = new System.Drawing.Point(3, 3);
			this.ImagePanel.Margin = new System.Windows.Forms.Padding(0);
			this.ImagePanel.Name = "ImagePanel";
			this.ImagePanel.Size = new System.Drawing.Size(653, 682);
			this.ImagePanel.TabIndex = 2;
			// 
			// PictureBox
			// 
			this.PictureBox.Location = new System.Drawing.Point(3, 2);
			this.PictureBox.Margin = new System.Windows.Forms.Padding(0);
			this.PictureBox.Name = "PictureBox";
			this.PictureBox.Size = new System.Drawing.Size(100, 100);
			this.PictureBox.SizeMode = System.Windows.Forms.PictureBoxSizeMode.AutoSize;
			this.PictureBox.TabIndex = 2;
			this.PictureBox.TabStop = false;
			// 
			// BrowserForm
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 20F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(1006, 721);
			this.Controls.Add(this.splitContainer1);
			this.Name = "BrowserForm";
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
			this.splitContainer1.Panel1.ResumeLayout(false);
			this.splitContainer1.Panel2.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
			this.splitContainer1.ResumeLayout(false);
			this.TabControl.ResumeLayout(false);
			this.PropertyTabPage.ResumeLayout(false);
			this.PreviewTabPage.ResumeLayout(false);
			this.SongPanel.ResumeLayout(false);
			this.ImagePanel.ResumeLayout(false);
			this.ImagePanel.PerformLayout();
			((System.ComponentModel.ISupportInitialize)(this.PictureBox)).EndInit();
			this.ResumeLayout(false);

		}

		#endregion

		private SplitContainer splitContainer1;
		public TreeView TreeView;
		public TabControl TabControl;
		public PropertyGrid PropertyGrid;
		public Panel SongPanel;
		public TabPage PropertyTabPage;
		public TabPage PreviewTabPage;
		public Label SongNameLabel;
		private Label label4;
		private Label label3;
		private Label label2;
		private Label label1;
		public Label NoiseLabel;
		public Label TriangleLabel;
		public Label Square2Label;
		public Label SquareLabel;
		public Button PlayButton;
		public Panel ImagePanel;
		public PictureBox PictureBox;
	}
}