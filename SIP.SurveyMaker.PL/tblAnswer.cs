using System;
using System.Collections.Generic;

namespace SIP.SurveyMaker.PL;

public partial class tblAnswer
{
    public Guid Id { get; set; }

    public string Text { get; set; } = null!;

    public virtual ICollection<tblQuestionAnswer> tblQuestionAnswers { get; } = new List<tblQuestionAnswer>();
}
