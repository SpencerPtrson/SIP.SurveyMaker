using Microsoft.EntityFrameworkCore.Storage;
using System.Security.Cryptography;

namespace SIP.SurveyMaker.PL.Test
{
    [TestClass]
    public class utQuestionAnswer
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
            Assert.IsTrue(dc.tblQuestionAnswers.Any());
        }

        [TestMethod]
        public void InsertTest()
        {
            int expected = 1;

            tblQuestionAnswer newrow = new tblQuestionAnswer()
            {
                Id = Guid.NewGuid(),
                QuestionId = (from q in dc.tblQuestions where q.Question == "Which state is FVTC in?" select q.Id).FirstOrDefault(),
                AnswerId = (from a in dc.tblAnswers where a.Answer == "No" select a.Id).FirstOrDefault()
            };

            dc.tblQuestionAnswers.Add(newrow);
            int actual = dc.SaveChanges();

            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void UpdateTest()
        {

            tblQuestionAnswer row = (from qa in dc.tblQuestionAnswers
                                     where qa.QuestionId == (from q in dc.tblQuestions where q.Question == "Which state is Florida in?" select q.Id).FirstOrDefault()
                                     && qa.AnswerId == (from a in dc.tblAnswers where a.Answer == "Ohio" select a.Id).FirstOrDefault()
                                     select qa).FirstOrDefault();

            if (row != null)
            {
                Guid aId = (from a in dc.tblAnswers where a.Answer == "Michigan" select a.Id).FirstOrDefault();
                // Change properties
                row.AnswerId = aId;

                // Update the row
                int result = dc.SaveChanges();

                Assert.IsTrue(result == 1);
            }
        }


        // Currently deletes a given QuestionId + AnswerId combo
        [TestMethod]
        public void DeleteTest()
        {
            tblQuestionAnswer row = (from qa in dc.tblQuestionAnswers
                                        where qa.QuestionId == (from q in dc.tblQuestions where q.Question == "Which state is Florida in?" select q.Id).FirstOrDefault()
                                        && qa.AnswerId == (from a in dc.tblAnswers where a.Answer == "Ohio" select a.Id).FirstOrDefault()
                                        select qa).FirstOrDefault();

            if (row != null)
            {
                // Delete the row
                dc.tblQuestionAnswers.Remove(row);
                int result = dc.SaveChanges();

                Assert.AreNotEqual(0, result);
            }
        }

        [TestMethod]
        public void LazyLoadingTest()
        {
            Assert.IsNotNull(dc.tblQuestionAnswers.FirstOrDefault().Answer);
        }
    }
}