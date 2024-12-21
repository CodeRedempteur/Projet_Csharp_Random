namespace tutoqrcode
{
    public partial class Form1 : Form
    {

        qrcodeprint qrcodeprint;
        public Form1()
        {
            InitializeComponent();
            qrcodeprint = new qrcodeprint();
            qrcodeprint.GenerateQRCode("https://www.youtube.com/@CodeRedempteur", qrcodeimage);
        }
    }
}
