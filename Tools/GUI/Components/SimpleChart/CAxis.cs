using System;

namespace Tools.GUI.Components.SimpleChart
{
	/// <summary>
	/// Summary description for CAxis.
	/// </summary>
	public class CAxis
	{
        #region members

        public delegate void RedrawHandler();
        protected RedrawHandler redraw_handler=null;

        private CLabel label=null;
        public CLabel Label
        {
            get
            {
                return this.label;
            }
            set
            {
                this.label=value;
                this.ReDraw();
            }
        }
        private bool visible=true;
        public bool Visible
        {
            get
            {
                return this.visible;
            }
            set
            {
                this.visible=value;
                this.ReDraw();
            }
        }
        private System.Drawing.Pen pen=null;
        public System.Drawing.Pen Pen
        {
            get
            {
                return this.pen;
            }
            set
            {
                this.pen=value;
                this.ReDraw();
            }
        }
        #endregion

        public CAxis(RedrawHandler redraw_handler)
        {
            this.pen=new System.Drawing.Pen(System.Drawing.Color.Black,2);
            this.pen.CustomEndCap = new System.Drawing.Drawing2D.AdjustableArrowCap(6, 6, false);
            this.label=new CLabel(new CLabel.RedrawHandler(redraw_handler));
            this.redraw_handler=redraw_handler;
        }
        protected void ReDraw()
        {
            if (this.redraw_handler!=null)
                this.redraw_handler();
        }
	}
}
