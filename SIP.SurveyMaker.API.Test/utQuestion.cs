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
    public class utQuestion : utBase
    {
        [TestMethod]
        public async Task LoadTestAsync()
        {
            await base.LoadTestAsync<Question>();
            // The base class has the assert.stuff, so it's not needed here
        }

        [TestMethod]
        public async Task LoadByIdTestAsync()
        {
            await base.LoadByIdTestAsync<Question>(new KeyValuePair<string, string> ("Text", "Does fire need oxygen?"));
            // The base class has the assert.stuff, so it's not needed here
        }

        [TestMethod]
        public async Task InsertTestAsync()
        {
            List<Activation> activations = await ActivationManager.Load();
            List<Answer> answers = await AnswerManager.Load();
            Question question = new Question { Text = "Hello", Answers = answers, Activations = activations };
            await base.InsertTestAsync<Question>(question);
        }

        [TestMethod]
        public async Task UpdateTestAsync()
        {
            List<Activation> activations = await ActivationManager.Load();
            List<Answer> answers = await AnswerManager.Load();
            Question question = new Question { Text = "Test", Activations = activations, Answers = answers };
            await base.UpdateTestAsync<Question>(new KeyValuePair<string, string>("Text", "Does fire need oxygen?"), question);
        }
    }
}