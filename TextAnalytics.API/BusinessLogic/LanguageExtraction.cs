using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Azure.CognitiveServices.Language.TextAnalytics;
using Microsoft.Azure.CognitiveServices.Language.TextAnalytics.Models;

namespace TextAnalytics.API.BusinessLogic
{
    public class LanguageExtraction
    {

        public void Language()
        {
            // Extracting language
            Console.WriteLine("===== LANGUAGE EXTRACTION ======");

            LanguageBatchResult result = client.DetectLanguage(
                    new BatchInput(
                        new List<Input>()
                        {
                          new Input("1", "This is a document written in English."),
                          new Input("2", "Este es un document escrito en Español."),
                          new Input("3", "这是一个用中文写的文件")
                        }));

            // Printing language results.
            foreach (var document in result.Documents)
            {
                Console.WriteLine("Document ID: {0} , Language: {1}", document.Id, document.DetectedLanguages[0].Name);
            }
        }


    }
}
