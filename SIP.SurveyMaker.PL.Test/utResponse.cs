using Microsoft.EntityFrameworkCore.Storage;

namespace SIP.SurveyMaker.PL.Test
{
    [TestClass]
    public class utResponse
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
            Assert.IsTrue(dc.tblResponses.Any());
        }

        [TestMethod]
        public void InsertTest()
        {
            int expected = 1;

            tblResponse newrow = new tblResponse()
            {
                Id = Guid.NewGuid(),
                QuestionId = (from q in dc.tblQuestions where q.Text == "Which state is FVTC in?" select q.Id).FirstOrDefault(),
                AnswerId = (from a in dc.tblAnswers where a.Text == "No" select a.Id).FirstOrDefault(),
                ResponseDate = DateTime.Now
            };

            dc.tblResponses.Add(newrow);
            int actual = dc.SaveChanges();

            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void UpdateTest()
        {
            // Get a row to update
            // Select * from tblAnswer where Answer = "Which state is Florida in?"

            tblResponse row = (from r in dc.tblResponses
                               where r.Id == dc.tblResponses.FirstOrDefault().Id
                               select r).FirstOrDefault();

            if (row != null)
            {
                // Change properties
                row.ResponseDate = DateTime.Now;

                // Update the row
                int result = dc.SaveChanges();

                Assert.IsTrue(result == 1);
            }
        }

        [TestMethod]
        public void DeleteTest()
        {
            tblResponse row = (from r in dc.tblResponses
                               where r.Id == dc.tblResponses.FirstOrDefault().Id
                               select r).FirstOrDefault();

            if (row != null)
            {
                // Delete the row
                dc.tblResponses.Remove(row);
                int result = dc.SaveChanges();

                Assert.AreNotEqual(0, result);
            }
        }
    }
}