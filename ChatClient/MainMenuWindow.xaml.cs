using ChatServer;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Runtime.Remoting.Messaging;
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
        string username = ""; //loggin in user username
        User us; //logged in user
        ChatRoom cr; //current selected room
        MainWindow loginMenu;
        private DataServerInterface foob;
        public MainMenuWindow (DataServerInterface inFoob, User theUser, MainWindow inLoginMenu)
        {
            InitializeComponent();
            this.foob = inFoob;
            this.us = theUser;
            this.username = us.getUserName();
            this.loginMenu = inLoginMenu;
            usernamelabel.Content = username;
            userID.Content = "ID: " + us.getID();
            List<ChatRoom> joinedRooms = foob.getJoinedServers(username);
            foreach (ChatRoom room in joinedRooms)
            {
                roomList.Items.Add(room.getChatRoomName());
            }
            refreshAvailableServer();
        }

        private void createButton_Click(object sender, RoutedEventArgs e)
        {
            string roomName = chatroombox.Text;
            chatroombox.Clear(); //clear the chat room box after creating the chat room
            if (!roomName.Equals("") && roomName != null)
            {
                if (!foob.checkIsRoomNameExist(roomName))
                {
                    foob.addServer(us, roomName);
                    roomList.Items.Add(roomName);
                    refreshAvailableServer();
                    us = foob.getUserAccountInfo(us.getUserName());
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
            List<ChatRoom> allServers = foob.getAllServers();
            foreach (ChatRoom room in allServers)
            {
                allserverlist.Items.Add(room.getChatRoomName());
            }
        }

        private void roomList_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            msgdisplaybox.Document.Blocks.Clear();
            participantlist.Items.Clear();
            if (roomList.SelectedItem != null)
            {
                string roomName = roomList.SelectedItem.ToString();
                List<ChatRoom> joinedRooms = foob.getJoinedServers(username);
                foreach (ChatRoom room in joinedRooms)
                {
                    if (room.getChatRoomName().Equals(roomName, StringComparison.OrdinalIgnoreCase))
                    {
                        cr = room;
                    }
                }
                currRoom.Content = cr.getId();

                int index = 0;
                string uss;
                List<String> messages = cr.getMessages();
                List<String> messagesby = cr.getMessagesBy();
                List<User> _users = cr.getUser();
                cr = foob.getServerInfo(cr.getChatRoomName());
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
        }

        private void joinbutton_Click(object sender, RoutedEventArgs e)
        {
            if (allserverlist.SelectedItem != null)
            {
                /*string selectedServer = allserverlist.SelectedItem.ToString();
                allserverlist.SelectedItem = null;
                foob.addJoinedServer(username, selectedServer);
                roomList.Items.Add(selectedServer);
                us = foob.getUserAccountInfo(username); //get latest user info*/
            }
            else
            {
                MessageBox.Show("Please select a server to join.");
            }
        }

        private void leavebtn_Click(object sender, RoutedEventArgs e)
        {
            if (cr != null)
            {
                string currRoomName = cr.getChatRoomName();
                foob.leaveRoom(username, currRoomName);
                roomList.Items.Remove(currRoomName);
                msgdisplaybox.Document.Blocks.Clear();
                participantlist.Items.Clear();
                us = foob.getUserAccountInfo(username); //get latest user info
                cr = null; //clear deleted selected room
            }
            else
            {
                MessageBox.Show("Please select a chat room.");
            }
        }

        private void sendmsgbtn_Click(object sender, RoutedEventArgs e)
        {
            if (cr != null)
            {
                foob.addMessages(username, msgtxtbox.Text, cr.getChatRoomName());
                string msg = username + ": " + msgtxtbox.Text;
                msgdisplaybox.AppendText(msg);
                msgdisplaybox.AppendText(Environment.NewLine);
                msgtxtbox.Clear();
                cr = foob.getServerInfo(cr.getChatRoomName());
            }
            else
            {
                MessageBox.Show("Please select a chat room.");
            }
        }

        private void uploadfilebtn_Click(object sender, RoutedEventArgs e)
        {
            //Stream theStream;
            //Paragraph paragraph = new Paragraph();
            //paragraph.Margin = new Thickness(0);
            //OpenFileDialog openFile = new OpenFileDialog();
            //if (openFile.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            //{
            //    Hyperlink hyperlink = new Hyperlink();
            //    hyperlink.IsEnabled = true;
            //    hyperlink.Inlines.Add("https://www.google.com/");
            //    hyperlink.NavigateUri = new Uri("https://www.google.com/");
            //    System.Diagnostics.Process.Start(openFile.FileName);
            //    msgdisplaybox.AppendText(openFile.FileName);
            //    paragraph.Inlines.Add(hyperlink);
            //    msgdisplaybox.Document.Blocks.Add(paragraph);


            //    /*if((theStream = openFile.OpenFile()) != null)
            //    {
            //        string fileName = openFile.FileName;
            //        String fileText = File.ReadAllText(fileName);

            //    }*/
            //}

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

    }
}
