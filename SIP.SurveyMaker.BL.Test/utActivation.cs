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
            List<Activation> activations = new List<Activation>();
            int results = 0;

            await Task.Run(() =>
            {
                using (SurveyMakerEntities dc = new SurveyMakerEntities())
                {
                    foreach (tblActivation tblActivation in dc.tblActivations.ToList())
                    {
                        Activation activation = new Activation
                        {
                            Id = tblActivation.Id,
                            QuestionId = tblActivation.Id,
                            StartDate = DateTime.Now,
                            EndDate = DateTime.Now,
                            ActivationCode = tblActivation.ActivationCode,
                        };
                        activations.Add(activation);
                    }
                }
            });

            Activation at = activations.FirstOrDefault(a => a.ActivationCode == "DFNON1");
            at.ActivationCode = "123456";

            results = await ActivationManager.Update(at, true);
            Assert.IsTrue(results > 0);
        }

        [Test]
        public async Task DeleteTest()
        {
            List<Activation> activations = new List<Activation>();
            int results = 0;
            await Task.Run(() =>
            {
                using (SurveyMakerEntities dc = new SurveyMakerEntities())
                {
                    foreach (tblActivation tblActivation in dc.tblActivations.ToList())
                    {
                        Activation activation = new Activation
                        {
                            Id = tblActivation.Id,
                            QuestionId = tblActivation.Id,
                            StartDate = DateTime.Now,
                            EndDate = DateTime.Now,
                            ActivationCode = tblActivation.ActivationCode,
                        };
                        activations.Add(activation);
                    }
                }
            });
            Activation activation = activations.FirstOrDefault(at => at.ActivationCode == "DFNON1");
            results = await ActivationManager.Delete(activation.Id, true);
            Assert.IsTrue(results > 0);
        }
    }
}