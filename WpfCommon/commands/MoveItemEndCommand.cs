using System.Windows.Controls;
using System.Windows;
using WpfCommon;
using System.Windows.Media;

namespace WpfCommon.commands
{
    public class MoveItemEndCommand : ICommand
    {
        #region ICommand Members

        public UserActions Action
        {
            get { return UserActions.COMMAND_MOVE_ITEM_END; }
        }

        public void execute()
        {
            Canvas.SetLeft(Element, TargetPosition.X);
            Canvas.SetTop(Element, TargetPosition.Y);
        }

        public void revert()
        {
            Canvas.SetLeft(Element, OriginalPosition.X);
            Canvas.SetTop(Element, OriginalPosition.Y);
        }

        #endregion

        public MoveItemEndCommand(UIElement ele, Point original, Point target)
        {
            Element = ele;
            OriginalPosition = original;
            TargetPosition = target;

            execute();
        }

        public Point OriginalPosition { get; set; }
        public UIElement Element { get; set; }
        public Point TargetPosition { get; set; }
    }
}
