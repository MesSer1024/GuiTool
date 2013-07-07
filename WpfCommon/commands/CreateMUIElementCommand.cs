using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using WpfCommon;
using MesserUI;
using System.Windows.Media;

namespace WpfCommon.commands {
    public class CreateMUIElementCommand : ICommand {
        public UserActions Action {
            get { return UserActions.COMMAND_CREATE_MUI_ITEM; }
        }

        public void execute() {
            Stage.addItem(WpfElement);
        }

        public void revert() {
            Stage.removeItem(WpfElement);
        }

        public CreateMUIElementCommand(UIElement ele, MUILayer layer) {
            Assert.True(ele);
            WpfElement = ele;
            Bounds = Globals.getBounds(ele);
            Type = Globals.getMUIType(ele);
            Layer = layer;
            MUIElement = MUIUtils.createInstanceFromType(Type, Bounds);

            execute();
        }

        public UIElement WpfElement { get; private set; }
        public MUIRectangle Bounds { get; private set; }
        public MUITypes Type { get; private set; }
        public MUIElement MUIElement { get; private set; }
        public MUILayer Layer { get; private set; }
    }
}
