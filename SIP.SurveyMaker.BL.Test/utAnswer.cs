using SIP.SurveyMaker.BL.Models;

namespace SIP.SurveyMaker.BL.Test
{
    public class utAnswer
    {
        [SetUp]
        public void Setup()
        {
        }

        [Test]
        public async Task LoadTest()
        {
            var task = await AnswerManager.Load();
            List<Answer> answers = task;
            Assert.AreEqual(13, answers.ToList().Count);
        }

        [Test]
        public async Task InsertTest()
        {
            int results = await AnswerManager.Insert(new Answer { Text = "Testing" }, true);
            Assert.IsTrue(results > 0);
        }

        [Test]
        public async Task UpdateTest()
        {
            List<Answer> answers = await AnswerManager.Load();
            Answer answer = answers.FirstOrDefault(c => c.Text == "Michigan");
            answer.Text = "Update Test";
            int results = await AnswerManager.Update(answer, true);
            Assert.IsTrue(results > 0);
        }


        // The Delete also deletes all QuestionAnswer combos that have the answer.
        [Test]
        public async Task DeleteTest()
        {
            List<Answer> answers = await AnswerManager.Load();
            Answer answer = answers.FirstOrDefault(c => c.Text == "Michigan");
            int results = await AnswerManager.Delete(answer.Id, true);
            Assert.IsTrue(results > 0);
        }
    }
}