using System;
using System.Collections.Generic;

namespace SIP.SurveyMaker.PL;

public partial class tblQuestionAnswer
{
    public Guid Id { get; set; }

    public Guid QuestionId { get; set; }

    public Guid AnswerId { get; set; }

    public bool IsCorrect { get; set; }

    public virtual tblAnswer Answer { get; set; } = null!;

    public virtual tblQuestion Question { get; set; } = null!;
}
