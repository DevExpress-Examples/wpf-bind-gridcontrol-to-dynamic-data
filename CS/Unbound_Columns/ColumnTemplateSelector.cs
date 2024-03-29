﻿using System.Windows;
using System.Windows.Controls;

namespace Unbound_Columns {
    public class ColumnTemplateSelector : DataTemplateSelector {
        public DataTemplate ColumnTemplate { get; set; }
        public DataTemplate UnboundColumnTemplate { get; set; }

        public override DataTemplate SelectTemplate(object item, DependencyObject container) {
            return item is Column column
                ? column.UnboundDataType != null ? UnboundColumnTemplate : ColumnTemplate
                : base.SelectTemplate(item, container);
        }
    }
}