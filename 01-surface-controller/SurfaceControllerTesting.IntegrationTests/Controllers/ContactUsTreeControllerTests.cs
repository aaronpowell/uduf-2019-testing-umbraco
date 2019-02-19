using Chauffeur.TestingTools;
using NSubstitute;
using SurfaceControllerTesting.Controllers;
using SurfaceControllerTesting.Models;
using SurfaceControllerTesting.Services;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using Umbraco.Core;
using Umbraco.Core.Configuration;
using Umbraco.Web;
using Umbraco.Web.Routing;
using Umbraco.Web.Security;
using Xunit;

namespace SurfaceControllerTesting.IntegrationTests.Controllers
{
    public class ContactUsTreeControllerTests : UmbracoHostTestBase
    {
        [Fact]
        public async Task CanGetFeedbackFromDb()
        {
            await this.InstallUmbraco();
            await Host.Run(new[] { "create-contact-us-table" });

            var model = new ContactUsModel
            {
                Name = "Test",
                Email = "test@test.com",
                Message = "From test"
            };
            var service = new ContactUsService(
                ApplicationContext.Current.DatabaseContext.Database,
                ApplicationContext.Current.DatabaseContext.SqlSyntax
            );
            var id = await service.AddFeedbackAsync(model.Name, model.Email, model.Message);

            var result = await service.GetFeedbackAsync();

            Assert.Single(result);
            Assert.Equal(id, result.First().Id);
        }
    }
}
