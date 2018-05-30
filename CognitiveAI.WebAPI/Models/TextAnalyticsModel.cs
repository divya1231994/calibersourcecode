using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CognitiveAI.WebAPI.Models
{
    public class TextAnalyticsModel
    {

        public string Language { get; set; }
        public string KeyPhrases { get; set; }
        public string SentimentScore { get; set; }

    }
}
