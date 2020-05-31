using System;
using System.ComponentModel;
using System.Drawing;
using System.Runtime.InteropServices;
using System.Windows.Forms;
using Ironide.Tools;

namespace Ironide {
    /// <summary>
    /// Form of Ironide.
    /// </summary>
    public partial class IronideForm:Form, IIronideControl {
        #region Fields

        private const short
            gripSize = 16,
            captionSize = 32;

        private const int
            SIZE_PROCESS = 0x84,
            WM_NCLBUTTONDOWN = 0xA1,
            HT_CAPTION = 0x2;

        #endregion

        #region Constructors

        /// <summary>
        /// Constructor.
        /// </summary>
        public IronideForm() {
            Init();
        }

        #endregion

        #region API

        [DllImport("user32.dll")]
        internal static extern int SendMessage(IntPtr hWnd,int Msg,int wParam,int lParam);
        [DllImport("user32.dll")]
        internal static extern bool ReleaseCapture();

        #endregion

        #region Drawing

        protected override void OnPaint(PaintEventArgs e) {
            DrawText(e.Graphics);
            DrawBorder(e.Graphics);
            DrawEnable(e.Graphics);
        }

        /// <summary>
        /// Draw background.
        /// </summary>
        /// <param name="graphics">Graphics.</param>
        public virtual void DrawBackground(Graphics graphics) {
            using(var bgbrush = new SolidBrush(BackColor))
                graphics.FillRectangle(bgbrush,ClientRectangle);
        }

        /// <summary>
        /// Draw enable.
        /// </summary>
        /// <param name="graphics">Graphics.</param>
        public virtual void DrawEnable(Graphics graphics) {
            if(Enabled)
                return;

            using(var enbrush = new SolidBrush(Color.FromArgb(200,Color.Gainsboro)))
                graphics.FillRectangle(enbrush,ClientRectangle);
        }

        /// <summary>
        /// Draw borders.
        /// </summary>
        /// <param name="graphics">Graphics.</param>
        public virtual void DrawBorder(Graphics graphics) {
            var bs = IronideConvert.ToButtonBorderStyle(BorderStyle);
            ControlPaint.DrawBorder(graphics,ClientRectangle,BorderColor,
                BorderThickness,bs,BorderColor,BorderThickness,bs,
                BorderColor,BorderThickness,bs,BorderColor,
                BorderThickness,bs);
        }

        /// <summary>
        /// Draw text.
        /// </summary>
        /// <param name="graphics">Graphics.</param>
        public virtual void DrawText(Graphics graphics) {
            using(var fgbrush = new SolidBrush(ForeColor))
            using(var format = IronideConvert.ToStringFormat(TextAlign))
                graphics.DrawString(Text,Font,fgbrush,
                    ShowTitlebar ?
                        new Rectangle(BorderThickness,
                        titlePanel.Location.Y+titlePanel.Height,Width - BorderThickness,
                        Height-(titlePanel.Location.Y + titlePanel.Height))
                        : new Rectangle(
                            BorderThickness,BorderThickness,
                            Width - BorderThickness,Height - BorderThickness),
                    format);
        }

        #endregion

        #region closeButton

        private void CloseButton_Click(object sender,EventArgs e) {
            Close();
        }

        #endregion

        #region maximizeButton

        private void MaximizeButton_Click(object sender,EventArgs e) {
            WindowState =
                WindowState == FormWindowState.Normal ?
                    FormWindowState.Maximized :
                    FormWindowState.Normal;
            maximizeButton.Text =
                WindowState == FormWindowState.Normal ?
                    "⬜" :
                    "❐";
        }

        #endregion

        #region minimizeButton

        private void MinimizeButton_Click(object sender,EventArgs e) {
            WindowState = FormWindowState.Minimized;
        }

        #endregion

        #region titlePanel

        private void TitlePanel_MouseMove(object sender,MouseEventArgs e) {
            if(Movable) {
                ReleaseCapture();
                SendMessage(Handle,WM_NCLBUTTONDOWN,HT_CAPTION,0);
            }
        }

        private void TitlePanel_MouseDoubleClick(object sender,MouseEventArgs e) {
            if(ResizeDoubleClick)
                if(e.Button == MouseButtons.Left) {
                    MaximizeButton_Click(null,null);
                }
        }

        #endregion

        #region System override

