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
            this.ıronideButton1 = new Ironide.IronideButton();
            this.SuspendLayout();
            // 
            // ıronideButton1
            // 
            this.ıronideButton1.BorderThickness = 2;
            this.ıronideButton1.HoverColor = System.Drawing.Color.DarkTurquoise;
            this.ıronideButton1.Image = ((System.Drawing.Image)(resources.GetObject("ıronideButton1.Image")));
            this.ıronideButton1.ImageLocation = new System.Drawing.Point(1, 1);
            this.ıronideButton1.ImageSize = new System.Drawing.Size(50, 50);
            this.ıronideButton1.Location = new System.Drawing.Point(302, 157);
            this.ıronideButton1.Name = "ıronideButton1";
            this.ıronideButton1.Size = new System.Drawing.Size(129, 54);
            this.ıronideButton1.TabIndex = 0;
            this.ıronideButton1.Text = "ıronideButton1";
            this.ıronideButton1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // app
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.ıronideButton1);
            this.Name = "app";
            this.Text = "app";
            this.ResumeLayout(false);

        }

        #endregion

        private Ironide.IronideButton ıronideButton1;
    }
}