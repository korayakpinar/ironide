using System;
using System.Drawing;
using System.Drawing.Drawing2D;
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

        /// <summary>
        /// Returns Rectangle as rounded GraphicsPath.
        /// </summary>
        /// <param name="rect">Rectangle.</param>
        /// <param name="angle">Round angle.</param>
        public static GraphicsPath ToRoundedPath(Rectangle rect,int angle) {
            var gp = new GraphicsPath();
            gp.StartFigure();
            gp.AddArc(0,0,angle,angle,180,90);
            gp.AddLine(angle,1,rect.Width - angle,0.7F);
            gp.AddArc(rect.Width - angle,0,angle,angle,270,90);
            gp.AddLine(rect.Width,angle,rect.Width,rect.Height - angle);
            gp.AddArc(rect.Width - angle,rect.Height - angle,angle,angle,0,90);
            gp.AddLine(rect.Width - angle,rect.Height,angle,rect.Height);
            gp.AddArc(0,rect.Height - angle,angle,angle,90,90);
            gp.AddLine(1,angle,1,rect.Height - angle);
            gp.CloseFigure();
            return gp;
        }

        /// <summary>
        /// Returns Rectangle as rounded Region.
        /// </summary>
        /// <param name="rect">Rectangle.</param>
        /// <param name="angle">Round angle.</param>
        public static Region ToRoundedRegion(Rectangle rect,int angle) {
            var gp = ToRoundedPath(rect,angle);
            var rg = new Region(gp);
            gp.Dispose();
            return rg;
        }
    }
}
