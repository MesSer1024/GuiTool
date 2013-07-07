using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Input;
using MesserGUISystem.utils;
using MesserGUISystem.logic;
using System.Windows.Controls;
using System.Windows.Media;

namespace MesserGUISystem.tools {
    class SelectTool : ToolBase {
        private UIElement _selectedObject;

        public SelectTool():base(Tools.Select) {
            
        }

        public override void lmbBegin(Point point) {
            var foo = Stage.HittestItems(point);
            _selectedObject = foo as UIElement;
            Controller.handle(UserActions.OBJECT_CLICKED, _selectedObject);
            Logger.log("hitTestObject:" + foo);
        }
    }
}
