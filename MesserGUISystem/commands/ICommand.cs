using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MesserGUISystem.logic;

namespace MesserGUISystem.commands
{
    public interface ICommand
    {
        Controller.UserActions Action { get; }

        void execute();
        void revert();
    }
}
