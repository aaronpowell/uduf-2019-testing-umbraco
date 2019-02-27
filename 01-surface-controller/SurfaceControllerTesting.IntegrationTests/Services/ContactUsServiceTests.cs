using Chauffeur.TestingTools;
using SurfaceControllerTesting.Models;
using SurfaceControllerTesting.Services;
using System.Linq;
using System.Threading.Tasks;
using Umbraco.Core;
using Xunit;

namespace SurfaceControllerTesting.IntegrationTests.Services
{
    public class ContactUsServiceTests : UmbracoHostTestBase
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
