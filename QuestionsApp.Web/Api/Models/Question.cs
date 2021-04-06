using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace QuestionsApp.Web.Api.Models
{
    public class Question
    {
        public int ID { get; set; }
        public string Content { get; set; }
        public int Votes { get; set; }
    }
}
