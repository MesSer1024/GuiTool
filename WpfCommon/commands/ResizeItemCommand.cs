using MesserUI;
using WpfCommon;
using System.Windows.Controls;
using System.Windows.Shapes;

namespace WpfCommon.commands
{
    public class ResizeItemCommand : ICommand
    {
        public UserActions Action
        {
            get { return UserActions.USER_RESIZE_ITEM; }
        }

        public void execute()
        {
            Element.Width = Target.W;
            Element.Height = Target.H;
            Canvas.SetLeft(Element, Target.X);
            Canvas.SetTop(Element, Target.Y);
        }

        public void revert()
        {
            Element.Width = Original.W;
            Element.Height = Original.H;
            Canvas.SetLeft(Element, Original.X);
            Canvas.SetTop(Element, Original.Y);
        }

        public ResizeItemCommand(Shape ele, MUIRectangle original, MUIRectangle target) {
            Assert.True(ele);
            Element = ele;
            Original = original;
            Target = target;

            execute();
        }

        public MUIRectangle Target { get; set; }
        public MUIRectangle Original { get; set; }
        public Shape Element { get; set; }
    }
}
