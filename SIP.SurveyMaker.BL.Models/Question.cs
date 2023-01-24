namespace SIP.SurveyMaker.BL.Models
{
    public class Question
    {
        public List<Answer> Answers { get; set; }
        public Guid Id { get; set; }
        public string Text { get; set; }
    }
}