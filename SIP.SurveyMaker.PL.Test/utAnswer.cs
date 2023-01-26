using Microsoft.EntityFrameworkCore.Storage;

namespace SIP.SurveyMaker.PL.Test
{
    [TestClass]
    public class utAnswer
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
            Assert.IsTrue(dc.tblAnswers.Any());
        }

        [TestMethod]
        public void InsertTest()
        {
            int expected = 1;

            tblAnswer newrow = new tblAnswer()
            {
                Id = Guid.NewGuid(),
                Answer = "Test Answer"
            };

            dc.tblAnswers.Add(newrow);
            int actual = dc.SaveChanges();

            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void UpdateTest()
        {
            // Get a row to update
            // Select * from tblAnswer where Answer = "Which state is Florida in?"

            tblAnswer row = (from a in dc.tblAnswers
                               where a.Answer == "Which state is Florida in?"
                               select a).FirstOrDefault();

            if (row != null)
            {
                // Change properties
                row.Answer = "Which state is Chicago in?";

                // Update the row
                int result = dc.SaveChanges();

                Assert.IsTrue(result == 1);
            }
        }

        [TestMethod]
        public void DeleteTest()
        {
            tblAnswer row = (from a in dc.tblAnswers
                               where a.Answer == "Which state is Florida in?"
                               select a).FirstOrDefault();

            if (row != null)
            {
                // Delete the row
                dc.tblAnswers.Remove(row);
                int result = dc.SaveChanges();

                Assert.AreNotEqual(0, result);
            }
        }
    }
}