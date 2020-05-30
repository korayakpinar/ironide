using System.ComponentModel;
using System.Drawing;

namespace Ironide {
    /// <summary>
    /// Interface for Ironide controls.
    /// </summary>
    public interface IIronideControl {
        #region Properties

        /// <summary>
        /// Color of background.
        /// </summary>
        [Description("Color of background.")]
        Color BackColor { get; set; }

        /// <summary>
        /// Color of foreground.
        /// </summary>
        [Description("Color of foreground.")]
        Color ForeColor { get; set; }

        /// <summary>
        /// Color of border.
        /// </summary>
        [Description("Color of border.")]
        Color BorderColor { get; set; }

        /// <summary>
        /// Thickness of border.
        /// </summary>
        [Description("Thickness of boder.")]
        int BorderThickness { get; set; }

        #endregion
    }
}
