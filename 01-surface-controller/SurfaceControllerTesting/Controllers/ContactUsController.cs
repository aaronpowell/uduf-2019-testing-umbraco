using SurfaceControllerTesting.Models;
using SurfaceControllerTesting.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using Umbraco.Core.Persistence;
using Umbraco.Web.Mvc;

namespace SurfaceControllerTesting.Controllers
{
    public class ContactUsController : SurfaceController
    {
        private readonly IContactUsService service;

        public ContactUsController()
        {
            this.service = new ContactUsService(ApplicationContext.DatabaseContext.Database, ApplicationContext.DatabaseContext.SqlSyntax);
        }

        public ContactUsController(IContactUsService service)
        {
            this.service = service;
        }

        [HttpGet]
        [ActionName("ContactUs")]
        public Task<PartialViewResult> RenderContactUsTask()
        {
            return Task.FromResult(PartialView(new ContactUsModel()));
        }

        [HttpPost]
        [ActionName("ContactUs")]
        public async Task<ActionResult> PostContactUsAsync(ContactUsModel model)
        {
            if (!ModelState.IsValid)
                return CurrentUmbracoPage();

            var result = await service.AddFeedbackAsync(model.Name, model.Email, model.Message);

            if (result > 0)
            {
                TempData["SubmissionSuccess"] = true;
                return RedirectToCurrentUmbracoPage();
            }

            return CurrentUmbracoPage();
        }
    }
}