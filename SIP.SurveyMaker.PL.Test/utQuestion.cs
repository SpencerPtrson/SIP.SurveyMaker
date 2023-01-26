using Microsoft.EntityFrameworkCore.Storage;

namespace SIP.SurveyMaker.PL.Test
{
    [TestClass]
    public class utQuestion
    {
        protected SurveyEntities dc;
        protected IDbContextTransaction transaction;

        [TestInitialize]
        public void TestInitialize()
        {
            dc = new SurveyEntities();
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
            Assert.IsTrue(dc.tblQuestions.Any());
        }

        [TestMethod]
        public void InsertTest()
        {
            int expected = 1;

            tblQuestion newrow = new tblQuestion()
            {
                Id = Guid.NewGuid(),
                Question = "Test Question"
            };

            dc.tblQuestions.Add(newrow);
            int actual = dc.SaveChanges();

            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void UpdateTest()
        {
            // Get a row to update
            // Select * from tblQuestion where Question = "Which state is Florida in?"

            tblQuestion row = (from q in dc.tblQuestions
                               where q.Question == "Which state is Florida in?"
                               select q).FirstOrDefault();

            if (row != null )
            {
                // Change properties
                row.Question = "Which state is Chicago in?";

                // Update the row
                int result = dc.SaveChanges();

                Assert.IsTrue(result == 1);
            }
        }

        [TestMethod]
        public void DeleteTest()
        {
            tblQuestion row = (from q in dc.tblQuestions
                               where q.Question == "Which state is Florida in?"
                               select q).FirstOrDefault();

            if (row != null)
            {
                // Delete the row
                dc.tblQuestions.Remove(row);
                int result = dc.SaveChanges();

                Assert.AreNotEqual(0, result);
            }
        }
    }
}