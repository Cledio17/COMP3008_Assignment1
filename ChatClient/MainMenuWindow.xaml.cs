using ChatServer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Interop;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace ChatClient
{
    /// <summary>
    /// Interaction logic for MainMenuWindow.xaml
    /// </summary>
    public partial class MainMenuWindow : Window
    {
        string username = string.Empty;
        string roomName = string.Empty;
        ChatRoom cr;
        ChatServerManager cs;
        User us;
        string uss;
        public MainMenuWindow(string username)
        {
            InitializeComponent();
            this.username = username;
            usernamelabel.Content = username;
            us = new User(username, "123");
            cs = new ChatServerManager();
            cr = new ChatRoom(null);
        }

        private void logoutbutton_Click(object sender, RoutedEventArgs e)
        {
            System.Windows.Application.Current.Shutdown();
        }

        private void chatroombox_TextChanged(object sender, TextChangedEventArgs e)
        {

        }

        private void createButton_Click(object sender, RoutedEventArgs e)
        {
            roomList.Items.Clear();
            roomName = chatroombox.Text;
            cr = new ChatRoom(roomName);
            cs.addServer(cr);
            HashSet<ChatRoom> avaserver = cs.getAllServer();
            foreach (ChatRoom room in avaserver)
            {
                roomList.Items.Add(room.getChatRoomId());
            }
        }

        private void roomList_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            msgdisplaybox.Document.Blocks.Clear();
            int index = 0;
            participantlist.Items.Clear();
            roomName = roomList.SelectedItem.ToString();
            cr = new ChatRoom(roomName);
            List<String> messages = cr.getMessages();
            List<String> messagesby = cr.getMessagesBy();
            cr.addUser(us);
            List<User> _users = cr.getUser();
            foreach (User user in _users)
            {
                uss = user.getUserName();
                participantlist.Items.Add(uss);
            }
            foreach (string messageBy in messagesby)
            {
                string message = messages[index];
                string combinedMessage = $"{messageBy}: {message}";
                Console.WriteLine(combinedMessage); 
                msgdisplaybox.AppendText(combinedMessage);
                index++;
            }
        }

        private void sendmsgbtn_Click(object sender, RoutedEventArgs e)
        {
            cr.addMessages(username, msgtxtbox.Text);
            string msg = "\n" + username + ": " + msgtxtbox.Text;
            msgdisplaybox.AppendText(msg);
        }
    }
}
