﻿using System.Windows;

namespace MultiClient.Services
{
    public class DialogContent : BaseViewModel, IDialogContent
    {
        public string Title { get; set; }

        public object InternalViewModel
        {
            get { return this; }
        }

        public Window Surface { get; set; }

        public void Close(bool result)
        {
            if (Surface != null)
            {
                Surface.DialogResult = result;
                Surface.Close();
            }
        }
    }
}
