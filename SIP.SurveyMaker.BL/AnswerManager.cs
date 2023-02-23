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
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async static Task<Answer> LoadById(Guid id)
        {
            try
            {
                using (SurveyMakerEntities dc = new SurveyMakerEntities())
                {
                    tblAnswer tblAnswer = dc.tblAnswers.Where(c => c.Id == id).FirstOrDefault();
                    Answer answer = new Answer();

                    if (tblAnswer != null)
                    {
                        answer.Id = tblAnswer.Id;
                        answer.Text = tblAnswer.Text;
                        return answer;
                    }
                    else
                        throw new Exception("Could not find the row");
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async static Task<int> Insert(Answer answer, bool rollback = false)
        {
            try
            {
                IDbContextTransaction transaction = null;

                using (SurveyMakerEntities dc = new SurveyMakerEntities())
                {
                    if (rollback) transaction = dc.Database.BeginTransaction();

                    tblAnswer newrow = new tblAnswer();
                    newrow.Id = Guid.NewGuid();
                    newrow.Text = answer.Text;

                    dc.tblAnswers.Add(newrow);
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

        public async static Task<int> Insert(Guid id, String text, bool rollback = false)
        {
            try
            {
                Answer answer = new Answer { Id = id, Text = text };
                return await Insert(answer, rollback);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async static Task<int> Update(Answer answer, bool rollback = false)
        {
            try
            {
                IDbContextTransaction transaction = null;
                using (SurveyMakerEntities dc = new SurveyMakerEntities())
                {
                    tblAnswer row = dc.tblAnswers.FirstOrDefault(c => c.Id == answer.Id);
                    int results = 0;
                    if (row != null)
                    {
                        if (rollback) transaction = dc.Database.BeginTransaction();
                        row.Text = answer.Text;
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

        public async static Task<int> Delete(Guid id, bool rollback = false)
        {
            try
            {
                IDbContextTransaction transaction = null;
                using (SurveyMakerEntities dc = new SurveyMakerEntities())
                {
                    tblAnswer row = dc.tblAnswers.FirstOrDefault(c => c.Id == id);
                    int results = 0;

                    if (row != null)
                    {
                        if (rollback) transaction = dc.Database.BeginTransaction();
                        dc.tblAnswers.Remove(row);
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
