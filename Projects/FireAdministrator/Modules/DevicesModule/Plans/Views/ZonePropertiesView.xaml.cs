﻿using System.Windows.Controls;

namespace DevicesModule.Plans.Views
{
    public partial class ZonePropertiesView : UserControl
    {
        public ZonePropertiesView()
        {
            InitializeComponent();
        }

        private void DataGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var dataGrid = sender as DataGrid;
            if (dataGrid.SelectedItem != null)
            {
                dataGrid.ScrollIntoView(dataGrid.SelectedItem);
            }
        }
    }
}