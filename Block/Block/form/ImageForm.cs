using System;
using System.Drawing;
using System.Windows.Forms;
using System.Collections.Generic;
using System.IO;

using Block.user;
using Block.user.level;
using Block.manage;
using Block.Algorithm;

namespace Block.form
{
	public partial class ImageForm : Form
	{
		public ImageForm(string imagePath)
		{
			InitializeComponent();
			Image img = Image.FromFile(imagePath);
			pictureBox1.Size = img.Size;
			pictureBox1.Image = img;
			Size = new Size(pictureBox1.Size.Width, pictureBox1.Size.Height + 20);
			Show();
		}
		
		void ImageFormFormClosed(object sender, FormClosedEventArgs e)
		{
			Hide();
		}
	}
}
