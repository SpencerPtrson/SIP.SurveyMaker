using Microsoft.EntityFrameworkCore.Migrations.Operations;
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
    public class QuestionManager
    {
        public async static Task<List<Question>> Load()
        {
            try
            {
                List<Question> questions = new List<Question>();

                await Task.Run(() =>
                {
                    using (SurveyMakerEntities dc = new SurveyMakerEntities())
                    {
                        foreach (tblQuestion q in dc.tblQuestions.ToList())
                        {
                            Question question = new Question { Id = q.Id, Text = q.Text };

                            question.Answers = new List<Answer>();

                            foreach (tblQuestionAnswer qa in q.tblQuestionAnswers.ToList())
                            {
                                Answer answer = new Answer { Id = qa.Id, IsCorrect = qa.IsCorrect, Text = qa.Answer.Text };
                                question.Answers.Add(answer);
                            }
                            questions.Add(question);
                        }
                    }
                });
                return questions;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async static Task<Question> LoadById(Guid id)
        {
            try
            {
                using (SurveyMakerEntities dc = new SurveyMakerEntities())
                {
                    tblQuestion tblQuestion = dc.tblQuestions.Where(c => c.Id == id).FirstOrDefault();
                    Question question = new Question();

                    if (tblQuestion != null)
                    {
                        question.Id = tblQuestion.Id;
                        question.Text = tblQuestion.Text;

                        foreach (tblQuestionAnswer qa in dc.tblQuestionAnswers.Where(qa => qa.QuestionId == id).ToList())
                        {
                            Answer answer = new Answer { Id = qa.Id, IsCorrect = qa.IsCorrect, Text = qa.Answer.Text };
                            question.Answers.Add(answer);
                        }

                        return question;
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

        public async static Task<int> Insert(Question question, bool rollback = false)
        {
            try
            {
                IDbContextTransaction transaction = null;

                using (SurveyMakerEntities dc = new SurveyMakerEntities())
                {
                    if (rollback) transaction = dc.Database.BeginTransaction();

                    tblQuestion newrow = new tblQuestion();
                    newrow.Id = Guid.NewGuid();
                    newrow.Text = question.Text;

                    dc.tblQuestions.Add(newrow);
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
                Question question = new Question { Id = id, Text = text };
                return await Insert(question, rollback);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async static Task<int> Update(Question question, bool rollback = false)
        {
            try
            {
                IDbContextTransaction transaction = null;
                using (SurveyMakerEntities dc = new SurveyMakerEntities())
                {
                    tblQuestion row = dc.tblQuestions.FirstOrDefault(c => c.Id == question.Id);
                    int results = 0;
                    if (row != null)
                    {
                        if (rollback) transaction = dc.Database.BeginTransaction();
                        row.Text = question.Text;
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
                    tblQuestion row = dc.tblQuestions.FirstOrDefault(c => c.Id == id);
                    int results = 0;

                    if (row != null)
                    {
                        if (rollback) transaction = dc.Database.BeginTransaction();

                        foreach(tblQuestionAnswer qa in dc.tblQuestionAnswers.Where(qa => qa.QuestionId == id))
                        {
                            dc.tblQuestionAnswers.Remove(qa);
                        }

                        dc.tblQuestions.Remove(row);

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
