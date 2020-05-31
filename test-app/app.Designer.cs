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
            this.ıronidePanel1 = new Ironide.IronidePanel();
            this.SuspendLayout();
            // 
            // ıronidePanel1
            // 
            this.ıronidePanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.ıronidePanel1.Location = new System.Drawing.Point(0, 0);
            this.ıronidePanel1.Name = "ıronidePanel1";
            this.ıronidePanel1.Size = new System.Drawing.Size(800, 450);
            this.ıronidePanel1.TabIndex = 2;
            this.ıronidePanel1.Text = "ıronidePanel1";
            // 
            // app
            // 
            this.Animation = Ironide.IronideFormAnimation.Fade;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.Control;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.ıronidePanel1);
            this.Name = "app";
            this.Controls.SetChildIndex(this.ıronidePanel1, 0);
            this.ResumeLayout(false);

        }

        #endregion

        private Ironide.IronidePanel ıronidePanel1;
    }
}