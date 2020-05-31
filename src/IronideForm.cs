using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using Ironide.Tools;

namespace Ironide {
    /// <summary>
    /// Form of Ironide.
    /// </summary>
    public partial class IronideForm:Form, IIronideControl {
        #region Constructors

        /// <summary>
        /// Constructor.
        /// </summary>
        public IronideForm() {
            Init();
        }

        #endregion

        #region Drawing

        protected override void OnPaintBackground(PaintEventArgs e) {
            DrawBackground(e.Graphics);
        }

        protected override void OnPaint(PaintEventArgs e) {
            DrawText(e.Graphics);
            DrawBorder(e.Graphics);
            DrawEnable(e.Graphics);
        }

        /// <summary>
        /// Draw background.
        /// </summary>
        /// <param name="graphics">Graphics.</param>
        public virtual void DrawBackground(Graphics graphics) {
            using(var bgbrush = new SolidBrush(BackColor))
                graphics.FillRectangle(bgbrush,ClientRectangle);
        }

        /// <summary>
        /// Draw enable.
        /// </summary>
        /// <param name="graphics">Graphics.</param>
        public virtual void DrawEnable(Graphics graphics) {
            if(Enabled)
                return;

            using(var enbrush = new SolidBrush(Color.FromArgb(200,Color.Gainsboro)))
                graphics.FillRectangle(enbrush,ClientRectangle);
        }

        /// <summary>
        /// Draw borders.
        /// </summary>
        /// <param name="graphics">Graphics.</param>
        public virtual void DrawBorder(Graphics graphics) {
            var bs = IronideConvert.ToButtonBorderStyle(BorderStyle);
            ControlPaint.DrawBorder(graphics,ClientRectangle,BorderColor,
                BorderThickness,bs,BorderColor,BorderThickness,bs,
                BorderColor,BorderThickness,bs,BorderColor,
                BorderThickness,bs);
        }

        /// <summary>
        /// Draw text.
        /// </summary>
        /// <param name="graphics">Graphics.</param>
        public virtual void DrawText(Graphics graphics) {
            using(var fgbrush = new SolidBrush(ForeColor))
            using(var format = IronideConvert.ToStringFormat(TextAlign))
                graphics.DrawString(Text,Font,fgbrush,ClientRectangle,format);
        }

        #endregion

        #region Properties

        private string title = "Title";
        /// <summary>
        /// Title of form.
        /// </summary>
        [Description("Title of form.")]
        [DefaultValue("Title")]
        public string Title {
            get => title;
            set {
                if(value == title)
                    return;

                title=value;
                titlePanel.Text = value;
                Invalidate();
            }
        }

        private Color borderColor = Color.DodgerBlue;
        [Description("Color of border.")]
        [DefaultValue(typeof(Color),"DodgerBlue")]
        public virtual Color BorderColor {
            get => borderColor;
            set {
                if(value == borderColor)
                    return;

                borderColor = value;
                Invalidate();
            }
        }

        private int borderThickness = 1;
        [Description("Thickness of boder.")]
        [DefaultValue(1)]
        public virtual int BorderThickness {
            get => borderThickness;
            set {
                if(borderThickness == value)
                    return;

                borderThickness = value;
                Invalidate();
            }
        }

        /// <summary>
        /// Color of background.
        /// </summary>
        [Description("Color of background.")]
        [DefaultValue(typeof(Color),"White")]
        public override Color BackColor { get => base.BackColor; set => base.BackColor=value; }

        /// <summary>
        /// Color of foreground.
        /// </summary>
        [Description("Color of foreground.")]
        [DefaultValue(typeof(Color),"Black")]
        public override Color ForeColor { get => base.ForeColor; set => base.ForeColor=value; }

        /// <summary>
        /// Form content.
        /// </summary>
        [Description("Form content.")]
        [DefaultValue("")]
        public override string Text {
            get => base.Text;
            set {
                if(value == base.Text)
                    return;

                base.Text=value;
                Invalidate();
            }
        }

        private IronideBorderStyle borderStyle = IronideBorderStyle.Solid;
        [Description("Style of border.")]
        [DefaultValue(typeof(IronideBorderStyle),"Solid")]
        public virtual IronideBorderStyle BorderStyle {
            get => borderStyle;
            set {
                if(value == borderStyle)
                    return;

                borderStyle = value;
                Invalidate();
            }
        }

        private ContentAlignment textAlign = ContentAlignment.TopLeft;
        [Description("Align of text.")]
        [DefaultValue(typeof(ContentAlignment),"TopLeft")]
        public virtual ContentAlignment TextAlign {
            get => textAlign;
            set {
                if(value == textAlign)
                    return;

                textAlign = value;
                if(!string.IsNullOrEmpty(Text))
                    Invalidate();
            }
        }

        [Browsable(false)]
        [EditorBrowsable(EditorBrowsableState.Never)]
        public new FormBorderStyle FormBorderStyle {
            get => base.FormBorderStyle;
            private set => base.FormBorderStyle = value;
        }

        /// <summary>
        /// Show form icon.
        /// </summary>
        [Description("Show form icon.")]
        [DefaultValue(true)]
        public new bool ShowIcon {
            get => base.ShowIcon;
            set {
                if(value == base.ShowIcon)
                    return;

                base.ShowIcon = value;
                if(value)
                    titlePanel.Location = new Point(iconBox.Location.X+iconBox.Width,1);
                else
                    titlePanel.Location = new Point(1,1);
                titlePanel.Size = new Size(Width - titlePanel.Location.X-1,titlePanel.Height);
            }
        }

        /// <summary>
        /// Icon of form.
        /// </summary>
        [Description("Icon of form.")]
        public new Icon Icon {
            get => base.Icon;
            set {
                if(value == base.Icon)
                    return;

                base.Icon = value;
                iconBox.Image = value.ToBitmap();
            }
        }

        #endregion
    }

    // Designer.
    public partial class IronideForm {
        #region Components

        private IronidePictureBox iconBox;
        private IronidePanel titlePanel;

        #endregion

        /// <summary>
        /// Initialize component.
        /// </summary>
        protected virtual void Init() {
            #region Base

            StartPosition = FormStartPosition.CenterScreen;
            FormBorderStyle = FormBorderStyle.None;
            ForeColor = Color.Black;
            BackColor = Color.White;
            Size = new Size(800,450);

            #endregion

            #region titlePanel

            titlePanel = new IronidePanel();
            titlePanel.BackColor = Color.Gainsboro;
            titlePanel.BorderThickness = 0;
            titlePanel.TextRender = true;
            titlePanel.Location = new Point(26,1);
            titlePanel.TextAlign = ContentAlignment.MiddleLeft;
            titlePanel.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            titlePanel.Size = new Size(Width-27,25);
            titlePanel.Text = Title;
            Controls.Add(titlePanel);

            #endregion

            #region iconBox

            iconBox = new IronidePictureBox();
            iconBox.Location = new Point(1,1);
            iconBox.Size = new Size(titlePanel.Location.X,titlePanel.Height);
            iconBox.BorderThickness = 0;
            iconBox.BackColor = titlePanel.BackColor;
            iconBox.Image = Icon.ToBitmap();
            iconBox.SizeMode = IronideImageSizeMode.Stretch;
            Controls.Add(iconBox);

            #endregion
        }
    }
}
