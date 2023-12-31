﻿using Microsoft.EntityFrameworkCore.Migrations.Operations;
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

                            // Populate answer list
                            question.Answers = new List<Answer>();
                            foreach (tblQuestionAnswer qa in q.tblQuestionAnswers.ToList())
                            {
                                Answer answer = new Answer { Id = qa.Id, IsCorrect = qa.IsCorrect, Text = qa.Answer.Text };
                                question.Answers.Add(answer);
                            }

                            // Populate activations
                            question.Activations = new List<Activation>();
                            foreach(tblActivation at in q.tblActivations)
                            {
                                Activation activation = new Activation { Id = at.Id, 
                                                                            ActivationCode = at.ActivationCode,
                                                                            StartDate = at.StartDate,
                                                                            EndDate = at.EndDate
                                };
                            }
                            questions.Add(question);
                        }
                    }
                });
                return questions;
            }
            catch (Exception ex) { throw ex; }
        }

        public async static Task<Question> LoadById(Guid id)
        {
            try
            {
                using (SurveyMakerEntities dc = new SurveyMakerEntities())
                {
                    Question question = new Question();
                    List<Answer> answerList = await AnswerManager.LoadById(id);
                    List<Activation> activationList = await ActivationManager.LoadByQuestionId(id);

                    await Task.Run(() =>
                    {
                        tblQuestion tblQuestion = dc.tblQuestions.Where(c => c.Id == id).FirstOrDefault();

                        if (tblQuestion != null)
                        {
                            question.Id = tblQuestion.Id;
                            question.Text = tblQuestion.Text;
                            question.Answers = answerList;
                            question.Activations = activationList;
                            System.Diagnostics.Debug.WriteLine(question.Text);
                        }
                        else
                            throw new Exception("Could not find the row");
                    });
                    return question;
                }
            }
            catch (Exception ex) { throw ex; }
        }

        public async static Task<List<Activation>> LoadByActivationCode(String code)
        {
            try
            {
                List<Activation> activations = new List<Activation>();

                using (SurveyMakerEntities dc = new SurveyMakerEntities())
                {
                    await Task.Run(() =>
                    {
                        List<tblActivation> tblActivations = dc.tblActivations.Where(at => at.ActivationCode == code).ToList();

                        foreach(tblActivation t in tblActivations)
                        {
                            Activation activation = new Activation
                            {
                                Id = t.Id,
                                ActivationCode = t.ActivationCode,
                                StartDate = t.StartDate,
                                EndDate = t.EndDate,
                                QuestionId = t.QuestionId
                            };
                            activations.Add(activation);
                        }
                    });
                }
                return activations;
            }
            catch (Exception ex) { throw ex; }
        }

        public async static Task<int> Insert(Question question, bool rollback = false)
        {
            try
            {
                int results = 0;
                await Task.Run(() =>
                {
                    IDbContextTransaction transaction = null;

                    using (SurveyMakerEntities dc = new SurveyMakerEntities())
                    {
                        if (rollback) transaction = dc.Database.BeginTransaction();

                        tblQuestion newrow = new tblQuestion();
                        newrow.Id = Guid.NewGuid();
                        newrow.Text = question.Text;
                        question.Id = newrow.Id;

                        dc.tblQuestions.Add(newrow);
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
                Question question = new Question { Id = id, Text = text };
                return await Insert(question, rollback);
            }
            catch (Exception ex) { throw ex; }
        }

        public async static Task<int> Update(Question question, bool rollback = false)
        {
            try
            {
                int results = 0;
                await Task.Run(() =>
                {
                    IDbContextTransaction transaction = null;
                    using (SurveyMakerEntities dc = new SurveyMakerEntities())
                    {
                        tblQuestion row = dc.tblQuestions.FirstOrDefault(c => c.Id == question.Id);
                        if (row != null)
                        {
                            if (rollback) transaction = dc.Database.BeginTransaction();
                            row.Text = question.Text;
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
                        tblQuestion row = dc.tblQuestions.FirstOrDefault(c => c.Id == id);

                        if (row != null)
                        {
                            if (rollback) transaction = dc.Database.BeginTransaction();

                            foreach (tblQuestionAnswer qa in dc.tblQuestionAnswers.Where(qa => qa.QuestionId == id))
                            {
                                dc.tblQuestionAnswers.Remove(qa);
                            }
                            foreach(tblActivation at in dc.tblActivations.Where(ac => ac.QuestionId == id))
                            {
                                dc.tblActivations.Remove(at);
                            }
                            foreach (tblResponse r in dc.tblResponses.Where(r => r.QuestionId == id))
                            {
                                dc.tblResponses.Remove(r);
                            }
                            dc.tblQuestions.Remove(row);

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
