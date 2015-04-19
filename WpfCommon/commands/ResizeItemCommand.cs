using MesserUI;
using WpfCommon;
using System.Windows.Controls;
using System.Windows.Shapes;
using System.Windows;

namespace WpfCommon.commands
{
    public class ResizeItemCommand : ICommand
    {
        public UserActions Action
        {
            get { return UserActions.USER_RESIZE_ITEM; }
        }

        Rect castToRect(MUIRectangle r)
        {
            return new Rect(r.X, r.Y, r.W, r.H);
        }

        public void execute()
        {
            Element.MUIBounds = castToRect(Target);
            //Element.Width = Target.W;
            //Element.Height = Target.H;
            //Canvas.SetLeft(Element.ShapeWPF, Target.X);
            //Canvas.SetTop(Element.ShapeWPF, Target.Y);
        }

        public void revert()
        {
            Element.MUIBounds = castToRect(Original);
            //Element.Width = Original.W;
            //Element.Height = Original.H;
            //Canvas.SetLeft(Element.ShapeWPF, Original.X);
            //Canvas.SetTop(Element.ShapeWPF, Original.Y);        
        }

        public ResizeItemCommand(IManualMUIObject ele, MUIRectangle original, MUIRectangle target) {
            Assert.True(ele);
            Element = ele;
            Original = original;
            Target = target;

            execute();
        }

        public MUIRectangle Target { get; set; }
        public MUIRectangle Original { get; set; }
        public IManualMUIObject Element { get; set; }
    }
}
