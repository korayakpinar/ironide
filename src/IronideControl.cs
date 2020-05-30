using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using Ironide.Tools;

namespace Ironide {
    /// <summary>
    /// Control template of Ironide.
    /// </summary>
    public abstract partial class IronideControl:Control, IIronideControl {
        #region Constructors

        /// <summary>
        /// Constructor.
        /// </summary>
        public IronideControl() {
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

        #endregion
    }

    // Designer.
    public abstract partial class IronideControl {
        /// <summary>
        /// Initialize component.
        /// </summary>
        protected void Init() {
            SetStyle(ControlStyles.AllPaintingInWmPaint |
                ControlStyles.ResizeRedraw |
                ControlStyles.SupportsTransparentBackColor |
                ControlStyles.OptimizedDoubleBuffer,true);

            BackColor = Color.White;
            ForeColor = Color.Black;
        }
    }

    /// <summary>
    /// Border styles of Ironide.
    /// </summary>
    public enum IronideBorderStyle {
        Solid = 0,
        Dashed = 1,
        Dotted = 2,
        Inset = 3,
        Outset = 4
    }
}
