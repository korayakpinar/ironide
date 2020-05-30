using System;
using System.Drawing;
using System.Windows.Forms;

namespace Ironide.Tools {
    /// <summary>
    /// Converter of Ironide.
    /// </summary>
    public static class IronideConvert {
        /// <summary>
        /// Returns IronideBorderStyle as ButtonBorderStyle.
        /// </summary>
        /// <param name="style">Style to convert.</param>
        public static ButtonBorderStyle ToButtonBorderStyle(IronideBorderStyle style) {
            return
                (ButtonBorderStyle)
                Enum.Parse(typeof(ButtonBorderStyle),style.ToString());
        }

        /// <summary>
        /// Returns ContentAlignment as StringFormat.
        /// </summary>
        /// <param name="align">Align to convert.</param>
        public static StringFormat ToStringFormat(ContentAlignment align) {
            var format = new StringFormat();
            if(align == ContentAlignment.TopLeft) {
                format.Alignment = StringAlignment.Near;
                format.LineAlignment = StringAlignment.Near;
            } else if(align == ContentAlignment.TopCenter) {
                format.Alignment = StringAlignment.Center;
                format.LineAlignment = StringAlignment.Near;
            } else if(align == ContentAlignment.TopRight) {
                format.Alignment = StringAlignment.Far;
                format.LineAlignment = StringAlignment.Near;
            } else if(align == ContentAlignment.MiddleLeft) {
                format.Alignment = StringAlignment.Near;
                format.LineAlignment = StringAlignment.Center;
            } else if(align == ContentAlignment.MiddleCenter) {
                format.Alignment = StringAlignment.Center;
                format.LineAlignment = StringAlignment.Center;
            } else if(align == ContentAlignment.MiddleRight) {
                format.Alignment = StringAlignment.Far;
                format.LineAlignment = StringAlignment.Center;
            } else if(align == ContentAlignment.BottomLeft) {
                format.Alignment = StringAlignment.Near;
                format.LineAlignment = StringAlignment.Far;
            } else if(align == ContentAlignment.BottomCenter) {
                format.Alignment = StringAlignment.Center;
                format.LineAlignment = StringAlignment.Far;
            } else {
                format.Alignment = StringAlignment.Far;
                format.LineAlignment = StringAlignment.Far;
            }

            return format;
        }
    }
}
