using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using ChatClient.ServiceChat;

namespace ChatClient
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window, ChatClient.ServiceChat.IService1Callback
    {
        bool isConnected = false;
        ServiceChat.Service1Client client;
        int ID;
        Window1 win1 = new Window1 ();
        
        public MainWindow()
        {
            InitializeComponent();  
        }

        void ConnectUser()
        {
            if (!isConnected)
            {
                client = new Service1Client(new System.ServiceModel.InstanceContext(this));
                ID = client.Connect(tbUserName.Text);
                tbUserName.IsEnabled = false;
                btConnect.Content = "Disconnect";
                isConnected = true;
            }
        }

        void DisconnectUser()
        {
            if (isConnected)
            {
                client.Disconnect(ID);
                client = null;
                tbUserName.IsEnabled = true;
                btConnect.Content = "Connect";
                isConnected = false;
            }
        }

        private void btConnect_Click(object sender, RoutedEventArgs e)
        {
            win1.sus2 = tbUserName.Text;
            if (isConnected)
            {
                DisconnectUser();
            }
            else
            {
                ConnectUser();
            }
        }

        public void MsgCallBack(string msg)
        {
            lbChat.Items.Add(msg);
        }


        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            DisconnectUser();
        }

        private void tbMessage_KeyDown(object sender, KeyEventArgs e)
        {
            if(e.Key == Key.Enter)
            {
                if (client != null)
                {
                    client.SendMsg(tbMessage.Text, ID);
                    tbMessage.Text = String.Empty;
                }
            }
        }

        private void btCheck_Click(object sender, RoutedEventArgs e)
        {
            lbcheck.Items.Clear();
            string[] bob = client.ListUsers();
            foreach (var user in bob)
            {
                if (user != tbUserName.Text.ToString())
                {
                    lbcheck.Items.Add(user);
                    MessageBox.Show(user);
                }
            }
        }

        private void lbcheck_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            int selectGuy = client.Lichka(lbcheck.SelectedItem.ToString());
            string Names1 = lbcheck.SelectedItem.ToString();
            string Names2 = tbUserName.Text.ToString();

            win1.sus = Names1.ToString();
            win1.sus2 = Names2.ToString();


            win1.Show();
        }
    }
}
