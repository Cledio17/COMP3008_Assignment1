using BusinessDataServer;
using ChatServer;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Linq.Expressions;
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
    public delegate void UpdateChatRoomDelegate();
    public delegate void UpdateChatMessageDelegate();
    public delegate void UpdateRoomParticipants();
    public delegate void UpdatePrivateMessageDelegate();
    public partial class MainMenuWindow : Window
    {
        int ID = 0;
        string username = ""; //loggin in user username
        string currRoomName; //current selected room
        string currPmName; //current private message name
        MainWindow loginMenu;
        private BusinessServerInterface foob;
        private Thread serverListenerThread;
        private UpdateChatRoomDelegate updateChatRoomDelegate;
        private UpdateChatMessageDelegate updateChatMessageDelegate;
        private UpdateRoomParticipants updateRoomParticipants;
        private UpdatePrivateMessageDelegate updatePrivateMessage;
        private bool newChatMessage = true;
        private bool newParticipant = true;
        public MainMenuWindow (BusinessServerInterface inFoob, string inUsername, MainWindow inLoginMenu)
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

            updateChatRoomDelegate += UpdateChatRoom;
            updateChatMessageDelegate += UpdateChatMessage;
            updateRoomParticipants += UpdateRoomParticpants;
            updatePrivateMessage += UpdatePrivateMessage;

            serverListenerThread = new Thread(ListenToServer);
            serverListenerThread.Start();
        }

        private void UpdateChatRoom()
        {
            Dispatcher.Invoke(() => {
                refreshAvailableServer();
            });
        }

        private void UpdateChatMessage()
        {
            Dispatcher.Invoke(() => {
                if (roomList.SelectedItem != null)
                {
                    msgdisplaybox.Document.Blocks.Clear();
                    List<string> messages = foob.getMessages(currRoomName);
                    int i = 0;
                    bool isFile = false;
                    List<int> fileLoc = foob.getFileLoc(currRoomName);
                    foreach (string message in messages)
                    {
                        Console.WriteLine(message);
                        foreach (int loc in fileLoc)
                        {
                            if (loc == i)
                            {
                                isFile = true;
                            }
                        }
                        if (isFile)
                        {
                            loadFileServer(message);
                        }
                        else
                        {
                            msgdisplaybox.AppendText(message);
                            msgdisplaybox.AppendText(Environment.NewLine);
                        }
                        i++;
                        isFile = false;
                    }
                }
            });
        }

        private void UpdateRoomParticpants()
        {
            Dispatcher.Invoke(() =>
            {
                if(roomList.SelectedItem != null)
                {
                    participantlist.Items.Clear();
                    List<string> participants = foob.getParticipants(currRoomName);
                    foreach (string user in participants)
                    {
                        participantlist.Items.Add(user);
                    }
                }
            });
        }

        private void UpdatePrivateMessage()
        {
            Dispatcher.Invoke(() =>
            {
                privatemsgdisplaybox.Document.Blocks.Clear();
                refreshPMBox();
            });
        }

        private void ListenToServer()
        {
            while (true)
            {
                Thread.Sleep(5000);
                updateChatRoomDelegate?.Invoke();

                if (newChatMessage)
                {
                    updateChatMessageDelegate?.Invoke();

                }

                if(newParticipant)
                {
                    updateRoomParticipants?.Invoke();
                }

                updatePrivateMessage?.Invoke();
            }
        }

        private void createButton_Click(object sender, RoutedEventArgs e)
        {
            string roomName = chatroombox.Text;
            chatroombox.Clear(); //clear the chat room box after creating the chat room
            if (!roomName.Equals("") && roomName != null)
            {
                if (!foob.checkIsRoomNameExist(roomName))
                {
                    foob.addServer(username, roomName);
                    roomList.Items.Add(roomName);
                    refreshAvailableServer();
                    MessageBox.Show("Room " + roomName + " created.");
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
                        currRoomName = room ;
                    }
                }
                currRoom.Content = foob.getServerID(currRoomName);

                List<string> messages = foob.getMessages(currRoomName);
                List<string> participants = foob.getParticipants(currRoomName);
                foreach (string user in participants)
                {
                    participantlist.Items.Add(user);
                }
                int i = 0;
                bool isFile = false;
                List <int> fileLoc = foob.getFileLoc(currRoomName);
                foreach (string message in messages)
                {
                    Console.WriteLine(message);
                    foreach (int loc  in fileLoc)
                    {
                        if (loc == i)
                        {
                            isFile = true;
                        }
                    }
                    if (isFile)
                    {
                        loadFileServer(message);
                    }
                    else
                    {
                        msgdisplaybox.AppendText(message);
                        msgdisplaybox.AppendText(Environment.NewLine);
                    }
                    i++;
                    isFile = false;
                }
            }
        }

        private void joinbutton_Click(object sender, RoutedEventArgs e)
        {
            if (allserverlist.SelectedItem != null)
            {
                string selectedServer = allserverlist.SelectedItem.ToString();
                allserverlist.SelectedItem = null;
                if (foob.addJoinedServer(username, selectedServer))
                {
                    roomList.Items.Add(selectedServer);
                    MessageBox.Show("User " + username + " joined room " + selectedServer);
                }
                newParticipant = true;
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
                MessageBox.Show("User " + username + " leaved room " + currRoomName);
                currRoomName = null; //clear deleted selected room
                newParticipant = true;
            }
            else
            {
                MessageBox.Show("Please select a chat room.");
            }
        }

        private void sendmsgbtn_Click(object sender, RoutedEventArgs e)
        {
            if (currRoomName != null)
            {
                if (msgtxtbox.Text.Equals(""))
                {
                    MessageBox.Show("Please enter a message.");
                }
                else
                {
                    string message = username + ": " + msgtxtbox.Text;
                    msgtxtbox.Clear();
                    foob.addMessages(message, currRoomName, false);
                    msgdisplaybox.AppendText(message);
                    msgdisplaybox.AppendText(Environment.NewLine);
                }
            }
            else
            {
                MessageBox.Show("Please select a chat room.");
            }
        }

        private void uploadfilebtn_Click(object sender, RoutedEventArgs e)
        {
            if (currRoomName != null)
            {
                Microsoft.Win32.OpenFileDialog openFileDialog = new Microsoft.Win32.OpenFileDialog();
                bool? response = openFileDialog.ShowDialog();
                string filepath = "";
                if (response == true)
                {
                    filepath = openFileDialog.FileName;
                    msgtxtbox.Clear();
                    foob.addMessages(username + ": ", currRoomName, false);
                    foob.addMessages(filepath, currRoomName, true);
                    msgdisplaybox.AppendText(username + ": ");
                    loadFileServer(filepath);
                }
                else
                {
                    MessageBox.Show("No file selected.");
                }
            }
            else
            {
                MessageBox.Show("Please select a chat room.");
            }
        }

        private void loadFileServer (string filepath)
        {
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
            msgdisplaybox.AppendText(Environment.NewLine);
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
            catch (Exception) { throw; }
        }

        //Log out
        private void logoutbutton_Click(object sender, RoutedEventArgs e)
        {
            foob.setLoggedOut(username);
            loginMenu.Show();
            this.Close();
        }

        //Exit program
        private void mainbtnexit_Click(object sender, RoutedEventArgs e)
        {
            foob.setLoggedOut(username);
            this.Close();
        }

        private void Border_MouseDown(object sender, MouseButtonEventArgs e)
        {
            try
            {
                DragMove();
            }
            catch (Exception) { }
        }

        private void allserverlist_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
        }

        private void refreshbtn_Click(object sender, RoutedEventArgs e)
        {
            refreshAvailableServer();
            refreshPMBox();

            //refresh room list and messages
            if (roomList.SelectedItem != null)
            {
                msgdisplaybox.Document.Blocks.Clear();
                List<string> messages = foob.getMessages(currRoomName);
                int i = 0;
                bool isFile = false;
                List<int> fileLoc = foob.getFileLoc(currRoomName);
                foreach (string message in messages)
                {
                    Console.WriteLine(message);
                    foreach (int loc in fileLoc)
                    {
                        if (loc == i)
                        {
                            isFile = true;
                        }
                    }
                    if (isFile)
                    {
                        loadFileServer(message);
                    }
                    else
                    {
                        msgdisplaybox.AppendText(message);
                        msgdisplaybox.AppendText(Environment.NewLine);
                    }
                    i++;
                    isFile = false;
                }
            }

            //refresh participant list
            if (roomList.SelectedItem != null)
            {
                participantlist.Items.Clear();
                List<string> participants = foob.getParticipants(currRoomName);
                foreach (string user in participants)
                {
                    participantlist.Items.Add(user);
                }
            }
        }

        private void participantlist_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (participantlist.SelectedItem != null)
            {
                currPmName = participantlist.SelectedItem.ToString();
                sendtolabel.Content = currPmName;
                refreshPMBox();
            }
        }

        private void refreshPMBox ()
        {
            privatemsgdisplaybox.Document.Blocks.Clear();
            List<string> messages = foob.getPrivateMessages(username, currPmName);
            int i = 0;
            bool isFile = false;
            List<int> fileLoc = foob.getPMFileLoc(username, currPmName);
            foreach (string message in messages)
            {
                Console.WriteLine(message);
                foreach (int loc in fileLoc)
                {
                    if (loc == i)
                    {
                        isFile = true;
                    }
                }
                if (isFile)
                {
                    loadFilePM(message);
                }
                else
                {
                    privatemsgdisplaybox.AppendText(message);
                    privatemsgdisplaybox.AppendText(Environment.NewLine);
                }
                i++;
                isFile = false;
            }
        }

        private void privatesendmsgbtn_Click(object sender, RoutedEventArgs e)
        {
            if (currPmName != null)
            {
                refreshPMBox();
                if (privatemsgtxtbox.Text.Equals(""))
                {
                    MessageBox.Show("Please enter a message.");
                }
                else
                {
                    string message = username + ": " + privatemsgtxtbox.Text;
                    privatemsgtxtbox.Clear();
                    foob.addPrivateMessage(username, currPmName, message, false);
                    privatemsgdisplaybox.AppendText(message);
                    privatemsgdisplaybox.AppendText(Environment.NewLine);
                }   
            }
            else
            {
                MessageBox.Show("Please select a contact.");
            }
        }

        private void privateuploadfilebtn_Click(object sender, RoutedEventArgs e)
        {
            if (currPmName != null)
            {
                refreshPMBox();
                Microsoft.Win32.OpenFileDialog openFileDialog = new Microsoft.Win32.OpenFileDialog();
                bool? response = openFileDialog.ShowDialog();
                string filepath = "";
                if (response == true)
                {
                    filepath = openFileDialog.FileName;
                    privatemsgtxtbox.Clear();
                    foob.addPrivateMessage(username, currPmName, username + ": ", false);
                    foob.addPrivateMessage(username, currPmName, filepath, true);
                    privatemsgdisplaybox.AppendText(username + ": ");
                    loadFilePM(filepath);
                }
                else
                {
                    MessageBox.Show("No file selected.");
                }
            }
            else
            {
                MessageBox.Show("Please select a contact.");
            }
        }

        private void loadFilePM(string filepath)
        {
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
            privatemsgdisplaybox.Document.Blocks.Add(paragraph);
            privatemsgdisplaybox.IsDocumentEnabled = true;
            privatemsgdisplaybox.IsReadOnly = true;
            privatemsgdisplaybox.AppendText(Environment.NewLine);
        }
    }
}
