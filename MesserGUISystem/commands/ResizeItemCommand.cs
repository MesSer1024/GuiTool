using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MesserGUISystem.logic;
using System.Windows;
using System.Windows.Shapes;
using MesserGUISystem.utils;
using System.Windows.Controls;
using MesserUI;

namespace MesserGUISystem.commands
{
    class ResizeItemCommand : ICommand
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
