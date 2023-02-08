namespace SIP.SurveyMaker.BL.Models
{
    public class Question
    {
        public Guid Id { get; set; }
        public List<Answer> Answers { get; set; }
        public string Text { get; set; }
    }
}