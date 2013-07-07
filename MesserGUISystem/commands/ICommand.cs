using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MesserGUISystem.utils;

namespace MesserGUISystem.commands
{
    public interface ICommand
    {
        UserActions Action { get; }

        void execute();
        void revert();
    }
}
