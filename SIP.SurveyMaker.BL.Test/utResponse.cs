using SIP.SurveyMaker.BL.Models;

namespace SIP.SurveyMaker.BL.Test
{
    public class utResponse
    {
        [SetUp]
        public void Setup()
        {
        }

        [Test]
        public async Task LoadByQuestionIdTest()
        {
            List<Question> questions = await QuestionManager.Load();
            Question question = questions.FirstOrDefault(c => c.Text == "Does fire need oxygen?");

            var task = await ResponseManager.LoadByQuestionId(question.Id);
            List<Response> responses = task;
            Assert.AreEqual(2, responses.ToList().Count);
        }

        [Test]
        public async Task InsertTest()
        {
            List<Question> questions = await QuestionManager.Load();
            Question question = questions.FirstOrDefault(c => c.Text == "Does fire need oxygen?");

            List<Answer> answers = await AnswerManager.Load();
            Answer answer = answers.FirstOrDefault(c => c.Text == "Florida");

            int results = await ResponseManager.Insert(new Response
            {
                QuestionId = question.Id,
                AnswerId = answer.Id,
                ResponseDate = DateTime.Now
            }, true);
            Assert.IsTrue(results > 0);
        }
    }
}