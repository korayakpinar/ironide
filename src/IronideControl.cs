using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace Ironide {
    /// <summary>
    /// Control template of Ironide.
    /// </summary>
    public abstract class IronideControl:Control, IIronideControl {
        #region Constructors

        /// <summary>
        /// Constructor.
        /// </summary>
        public IronideControl() {
            BackColor = Color.White;
            ForeColor = Color.Black;
        }

        #endregion

        #region Drawing

        protected override void OnPaintBackground(PaintEventArgs e) {
            using(var bgbrush = new SolidBrush(BackColor))
                e.Graphics.FillRectangle(bgbrush,ClientRectangle);

            ControlPaint.DrawBorder(e.Graphics,ClientRectangle,BorderColor,
                BorderThickness,ButtonBorderStyle.Solid,BorderColor,BorderThickness,ButtonBorderStyle.Solid,
                BorderColor,BorderThickness,ButtonBorderStyle.Solid,BorderColor,
                BorderThickness,ButtonBorderStyle.Solid);
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
        public IronideBorderStyle BorderStyle {
            get => borderStyle;
            set {
                if(value == borderStyle)
                    return;

                borderStyle = value;
                Invalidate();
            }
        }

        #endregion
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
