﻿using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace SecurityModule.Views
{
    public partial class RolesMenuView : UserControl
    {
        public RolesMenuView()
        {
            InitializeComponent();
			Current = this;
			AcceptKeyboard = true;
        }

		static RolesMenuView()
		{
			EventManager.RegisterClassHandler(typeof(Window), Keyboard.KeyDownEvent, new KeyEventHandler(OnKeyDownStatic), true);
		}

		public static RolesMenuView Current { get; private set; }
		public bool AcceptKeyboard { get; set; }

		static void OnKeyDownStatic(object sender, KeyEventArgs e)
		{
			Current.OnKeyDown(sender, e);
		}

		void OnKeyDown(object sender, KeyEventArgs e)
		{
			if (AcceptKeyboard != true)
				return;

			if (e.Key == Key.Delete && Keyboard.Modifiers == ModifierKeys.Control)
				PressButton(deleteButton);

			if (e.Key == Key.R && Keyboard.Modifiers == ModifierKeys.Control)
				PressButton(editButton);

			if (e.Key == Key.N && Keyboard.Modifiers == ModifierKeys.Control)
				PressButton(addButton);
		}

		void PressButton(Button button)
		{
			if (button.Command.CanExecute(null))
				button.Command.Execute(null);
		}
    }
}