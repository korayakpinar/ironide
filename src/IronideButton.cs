using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

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
                IsMouseEnter ? HoverColor : BackColor))
                e.Graphics.FillRectangle(bgbrush,ClientRectangle);

            DrawBorder(e.Graphics);
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

        #endregion
    }
}
