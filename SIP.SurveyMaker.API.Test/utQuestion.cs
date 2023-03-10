using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.AspNetCore.OutputCaching;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.Extensions.Hosting;
using Microsoft.VisualStudio.TestPlatform.TestHost;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Drawing;
using System.Reflection;

namespace SIP.SurveyMaker.API.Test
{
    [TestClass]
    public class utQuestion : utBase
    {
        //[TestMethod]
        //public async Task LoadTestAsync()
        //{
        //    await base.LoadTestAsync<Color>();
        //    // The base class has the assert.stuff, so it's not needed here
        //}

        //[TestMethod]
        //public async Task InsertTestAsync()
        //{
        //    Color color = new Color { Description = "New Color" };
        //    color.Code = BitConverter.ToInt32(new byte[] { 255, 0, 0, 0x00 }, 0);
        //    await base.InsertTestAsync<Color>(color);
        //}

        //[TestMethod]
        //public async Task UpdateTestAsync()
        //{
        //    Color color = new Color { Description = "Updated Color" };
        //    color.Code = BitConverter.ToInt32(new byte[] { 255, 0, 0, 0x00 }, 0);
        //    await base.UpdateTestAsync<Color>(new KeyValuePair<string, string>("Description", "Cornflower Blue"), color);
        //}

        //[TestMethod]
        //public async Task DeleteTestAsync()
        //{
        //    await base.DeleteTestAsync<Color>(new KeyValuePair<string, string>("Description", "Cornflower Blue"));
        //}
    }
}