using DocuSign.eSign.Client;
using DocuSign.eSign.Client.Auth;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Net;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Channels;
using System.ServiceModel.Web;
using System.Text;
using System.Web;

namespace WcfService1
{
    // OBSERVAÇÃO: Você pode usar o comando "Renomear" no menu "Refatorar" para alterar o nome da classe "Service1" no arquivo de código, svc e configuração ao mesmo tempo.
    // OBSERVAÇÃO: Para iniciar o cliente de teste do WCF para testar esse serviço, selecione Service1.svc ou Service1.svc.cs no Gerenciador de Soluções e inicie a depuração.
    public class Service1 : IService1
    {
        [DataMember]
        public const string RestApiUrl = "https://demo.docusign.net/restapi";

        // These items are all registered at the DocuSign Admin console and are required 
        // to perform the OAuth flow.
        [DataMember]
        public const string client_id = "d2f37ac6-e1d5-459b-af4a-6ae4811adb93";
        [DataMember]
        public const string client_secret = "0f891e9d-ad6c-4f1c-a3f9-8cd478eb9d63";
        [DataMember]
        public const string redirect_uri = "https://account-d.docusign.com";//https://account-d.docusign.com

        // This is an application-speicifc param that may be passed around during the OAuth
        // flow. It allows the app to track its flow, in addition to more security.
        [DataMember]
        public const string stateOptional = "testState";

        // This will be returned to the test via the callback url after the
        // user authenticates via the browser.


        // This will be filled in with the access_token retrieved from the token endpoint using the code above.
        // This is the Bearer token that will be used to make API calls.
        public static string AccessCode { get; internal set; }

        // This will be filled in with the access_token retrieved from the token endpoint using the code above.
        // This is the Bearer token that will be used to make API calls.
        public static string AccessToken { get; set; }
        public static string StateValue { get; internal set; }

        public static string AccountId { get; set; }
        public static string BaseUri { get; set; }

        public static string ACCESS_TOKEN { get; set; }

        public string GetData(int value)
        {
            return string.Format("You entered: {0}", value);
        }

        public CompositeType GetDataUsingDataContract(CompositeType composite)
        {
            if (composite == null)
            {
                throw new ArgumentNullException("composite");
            }
            if (composite.BoolValue)
            {
                composite.StringValue += "Suffix";
            }
            return composite;
        }

        public void Auth()
        {
            CSharpSDKSamples.Main();


        }

        public void PostToken() {
            ApiClient apiClient = new ApiClient(RestApiUrl);
            DocuSign.eSign.Client.Configuration.Default.ApiClient = apiClient;
            //HttpContext httpContext = HttpContext.Current;
           // var request = HttpContext.Current.Request;
            //var v =   request.Cookies.Get("AcessToken").Value;
           // var v = request.Cookies["AccessToken"].Value;
            // var v = WebOperationContext.Current.IncomingRequest.Headers[HttpRequestHeader.Cookie]; 


            var vT = AccessToken;

            //HttpContext.Current.Response.Headers.Add("Authorization", vT);
            ACCESS_TOKEN = apiClient.GetOAuthToken(client_id, client_secret, true, vT);

            OAuth.UserInfo userInfo = apiClient.GetUserInfo(ACCESS_TOKEN);
            Console.WriteLine("Access_token: " + ACCESS_TOKEN);

        }

        public void Callback() {

            HttpContext httpContext = HttpContext.Current;
            var headerValue = httpContext.Request.Params["code"];
            AccessToken = headerValue;


            ApiClient apiClient = new ApiClient(RestApiUrl);
            DocuSign.eSign.Client.Configuration.Default.ApiClient = apiClient;
          //  string accessToken = apiClient.GetOAuthToken(client_id, client_secret, true, AccessToken);

            System.Diagnostics.Process.Start("http://localhost:63208/Service1.svc/PostToken");
         
           //var authorizationField = headerList.Get("code");
           // var v = code;
           var vt = "";
        }


        public string GetUserIfo() {
            ApiClient apiClient = new ApiClient(RestApiUrl);
            DocuSign.eSign.Client.Configuration.Default.ApiClient = apiClient;

            OAuth.UserInfo userInfo = apiClient.GetUserInfo(ACCESS_TOKEN);

            string accountId = string.Empty;

            // find default account (if multiple present)
            foreach (var item in userInfo.GetAccounts())
            {
                if (item.GetIsDefault() == "true")
                {
                    accountId = item.AccountId();
                    apiClient = new ApiClient(item.GetBaseUri() + "/restapi");
                    break;
                }
            }

            return accountId;
        }

    }
}
