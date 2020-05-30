using System.Drawing;
using System.Windows.Forms;

namespace Ironide {
    /// <summary>
    /// Control template of Ironide.
    /// </summary>
    public abstract class IronideControl:Control, IIronideControl {
        #region Properties

        private Color borderColor;
        public virtual Color BorderColor {
            get => borderColor;
            set {
                if(value == borderColor)
                    return;

                borderColor = value;
                Invalidate();
            }
        }

        private int borderThickness;
        public virtual int BorderThickness {
            get => borderThickness;
            set {
                if(borderThickness == value)
                    return;

                borderThickness = value;
                Invalidate();
            }
        }

        #endregion
    }
}
