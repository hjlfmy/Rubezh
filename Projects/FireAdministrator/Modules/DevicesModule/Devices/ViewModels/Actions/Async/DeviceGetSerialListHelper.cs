﻿using System.Collections.Generic;
using FiresecAPI;
using FiresecAPI.Models;
using FiresecClient;
using Infrastructure;
using Infrastructure.Common.Windows;

namespace DevicesModule.ViewModels
{
    public static class DeviceGetSerialListHelper
    {
        static Device _device;
        static OperationResult<List<string>> _operationResult;

        public static void Run(Device device)
        {
            _device = device;
            ServiceFactory.ProgressService.Run(OnPropgress, OnlCompleted, _device.PresentationAddressAndDriver + ". Получение списка устройств");
        }

        static void OnPropgress()
        {
            _operationResult = FiresecManager.DeviceGetSerialList(_device.UID);
        }

        static void OnlCompleted()
        {
            if (_operationResult.HasError)
            {
				MessageBoxService.ShowError(_operationResult.Error, "Ошибка при выполнении операции");
                return;
            }
            DialogService.ShowModalWindow(new BindMsViewModel(_device, _operationResult.Result));
        }
    }
}