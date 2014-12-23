using System;

namespace Tools.GUI.Components.SimpleChart
{
	/// <summary>
	/// Summary description for CLabel.
	/// </summary>
	public class CLabel
	{
        #region members
        public delegate void RedrawHandler();
        protected RedrawHandler redraw_handler=null;

        protected System.Drawing.Font font=null;
        public System.Drawing.Font Font
        {
            get
            {
                return this.font;
            }
            set
            {
                this.font=value;
                this.ReDraw();
            }
        }


        protected System.Drawing.Brush brush=null;
        public System.Drawing.Brush Brush
        {
            get
            {
                return this.brush;
            }
            set
            {
                this.brush=value;
                this.ReDraw();
            }
        }
        protected string label="";
        public string Label
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
        protected int angle=0;
        public int Angle
        {
            get
            {
                return this.angle;
            }
            set
            {
                this.angle=value;
                this.ReDraw();
            }
        }
        protected bool visible=true;
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
        protected Tools.Drawing.CDrawString.TEXT_POSITION position_relative_to_control=Tools.Drawing.CDrawString.TEXT_POSITION.OUTSIDE_BOTTOM;
        public Tools.Drawing.CDrawString.TEXT_POSITION PositionRelativeToControl
        {
            get
            {
                return this.position_relative_to_control;
            }
            set
            {
                this.position_relative_to_control=value;
                this.ReDraw();
            }
        }
        #endregion

        public CLabel(RedrawHandler redraw_handler)
        {
            this.redraw_handler=redraw_handler;
            this.brush=new System.Drawing.SolidBrush(System.Drawing.Color.Black);
            this.font=new System.Drawing.Font("Arial",10);
        }

        protected void ReDraw()
        {
            if (this.redraw_handler!=null)
                this.redraw_handler();
        }
	}
}
