using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Text;

namespace WcfService1
{
    // OBSERVAÇÃO: Você pode usar o comando "Renomear" no menu "Refatorar" para alterar o nome da interface "IService1" no arquivo de código e configuração ao mesmo tempo.
    [ServiceContract]
    public interface IService1
    {

        [OperationContract]
        string GetData(int value);

        [OperationContract]
        CompositeType GetDataUsingDataContract(CompositeType composite);

        [OperationContract]
        [WebInvoke(Method = "GET",

           ResponseFormat = WebMessageFormat.Json,

           BodyStyle = WebMessageBodyStyle.Wrapped,

           UriTemplate = "Auth")]
        void Auth();

        [OperationContract]
        [WebInvoke(Method = "POST",

            ResponseFormat = WebMessageFormat.Json,

            BodyStyle = WebMessageBodyStyle.Wrapped,

            UriTemplate = "PostToken")]
        void PostToken();


        [OperationContract]
        [WebInvoke(Method = "GET",

           ResponseFormat = WebMessageFormat.Json,

           BodyStyle = WebMessageBodyStyle.Bare,
            
           UriTemplate = "Callback")]
        void Callback();


        [OperationContract]
        [WebInvoke(Method = "GET",

           ResponseFormat = WebMessageFormat.Json,

           BodyStyle = WebMessageBodyStyle.Bare,

           UriTemplate = "GetUserIfo")]
        string GetUserIfo();
        // TODO: Adicione suas operações de serviço aqui
    }


    // Use um contrato de dados como ilustrado no exemplo abaixo para adicionar tipos compostos a operações de serviço.
    [DataContract]
    public class CompositeType
    {
        bool boolValue = true;
        string stringValue = "Hello ";

        [DataMember]
        public bool BoolValue
        {
            get { return boolValue; }
            set { boolValue = value; }
        }

        [DataMember]
        public string StringValue
        {
            get { return stringValue; }
            set { stringValue = value; }
        }
    }
}
