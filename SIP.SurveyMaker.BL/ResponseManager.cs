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
        public async static Task<List<Response>> LoadByQuestionId(Guid QuestionId)
        {
            try {
                List<Response> responses = new List<Response>();

                await Task.Run(() =>
                {
                    using (SurveyMakerEntities dc = new SurveyMakerEntities())
                    {
                        // Get a list of question / answer pairs with the given question id
                        List<tblResponse> tblResponses = dc.tblResponses.Where(c => c.QuestionId == QuestionId).ToList();

                        // Iterate through that list and add the corresponding tblAnswer value to "answers"
                        foreach (tblResponse r in tblResponses)
                        {
                            Response response = new Response()
                            {
                                Id = r.Id,
                                QuestionId = r.QuestionId,
                                AnswerId = r.AnswerId,
                                ResponseDate = r.ResponseDate
                            };
                            responses.Add(response);
                        } 
                    }
                });
                return responses;
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
                        response.Id = newrow.Id;

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
