using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using QRCoder;
namespace tutoqrcode
{
    internal class qrcodeprint
    {
        public void GenerateQRCode(string text, PictureBox picture)
        {
            using (QRCodeGenerator qrGenerator = new QRCodeGenerator())
            {
                QRCodeData qRCodeData = qrGenerator.CreateQrCode(text, QRCodeGenerator.ECCLevel.Q);
                using (QRCode qrcode = new QRCode(qRCodeData))
                {
                    Bitmap qrcodeImage = qrcode.GetGraphic(5, Color.Black, Color.White, true);

                    if (picture.InvokeRequired)
                    {
                        picture.Invoke(new MethodInvoker(() =>
                        {
                            picture.Image?.Dispose();
                            picture.Image = qrcodeImage;
                            picture.SizeMode = PictureBoxSizeMode.Zoom;

                        }));
                    }
                    else
                    {
                        picture.Image?.Dispose();
                        picture.Image = qrcodeImage;
                        picture.SizeMode = PictureBoxSizeMode.Zoom;
                    }
                }
            }


        }
    }
}