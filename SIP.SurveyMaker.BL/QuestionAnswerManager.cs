using Microsoft.EntityFrameworkCore.Storage;
using SIP.SurveyMaker.BL.Models;
using SIP.SurveyMaker.PL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SIP.SurveyMaker.BL
{
    public class QuestionAnswerManager
    {
        public async static Task<int> Insert(Question question, Answer answer, bool isCorrect, bool rollback = false)
        {
            try
            {
                IDbContextTransaction transaction = null;

                using (SurveyMakerEntities dc = new SurveyMakerEntities())
                {
                    if (rollback) transaction = dc.Database.BeginTransaction();

                    tblQuestionAnswer newrow = new tblQuestionAnswer();
                    newrow.Id = Guid.NewGuid();
                    newrow.QuestionId = question.Id;
                    newrow.AnswerId = answer.Id;
                    newrow.IsCorrect = isCorrect;

                    dc.tblQuestionAnswers.Add(newrow);
                    int results = dc.SaveChanges();

                    if (rollback) transaction.Rollback();

                    return results;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async static Task<int> Delete(Guid QuestionId, Guid AnswerId, bool rollback = false)
        {
            try
            {
                IDbContextTransaction transaction = null;
                using (SurveyMakerEntities dc = new SurveyMakerEntities())
                {
                    tblQuestionAnswer row = dc.tblQuestionAnswers.FirstOrDefault(c => c.QuestionId == QuestionId && c.AnswerId == AnswerId);
                    int results = 0;

                    if (row != null)
                    {
                        if (rollback) transaction = dc.Database.BeginTransaction();
                        dc.tblQuestionAnswers.Remove(row);

                        results = dc.SaveChanges();
                        if (rollback) transaction.Rollback();
                    }
                    else
                        throw new Exception("Row was not found.");
                    return results;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
