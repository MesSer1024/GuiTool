using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Documents;
using System.Windows.Media;
using System.Windows;

namespace MesserGUISystem.wpf_magic {
    class SelectionAdorner : Adorner {
        public SelectionAdorner(UIElement ele)
            : base(ele) {
            this.IsHitTestVisible = false;
        }

        protected override void OnRender(DrawingContext drawingContext) {
            Rect adornedElementRect = new Rect(this.AdornedElement.DesiredSize);
            var selectionOffset = 2d;
            adornedElementRect.X -= selectionOffset;
            adornedElementRect.Y -= selectionOffset;
            adornedElementRect.Width += 2*selectionOffset;
            adornedElementRect.Height += 2*selectionOffset;

            // Some arbitrary drawing implements.
            SolidColorBrush renderBrush = new SolidColorBrush(Colors.Green);
            renderBrush.Opacity = 0.2;
            Pen renderPen = new Pen(new SolidColorBrush(Colors.Navy), 1.5);
            double renderRadius = 5.0;

            // Draw a circle at each corner.
            //drawingContext.DrawLine(renderPen, adornedElementRect.TopLeft, adornedElementRect.TopRight);
            //drawingContext.DrawLine(renderPen, adornedElementRect.TopRight, adornedElementRect.BottomRight);
            //drawingContext.DrawLine(renderPen, adornedElementRect.BottomRight, adornedElementRect.BottomLeft);
            //drawingContext.DrawLine(renderPen, adornedElementRect.BottomLeft, adornedElementRect.TopLeft);

            drawingContext.DrawRectangle(renderBrush, renderPen, adornedElementRect);
        }
    }
}
