using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MesserUI {
    public enum MUITypes {
        None = 0,
        Fill,
        Text,
    }

    public class MUIBase {
        private static uint idCounter;

        public MUIRectangle Bounds { get; set; }
        public MUITypes Type { get; protected set; }
        public uint ZIndex { get; set; }
        public uint IdKey { get; set; }
        public string elementName { get; set; }

        public MUIBase(MUITypes type) {
            Type = type;
            ZIndex = 0;
            IdKey = idCounter++;
        }
    }

    public class MUIFill : MUIBase {
        public MUIFill()
            : base(MUITypes.Fill) {

        }
    }

    public class MUIText : MUIBase {
        public MUIText()
            : base(MUITypes.Text) {

        }
    }
}
