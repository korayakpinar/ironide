namespace test_app {
    partial class app {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing) {
            if(disposing && (components != null)) {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent() {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(app));
            this.ıronidePictureBox1 = new Ironide.IronidePictureBox();
            this.SuspendLayout();
            // 
            // ıronidePictureBox1
            // 
            this.ıronidePictureBox1.Image = ((System.Drawing.Image)(resources.GetObject("ıronidePictureBox1.Image")));
            this.ıronidePictureBox1.Location = new System.Drawing.Point(298, 135);
            this.ıronidePictureBox1.Name = "ıronidePictureBox1";
            this.ıronidePictureBox1.Size = new System.Drawing.Size(183, 151);
            this.ıronidePictureBox1.TabIndex = 0;
            this.ıronidePictureBox1.Text = "ıronidePictureBox1";
            // 
            // app
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.ıronidePictureBox1);
            this.Name = "app";
            this.Text = "app";
            this.ResumeLayout(false);

        }

        #endregion

        private Ironide.IronidePictureBox ıronidePictureBox1;
    }
}