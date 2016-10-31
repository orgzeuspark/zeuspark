using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ZeusPark.Service.Model
{
    public class QuestionVM
    {
        public long QuestionID { get; set; }

        public int ProductID { get; set; }

        public string ProductName { get; set; }

        public string Label { get; set; }

        public string Question { get; set; }
        public string Answer { get; set; }
        public DateTime SubmitTime { get; set; }
        public string SubmitBy { get; set; }
        public DateTime AnswerTime { get; set; }
    }
}
