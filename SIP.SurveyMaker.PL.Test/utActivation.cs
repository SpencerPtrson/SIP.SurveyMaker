using Microsoft.EntityFrameworkCore.Storage;

namespace SIP.SurveyMaker.PL.Test
{
    [TestClass]
    public class utActivation
    {
        protected SurveyMakerEntities dc;
        protected IDbContextTransaction transaction;


        [TestInitialize]
        public void TestInitialize()
        {
            dc = new SurveyMakerEntities();
            transaction = dc.Database.BeginTransaction();
        }

        [TestCleanup]
        public void TestCleanup()
        {
            transaction.Rollback();
            transaction.Dispose();
            dc = null;
        }


        [TestMethod]
        public void LoadTest()
        {
            Assert.IsTrue(dc.tblActivations.Any());
        }

        [TestMethod]
        public void InsertTest()
        {
            int expected = 1;

            tblActivation newrow = new tblActivation()
            {
                Id = Guid.NewGuid(),
                QuestionId = (from q in dc.tblQuestions where q.Text == "Which state is FVTC in?" select q.Id).FirstOrDefault(),
                StartDate = DateTime.Now,
                EndDate = DateTime.Now,
                ActivationCode = "FJFJFJ"
            };

            dc.tblActivations.Add(newrow);
            int actual = dc.SaveChanges();

            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void UpdateTest()
        {
            // Get a row to update
            // Select * from tblAnswer where Answer = "Which state is Florida in?"

            tblActivation row = (from at in dc.tblActivations
                               where at.Id == dc.tblResponses.FirstOrDefault().Id
                               select at).FirstOrDefault();

            if (row != null)
            {
                // Change properties
                row.ActivationCode = "ABABAB";

                // Update the row
                int result = dc.SaveChanges();

                Assert.IsTrue(result == 1);
            }
        }

        [TestMethod]
        public void DeleteTest()
        {
            tblActivation row = (from at in dc.tblActivations
                                 where at.Id == dc.tblResponses.FirstOrDefault().Id
                                 select at).FirstOrDefault();

            if (row != null)
            {
                // Delete the row
                dc.tblActivations.Remove(row);
                int result = dc.SaveChanges();

                Assert.AreNotEqual(0, result);
            }
        }
    }
}