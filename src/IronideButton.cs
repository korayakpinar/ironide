using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using Ironide.Tools;

namespace Ironide {
    /// <summary>
    /// Button of Ironide.
    /// </summary>
    public partial class IronideButton:IronideControl {
        #region Constructors

        /// <summary>
        /// Constructor.
        /// </summary>
        public IronideButton() {

        }

        #endregion

        #region Drawing

        protected override void OnPaintBackground(PaintEventArgs e) {
            using(var bgbrush = new SolidBrush(
                IsEntered ? EnterColor : IsMouseEnter ? HoverColor : BackColor))
                e.Graphics.FillRectangle(bgbrush,ClientRectangle);

            DrawBorder(e.Graphics);
        }

        protected override void OnPaint(PaintEventArgs e) {
            if(Image == null)
                DrawText(e.Graphics);
            else {
                var X = ImageLocation.X + ImageSize.Width;
                e.Graphics.DrawImage(Image,new Rectangle(ImageLocation,ImageSize));
                using(var fgbrush = new SolidBrush(ForeColor))
                using(var format = IronideConvert.ToStringFormat(TextAlign))
                    e.Graphics.DrawString(Text,Font,fgbrush,new Rectangle(
                        X,0,Width-X,Height),format);
            }
        }

        #endregion

        #region Mouse override

        protected override void OnMouseEnter(EventArgs e) {
            IsMouseEnter = true;

            base.OnMouseEnter(e);
        }

        protected override void OnMouseLeave(EventArgs e) {
            IsMouseEnter = false;

            base.OnMouseLeave(e);
        }

        protected override void OnMouseDown(MouseEventArgs e) {
            IsEntered = true;

            base.OnMouseDown(e);
        }

        protected override void OnMouseUp(MouseEventArgs e) {
            IsEntered = false;

            base.OnMouseUp(e);
        }

        #endregion

        #region Key override

        protected override void OnKeyDown(KeyEventArgs e) {
            if(e.KeyCode == Keys.Enter)
                IsEntered = true;

            base.OnKeyDown(e);
        }

        protected override void OnKeyUp(KeyEventArgs e) {
            if(e.KeyCode == Keys.Enter)
                IsEntered = false;

            base.OnKeyUp(e);
        }

        #endregion

        #region Properties

        private bool isMouseEnter = false;
        /// <summary>
        /// Mouse is hover.
        /// </summary>
        [Browsable(false)]
        [EditorBrowsable(EditorBrowsableState.Never)]
        public virtual bool IsMouseEnter {
            get => isMouseEnter;
            private set {
                if(value == isMouseEnter)
                    return;

                isMouseEnter = value;
                Invalidate();
            }
        }

        private Color hoverColor = Color.DodgerBlue;
        /// <summary>
        /// Mouse hover color.
        /// </summary>
        [Description("Mouse hover color.")]
        [DefaultValue(typeof(Color),"DodgerBlue")]
        public virtual Color HoverColor {
            get => hoverColor;
            set {
                if(value == hoverColor)
                    return;

                hoverColor = value;
                if(IsMouseEnter)
                    Invalidate();
            }
        }

        private bool isEntered = false;
        /// <summary>
        /// Is entered.
        /// </summary>
        [Browsable(false)]
        [EditorBrowsable(EditorBrowsableState.Never)]
        public virtual bool IsEntered {
            get => isEntered;
            private set {
                if(value == isEntered)
                    return;

                isEntered = value;
                Invalidate();
            }
        }

        private Color enterColor = Color.SteelBlue;
        /// <summary>
        /// Mouse entering color.
        /// </summary>
        [Description("Mouse entering color.")]
        [DefaultValue(typeof(Color),"SteelBlue")]
        public virtual Color EnterColor {
            get => enterColor;
            set {
                if(value == enterColor)
                    return;

                enterColor = value;
                if(IsEntered)
                    Invalidate();
            }
        }

        private Image image = null;
        /// <summary>
        /// Image.
        /// </summary>
        [Description("Image")]
        [DefaultValue(null)]
        public virtual Image Image {
            get => image;
            set {
                if(value == image)
                    return;

                image = value;
                Invalidate();
            }
        }

        private Point imageLocation = new Point(1,1);
        /// <summary>
        /// Location of image.
        /// </summary>
        [Description("Location of image.")]
        [DefaultValue(typeof(Size),"1;1")]
        public virtual Point ImageLocation {
            get => imageLocation;
            set {
                if(value == imageLocation)
                    return;

                imageLocation = value;
                if(Image != null)
                    Invalidate();
            }
        }

        private Size imageSize = new Size(10,10);
        /// <summary>
        /// Size of image.
        /// </summary>
        [Description("Width of image.")]
        [DefaultValue(typeof(Size),"10;10")]
        public virtual Size ImageSize {
            get => imageSize;
            set {
                if(value == imageSize)
                    return;

                imageSize = value;
                if(Image != null)
                    Invalidate();
            }
        }

        #endregion
    }
}
