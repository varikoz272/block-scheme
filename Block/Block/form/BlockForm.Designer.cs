using System.Windows.Forms;
using Block.manage;

namespace Block.form
{
	partial class BlockForm
	{
		private System.ComponentModel.IContainer components = null;
		protected override void Dispose(bool disposing)
		{
			if (disposing) {
				if (components != null) {
					components.Dispose();
				}
			}
			base.Dispose(disposing);
		}
		
		private void InitializeComponent()
		{
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(BlockForm));
			this.pageControl = new System.Windows.Forms.TabControl();
			this.coursePage = new System.Windows.Forms.TabPage();
			this.trackBar = new System.Windows.Forms.TrackBar();
			this.roundButton1 = new Block.form.RoundButton();
			this.darkRadioButton = new System.Windows.Forms.RadioButton();
			this.lightRadioButton = new System.Windows.Forms.RadioButton();
			this.colorsLabel = new System.Windows.Forms.Label();
			this.pageControl.SuspendLayout();
			this.coursePage.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.trackBar)).BeginInit();
			this.SuspendLayout();
			// 
			// pageControl
			// 
			this.pageControl.Controls.Add(this.coursePage);
			this.pageControl.Location = new System.Drawing.Point(0, 0);
			this.pageControl.Name = "pageControl";
			this.pageControl.SelectedIndex = 0;
			this.pageControl.Size = new System.Drawing.Size(585, 863);
			this.pageControl.TabIndex = 1;
			// 
			// coursePage
			// 
			this.coursePage.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(174)))), ((int)(((byte)(184)))), ((int)(((byte)(254)))));
			this.coursePage.Controls.Add(this.trackBar);
			this.coursePage.Controls.Add(this.roundButton1);
			this.coursePage.Location = new System.Drawing.Point(4, 22);
			this.coursePage.Name = "coursePage";
			this.coursePage.Padding = new System.Windows.Forms.Padding(3);
			this.coursePage.Size = new System.Drawing.Size(577, 837);
			this.coursePage.TabIndex = 1;
			this.coursePage.Text = "Курс";
			// 
			// trackBar
			// 
			this.trackBar.LargeChange = 1;
			this.trackBar.Location = new System.Drawing.Point(532, 0);
			this.trackBar.Maximum = 9;
			this.trackBar.Name = "trackBar";
			this.trackBar.Orientation = System.Windows.Forms.Orientation.Vertical;
			this.trackBar.Size = new System.Drawing.Size(45, 834);
			this.trackBar.TabIndex = 2;
			this.trackBar.TickStyle = System.Windows.Forms.TickStyle.Both;
			this.trackBar.Value = 9;
			this.trackBar.Scroll += new System.EventHandler(this.TrackBarScroll);
			// 
			// roundButton1
			// 
			this.roundButton1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(117)))), ((int)(((byte)(139)))), ((int)(((byte)(253)))));
			this.roundButton1.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(241)))), ((int)(((byte)(242)))), ((int)(((byte)(246)))));
			this.roundButton1.Location = new System.Drawing.Point(8, 6);
			this.roundButton1.Name = "roundButton1";
			this.roundButton1.Size = new System.Drawing.Size(44, 41);
			this.roundButton1.TabIndex = 0;
			this.roundButton1.Text = "->";
			this.roundButton1.UseVisualStyleBackColor = false;
			this.roundButton1.Click += new System.EventHandler(this.SwitchCourseButtonClick);
			// 
			// darkRadioButton
			// 
			this.darkRadioButton.Location = new System.Drawing.Point(9, 60);
			this.darkRadioButton.Name = "darkRadioButton";
			this.darkRadioButton.Size = new System.Drawing.Size(104, 24);
			this.darkRadioButton.TabIndex = 2;
			this.darkRadioButton.Text = "Тёмное";
			this.darkRadioButton.UseVisualStyleBackColor = true;
			// 
			// lightRadioButton
			// 
			this.lightRadioButton.Checked = true;
			this.lightRadioButton.Location = new System.Drawing.Point(9, 30);
			this.lightRadioButton.Name = "lightRadioButton";
			this.lightRadioButton.Size = new System.Drawing.Size(104, 24);
			this.lightRadioButton.TabIndex = 1;
			this.lightRadioButton.TabStop = true;
			this.lightRadioButton.Text = "Светлое";
			this.lightRadioButton.UseVisualStyleBackColor = true;
			// 
			// colorsLabel
			// 
			this.colorsLabel.Location = new System.Drawing.Point(9, 4);
			this.colorsLabel.Name = "colorsLabel";
			this.colorsLabel.Size = new System.Drawing.Size(100, 23);
			this.colorsLabel.TabIndex = 0;
			this.colorsLabel.Text = "Оформление";
			this.colorsLabel.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
			// 
			// BlockForm
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(584, 861);
			this.Controls.Add(this.pageControl);
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
			this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
			this.Name = "BlockForm";
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
			this.Text = "Блок-схемы для чайников";
			this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.BlockFormFormClosed);
			this.pageControl.ResumeLayout(false);
			this.coursePage.ResumeLayout(false);
			this.coursePage.PerformLayout();
			((System.ComponentModel.ISupportInitialize)(this.trackBar)).EndInit();
			this.ResumeLayout(false);

		}
		private Block.form.RoundButton roundButton1;
		private System.Windows.Forms.TabPage coursePage;
		private System.Windows.Forms.TabControl pageControl;
		private System.Windows.Forms.TrackBar trackBar;
		private System.Windows.Forms.RadioButton darkRadioButton;
		private System.Windows.Forms.RadioButton lightRadioButton;
		private System.Windows.Forms.Label colorsLabel;
	}
}
