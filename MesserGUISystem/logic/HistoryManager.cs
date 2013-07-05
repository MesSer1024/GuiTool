using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MesserGUISystem.commands;
using MesserGUISystem.utils;

namespace MesserGUISystem.logic
{
    class HistoryManager : IObserver
    {
        private List<ICommand> _performedCommands;
        private int _timeInHistory;

        public HistoryManager()
        {
            _performedCommands = new List<ICommand>();
            Controller.Instance.addObserver(this);
            _timeInHistory = 0;
        }

        ~HistoryManager()
        {
            Controller.Instance.removeObserver(this);
        }

        public void onMessage(Controller.UserActions action, object data)
        {
            switch (action)
            {
                case Controller.UserActions.MOVE_ITEM_END:
                    {
                        var cmd = data as MoveItemEndCommand;
                        addToHistory(cmd);
                    }
                    break;
                case Controller.UserActions.USER_UNDO:
                    undo();
                    break;
                case Controller.UserActions.USER_REDO:
                    redo();
                    break;
                case Controller.UserActions.USER_RESIZE_ITEM:
                    {
                        var cmd = data as ResizeItemCommand;
                        addToHistory(cmd);
                    }
                    break;
            }
        }

        private void restoreToPointInHistory(int index)
        {
            Assert.Validate(_timeInHistory > index, String.Format("Restore History to invalid point int time current={0} target={1}", _timeInHistory, index));
            while (_timeInHistory > index)
            {
                undo();
            }
        }

        private void addToHistory(ICommand cmd)
        {
            if (cmd != null)
            {
                //remove any commands no longer suppposed to be in history
                if (_timeInHistory < _performedCommands.Count)
                {
                    _performedCommands.RemoveRange(_timeInHistory , _performedCommands.Count);
                }
                _performedCommands.Add(cmd);
                _timeInHistory = _performedCommands.Count;
            }
        }

        private void undo()
        {
            if (_timeInHistory < 1)
            {
                Logger.error(String.Format("Can't undo on index {0}", _timeInHistory));
                return;
            }
            var cmd = _performedCommands[--_timeInHistory];
            cmd.revert();
        }

        private void redo()
        {
            if (_timeInHistory >= _performedCommands.Count)
            {
                Logger.error(String.Format("Can't redo on index {0}", _timeInHistory));
                return;
            }
            var cmd = _performedCommands[_timeInHistory++];
            cmd.execute();
        }
    }
}
