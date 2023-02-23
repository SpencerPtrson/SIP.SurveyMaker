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

        public async Task<Question> LoadById(Guid id)
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

    }
}
