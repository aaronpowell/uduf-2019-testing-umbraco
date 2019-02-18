using NSubstitute;
using SurfaceControllerTesting.Controllers;
using SurfaceControllerTesting.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using Umbraco.Core;
using Umbraco.Core.Configuration.UmbracoSettings;
using Umbraco.Core.Logging;
using Umbraco.Core.ObjectResolution;
using Umbraco.Core.Persistence;
using Umbraco.Core.Persistence.SqlSyntax;
using Umbraco.Core.Profiling;
using Umbraco.Core.Services;
using Umbraco.Web;
using Umbraco.Web.Routing;
using Umbraco.Web.Security;
using Umbraco.Web.WebApi;
using Xunit;
using static SurfaceControllerTesting.Services.ContactUsService;

namespace SurfaceControllerTesting.UnitTests.Controllers
{
    public class ContactUsTreeControllerTests
    {
        public ContactUsTreeControllerTests()
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
        public void FeedbackBecomesNodes()
        {
            var service = Substitute.For<IContactUsService>();
            service.GetFeedbackAsync()
                .Returns(Task.FromResult(new List<ContactUsPoco>() { new ContactUsPoco() }));

            var controller = new ContactUsTreeController(service);

            var result = controller.GetNodes(null, null);

            Assert.Single(result);
        }
    }
}
