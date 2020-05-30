using System.Drawing;

namespace Ironide {
    /// <summary>
    /// Interface for Ironide controls.
    /// </summary>
    public interface IIronideControl {
        #region Properties

        Color BackColor { get; set; }
        Color ForeColor { get; set; }

        /// <summary>
        /// Style of border.
        /// </summary>
        IronideBorderStyle BorderStyle { get; set; }

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