        protected override void WndProc(ref Message m) {
            if(m.Msg == SIZE_PROCESS && Sizable) {
                Point pos = new Point(m.LParam.ToInt32());
                pos = PointToClient(pos);
                if( // Bottom-Right
                    pos.X >= ClientSize.Width - gripSize + 10 &&
                    pos.Y >= ClientSize.Height - gripSize + 10) {
                    m.Result = (IntPtr)17;
                    return;
                } else if( // Bottom-Left
                    pos.X <= ClientRectangle.X + 20 - gripSize &&
                    pos.Y >= ClientSize.Height - gripSize + 10) {
                    m.Result = (IntPtr)16;
                    return;
                } else if( // Top-Left.
                    pos.X <= ClientRectangle.X + 25 - gripSize &&
                    pos.Y <= 10) {
                    m.Result = (IntPtr)13;
                    return;
                } else if( // Top-Right.
                    pos.X >= ClientSize.Width - gripSize + 10 &&
                    pos.Y <= 10) {
                    m.Result = (IntPtr)14;
                    return;
                } else if( // Right.
                    pos.X >= ClientSize.Width - gripSize + 10) {
                    m.Result = (IntPtr)11;
                    return;
                } else if( // Left.
                    pos.X <= ClientRectangle.X + 20 - gripSize) {
                    m.Result = (IntPtr)10;
                    return;
                } else if( // Top.
                    pos.Y <= 10) {
                    m.Result = (IntPtr)12;
                    return;
                } else if( // Bottom.
                    pos.Y >= ClientSize.Height - gripSize + 10) {
                    m.Result = (IntPtr)15;
                    return;
                }
            }

            base.WndProc(ref m);
        }

        #endregion

        #region Form override

        protected override void OnLoad(EventArgs e) {
            if(Animation == IronideFormAnimation.Fade) {
                Opacity = 0;
                Refresh();
            }

            base.OnLoad(e);
        }

        protected override void OnShown(EventArgs e) {
            if(Animation == IronideFormAnimation.Fade)
                IronideAnimator.FormFadeShow(this,(int)AnimationDelay);

            if(FocusOnLoad)
                Focus();

            base.OnShown(e);
        }

        protected override void OnFormClosing(FormClosingEventArgs e) {
            if(Animation == IronideFormAnimation.Fade)
                IronideAnimator.FormFadeHide(this,(int)AnimationDelay);

            base.OnFormClosing(e);
        }

        #endregion

        #region Size override

        protected override void OnSizeChanged(EventArgs e) {
            Invalidate();
            base.OnSizeChanged(e);
        }

        #endregion

        #region Location override

        protected override void OnLocationChanged(EventArgs e) {
            if(maximizeButton.Text != "⬜")
                maximizeButton.Text = "⬜";

            base.OnLocationChanged(e);
        }

        #endregion

        #region Methods

        /// <summary>
        /// Bring to front Titlebar.
        /// </summary>
        public void BringToFrontTitlebar() {
            iconBox.BringToFront();
            titlePanel.BringToFront();
        }

        /// <summary>
        /// Send to front Titlebar.
        /// </summary>
        public void SendToBackTitlebar() {
            iconBox.SendToBack();
            titlePanel.SendToBack();
        }

        #endregion

        #region Properties

        private string title = "Title";
        /// <summary>
        /// Title of form.
        /// </summary>
        [Description("Title of form.")]
        [DefaultValue("Title")]
        public string Title {
            get => title;
            set {
                if(value == title)
                    return;

                title=value;
                titlePanel.Text = value;
                Invalidate();
            }
        }

        private Color borderColor = Color.DodgerBlue;
        [Description("Color of border.")]
        [DefaultValue(typeof(Color),"DodgerBlue")]
        public virtual Color BorderColor {
            get => borderColor;
            set {
                if(value == borderColor)
                    return;

                borderColor = value;
                Invalidate();
            }
        }

        private int borderThickness = 1;
        [Description("Thickness of boder.")]
        [DefaultValue(1)]
        public virtual int BorderThickness {
            get => borderThickness;
            set {
                if(borderThickness == value)
                    return;

                borderThickness = value;
                iconBox.Location = new Point(value,value);
                titlePanel.Location = new Point(
                    ShowIcon ?
                        iconBox.Location.X + iconBox.Width :
                        value
                    ,value);
                titlePanel.Size = new Size(Width - titlePanel.Location.X-BorderThickness,titlePanel.Height);
                Invalidate();
            }
        }

