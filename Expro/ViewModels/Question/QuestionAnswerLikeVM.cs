using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Expro.Common;
using Expro.Common.Utilities;
using Expro.Models;

namespace Expro.ViewModels
{
    public class QuestionAnswerLikeCreateVM
    {
        public int QuestionAnswerID { get; set; }
        public bool IsPositive { get; set; }

        //public QuestionAnswer ToModel()
        //{
        //    QuestionAnswer answer = new QuestionAnswer()
        //    {
        //        Text = this.Text,
        //        QuestionID = this.QuestionID
        //    };

        //    return answer;
        //}
    }
}
