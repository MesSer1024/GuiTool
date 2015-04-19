using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Input;
using MesserGUISystem.logic;
using System.Windows.Controls;
using System.Windows.Media;
using WpfCommon;

namespace MesserGUISystem.tools {
    class SelectTool : ToolBase {
        private UIElement _selectedObject;
        private int _originalZIndex;

        public SelectTool():base(Tools.Select) {
            
        }

        public override void lmbBegin(Point point) {

            var foo = Stage.HittestItems(point);
            _selectedObject = foo as UIElement;
            Controller.handle(UserActions.MUIELEMENT_DESELECTED);
            if (Globals.isValidObject(_selectedObject)) {
                _originalZIndex = Canvas.GetZIndex(_selectedObject);
                Controller.handle(UserActions.MUIELEMENT_SELECTED_VALID, _selectedObject);
            }
        }

        public override void destroyed()
        {
            if (_selectedObject != null)
                Canvas.SetZIndex(_selectedObject, _originalZIndex);
        }
    }
}
