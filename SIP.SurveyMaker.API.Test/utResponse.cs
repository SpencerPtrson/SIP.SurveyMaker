using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.AspNetCore.OutputCaching;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.Extensions.Hosting;
using Microsoft.VisualStudio.TestPlatform.TestHost;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using SIP.SurveyMaker.BL;
using SIP.SurveyMaker.BL.Models;
using System.Drawing;
using System.Reflection;

namespace SIP.SurveyMaker.API.Test
{
    [TestClass]
    public class utReponse : utBase
    {
        [TestMethod]
        public async Task InsertTestAsync()
        {
            List<Question> questions = await QuestionManager.Load();
            List<Answer> answers = await AnswerManager.Load();
            Response response = new Response
            {
                QuestionId = questions[0].Id,
                AnswerId = answers[0].Id,
                ResponseDate = DateTime.Now
            };
            await base.InsertTestAsync<Response>(response);
        }
    }
}