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
        Color BackColor { get; set; }

        /// <summary>
        /// Color of foreground.
        /// </summary>
        Color ForeColor { get; set; }

        /// <summary>
        /// Color of border.
        /// </summary>
        Color BorderColor { get; set; }

        /// <summary>
        /// Thickness of border.
        /// </summary>
        int BorderThickness { get; set; }

        #endregion
    }
}
