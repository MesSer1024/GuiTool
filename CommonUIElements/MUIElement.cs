using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MesserUI {
    public enum MUITypes {
        None = 0,
        Layer,
        Fill,
        Text,
    }

    public class MUIBase {
        private static uint idCounter = 0;
        public uint IdKey { get; private set; }
        public MUIBase Parent { get; private set; }
        public MUITypes Type { get; private set; }

        public MUIBase(MUITypes type) {
            Type = type;
            IdKey = ++idCounter;
        }
    }

    public class MUIElement : MUIBase {
        public MUIRectangle Bounds { get; set; }
        public uint ZIndex { get; set; }
        public uint LayerParent { get; set; }
        public string elementName { get; set; }

        public MUIElement(MUITypes type)
            : base(type) {
            ZIndex = 0;
        }
    }

    public class MUILayer : MUIBase {
        public List<MUIElement> Items { get; set; }
        public string Name { get; set; }

        public void add(MUIElement item) {
            Items.Add(item);
        }

        public void remove(MUIElement item) {
            Items.Remove(item);
        }

        public MUILayer()
            : base(MUITypes.Layer) {
            Name = "New Layer";
            Items = new List<MUIElement>();
        }
    }

    public class MUIFill : MUIElement {
        public MUIFill()
            : base(MUITypes.Fill) {
            
        }
    }

    public class MUIText : MUIElement {
        public MUIText()
            : base(MUITypes.Text) {

        }
    }
}
