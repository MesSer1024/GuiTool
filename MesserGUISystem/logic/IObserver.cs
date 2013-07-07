using MesserGUISystem.utils;

namespace MesserGUISystem.logic {
    public interface IObserver {
        void onMessage(UserActions action, object data);
    }
}
