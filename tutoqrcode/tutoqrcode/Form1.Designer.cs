namespace tutoqrcode
{
    partial class Form1
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
            qrcodeimage = new PictureBox();
            ((System.ComponentModel.ISupportInitialize)qrcodeimage).BeginInit();
            SuspendLayout();
            // 
            // qrcodeimage
            // 
            qrcodeimage.Location = new Point(309, 87);
            qrcodeimage.Name = "qrcodeimage";
            qrcodeimage.Size = new Size(219, 200);
            qrcodeimage.TabIndex = 0;
            qrcodeimage.TabStop = false;
            // 
            // Form1
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(800, 450);
            Controls.Add(qrcodeimage);
            Name = "Form1";
            Text = "Form1";
            ((System.ComponentModel.ISupportInitialize)qrcodeimage).EndInit();
            ResumeLayout(false);
        }

        #endregion

        private PictureBox qrcodeimage;
    }
}
