﻿using System.Windows;
using System.Windows.Controls;
using JournalModule.ViewModels;

namespace JournalModule.Views
{
    public partial class ArchiveSettingsView : UserControl
    {
        public ArchiveSettingsView()
        {
            InitializeComponent();
        }

        void _archiveDefaultStateTypes_DataContextChanged(object sender, DependencyPropertyChangedEventArgs e)
        {
            OnCheckedArchiveDefaultStateChanged();
        }

        void RadioButton_Checked(object sender, System.Windows.RoutedEventArgs e)
        {
            OnCheckedArchiveDefaultStateChanged();
        }

        void OnCheckedArchiveDefaultStateChanged()
        {
            switch ((DataContext as ArchiveSettingsViewModel).CheckedArchiveDefaultStateType)
            {
                case FiresecAPI.Models.ArchiveDefaultStateType.LastHours:
                    _countHoursPanel.Visibility = Visibility.Visible;
                    _countDaysPanel.Visibility = Visibility.Collapsed;
                    _startDatePanel.Visibility = Visibility.Collapsed;
                    _endDatePanel.Visibility = Visibility.Collapsed;
                    break;

                case FiresecAPI.Models.ArchiveDefaultStateType.LastDays:
                    _countHoursPanel.Visibility = Visibility.Collapsed;
                    _countDaysPanel.Visibility = Visibility.Visible;
                    _startDatePanel.Visibility = Visibility.Collapsed;
                    _endDatePanel.Visibility = Visibility.Collapsed;
                    break;

                case FiresecAPI.Models.ArchiveDefaultStateType.All:
                    _countHoursPanel.Visibility = Visibility.Collapsed;
                    _countDaysPanel.Visibility = Visibility.Collapsed;
                    _startDatePanel.Visibility = Visibility.Collapsed;
                    _endDatePanel.Visibility = Visibility.Collapsed;
                    break;

                case FiresecAPI.Models.ArchiveDefaultStateType.FromDate:
                    _countHoursPanel.Visibility = Visibility.Collapsed;
                    _countDaysPanel.Visibility = Visibility.Collapsed;
                    _startDatePanel.Visibility = Visibility.Visible;
                    _endDatePanel.Visibility = Visibility.Collapsed;
                    break;

                case FiresecAPI.Models.ArchiveDefaultStateType.RangeDate:
                    _countHoursPanel.Visibility = Visibility.Collapsed;
                    _countDaysPanel.Visibility = Visibility.Collapsed;
                    _startDatePanel.Visibility = Visibility.Visible;
                    _endDatePanel.Visibility = Visibility.Visible;
                    break;

                default:
                    break;
            }
        }
    }
}