using ChatServer;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Runtime.Remoting.Messaging;
using System.ServiceModel;
using System.ServiceModel.Channels;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Forms;
using System.Windows.Input;
using System.Windows.Interop;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using MessageBox = System.Windows.Forms.MessageBox;

namespace ChatClient 
{
    /// <summary>
    /// Interaction logic for MainMenuWindow.xaml
    /// </summary>
    public partial class MainMenuWindow : Window
    {
        int ID = 0;
        string username = ""; //loggin in user username
        string currRoomName; //current selected room
        MainWindow loginMenu;
        private DataServerInterface foob;
        public MainMenuWindow (DataServerInterface inFoob, string inUsername, MainWindow inLoginMenu)
        {
            InitializeComponent();
            this.foob = inFoob;
            this.username = inUsername;
            this.ID = foob.getUserID(username);
            this.loginMenu = inLoginMenu;
            usernamelabel.Content = username;
            userID.Content = "ID: " + ID;
            List<string> joinedRooms = foob.getJoinedServers(username);
            foreach (string room in joinedRooms)
            {
                roomList.Items.Add(room);
            }
            refreshAvailableServer();
        }

        private void createButton_Click(object sender, RoutedEventArgs e)
        {
            string roomName = chatroombox.Text;
            chatroombox.Clear(); //clear the chat room box after creating the chat room
            if (!roomName.Equals("") || roomName != null)
            {
                if (!foob.checkIsRoomNameExist(roomName))
                {
                    foob.addServer(username, roomName);
                    roomList.Items.Add(roomName);
                    refreshAvailableServer();
                    //us = foob.getUserAccountInfo(us.Username);
                }
                else
                {
                    MessageBox.Show("Room name used. Try again!");
                }
            }
            else
            {
                MessageBox.Show("Please enter a room name");
            }
        }

        private void refreshAvailableServer()
        {
            allserverlist.Items.Clear();
            List<string> allServers = foob.getAllServers();
            if (allServers.Count > 0)
            {
                foreach (string room in allServers)
                {
                    allserverlist.Items.Add(room);
                }
            }
        }

        private void roomList_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            msgdisplaybox.Document.Blocks.Clear();
            participantlist.Items.Clear();
            if (roomList.SelectedItem != null)
            {
                string roomName = roomList.SelectedItem.ToString();
                List<string> joinedRooms = foob.getJoinedServers(username);
                foreach (string room in joinedRooms)
                {
                    if (room.Equals(roomName, StringComparison.OrdinalIgnoreCase))
                    {
                        currRoomName = room;
                    }
                }
                currRoom.Content = foob.getServerID(currRoomName);

                int index = 0;
                string uss;
                /*List<String> messages = cr.Messages;
                List<String> messagesby = cr.MessagesBy;
                List<User> _users = cr.RoomUsers;
                cr = foob.getServerInfo(cr.RoomName);
                foreach (User user in _users)
                {
                    uss = user.Username;
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
                }*/
            }
        }

        private void joinbutton_Click(object sender, RoutedEventArgs e)
        {
            if (allserverlist.SelectedItem != null)
            {
                string selectedServer = allserverlist.SelectedItem.ToString();
                allserverlist.SelectedItem = null;
                foob.addJoinedServer(username, selectedServer);
                roomList.Items.Add(selectedServer);
            }
            else
            {
                MessageBox.Show("Please select a server to join.");
            }
        }

        private void leavebtn_Click(object sender, RoutedEventArgs e)
        {
            if (currRoomName != null)
            {
                foob.leaveRoom(username, currRoomName);
                roomList.Items.Remove(currRoomName);
                msgdisplaybox.Document.Blocks.Clear();
                participantlist.Items.Clear();
                currRoomName = null; //clear deleted selected room
            }
            else
            {
                MessageBox.Show("Please select a chat room.");
            }
        }

        private void sendmsgbtn_Click(object sender, RoutedEventArgs e)
        {
            /*if (cr != null)
            {
                foob.addMessages(username, msgtxtbox.Text, cr.RoomName);
                string msg = username + ": " + msgtxtbox.Text;
                msgdisplaybox.AppendText(msg);
                msgdisplaybox.AppendText(Environment.NewLine);
                msgtxtbox.Clear();
                cr = foob.getServerInfo(cr.RoomName);
            }
            else
            {
                MessageBox.Show("Please select a chat room.");
            }*/
        }

        private void uploadfilebtn_Click(object sender, RoutedEventArgs e)
        {
            Microsoft.Win32.OpenFileDialog openFileDialog = new Microsoft.Win32.OpenFileDialog();
            bool? response = openFileDialog.ShowDialog();
            if (response == true)
            {
                string filepath = openFileDialog.FileName;
                string filename = System.IO.Path.GetFileName(filepath); // Get only the file name

                // Create a new Hyperlink
                Hyperlink hyperlink = new Hyperlink(new Run(filename));
                hyperlink.IsEnabled = true;
                hyperlink.NavigateUri = new Uri(filepath); // Set the URI to the file path

                // Handle the click event to open the file when the hyperlink is clicked
                hyperlink.RequestNavigate += Hyperlink_RequestNavigate;

                // Create a Paragraph and add the Hyperlink to it
                Paragraph paragraph = new Paragraph(hyperlink);
                paragraph.IsEnabled = true;

                // Add the Paragraph to the RichTextBox
                msgdisplaybox.Document.Blocks.Add(paragraph);
                msgdisplaybox.IsDocumentEnabled = true;
                msgdisplaybox.IsReadOnly = true;
                msgdisplaybox.AppendText("\n");
            }
        }

        private void Hyperlink_RequestNavigate(object sender, RequestNavigateEventArgs e)
        {
            try
            {
                if(sender is Hyperlink hyperlink)
                {
                    Process process = new Process();
                    process.StartInfo.UseShellExecute = true;
                    process.StartInfo.FileName = hyperlink.NavigateUri.ToString();
                    process.Start();
                }
            }
            catch (Exception ex) { throw; }
        }

        //Log out
        private void logoutbutton_Click(object sender, RoutedEventArgs e)
        {
            loginMenu.Show();
            this.Close();
        }

        //Exit program
        private void mainbtnexit_Click(object sender, RoutedEventArgs e)
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

        private void allserverlist_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }
    }
}
