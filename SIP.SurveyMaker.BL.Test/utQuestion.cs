using SIP.SurveyMaker.BL.Models;

namespace SIP.SurveyMaker.BL.Test
{
    public class utQuestion
    {
        [SetUp]
        public void Setup()
        {
        }

        [Test]
        public async Task LoadTest()
        {
            var task = await QuestionManager.Load();
            List<Question> questions = task;
            Assert.AreEqual(5, questions.ToList().Count);
        }

        [Test]
        public async Task InsertTest()
        {
            int results = await QuestionManager.Insert(new Question { Text = "Testing" }, true);
            Assert.IsTrue(results > 0);
        }

        [Test]
        public async Task UpdateTest()
        {
            List<Question> questions = await QuestionManager.Load();
            Question question = questions.FirstOrDefault(c => c.Text == "Does fire need oxygen?");
            question.Text = "Update Test";
            int results = await QuestionManager.Update(question, true);
            Assert.IsTrue(results > 0);
        }


        // The Delete also deletes all QuestionAnswer combos that have the question.
        [Test]
        public async Task DeleteTest()
        {
            List<Question> questions = await QuestionManager.Load();
            Question question = questions.FirstOrDefault(c => c.Text == "Does fire need oxygen?");
            int results = await QuestionManager.Delete(question.Id, true);
            Assert.IsTrue(results > 0);
        }
    }
}