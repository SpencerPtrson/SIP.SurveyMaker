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
    public class ActivationManager
    {
        public async static Task<List<Activation>> Load()
        {
            try
            {
                List<Activation> activations = new List<Activation>();
                await Task.Run(() =>
                {
                    using (SurveyMakerEntities dc = new SurveyMakerEntities())
                    {
                        foreach (tblActivation tblActivation in dc.tblActivations.ToList())
                        {
                            Activation activation = new Activation
                            {
                                Id = tblActivation.Id,
                                QuestionId = tblActivation.Id,
                                StartDate = DateTime.Now,
                                EndDate = DateTime.Now,
                                ActivationCode = tblActivation.ActivationCode,
                            };
                            activations.Add(activation);
                        }
                    }
                });
                return activations;
            }
            catch (Exception ex) { throw ex; }
        }

        public async static Task<List<Activation>> LoadByQuestionId(Guid QuestionId)
        {
            try
            {
                List<Activation> activations = new List<Activation>();
                await Task.Run(() =>
                {
                    using (SurveyMakerEntities dc = new SurveyMakerEntities())
                    {
                        List<tblQuestion> tblQuestions = dc.tblQuestions.Where(c => c.Id ==  QuestionId).ToList();

                        foreach (tblQuestion q in tblQuestions)
                        {
                            tblActivation tblActivation = dc.tblActivations.Where(d => d.QuestionId == q.Id).FirstOrDefault();
                            Activation activation = new Activation()
                            {
                                Id = tblActivation.Id,
                                QuestionId = tblActivation.Id,
                                StartDate = DateTime.Now,
                                EndDate = DateTime.Now,
                                ActivationCode = tblActivation.ActivationCode
                            };
                            activations.Add(activation);
                        }
                    }
                });
                return activations;
            }
            catch (Exception ex) { throw ex; }
        }

        public async static Task<int> Insert(Activation activation, bool rollback = false)
        {
            try
            {
                int results = 0;
                IDbContextTransaction? transaction = null;

                using (SurveyMakerEntities dc = new SurveyMakerEntities())
                {
                    if (rollback) transaction = await dc.Database.BeginTransactionAsync().ConfigureAwait(false);

                    tblActivation newrow = new tblActivation();
                    newrow.Id = Guid.NewGuid();
                    newrow.QuestionId = activation.QuestionId;
                    newrow.StartDate = activation.StartDate;
                    newrow.EndDate = activation.EndDate;
                    newrow.ActivationCode = activation.ActivationCode;

                    dc.tblActivations.Add(newrow);
                    results = await dc.SaveChangesAsync().ConfigureAwait(false);
                    if (transaction != null) await transaction.RollbackAsync().ConfigureAwait(false);
                }
                return results;
            }
            catch (Exception ex) { throw ex; }
        }

        public async static Task<int> Update(Activation activation, bool rollback = false)
        {
            try
            {
                int results = 0;
                await Task.Run(() =>
                {
                    IDbContextTransaction transaction = null;
                    using (SurveyMakerEntities dc = new SurveyMakerEntities())
                    {
                        tblActivation row = dc.tblActivations.FirstOrDefault(c => c.Id == activation.Id);
                        if (row != null)
                        {
                            if (rollback) transaction = dc.Database.BeginTransaction();
                            row.StartDate = activation.StartDate;
                            row.EndDate = activation.EndDate;
                            row.ActivationCode = activation.ActivationCode;

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
                        tblActivation row = dc.tblActivations.FirstOrDefault(at => at.Id == id);

                        if (row != null)
                        {
                            if (rollback) transaction = dc.Database.BeginTransaction();
                            dc.tblActivations.Remove(row);

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
