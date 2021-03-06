﻿using System.Configuration;
using System.IO;
using System.Windows;
using System.Xml.Serialization;
using FiresecClient;
using ItvIntergation.Ngi;

namespace RepFileManager
{
	public partial class MainWindow : Window
	{
		public MainWindow()
		{
			InitializeComponent();
		}

		private void Button_Click(object sender, RoutedEventArgs e)
		{
			string serverAddress = ConfigurationManager.AppSettings["ServiceAddress"] as string;
			string defaultLogin = ConfigurationManager.AppSettings["DefaultLogin"] as string;
			string defaultPassword = ConfigurationManager.AppSettings["DefaultPassword"] as string;
			string result = ItvManager.Connect(serverAddress, defaultLogin, defaultPassword);
			if (result != null)
			{
				MessageBox.Show(result);
				return;
			}

			Directory.CreateDirectory("BMP");

			var repositoryModule = new repositoryModule();
			repositoryModule.name = "Rubezh devices";
			repositoryModule.version = "1.0.0";
			repositoryModule.port = "1234";
			var repository = new repository();
			repository.module = repositoryModule;

			repositoryModule.device = DevicesGenerator.Generate().ToArray();

			var serializer = new XmlSerializer(typeof(repositoryModule));
			using (var fileStream = File.CreateText("Rubezh.rep"))
			{
				serializer.Serialize(fileStream, repositoryModule);
			}
		}

		private void Button_Click_1(object sender, RoutedEventArgs e)
		{
		}
	}
}