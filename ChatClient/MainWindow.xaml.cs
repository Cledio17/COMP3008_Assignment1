﻿using ChatServer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Security;
using System.ServiceModel;
using System.ServiceModel.Configuration;
using System.ServiceModel.Description;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Forms;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using MessageBox = System.Windows.Forms.MessageBox;

namespace ChatClient
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    [ServiceBehavior(MaxItemsInObjectGraph = 10000000)]
    public partial class MainWindow : Window
    {
        string username = "";
        private static DataServerInterface foob;
        public MainWindow()
        {
            InitializeComponent();

            ChannelFactory<DataServerInterface> foobFactory;
            NetTcpBinding tcp = new NetTcpBinding();
            string URL = "net.tcp://localhost:8100/DataService";
            foobFactory = new ChannelFactory<DataServerInterface>(tcp, URL);
            foobFactory.Endpoint.EndpointBehaviors.Add(new CustomClientEndpointBehavior(1000000));
            foob = foobFactory.CreateChannel();
        }

        private void btnLogin_Click(object sender, RoutedEventArgs e)
        {
            username = txtusername.Text;
            if (!foob.checkIsUsernameExist(username))
            {
                User theUser = foob.addUserAccountInfo(username);
                MainMenuWindow mainMenuWindow = new MainMenuWindow(foob, theUser, this);
                mainMenuWindow.Show();
                this.Hide();
            }
            else
            {
                //MessageBox.Show("The username is already being used. Please re-enter another username again.");
                User theUser = foob.getUserAccountInfo(username);
                MainMenuWindow mainMenuWindow = new MainMenuWindow(foob, theUser, this);
                mainMenuWindow.Show();
                this.Hide();
            }
        }

        private void addClient_Click(object sender, RoutedEventArgs e) //Add another client
        {
            MainWindow mainWindow = new MainWindow();
            mainWindow.Show();
        }

        private void btnExit_Click(object sender, RoutedEventArgs e)
        {
            System.Windows.Application.Current.Shutdown();
        }

        private void Border_MouseDown(object sender, MouseButtonEventArgs e)
        {
            try
            {
                DragMove();
            }
            catch (Exception ex) { }
        }
    }
}
