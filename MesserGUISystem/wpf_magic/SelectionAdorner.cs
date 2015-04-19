using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Documents;
using System.Windows.Media;
using System.Windows;
using WpfCommon;
using System.Windows.Controls;

namespace MesserGUISystem.wpf_magic {
    class SelectionAdorner : Adorner {
        private int _originalZIndex;

        public SelectionAdorner(UIElement ele)
            : base(ele) {
            this.IsHitTestVisible = false;
        }

        protected override void OnRender(DrawingContext drawingContext) {
            Rect adornedElementRect = (this.AdornedElement as IManualMUIObject).MUIBounds;
            var selectionOffset = 3d;
            adornedElementRect.X -= selectionOffset;
            adornedElementRect.Y -= selectionOffset;
            adornedElementRect.Width += 2*selectionOffset;
            adornedElementRect.Height += 2*selectionOffset;

            // Some arbitrary drawing implements.
            SolidColorBrush fillBrush = new SolidColorBrush(Colors.DarkGray);
            fillBrush.Opacity = 0.45;
            Pen borderPen = new Pen(new SolidColorBrush(Colors.Black), 2.0);

            // Draw a circle at each corner.
            //drawingContext.DrawLine(renderPen, adornedElementRect.TopLeft, adornedElementRect.TopRight);
            //drawingContext.DrawLine(renderPen, adornedElementRect.TopRight, adornedElementRect.BottomRight);
            //drawingContext.DrawLine(renderPen, adornedElementRect.BottomRight, adornedElementRect.BottomLeft);
            //drawingContext.DrawLine(renderPen, adornedElementRect.BottomLeft, adornedElementRect.TopLeft);

            drawingContext.DrawRectangle(null, borderPen, adornedElementRect);
        }
    }
}
