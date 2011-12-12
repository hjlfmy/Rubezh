﻿using System.Windows;
using System.Windows.Documents;
using System.Windows.Media;

namespace PlansModule.Designer
{
    public class ResizeAdorner : Adorner
    {
        private VisualCollection visuals;
        private ResizeChrome chrome;

        public ResizeAdorner(DesignerItem designerItem)
            : base(designerItem)
        {
            this.chrome = new ResizeChrome(designerItem);
            this.visuals = new VisualCollection(this);
            this.visuals.Add(this.chrome);
            this.chrome.DataContext = designerItem;
        }

        protected override int VisualChildrenCount
        {
            get { return this.visuals.Count; }
        }

        protected override Visual GetVisualChild(int index)
        {
            return this.visuals[index];
        }

        protected override Size ArrangeOverride(Size arrangeBounds)
        {
            this.chrome.Arrange(new Rect(arrangeBounds));
            return arrangeBounds;
        }
    }
}