        /// <summary>
        /// Color of background.
        /// </summary>
        [Description("Color of background.")]
        [DefaultValue(typeof(Color),"White")]
        public override Color BackColor { get => base.BackColor; set => base.BackColor=value; }

        /// <summary>
        /// Color of foreground.
        /// </summary>
        [Description("Color of foreground.")]
        [DefaultValue(typeof(Color),"Black")]
        public override Color ForeColor { get => base.ForeColor; set => base.ForeColor=value; }

        /// <summary>
        /// Form content.
        /// </summary>
        [Description("Form content.")]
        [DefaultValue("")]
        public override string Text {
            get => base.Text;
            set {
                if(value == base.Text)
                    return;

                base.Text=value;
                Invalidate();
            }
        }

        private IronideBorderStyle borderStyle = IronideBorderStyle.Solid;
        [Description("Style of border.")]
        [DefaultValue(typeof(IronideBorderStyle),"Solid")]
        public virtual IronideBorderStyle BorderStyle {
            get => borderStyle;
            set {
                if(value == borderStyle)
                    return;

                borderStyle = value;
                Invalidate();
            }
        }

        private ContentAlignment textAlign = ContentAlignment.TopLeft;
        [Description("Align of text.")]
        [DefaultValue(typeof(ContentAlignment),"TopLeft")]
        public virtual ContentAlignment TextAlign {
            get => textAlign;
            set {
                if(value == textAlign)
                    return;

                textAlign = value;
                if(!string.IsNullOrEmpty(Text))
                    Invalidate();
            }
        }

        [Browsable(false)]
        [EditorBrowsable(EditorBrowsableState.Never)]
        public new FormBorderStyle FormBorderStyle {
            get => base.FormBorderStyle;
            private set => base.FormBorderStyle = value;
        }

        [Browsable(false)]
        [EditorBrowsable(EditorBrowsableState.Never)]
        public new bool HelpButton {
            get => base.HelpButton;
            private set => base.HelpButton = value;
        }

        /// <summary>
        /// Show form icon.
        /// </summary>
        [Description("Show form icon.")]
        [DefaultValue(true)]
        public new bool ShowIcon {
            get => base.ShowIcon;
            set {
                if(value == base.ShowIcon)
                    return;

                base.ShowIcon = value;
                if(value)
                    titlePanel.Location = new Point(iconBox.Location.X+iconBox.Width,1);
                else
                    titlePanel.Location = new Point(BorderThickness,BorderThickness);
                titlePanel.Size = new Size(Width - titlePanel.Location.X-BorderThickness,titlePanel.Height);
            }
        }

        /// <summary>
        /// Icon of form.
        /// </summary>
        [Description("Icon of form.")]
        public new Icon Icon {
            get => base.Icon;
            set {
                if(value == base.Icon)
                    return;

                base.Icon = value;
                iconBox.Image = value.ToBitmap();
            }
        }

        /// <summary>
        /// Color of Titlebar background.
        /// </summary>
        [Description("Color of Titlebar background.")]
        [DefaultValue(typeof(Color),"Gainsboro")]
        public Color TitleBarBackColor {
            get => titlePanel.BackColor;
            set {
                titlePanel.BackColor=value;
                iconBox.BackColor=value;
                closeButton.BackColor=value;
                maximizeButton.BackColor=value;
                minimizeButton.BackColor=value;
            }
        }

        /// <summary>
        /// Color of Titlebar foreground.
        /// </summary>
        [Description("Color of Titlebar foreground.")]
        [DefaultValue(typeof(Color),"Black")]
        public Color TitlebarForeColor {
            get => titlePanel.ForeColor;
            set {
                titlePanel.ForeColor=value;
                closeButton.ForeColor=value;
                maximizeButton.ForeColor=value;
                minimizeButton.ForeColor=value;
            }
        }

        /// <summary>
        /// Color of CloseBox hover.
        /// </summary>
        [Description("Color of CloseBox hover.")]
        [DefaultValue(typeof(Color),"LightCoral")]
        public Color CloseBoxHoverColor {
            get => closeButton.HoverColor;
            set => closeButton.HoverColor=value;
        }

