using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;

namespace WCF_chat
{
    // ПРИМЕЧАНИЕ. Можно использовать команду "Переименовать" в меню "Рефакторинг", чтобы изменить имя интерфейса "IService1" в коде и файле конфигурации.
    [ServiceContract(CallbackContract = typeof(IServerChatCallback))]
    public interface IService1
    {
        [OperationContract]
        int Connect(string name);

        [OperationContract]
        void Disconnect(int id);

        [OperationContract(IsOneWay = true)]
        void SendMsg(string msg, int id );

        [OperationContract]
        string[] ListUsers();

        [OperationContract]
        int Lichka(string human);

        [OperationContract]
        void SendMessagePrivate(string message, string url);

        [OperationContract]
        string[] update(string url);

        [OperationContract]
        string SearchDialog(string user_1, string user_2);


    }

    public interface IServerChatCallback
    {
        [OperationContract(IsOneWay = true)]

        void MsgCallBack(string msg);
    }
}
