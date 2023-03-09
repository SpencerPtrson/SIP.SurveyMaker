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
    public class AnswerManager
    {
        public async static Task<List<Answer>> Load()
        {
            try
            {
                List<Answer> answers = new List<Answer>();

                await Task.Run(() =>
                {
                    using (SurveyMakerEntities dc = new SurveyMakerEntities())
                    {
                        foreach (tblAnswer q in dc.tblAnswers.ToList())
                        {
                            Answer answer = new Answer { Id = q.Id, Text = q.Text };
                            answers.Add(answer);
                        }
                    }
                });
                return answers;
            }
            catch (Exception ex) { throw ex; }
        }

        public async static Task<List<Answer>> LoadById(Guid QuestionId)
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

        public async static Task<int> Insert(Answer answer, bool rollback = false)
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

                        tblAnswer newrow = new tblAnswer();
                        newrow.Id = Guid.NewGuid();
                        newrow.Text = answer.Text;

                        dc.tblAnswers.Add(newrow);
                        results = dc.SaveChanges();
                        if (rollback) transaction.Rollback();
                    }
                });
                return results;
            }
            catch (Exception ex) { throw ex; }
        }

        public async static Task<int> Insert(Guid id, String text, bool rollback = false)
        {
            try
            {
                Answer answer = new Answer { Id = id, Text = text };
                return await Insert(answer, rollback);
            }
            catch (Exception ex) { throw ex; }
        }

        public async static Task<int> Update(Answer answer, bool rollback = false)
        {
            try
            {
                System.Diagnostics.Debug.WriteLine("Attempting update on answer: " + answer.Text + " with GUID: " + answer.Id);
                int results = 0;
                await Task.Run(() =>
                {
                    IDbContextTransaction transaction = null;
                    using (SurveyMakerEntities dc = new SurveyMakerEntities())
                    {
                        tblAnswer row = dc.tblAnswers.FirstOrDefault(c => c.Id == answer.Id);
                        if (row != null)
                        {
                            if (rollback) transaction = dc.Database.BeginTransaction();
                            row.Text = answer.Text;
                            results = dc.SaveChanges();
                            if (rollback) transaction.Rollback();
                        }
                        else
                            throw new Exception("Row was not found.");              
                    }
                });
                return results;
            }
            catch (Exception ex) { throw ex; }
        }

        public async static Task<int> Delete(Guid id, bool rollback = false)
        {
            try
            {
                int results = 0;
                await Task.Run(() =>
                {
                    IDbContextTransaction transaction = null;
                    using (SurveyMakerEntities dc = new SurveyMakerEntities())
                    {
                        tblAnswer row = dc.tblAnswers.FirstOrDefault(c => c.Id == id);

                        if (row != null)
                        {
                            if (rollback) transaction = dc.Database.BeginTransaction();

                            foreach (tblQuestionAnswer qa in dc.tblQuestionAnswers.Where(qa => qa.AnswerId == id))
                            {
                                dc.tblQuestionAnswers.Remove(qa);
                            }
                            foreach (tblResponse r in dc.tblResponses.Where(r => r.AnswerId == id))
                            {
                                dc.tblResponses.Remove(r);
                            }
                            dc.tblAnswers.Remove(row);

                            results = dc.SaveChanges();
                            if (rollback) transaction.Rollback();
                        }
                        else
                            throw new Exception("Row was not found.");
                    }
                });
                return results;
            }
            catch (Exception ex) { throw ex; }
        }
    }
}