        /// <summary>
        /// Color of CloseBox enter.
        /// </summary>
        [Description("Color of CloseBox enter.")]
        [DefaultValue(typeof(Color),"Red")]
        public Color CloseBoxEnterColor {
            get => closeButton.EnterColor;
            set => closeButton.EnterColor=value;
        }

        public new bool MaximizeBox {
            get => base.MaximizeBox;
            set {
                base.MaximizeBox = value;
                maximizeButton.Visible=value;
                maximizeButton.Location = new Point(
                    closeButton.Location.X-minimizeButton.Width,0);
                if(MinimizeBox && !value) {
                    minimizeButton.Location = new Point(
                        closeButton.Location.X-minimizeButton.Width,0);
                }
            }
        }

        /// <summary>
        /// Color of MaximizeBox hover.
        /// </summary>
        [Description("Color of MaximizeBox hover.")]
        [DefaultValue(typeof(Color),"DeepSkyBlue")]
        public Color MaximizeBoxHoverColor {
            get => maximizeButton.HoverColor;
            set => maximizeButton.HoverColor=value;
        }

        /// <summary>
        /// Color of MaximizeBox enter.
        /// </summary>
        [Description("Color of MaximizeBox enter.")]
        [DefaultValue(typeof(Color),"DodgerBlue")]
        public Color MaximizeBoxEnterColor {
            get => maximizeButton.EnterColor;
            set => maximizeButton.EnterColor=value;
        }

        public new bool MinimizeBox {
            get => base.MinimizeBox;
            set {
                base.MinimizeBox = value;
                minimizeButton.Visible=value;
                if(MaximizeBox) {
                    minimizeButton.Location = new Point(
                        maximizeButton.Location.X-minimizeButton.Width,0);
                } else {
                    minimizeButton.Location = new Point(
                        closeButton.Location.X-minimizeButton.Width,0);
                }
            }
        }

        /// <summary>
        /// Color of MinimizeBox hover.
        /// </summary>
        [Description("Color of MinimizeBox hover.")]
        [DefaultValue(typeof(Color),"DeepSkyBlue")]
        public Color MinimizeBoxHoverColor {
            get => minimizeButton.HoverColor;
            set => minimizeButton.HoverColor=value;
        }

        /// <summary>
        /// Color of MinimizeBox enter.
        /// </summary>
        [Description("Color of MinimizeBox enter.")]
        [DefaultValue(typeof(Color),"DodgerBlue")]
        public Color MinimizeBoxEnterColor {
            get => minimizeButton.EnterColor;
            set => minimizeButton.EnterColor=value;
        }

        /// <summary>
        /// Movable form.
        /// </summary>
        [Description("Movable form.")]
        [DefaultValue(true)]
        public bool Movable { get; set; } = true;

        /// <summary>
        /// Resize form with double click on Titlebar.
        /// </summary>
        [Description("Resize form with double click on Titlebar.")]
        [DefaultValue(true)]
        public bool ResizeDoubleClick { get; set; } = true;

        /// <summary>
        /// Show-Hide Animation of Form.
        /// </summary>
        [Description("Show-Hide Animation of Form.")]
        [DefaultValue(typeof(IronideFormAnimation),"None")]
        public IronideFormAnimation Animation { get; set; } = IronideFormAnimation.None;

        /// <summary>
        /// Frame delay of Fade animation.
        /// </summary>
        [Description("Frame delay of Fade animation.")]
        [DefaultValue(25)]
        public uint AnimationDelay { get; set; } = 25;

        /// <summary>
        /// Focus to form on form showing.
        /// </summary>
        [Description("Focus to form on form showing.")]
        [DefaultValue(true)]
        public bool FocusOnLoad { get; set; } = true;

        private bool showTitlebar = true;
        /// <summary>
        /// Show Titlebar.
        /// </summary>
        [Description("Show Titlebar.")]
        [DefaultValue(true)]
        public bool ShowTitlebar {
            get => showTitlebar;
            set {
                if(value == ShowTitlebar)
                    return;

                showTitlebar = value;
                iconBox.Visible = value;
                titlePanel.Visible = value;
                if(Text != "")
                    Invalidate();
            }
        }

        /// <summary>
        /// Sizable form.
        /// </summary>
        [Description("Sizable form.")]
        [DefaultValue(true)]
        public bool Sizable { get; set; } = true;

        #endregion
    }

    // Designer.
    public partial class IronideForm {
        #region Components

        private IronidePictureBox
            iconBox;

