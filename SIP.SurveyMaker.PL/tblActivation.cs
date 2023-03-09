using System;
using System.Collections.Generic;

namespace SIP.SurveyMaker.PL;

public partial class tblActivation
{
    public Guid Id { get; set; }

    public Guid QuestionId { get; set; }

    public DateTime StartDate { get; set; }

    public DateTime EndDate { get; set; }

    public string ActivationCode { get; set; } = null!;

    public virtual tblQuestion Question { get; set; } = null!;
}
