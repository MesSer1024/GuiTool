using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using MesserGUISystem.utils;

namespace MesserGUISystem.commands
{
    public class Bounds
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
                Assert.True(value > 0);
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
                Assert.True(value > 0);
                _w = value;
            }
        }

        public double Right { get { return _x + _w; } }
        public double Bottom { get { return _y + _h; } }
        public Point Center { get { return new Point(_x + _w/2, _y + _h/2); } }

        public Bounds()
        {
            _x = _y = _w = _h = 0;
        }

        public Bounds(double x, double y, double w, double h)
        {
            _x = x;
            _y = y;
            _w = w;
            _h = h;
        }
    }
}
