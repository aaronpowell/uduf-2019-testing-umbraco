using SurfaceControllerTesting.Models;
using SurfaceControllerTesting.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http;
using Umbraco.Web.WebApi;

namespace SurfaceControllerTesting.Controllers.Api
{
    public class ContactUsController : UmbracoAuthorizedApiController
    {
        private readonly IContactUsService service;

        public ContactUsController()
        {
            this.service = new ContactUsService(DatabaseContext.Database, DatabaseContext.SqlSyntax);
        }

        public ContactUsController(IContactUsService service)
        {
            this.service = service;
        }

        [HttpGet]
        public async Task<IHttpActionResult> GetById(int id)
        {
            var model = await service.GetByIdAsync(id);

            if (model == null)
                return NotFound();

            return Json(new ContactUsApiModel
            {
                Name = model.Name,
                Email = model.Email,
                Message = model.Message,
                CreatedDate = model.CreatedDate
            });
        }
    }
}