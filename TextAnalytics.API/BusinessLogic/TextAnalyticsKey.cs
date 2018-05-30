using Microsoft.Azure.CognitiveServices.Language.TextAnalytics;
using Microsoft.Azure.CognitiveServices.Language.TextAnalytics.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TextAnalytics.API.BusinessLogic
{
    //Sealed Class
    public sealed class TextAnalyticsKey
    {
        // Create a client.
        ITextAnalyticsAPI client = new TextAnalyticsAPI();
        
        client.AzureRegion = AzureRegions.Westcentralus;
            client.SubscriptionKey = "6499bf6efdbe41a18c7ebbd41d870777";

        //private constructor
        public TextAnalyticsKey()
        {
            
        }

       

        private static TextAnalyticsKey instance = null;
        private static readonly object padlock = new object();

        public static TextAnalyticsKey Instance
        {
            get {

                //Thread Safe
                lock (padlock)
                {
                    //not threadsafe
                    if (instance == null)
                    {
                        instance = new TextAnalyticsKey();
                    }

                    return instance;
                }
              
            }
        }

    }
}
