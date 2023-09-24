using ChatServer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
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
using UserDatabaseServer;
using MessageBox = System.Windows.Forms.MessageBox;

namespace ChatClient
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private static DataServerInterface foob;
        private static NetTcpBinding tcp;
        string username = string.Empty;
        public MainWindow()
        {
            InitializeComponent();
            ChannelFactory<DataServerInterface> foobFactory;
            tcp = new NetTcpBinding();
            string URL = "net.tcp://localhost:8100/DataService";
            foobFactory = new ChannelFactory<DataServerInterface>(tcp, URL);
            foob = foobFactory.CreateChannel();
        }

        private void btnLogin_Click(object sender, RoutedEventArgs e)
        {
            username = txtusername.Text;
            if (!foob.isUserNameAvailable(txtusername.Text))
            {
                MainMenuWindow mainMenuWindow = new MainMenuWindow(foob,txtusername.Text);
                mainMenuWindow.Show();
                this.Close();
            }
            else
            {
                MessageBox.Show("The username is already being used. Please re-enter another username again.");
            }
        }

        private void Border_MouseDown(object sender, MouseButtonEventArgs e)
        {
            try
            {
                DragMove();
            }
            catch(Exception ex) { }
        }

        private void btnExit_Click(object sender, RoutedEventArgs e)
        {
            System.Windows.Application.Current.Shutdown();
        }

        private void addClient_Click(object sender, RoutedEventArgs e)
        {
            MainWindow mainWindow = new MainWindow();
            mainWindow.Show();
        }

        public static NetTcpBinding getTcp()
        {
            return tcp;
        }
    }
}
