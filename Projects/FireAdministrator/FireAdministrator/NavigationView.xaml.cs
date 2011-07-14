﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.ComponentModel;
using Infrastructure;
using Infrastructure.Events;

namespace FireAdministrator
{
    public partial class NavigationView : UserControl, INotifyPropertyChanged
    {
        public NavigationView()
        {
            InitializeComponent();
            DataContext = this;

            ServiceFactory.Events.GetEvent<ShowDeviceEvent>().Subscribe(x => { _isDevicesSelected = true; OnPropertyChanged("IsDevicesSelected"); });
            ServiceFactory.Events.GetEvent<ShowZoneEvent>().Subscribe(x => { _isZonesSelected = true; OnPropertyChanged("IsZonesSelected"); });
            ServiceFactory.Events.GetEvent<ShowDirectionsEvent>().Subscribe(x => { _isDerectonsSelected = true; OnPropertyChanged("IsDerectonsSelected"); });
            ServiceFactory.Events.GetEvent<ShowLibraryEvent>().Subscribe(x => { _isLibrarySelected = true; OnPropertyChanged("IsLibrarySelected"); });
            ServiceFactory.Events.GetEvent<ShowPlansEvent>().Subscribe(x => { _isPlanSelected = true; OnPropertyChanged("IsPlanSelected"); });
            ServiceFactory.Events.GetEvent<ShowSecurityEvent>().Subscribe(x => { _isSecuritySelected = true; OnPropertyChanged("IsSecuritySelected"); });
            ServiceFactory.Events.GetEvent<ShowJournalEvent>().Subscribe(x => { _isJournalSelected = true; OnPropertyChanged("IsJournalSelected"); });
            ServiceFactory.Events.GetEvent<ShowSoundsEvent>().Subscribe(x => { _isSoundsSelected = true; OnPropertyChanged("IsSoundsSelected"); });
        }

        bool _isDevicesSelected;
        public bool IsDevicesSelected
        {
            get { return _isDevicesSelected; }
            set
            {
                _isDevicesSelected = value;
                if (value)
                {
                    ServiceFactory.Events.GetEvent<ShowDeviceEvent>().Publish(null);
                }
                OnPropertyChanged("IsDevicesSelected");
            }
        }

        bool _isZonesSelected;
        public bool IsZonesSelected
        {
            get { return _isZonesSelected; }
            set
            {
                _isZonesSelected = value;
                if (value)
                {
                    ServiceFactory.Events.GetEvent<ShowZoneEvent>().Publish(null);
                }
                OnPropertyChanged("IsZonesSelected");
            }
        }

        bool _isDerectonsSelected;
        public bool IsDerectonsSelected
        {
            get { return _isDerectonsSelected; }
            set
            {
                _isDerectonsSelected = value;
                if (value)
                {
                    ServiceFactory.Events.GetEvent<ShowDirectionsEvent>().Publish(null);
                }
                OnPropertyChanged("IsDerectonsSelected");
            }
        }

        bool _isLibrarySelected;
        public bool IsLibrarySelected
        {
            get { return _isLibrarySelected; }
            set
            {
                _isLibrarySelected = value;
                if (value)
                {
                    ServiceFactory.Events.GetEvent<ShowLibraryEvent>().Publish(null);
                }
                OnPropertyChanged("IsLibrarySelected");
            }
        }

        bool _isPlanSelected;
        public bool IsPlanSelected
        {
            get { return _isPlanSelected; }
            set
            {
                _isPlanSelected = value;
                if (value)
                {
                    ServiceFactory.Events.GetEvent<ShowPlansEvent>().Publish(null);
                }
                OnPropertyChanged("IsPlanSelected");
            }
        }

        bool _isSecuritySelected;
        public bool IsSecuritySelected
        {
            get { return _isSecuritySelected; }
            set
            {
                _isSecuritySelected = value;
                if (value)
                {
                    ServiceFactory.Events.GetEvent<ShowSecurityEvent>().Publish(null);
                }
                OnPropertyChanged("IsSecuritySelected");
            }
        }

        bool _isJournalSelected;
        public bool IsJournalSelected
        {
            get { return _isJournalSelected; }
            set
            {
                _isJournalSelected = value;
                if (value)
                {
                    ServiceFactory.Events.GetEvent<ShowJournalEvent>().Publish(null);
                }
                OnPropertyChanged("IsJournalSelected");
            }
        }

        bool _isSoundsSelected;
        public bool IsSoundsSelected
        {
            get { return _isSoundsSelected; }
            set
            {
                _isSoundsSelected = value;
                if (value)
                {
                    ServiceFactory.Events.GetEvent<ShowSoundsEvent>().Publish(null);
                }
                OnPropertyChanged("IsSoundsSelected");
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;
        void OnPropertyChanged(string name)
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(name));
        }
    }
}
