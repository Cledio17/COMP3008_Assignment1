using ChatServer;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
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
        string username = string.Empty;
        string roomName = string.Empty;
        User us;
        ChatRoom cr;
        string uss;
        private DataServerInterface foob;
        MainWindow loginMenu;
        public MainMenuWindow (DataServerInterface inFoob, User theUser, MainWindow inLoginMenu)
        {
            InitializeComponent();
            this.username = theUser.getUserName();
            us = theUser;
            usernamelabel.Content = username;
            userID.Content = us;
            this.foob = inFoob;
            loginMenu = inLoginMenu;
        }

        private void logoutbutton_Click(object sender, RoutedEventArgs e)
        {
            loginMenu.Show();   
            this.Close();
        }

        private void createButton_Click(object sender, RoutedEventArgs e)
        {
            roomName = chatroombox.Text;
            cr = foob.addServer(us, roomName);
            //foob.addJoinedServer(username, roomName);
            roomList.Items.Add(roomName);
            //refreshJoinedServer();
            chatroombox.Clear(); //clear the chat room box after creating the chat room
            us = foob.getUserAccountInfo(us.getUserName());
        }

        private void refreshJoinedServer()
        {
            roomList.Items.Clear();
            List<ChatRoom> joinedServers = foob.getJoinedServers(us.getUserName());
            foreach (ChatRoom room in joinedServers)
            {
                roomList.Items.Add(room.getChatRoomName());
            }
        }

        private void roomList_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            msgdisplaybox.Document.Blocks.Clear();
            int index = 0;
            participantlist.Items.Clear();
            roomName = roomList.SelectedItem.ToString();
            List<ChatRoom> availbleRoom = us.getChatRooms();
            foreach (ChatRoom room in availbleRoom)
            {
                if (room.getChatRoomName().Equals(roomName, StringComparison.OrdinalIgnoreCase))
                {
                    cr = room;
                }
            }
            currRoom.Content = cr.getId();
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
                msgdisplaybox.IsEnabled = true;
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

        private void findButton_Click(object sender, RoutedEventArgs e)
        {
            //serverID = int.Parse(findChatRoom.Text);
            //HashSet<ChatRoom> availbleRoom = ChatServerManager.getAllServer();
            //foreach (ChatRoom room in availbleRoom)
            //{
            //    if (room.getId() == serverID)
            //    {
            //        cr = room;
            //        cr.addUser(us);
            //        roomList.Items.Add(cr.getChatRoomName());
            //        currRoom.Content = cr.getId();
            //    }
            //}
            //findChatRoom.Clear();
        }
    }
}
