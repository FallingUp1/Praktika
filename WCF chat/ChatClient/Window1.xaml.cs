using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Policy;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using ChatClient.ServiceChat;

namespace ChatClient
{
    /// <summary>
    /// Логика взаимодействия для Window1.xaml
    /// </summary>
    public partial class Window1 : Window, ChatClient.ServiceChat.IService1Callback
    {
        ServiceChat.Service1Client client;
        public string sus { get; set; }
        public string sus2 { get; set; }
        int ID;
        string SendM;
        string URL;
        Thread tr;

        public Window1()
        {
            InitializeComponent();
        }

        public void MsgCallBack(string msg)
        {
            lbLS.Items.Add(msg);
        }

        void update()
        {
            ListBox listbox = lbLS;
            while (true)
            {
                Dispatcher.Invoke(() => listbox.Items.Clear());
                string[] Check3 = client.update(URL);
                if (URL != "URL ERROR")
                {
                    foreach(string message in Check3)
                    {
                        Dispatcher.Invoke(() => listbox.Items.Add(message));
                    }
                }
                else
                {
                    MessageBox.Show("URL ERROR");
                }
                Thread.Sleep(5000);
            }
        }
        void updateNow()
        {
            ListBox listbox = lbLS;
            listbox.Items.Clear();
            string[] Check4 = client.update(URL);
            if (URL != "URL ERROR")
            {
                foreach (string message in Check4)
                {
                    listbox.Items.Add(message);
                }
            }
            else
            {
                MessageBox.Show("URL ERROR");
            }
        }
   

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {

            HName.Text = sus;
            client = new Service1Client(new System.ServiceModel.InstanceContext(this));
            URL = client.SearchDialog(sus, sus2);
            tr = new Thread(new ThreadStart(update));
            tr.Start();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            SendM = tbLS.Text;
            URL = client.SearchDialog(sus, sus2);
            client.SendMessagePrivate(SendM,URL);
            updateNow();
        }
    }
}
