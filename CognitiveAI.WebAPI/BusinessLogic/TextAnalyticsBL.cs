using CognitiveAI.WebAPI.Models;
using Microsoft.Azure.CognitiveServices.Language.TextAnalytics;
using Microsoft.Azure.CognitiveServices.Language.TextAnalytics.Models;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CognitiveAI.WebAPI.BusinessLogic
{
    public class TextAnalyticsBL
    {

        private ITextAnalyticsAPI client;
        public TextAnalyticsBL()
        {
            // Create a client.
            client = new TextAnalyticsAPI();
            client.AzureRegion = AzureRegions.Westcentralus;
            client.SubscriptionKey = "6499bf6efdbe41a18c7ebbd41d870777";
        }

        public TextAnalyticsModel TextAnalysis(string value)
        {

            TextAnalyticsModel objTextAnalyticsModel = new TextAnalyticsModel();

            // Extracting language
            //"===== LANGUAGE EXTRACTION ======");

            LanguageBatchResult result = client.DetectLanguage(
                    new BatchInput(
                        new List<Input>()
                        {
                          new Input("1", value)
                        })
                        );

            StringBuilder strbld = new StringBuilder();
            string Language = string.Empty;

            var allCultures = CultureInfo.GetCultures(CultureTypes.AllCultures);
            string TwoLetterISOLanguageName = string.Empty;



            // Printing language results.
            foreach (var document in result.Documents)
            {
                //strbld.Append(" Language: " + document.DetectedLanguages[0].Name);
                Language = document.DetectedLanguages[0].Name;
                objTextAnalyticsModel.Language = document.DetectedLanguages[0].Name;
                TwoLetterISOLanguageName = allCultures.FirstOrDefault(c => c.DisplayName == Language).Name;
            }

            // Getting key-phrases
            //Console.WriteLine("\n\n===== KEY-PHRASE EXTRACTION ======");

            KeyPhraseBatchResult result2 = client.KeyPhrases(
                    new MultiLanguageBatchInput(
                        new List<MultiLanguageInput>()
                        {
                          new MultiLanguageInput(TwoLetterISOLanguageName, "1", value)
                        }));


            // Printing keyphrases
            foreach (var document in result2.Documents)
            {
                // strbld.Append(" Key Phrases: ");
                foreach (string keyphrase in document.KeyPhrases)
                {
                    strbld.Append(keyphrase + ", ");
                    //Console.WriteLine("\t\t" + keyphrase);
                    objTextAnalyticsModel.KeyPhrases = strbld.ToString();
                }
            }

            // Extracting sentiment
            //Console.WriteLine("\n\n===== SENTIMENT ANALYSIS ======");

            SentimentBatchResult result3 = client.Sentiment(
                    new MultiLanguageBatchInput(
                        new List<MultiLanguageInput>()
                        {
                          new MultiLanguageInput(TwoLetterISOLanguageName, "0", value)
                        }));


            // Printing sentiment results
            foreach (var document in result3.Documents)
            {
                //Console.WriteLine("Document ID: {0} , Sentiment Score: {1:0.00}", document.Id, document.Score);
                strbld.Append(" Sentiment Score: " + document.Score);
                objTextAnalyticsModel.SentimentScore = document.Score.ToString();
            }



            return objTextAnalyticsModel;
        }

    }
}
