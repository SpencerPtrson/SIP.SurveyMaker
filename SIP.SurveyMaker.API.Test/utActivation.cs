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
    public class utActivation : utBase
    {
        [TestMethod]
        public async Task LoadTestAsync()
        {
            await base.LoadTestAsync<Activation>();
            // The base class has the assert.stuff, so it's not needed here
        }

        [TestMethod]
        public async Task InsertTestAsync()
        {
            List<Question> questions = await QuestionManager.Load();

            Activation activation = new Activation { QuestionId = questions[0].Id, 
                                                    StartDate = DateTime.Now, 
                                                    EndDate = DateTime.Now, 
                                                    ActivationCode = "AABBCC"};
            await base.InsertTestAsync<Activation>(activation);
        }

        [TestMethod]
        public async Task UpdateTestAsync()
        {
            List<Activation> activations = await ActivationManager.Load();
            Activation activation = activations[0];
            activation.ActivationCode = "TEST12";

            await base.UpdateTestAsync<Activation>(new KeyValuePair<string, string>("ActivationCode", "DFNON1"), activation);
        }
    }
}