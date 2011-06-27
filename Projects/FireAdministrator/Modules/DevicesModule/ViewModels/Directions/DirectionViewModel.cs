﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Infrastructure.Common;
using Firesec.CoreConfig;
using FiresecClient;
using System.Collections.ObjectModel;
using FiresecClient.Models;

namespace DevicesModule.ViewModels
{
    public class DirectionViewModel : BaseViewModel
    {
        public DirectionViewModel(Direction direction)
        {
            AddZoneCommand = new RelayCommand(OnAddZone);
            RemoveZoneCommand = new RelayCommand(OnRemoveZone);
            Initialize(direction);
        }

        public Direction Direction { get; private set; }

        void Initialize(Direction direction)
        {
            Direction = direction;

            Zones = new ObservableCollection<ZoneViewModel>();
            SourceZones = new ObservableCollection<ZoneViewModel>();

            foreach (var zone in FiresecManager.Configuration.Zones)
            {
                ZoneViewModel zoneViewModel = new ZoneViewModel(zone);
                if (direction.Zones.Contains(Convert.ToInt32(zone.No)))
                {
                    Zones.Add(zoneViewModel);
                }
                else
                {
                    SourceZones.Add(zoneViewModel);
                }
            }
        }

        public void Update()
        {
            OnPropertyChanged("Id");
            OnPropertyChanged("Name");
            OnPropertyChanged("Description");
        }

        public int Id
        {
            get { return Direction.Id; }
        }

        public string Name
        {
            get { return Direction.Name; }
        }

        public string Description
        {
            get { return Direction.Description; }
        }

        ObservableCollection<ZoneViewModel> _zones;
        public ObservableCollection<ZoneViewModel> Zones
        {
            get { return _zones; }
            set
            {
                _zones = value;
                OnPropertyChanged("Zones");
            }
        }

        ZoneViewModel _selectedZone;
        public ZoneViewModel SelectedZone
        {
            get { return _selectedZone; }
            set
            {
                _selectedZone = value;
                OnPropertyChanged("SelectedZone");
            }
        }

        ObservableCollection<ZoneViewModel> _sourceZones;
        public ObservableCollection<ZoneViewModel> SourceZones
        {
            get { return _sourceZones; }
            set
            {
                _sourceZones = value;
                OnPropertyChanged("SourceZones");
            }
        }

        ZoneViewModel _selectedSourceZone;
        public ZoneViewModel SelectedSourceZone
        {
            get { return _selectedSourceZone; }
            set
            {
                _selectedSourceZone = value;
                OnPropertyChanged("SelectedSourceZone");
            }
        }

        public RelayCommand AddZoneCommand { get; private set; }
        void OnAddZone()
        {
            if (SelectedSourceZone != null)
            {
                Direction.Zones.Add(Convert.ToInt32(SelectedSourceZone.No));
                Zones.Add(SelectedSourceZone);
                SourceZones.Remove(SelectedSourceZone);
            }
        }

        public RelayCommand RemoveZoneCommand { get; private set; }
        void OnRemoveZone()
        {
            if (SelectedZone != null)
            {
                Direction.Zones.Remove(Convert.ToInt32(SelectedZone.No));
                SourceZones.Add(SelectedZone);
                Zones.Remove(SelectedZone);
            }
        }
    }
}