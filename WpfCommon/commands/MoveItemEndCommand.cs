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
            var foo = Element as IManualMUIObject;
            var bounds = foo.MUIBounds;
            bounds.X = TargetPosition.X;
            bounds.Y = TargetPosition.Y;
            foo.MUIBounds = bounds;
        }

        public void revert()
        {
            var foo = Element as IManualMUIObject;
            var bounds = foo.MUIBounds;
            bounds.X = OriginalPosition.X;
            bounds.Y = OriginalPosition.Y;
            foo.MUIBounds = bounds;
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
