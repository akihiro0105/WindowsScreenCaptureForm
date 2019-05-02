using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Drawing;
using System.Drawing.Imaging;
using System.Threading.Tasks;
using System.Windows.Interop;
using Window = System.Windows.Window;

namespace DesktopCapture
{
    /// <summary>
    /// MainWindow.xaml の相互作用ロジック
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            var img = new Bitmap(1280, 720, System.Drawing.Imaging.PixelFormat.Format24bppRgb);
            var option = BitmapSizeOptions.FromWidthAndHeight(img.Size.Width, img.Size.Height);
            var g = Graphics.FromImage(img);
            Task.Run(async () =>
            {
                while (true)
                {
                    this.Dispatcher.Invoke((Action) (() =>
                    {
                        g.CopyFromScreen(10, 10, 0, 0, img.Size);

                        var hbitmap = img.GetHbitmap();
                        CaptureImage.Source = Imaging.CreateBitmapSourceFromHBitmap(hbitmap, IntPtr.Zero, Int32Rect.Empty, option);
                        DeleteObject(hbitmap);
                    }));
                    await Task.Delay(50);
                }
            });
        }

        [System.Runtime.InteropServices.DllImport("gdi32.dll")]
        public static extern bool DeleteObject(IntPtr hObject);

    }
}
