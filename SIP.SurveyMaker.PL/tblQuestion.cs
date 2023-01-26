using System;
using System.Collections.Generic;

namespace SIP.SurveyMaker.PL;

public partial class tblQuestion
{
    public Guid Id { get; set; }

    public string Question { get; set; } = null!;

    public virtual ICollection<tblQuestionAnswer> tblQuestionAnswers { get; } = new List<tblQuestionAnswer>();
}
