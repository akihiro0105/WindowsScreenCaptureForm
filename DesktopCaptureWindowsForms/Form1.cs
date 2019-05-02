using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Imaging;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Point = System.Drawing.Point;

namespace DesktopCaptureWindowsForms
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            var bmp = new Bitmap(pictureBox1.Width, pictureBox1.Height, PixelFormat.Format24bppRgb);
            var g = Graphics.FromImage(bmp);
            Task.Run(async () =>
            {
                while (true)
                {
                    pictureBox1.Invoke(new PictureEventHandler(() =>
                    {
                        var p = pictureBox1.PointToScreen(new Point(0, 0));
                        g.CopyFromScreen(p.X, p.Y, 0, 0, bmp.Size);
                        pictureBox1.Image = bmp;
                    }));
                    await Task.Delay(10);
                }
            });
        }

        private delegate void PictureEventHandler();
    }
}
