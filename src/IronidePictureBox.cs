using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace Ironide {
    /// <summary>
    /// PictureBox of Ironide.
    /// </summary>
    public class IronidePictureBox:IronidePanel {
        #region Constructors

        /// <summary>
        /// Constructor.
        /// </summary>
        public IronidePictureBox() {

        }

        #endregion

        #region Drawing

        protected override void OnPaint(PaintEventArgs e) {
            if(Image != null) {
                e.Graphics.DrawImage(Image,ClientRectangle);
            }

            base.OnPaint(e);
        }

        #endregion

        #region Properties

        public Image image = null;
        /// <summary>
        /// Image.
        /// </summary>
        [Description("Image.")]
        [DefaultValue(null)]
        public Image Image {
            get => image;
            set {
                if(value == image)
                    return;

                image = value;
                Invalidate();
            }
        }

        #endregion
    }
}
