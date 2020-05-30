using System.ComponentModel;
using System.Windows.Forms;

namespace Ironide {
    /// <summary>
    /// Panel of Ironide.
    /// </summary>
    public class IronidePanel:IronideControl {
        #region Constructors

        /// <summary>
        /// Constructor.
        /// </summary>
        public IronidePanel() {

        }

        #endregion

        #region Drawing

        protected override void OnPaint(PaintEventArgs e) {
            if(TextRender)
                DrawText(e.Graphics);
            DrawEnable(e.Graphics);
        }

        #endregion

        #region Properties

        private bool textRender = false;
        /// <summary>
        /// Render text.
        /// </summary>
        [Description("Render text.")]
        [DefaultValue(false)]
        public bool TextRender {
            get => textRender;
            set {
                if(value == textRender)
                    return;

                textRender = value;
                Invalidate();
            }
        }

        #endregion
    }
}
