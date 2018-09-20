using DocuSign.eSign.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Threading;
using System.Web;
//8ed5689f-c7a0-4ad8-8ccb-53b4b7980e38 id tmplate
namespace WcfService1
{
    // C# setup and config

        [DataContract]
    public  class CSharpSDKSamples
    {
        // Point to DocuSign Demo (sandbox) environment for requests
        [DataMember]
        public const string RestApiUrl = "https://demo.docusign.net/restapi";

        // These items are all registered at the DocuSign Admin console and are required 
        // to perform the OAuth flow.
        [DataMember]
        public const string client_id = "d2f37ac6-e1d5-459b-af4a-6ae4811adb93";
        [DataMember]
        public const string client_secret = "************8185";
        [DataMember]
        public const string redirect_uri = "https://account-d.docusign.com";//https://account-d.docusign.com

        // This is an application-speicifc param that may be passed around during the OAuth
        // flow. It allows the app to track its flow, in addition to more security.
        [DataMember]
        public const string stateOptional = "testState";

        // This will be returned to the test via the callback url after the
        // user authenticates via the browser.
        public static string AccessCode { get; internal set; }

        // This will be filled in with the access_token retrieved from the token endpoint using the code above.
        // This is the Bearer token that will be used to make API calls.
        public static string AccessToken { get; set; }
        public static string StateValue { get; internal set; }

        public static string AccountId { get; set; }
        public static string BaseUri { get; set; }

        // This event handle is used to block the self-hosted Web service in the test
        // until the OAuth login is completed.
        public static ManualResetEvent WaitForCallbackEvent = null;

        // main entry method
        public static void Main()
        {
            /////////////////////////////////////////////////////////////////
            // Run Code Samples        
            /////////////////////////////////////////////////////////////////
            CSharpSDKSamples samples = new CSharpSDKSamples();

            // first we use the OAuth authorization code grant to get an API access_token
            samples.OAuthAuthorizationCodeFlowTest();
        }

        public void OAuthAuthorizationCodeFlowTest()
        {

            // Make an API call with the token
            ApiClient apiClient = new ApiClient(RestApiUrl);
            DocuSign.eSign.Client.Configuration.Default.ApiClient = apiClient;

            // Initiate the browser session to the Authentication server
            // so the user can login.
            
            string vTexto = "http://localhost:63208/Service1.svc/Callback";
            string accountServerAuthUrl = apiClient.GetAuthorizationUri(client_id, vTexto, true, stateOptional);


            System.Diagnostics.Process.Start(accountServerAuthUrl);
        }

        //public void Callback ()
    } // end class
}