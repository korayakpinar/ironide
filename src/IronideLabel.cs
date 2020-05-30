namespace Ironide {
    /// <summary>
    /// Label of Ironide.
    /// </summary>
    public class IronideLabel:IronideControl {
        #region Constructors

        /// <summary>
        /// Constructor.
        /// </summary>
        public IronideLabel() { }

        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="text">Text of label.</param>
        public IronideLabel(string text) {
            Text = text;
        }

        #endregion
    }
}
