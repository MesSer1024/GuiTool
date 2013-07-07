using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MesserUI;

namespace MesserUI
{
    public class MUIRectangle
    {
        private double _x;
        private double _y;
        private double _w;
        private double _h;

        public double X { get { return _x; } }
        public double Y { get { return _y; } }
        public double W { 
            get { return _w; } 
            set {
                if (value < 0)
                    throw new ArgumentException("Expected width to be non-negative");
                _w = value;
            }
        }

        public double Width
        {
            get { return _w; }
            set
            {
                W = value;
            }
        }
        public double Height
        {
            get { return _h; }
            set { H = value; }
        }


        public double H { 
            get { return _h; }
            set
            {
                if (value < 0)
                    throw new ArgumentException("Expected height to be non-negative");
                _h = value;
            }
        }

        public double Right { get { return _x + _w; } }
        public double Bottom { get { return _y + _h; } }

        public MUIRectangle()
        {
            _x = _y = _w = _h = 0;
        }

        public MUIRectangle(double x, double y, double w, double h)
        {
            _x = x;
            _y = y;
            _w = w;
            _h = h;
        }
    }
}
