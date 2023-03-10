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
        public async static Task<int> Insert(Activation activation, bool rollback = false)
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

                        tblActivation newrow = new tblActivation();
                        newrow.Id = Guid.NewGuid();
                        newrow.QuestionId = activation.QuestionId;
                        newrow.StartDate = activation.StartDate;
                        newrow.EndDate = activation.EndDate;
                        newrow.ActivationCode = activation.ActivationCode;

                        dc.tblActivations.Add(newrow);
                        results = dc.SaveChanges();
                        if (rollback) transaction.Rollback();
                    }
                });
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
