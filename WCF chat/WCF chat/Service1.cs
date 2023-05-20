using System;
using System.Collections.Generic;
using System.Data.SqlTypes;
using System.IO;
using System.Linq;
using System.Runtime.Serialization;
using System.Security.Cryptography.X509Certificates;
using System.Security.Policy;
using System.ServiceModel;
using System.ServiceModel.PeerResolvers;
using System.Text;

namespace WCF_chat
{
    [ServiceBehavior(InstanceContextMode = InstanceContextMode.Single)]
   
    public class Service1 : IService1
    {
        List<ServerUser> users = new List<ServerUser>();
        int nextId = 1;
        public StreamWriter SW;
        public StreamReader SR;
        Boolean tf;

        public int Connect(string name)
        {
            ServerUser user = new ServerUser() { 
            
                ID = nextId,
                Name = name,
                OperationContext = OperationContext.Current
           };  
            nextId++;

            SendMsg(user.Name + " подключился к чату!", 0);
            users.Add(user);
            return user.ID; 
        }

        public void Disconnect(int id)
        {
            var user = users.FirstOrDefault(i => i.ID == id);
            if (user != null)
            {
                users.Remove(user);
                SendMsg(user.Name + " покинул чат!", 0);
            }
        }

        public void SendMsg(string msg, int id)
        {
            foreach (var item in users)
            {
                string answer = DateTime.Now.ToShortTimeString();
                
                var user = users.FirstOrDefault(i => i.ID == id);
                if (user != null)
                {
                    answer += ": " + user.Name + " ";
                }
                answer += msg;

                item.OperationContext.GetCallbackChannel<IServerChatCallback>().MsgCallBack(answer);

            }
        }

        public string[] ListUsers()
        {
                List<string> people = new List<string>();
               foreach (var user in users)
                {
                    people.Add(user.Name);
                }
                string[] users2 = new string[people.Count];
                people.CopyTo(users2, 0);
                return users2;
        }
        public int Lichka(string human)
        {
            foreach (var user in users)
            {
                if(human == user.Name)
                {
                    return user.ID;
                }
            }
            return 0;
        }
        public void SendMessagePrivate(string message, string url)
        {
            string answer = DateTime.Now.ToShortTimeString();
            SW = new StreamWriter(url, true);
            SW.WriteLine(answer + " : " + message);
            SW.Close();
        }


        public string[] update(string url)
        {
            List<string> MessageList = new List<string>();
            using (SR = new StreamReader(url))
            {
                string line;
                while ((line = SR.ReadLine()) != null)
                {
                    MessageList.Add(line);
                }
            }
            return MessageList.ToArray();
        }

        public string SearchDialog(string user_1, string user_2)
        {
            string url2;
            string user1 = "user" + user_1 + "user" + user_2 + ".txt";
            string user2 = "user" + user_2 + "user" + user_1 + ".txt";
            tf = false;
            url2 = "C:\\Users\\user\\Documents\\11ИС-263\\WCF chat\\ChatHost\\Личка\\";
            string[] usersDialogs = Directory.GetFiles(url2);

            foreach(var dialog in usersDialogs)
            {
                if(dialog == url2 + user1)
                {
                    tf = true;
                    url2 += user1;
                    return url2;
                }
                else if (dialog == url2 + user2)
                {
                    tf = true;
                    url2 += user2;
                    return url2;
                }
            }
            if (tf == false)
            {
                url2 += user1;
                using (FileStream fs = File.Create(url2))
                {
                    byte[] info = new UTF8Encoding(true).GetBytes("");
                    fs.Write(info, 0, 0);
                }
                tf = true;
                return url2;
            }
            return "url";
        }
    }
}
