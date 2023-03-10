using SIP.SurveyMaker.BL.Models;
using SIP.SurveyMaker.PL;

namespace SIP.SurveyMaker.BL.Test
{
    public class utActivation
    {
        [SetUp]
        public void Setup()
        {
        }

        [Test]
        public async Task LoadTest()
        {
            var task = await ActivationManager.Load();
            List<Activation> activations = task;
            Assert.AreEqual(3, activations.ToList().Count);
        }

        [Test]
        public async Task LoadByQuestionIdTest()
        {
            List<Question> questions = await QuestionManager.Load();
            Question question = questions.FirstOrDefault(c => c.Text == "Does fire need oxygen?");

            var task = await ActivationManager.LoadByQuestionId(question.Id);
            List<Activation> activations = task;
            Assert.AreEqual(1, activations.ToList().Count);
        }

        [Test]
        public async Task InsertTest()
        {
            List<Question> questions = await QuestionManager.Load();
            Question question = questions.FirstOrDefault(c => c.Text == "Does fire need oxygen?");

            int results = await ActivationManager.Insert(new Activation
            {
                QuestionId = question.Id,
                StartDate = DateTime.Now,
                EndDate = DateTime.Now,
                ActivationCode = "123ABC"
            }, true);
            Assert.IsTrue(results > 0);
        }

        [Test]
        public async Task UpdateTest()
        {
            List<Activation> activations = await ActivationManager.Load();
            int results = 0;

            Activation at = activations.FirstOrDefault(a => a.ActivationCode == "DFNON1");
            at.ActivationCode = "123456";

            results = await ActivationManager.Update(at, true);
            Assert.IsTrue(results > 0);
        }

        [Test]
        public async Task DeleteTest()
        {
            List<Activation> activations = await ActivationManager.Load();
            int results = 0;

            Activation activation = activations.FirstOrDefault(at => at.ActivationCode == "DFNON1");
            results = await ActivationManager.Delete(activation.Id, true);
            Assert.IsTrue(results > 0);
        }
    }
}