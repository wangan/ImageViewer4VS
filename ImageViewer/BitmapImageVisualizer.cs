using System;
using System.Drawing;
using System.Windows.Forms;
using Microsoft.VisualStudio.DebuggerVisualizers;
using System.Diagnostics;
using ImageViewer;

[assembly: DebuggerVisualizer(typeof(BitmapImageVisualizer),
typeof(VisualizerObjectSource),
Target = typeof(Bitmap),
Description = "ImageViewer")]

namespace ImageViewer {
    public class BitmapImageVisualizer : DialogDebuggerVisualizer {
        protected override void Show(IDialogVisualizerService windowService, IVisualizerObjectProvider objectProvider) {
            var bitmap = (Bitmap)objectProvider.GetObject();
            if (null != bitmap) {
                var form = new Form {
                    Text = String.Format("ImageViewer - W: {0}px  H: {1}px", bitmap.Width, bitmap.Height),
                    ClientSize = new Size(bitmap.Width, bitmap.Height),
                    FormBorderStyle = FormBorderStyle.Sizable,
                    ShowInTaskbar = false,
                    ShowIcon = false
                };

                var pictureBox = new PictureBox {
                    SizeMode = PictureBoxSizeMode.Zoom,
                    Image = bitmap,
                    Parent = form,
                    Dock = DockStyle.Fill
                };

                form.KeyUp += new KeyEventHandler(EscToExit);
                form.ShowDialog();
            }
        }

        private void EscToExit(object sender, KeyEventArgs e) {
            if (e.KeyCode == Keys.Escape) {
                ((Form)sender).Close();
            }
        }
    }
}
