﻿using FiresecAPI.Models;
using FiresecClient;
using Infrastructure;

namespace DevicesModule.ViewModels
{
    public static class DeviceJournalHelper
    {
        static Device _device;
        static string _journal;

        public static void Run(Device device)
        {
            _device = device;
            AsyncOperationHelper.Run(OnPropgress, OnCompleted, _device.PresentationAddressDriver + ". Чтение журнала");
        }

        static void OnPropgress()
        {
            _journal = FiresecManager.ReadDeviceJournal(_device.UID);
        }

        static void OnCompleted()
        {
            ServiceFactory.UserDialogs.ShowModalWindow(new DeviceJournalViewModel(_journal));
        }
    }
}