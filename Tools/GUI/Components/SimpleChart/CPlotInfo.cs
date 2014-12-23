using System;

namespace Tools.GUI.Components.SimpleChart
{
	/// <summary>
	/// Summary description for CPlotInfo.
	/// </summary>
	public class CPlotInfo:CLabel
	{
        private double plot_value=0;
        public double Value
        {
            get
            {
                return this.plot_value;
            }
            set
            {
                this.plot_value=value;
                this.ReDraw();
            }
        }

		public CPlotInfo(RedrawHandler redraw_handler):base(redraw_handler)
		{
		}
        public CPlotInfo(RedrawHandler redraw_handler,double plot_value):base(redraw_handler)
        {
            this.plot_value=plot_value;
        }
        public CPlotInfo(RedrawHandler redraw_handler,double plot_value,string plot_label):base(redraw_handler)
        {
            this.label=plot_label;
            this.plot_value=plot_value;
        }
	}
}
