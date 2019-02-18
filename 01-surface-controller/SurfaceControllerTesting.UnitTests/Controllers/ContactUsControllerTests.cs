using NSubstitute;
using SurfaceControllerTesting.Controllers;
using SurfaceControllerTesting.Models;
using SurfaceControllerTesting.Services;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using Umbraco.Core;
using Umbraco.Core.Configuration.UmbracoSettings;
using Umbraco.Core.Logging;
using Umbraco.Core.Persistence;
using Umbraco.Core.Persistence.SqlSyntax;
using Umbraco.Core.Profiling;
using Umbraco.Core.Services;
using Umbraco.Web;
using Umbraco.Web.Routing;
using Umbraco.Web.Security;
using Xunit;

namespace SurfaceControllerTesting.UnitTests.Controllers
{
    public class ContactUsControllerTests
    {
        public ContactUsControllerTests()
        {
            var logger = Substitute.For<ILogger>();

            var dbContext = new DatabaseContext(
                Substitute.For<IDatabaseFactory>(),
                logger,
                new SqlSyntaxProviders(new ISqlSyntaxProvider[0])
            );

            var appContext = ApplicationContext.EnsureContext(
                dbContext,
                new ServiceContext(),
                CacheHelper.CreateDisabledCacheHelper(),
                new ProfilingLogger(logger, Substitute.For<IProfiler>()),
                true
            );

            var httpContext = Substitute.For<HttpContextBase>();

            UmbracoContext.EnsureContext(
                httpContext,
                appContext,
                new WebSecurity(httpContext, appContext),
                Substitute.For<IUmbracoSettingsSection>(),
                Enumerable.Empty<IUrlProvider>(),
                true
            );
        }

        [Fact]
        public async Task CanGetViewWithDefaultModel()
        {
            var service = Substitute.For<IContactUsService>();

            var controller = new ContactUsController(service);

            var result = await controller.RenderContactUsTask();

            var model = (ContactUsModel)result.Model;

            Assert.Null(model.Name);
            Assert.Null(model.Email);
            Assert.Null(model.Message);
        }

        [Fact]
        public async Task WillSaveDataWhenModelIsValid()
        {
            var service = Substitute.For<IContactUsService>();

            var controller = new ContactUsController(service);

            var model = new ContactUsModel
            {
                Name = "Aaron",
                Email = "test@test.com",
                Message = "Test message"
            };

            var _ = await controller.PostContactUsAsync(model);

            service
                .Received()
                .AddFeedbackAsync(Arg.Is(model.Name), Arg.Is(model.Email), Arg.Is(model.Message))
                .IgnoreAwaitForNSubstituteAssertion();
        }
    }
}
