using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using MesserUI;
using MesserGUISystem.utils;
using MesserGUISystem.logic;

namespace MesserGUISystem.commands {
    class CreateMUIElementCommand : ICommand {
        public UserActions Action {
            get { return UserActions.USER_CREATE_ITEM; }
        }

        public void execute() {
            Stage.addItem(WpfElement);
        }

        public void revert() {
            Stage.removeItem(WpfElement);
        }

        public CreateMUIElementCommand(UIElement ele) {
            Assert.True(ele);
            WpfElement = ele;
            Bounds = utils.Globals.getBounds(ele);
            Type = utils.Globals.getMUIType(ele);
            MUIElement = MUIUtils.createInstanceFromType(Type, Bounds);
        }

        public UIElement WpfElement { get; set; }
        public MUIRectangle Bounds { get; set; }
        public MUITypes Type { get; set; }
        public MUIBase MUIElement { get; set; }
    }
}
