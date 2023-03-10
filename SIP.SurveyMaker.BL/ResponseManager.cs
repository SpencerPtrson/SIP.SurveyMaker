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
    public class ResponseManager
    {
        public async static Task<List<Response>> LoadById(Guid QuestionId)
        {
            try
            {
                List<Answer> answers = new List<Answer>();

                await Task.Run(() =>
                {
                    using (SurveyMakerEntities dc = new SurveyMakerEntities())
                    {
                        // Get a list of question / answer pairs with the given question id
                        List<tblQuestionAnswer> tblQuestionAnswers = dc.tblQuestionAnswers.Where(c => c.QuestionId == QuestionId).ToList();

                        // Iterate through that list and add the corresponding tblAnswer value to "answers"
                        foreach (tblQuestionAnswer qa in tblQuestionAnswers)
                        {
                            tblAnswer tblAnswer = dc.tblAnswers.Where(d => d.Id == qa.AnswerId).FirstOrDefault();
                            Answer answer = new Answer()
                            {
                                Id = tblAnswer.Id,
                                Text = tblAnswer.Text,
                                IsCorrect = qa.IsCorrect
                            };
                            answers.Add(answer);
                        } 
                    }
                });
                return answers;
            }
            catch (Exception ex) { throw ex; }
        }

        public async static Task<int> Insert(Response response, bool rollback = false)
        {
            try
            {
                int results = 0;
                await Task.Run(() =>
                {
                    using (SurveyMakerEntities dc = new SurveyMakerEntities())
                    {
                        IDbContextTransaction transaction = null;
                        if (rollback) transaction = dc.Database.BeginTransaction();

                        tblResponse newrow = new tblResponse();
                        newrow.Id = Guid.NewGuid();
                        newrow.QuestionId = response.QuestionId;
                        newrow.AnswerId = response.AnswerId;
                        newrow.ResponseDate = response.ResponseDate;

                        dc.tblResponses.Add(newrow);
                        results = dc.SaveChanges();
                        if (rollback) transaction.Rollback();
                    }
                });
                return results;
            }
            catch (Exception ex) { throw ex; }
        }
    }
}