        private IronidePanel
            titlePanel;

        private IronideButton
            closeButton,
            maximizeButton,
            minimizeButton;

        #endregion

        /// <summary>
        /// Initialize component.
        /// </summary>
        protected virtual void Init() {
            #region Base

            SetStyle(
                ControlStyles.ResizeRedraw |
                ControlStyles.AllPaintingInWmPaint |
                ControlStyles.OptimizedDoubleBuffer
                ,true);

            StartPosition = FormStartPosition.CenterScreen;
            FormBorderStyle = FormBorderStyle.None;
            ForeColor = Color.Black;
            BackColor = Color.White;
            Size = new Size(800,450);

            #endregion

            #region titlePanel

            titlePanel = new IronidePanel();
            titlePanel.BackColor = Color.Gainsboro;
            titlePanel.BorderThickness = 0;
            titlePanel.TextRender = true;
            titlePanel.Location = new Point(26,1);
            titlePanel.TextAlign = ContentAlignment.MiddleLeft;
            titlePanel.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            titlePanel.Size = new Size(Width-27,25);
            titlePanel.Text = Title;
            titlePanel.MouseMove+=TitlePanel_MouseMove;
            titlePanel.MouseDoubleClick+=TitlePanel_MouseDoubleClick;
            Controls.Add(titlePanel);

            #endregion

            #region iconBox

            iconBox = new IronidePictureBox();
            iconBox.Location = new Point(1,1);
            iconBox.Size = new Size(titlePanel.Location.X,titlePanel.Height);
            iconBox.BorderThickness = 0;
            iconBox.BackColor = titlePanel.BackColor;
            iconBox.Image = Icon.ToBitmap();
            iconBox.SizeMode = IronideImageSizeMode.Stretch;
            Controls.Add(iconBox);

            #endregion

            #region closeButton

            closeButton = new IronideButton();
            closeButton.BorderThickness = 0;
            closeButton.BackColor = titlePanel.BackColor;
            closeButton.ForeColor = titlePanel.ForeColor;
            closeButton.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Right;
            closeButton.Text = "X";
            closeButton.HoverColor = Color.LightCoral;
            closeButton.EnterColor = Color.Red;
            closeButton.TextAlign = ContentAlignment.MiddleCenter;
            closeButton.Size = new Size(30,titlePanel.Height);
            closeButton.Location = new Point(
                titlePanel.Width-closeButton.Width,0);
            closeButton.Click +=CloseButton_Click;
            titlePanel.Controls.Add(closeButton);

            #endregion

            #region maximizeButton

            maximizeButton = new IronideButton();
            maximizeButton.BorderThickness = 0;
            maximizeButton.BackColor = titlePanel.BackColor;
            maximizeButton.ForeColor = titlePanel.ForeColor;
            maximizeButton.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Right;
            maximizeButton.Text = "⬜";
            maximizeButton.HoverColor = Color.DeepSkyBlue;
            maximizeButton.EnterColor = Color.DodgerBlue;
            maximizeButton.TextAlign = ContentAlignment.MiddleCenter;
            maximizeButton.Size = new Size(30,titlePanel.Height);
            maximizeButton.Location = new Point(
                closeButton.Location.X-maximizeButton.Width,0);
            maximizeButton.Click +=MaximizeButton_Click;
            titlePanel.Controls.Add(maximizeButton);

            #endregion

            #region minimizeButton

            minimizeButton = new IronideButton();
            minimizeButton.BorderThickness = 0;
            minimizeButton.BackColor = titlePanel.BackColor;
            minimizeButton.ForeColor = titlePanel.ForeColor;
            minimizeButton.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Right;
            minimizeButton.Text = "̶";
            minimizeButton.HoverColor = Color.DeepSkyBlue;
            minimizeButton.EnterColor = Color.DodgerBlue;
            minimizeButton.TextAlign = ContentAlignment.MiddleCenter;
            minimizeButton.Size = new Size(30,titlePanel.Height);
            minimizeButton.Location = new Point(
                maximizeButton.Location.X-minimizeButton.Width,0);
            minimizeButton.Click +=MinimizeButton_Click;
            titlePanel.Controls.Add(minimizeButton);

            #endregion
        }
    }

    /// <summary>
    /// Form animation of Ironide.
    /// </summary>
    public enum IronideFormAnimation {
        None = 0,
        Fade = 1
    }
}
