using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SIP.SurveyMaker.BL.Models
{
    public class Answer
    {
        public Guid Id { get; set; }
        public bool IsCorrect { get; set; }
        public string Text { get; set; }
    }
}
