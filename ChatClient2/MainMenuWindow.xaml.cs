using ChatServer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.ServiceModel;
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

namespace ChatClient2
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
        int serverIndex = 0;
        private DataServerInterface foob;
        public MainMenuWindow(DataServerInterface foob, string username)
        {
            InitializeComponent();
            this.username = username;
            usernamelabel.Content = username;
            us = new User(username, "123");
            cs = new ChatServerManager();
            cr = new ChatRoom(null, serverIndex);
            this.foob = foob;
            foob.addUserAccountInfo(us);
        }

        private void logoutbutton_Click(object sender, RoutedEventArgs e)
        {
            MainWindow mainWindow = new MainWindow();
            mainWindow.Show();
            this.Close();
        }

        private void createButton_Click(object sender, RoutedEventArgs e)
        {
            roomName = chatroombox.Text;
            cr = new ChatRoom(roomName, serverIndex);
            HashSet<ChatRoom> availbleServers = cs.getAllServer();
            cr.addUser(us);
            us.addChatRooms(cr);
            cs.addServer(cr);
            roomList.Items.Add(cr.getChatRoomName());
            serverIndex++;
            chatroombox.Clear(); //clear the chat room box after creating the chat room
            foob.updateUserAccountInfo(us);
        }

        private void roomList_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            msgdisplaybox.Document.Blocks.Clear();
            int index = 0, selectedRoom = 0;
            participantlist.Items.Clear();
            roomName = roomList.SelectedItem.ToString();
            selectedRoom = roomList.SelectedIndex;
            HashSet<ChatRoom> availbleRoom = cs.getAllServer();
            foreach (ChatRoom room in availbleRoom)
            {
                if (room.getChatRoomName().Equals(roomName, StringComparison.OrdinalIgnoreCase) && room.getId() == selectedRoom)
                {
                    cr = room;
                }
            }
            List<String> messages = cr.getMessages();
            List<String> messagesby = cr.getMessagesBy();
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
                msgdisplaybox.AppendText(Environment.NewLine);
                index++;
            }
        }

        private void sendmsgbtn_Click(object sender, RoutedEventArgs e)
        {
            if(roomList.SelectedItem != null)
            {
                cr.addMessages(username, msgtxtbox.Text);
                string msg = username + ": " + msgtxtbox.Text;
                msgdisplaybox.AppendText(msg);
                msgdisplaybox.AppendText(Environment.NewLine);
                msgtxtbox.Clear();
            }
        }
    }
}
