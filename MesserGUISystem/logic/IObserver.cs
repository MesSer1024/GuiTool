using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MesserGUISystem.logic {
    public interface IObserver {
        void onMessage(MesserGUISystem.logic.Controller.UserActions action, object data);
    }
}
