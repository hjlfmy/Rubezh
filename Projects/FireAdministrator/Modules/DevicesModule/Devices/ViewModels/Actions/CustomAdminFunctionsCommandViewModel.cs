﻿using System.Collections.Generic;
using FiresecAPI.Models;
using FiresecClient;
using Infrastructure.Common.Windows;
using Infrastructure.Common.Windows.ViewModels;

namespace DevicesModule.ViewModels
{
    public class CustomAdminFunctionsCommandViewModel : SaveCancelDialogViewModel
    {
        Device _device;
        public CustomAdminFunctionsCommandViewModel(Device device)
        {
            _device = device;
            Title = "Выбор функции";

            var operationResult = FiresecManager.FiresecService.DeviceCustomFunctionList(device.Driver.UID);
            if (operationResult.HasError)
            {
				MessageBoxService.ShowError(operationResult.Error, "Ошибка при выполнении операции");
                return;
            }
            Functions = operationResult.Result;
        }

        public List<DeviceCustomFunction> Functions { get; private set; }

        DeviceCustomFunction _selectedFunction;
        public DeviceCustomFunction SelectedFunction
        {
            get { return _selectedFunction; }
            set
            {
                _selectedFunction = value;
                OnPropertyChanged("SelectedFunction");
            }
        }

		protected override bool Save()
		{
			if (SelectedFunction != null)
			{
				DeviceCustomFunctionExecuteHelper.Run(_device, SelectedFunction.Code);
			}
			return base.Save();
		}
    }
}