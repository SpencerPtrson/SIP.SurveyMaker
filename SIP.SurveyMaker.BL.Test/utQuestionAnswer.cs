using SIP.SurveyMaker.BL.Models;

namespace SIP.SurveyMaker.BL.Test
{
    public class utQuestionAnswer
    {
        [SetUp]
        public void Setup()
        {
        }

        [Test]
        public async Task InsertTest()
        {
            List<Question> questions = await QuestionManager.Load();
            Question question = questions.FirstOrDefault(c => c.Text == "Does fire need oxygen?");

            List<Answer> answers = await AnswerManager.Load();
            Answer answer = answers.FirstOrDefault(c => c.Text == "Florida");

            int results = await QuestionAnswerManager.Insert(question, answer, false, true);
            Assert.IsTrue(results > 0);
        }

        // Delete one specific combination of question and answer
        [Test]
        public async Task DeleteTest()
        {
            List<Question> questions = await QuestionManager.Load();
            Question question = questions.FirstOrDefault(c => c.Text == "Does fire need oxygen?");

            List<Answer> answers = await AnswerManager.Load();
            Answer answer = answers.FirstOrDefault(c => c.Text == "No");

            int results = await QuestionAnswerManager.Delete(question.Id, answer.Id, true);
            Assert.IsTrue(results > 0);
        }

        // The Delete also deletes all QuestionAnswer combos that have the question.
        [Test]
        public async Task DeleteByIdTest()
        {
            List<Question> questions = await QuestionManager.Load();
            Question question = questions.FirstOrDefault(c => c.Text == "Does fire need oxygen?");

            int results = await QuestionAnswerManager.DeleteByQuestionId(question.Id, true);
            Assert.IsTrue(results > 0);
        }
    }
}