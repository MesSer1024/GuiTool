using System;
using System.Collections.Generic;

namespace WpfCommon {

    public enum UserActions {
        MOVE_TOOL,
        LMB_DOWN,
        LMB_RELEASE,
        LMB_DOWN_MOUSE_MOVED,
        MUIELEMENT_SELECTED_VALID,
        MOVE_ITEM_BEGIN,
        COMMAND_MOVE_ITEM_END,
        USER_UNDO,
        USER_REDO,
        USER_RESIZE_ITEM,
        USER_PRESS_ESCAPE_TEXTBOX,
        USER_REFRESH_PROPERTIES,
        COMMAND_CREATE_MUI_ITEM,
        CREATE_ITEM_SHOW_OVERLAY_SCREEN,
        CREATE_ITEM_ABORT,
        CREATE_ITEM_OBJECT_SELECTED,
        MUIELEMENT_DESELECTED,
        MUILAYER_VISIBILITY_CHANGED,
        COMMAND_CREATE_MUI_LAYER,
    }

    public interface IObserver {
        void onMessage(UserActions action, object data);
    }

    public interface ICommand {
        UserActions Action { get; }

        void execute();
        void revert();
    }

    public class Controller {
        private static Controller _instance = new Controller();

        public static Controller Instance {
            get { return Controller._instance; }
        }
        private Controller(){}

        private static List<IObserver> _observers = new List<IObserver>();

        public static void handle(ICommand cmd) {
            handle(cmd.Action, cmd);
        }

        public static void handle(UserActions action, Object e = null) {
            foreach (var i in _observers) {
                i.onMessage(action, e);
            }
        }

        public void addObserver(IObserver o) {
            _observers.Add(o);
        }

        public void removeObserver(IObserver o)
        {
            _observers.Remove(o);
        }
    }
}
