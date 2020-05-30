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
                if(SizeMode == IronideImageSizeMode.None)
                    e.Graphics.DrawImage(Image,Point.Empty);
                else
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
        public virtual Image Image {
            get => image;
            set {
                if(value == image)
                    return;

                image = value;
                Invalidate();
            }
        }

        private IronideImageSizeMode sizeMode = IronideImageSizeMode.None;
        /// <summary>
        /// Size mode of Image.
        /// </summary>
        [Description("Size mode of Image.")]
        [DefaultValue(typeof(IronideImageSizeMode),"None")]
        public virtual IronideImageSizeMode SizeMode {
            get => sizeMode;
            set {
                if(value == sizeMode)
                    return;

                sizeMode = value;
                if(Image != null)
                    Invalidate();
            }
        }

        #endregion
    }

    /// <summary>
    /// Image size mode of Ironide.
    /// </summary>
    public enum IronideImageSizeMode {
        None = 0,
        Stretch = 1
    }
}
