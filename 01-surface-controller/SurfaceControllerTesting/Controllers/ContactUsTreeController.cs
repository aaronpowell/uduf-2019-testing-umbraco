using SurfaceControllerTesting.Services;
using System.Net.Http.Formatting;
using System.Threading.Tasks;
using Umbraco.Web.Models.Trees;
using Umbraco.Web.Mvc;
using Umbraco.Web.Trees;

namespace SurfaceControllerTesting.Controllers
{
    [Tree("content", "contactUsTreeAlias", "Contact Us Responses")]
    [PluginController("ContactUs")]
    public class ContactUsTreeController : TreeController
    {
        private readonly IContactUsService service;

        public ContactUsTreeController()
        {
            service = new ContactUsService(DatabaseContext.Database, DatabaseContext.SqlSyntax);
        }

        public ContactUsTreeController(IContactUsService service)
        {
            this.service = service;
        }

        protected override MenuItemCollection GetMenuForNode(string id, FormDataCollection queryStrings)
        {
            return new MenuItemCollection();
        }

        protected override TreeNodeCollection GetTreeNodes(string id, FormDataCollection queryStrings)
        {
            var nodes = new TreeNodeCollection();

            var feedback = service.GetFeedbackAsync().Result;

            foreach (var item in feedback)
                nodes.Add(CreateTreeNode(item.Id.ToString(), "-1", queryStrings, item.CreatedDate.ToString("dd/MM/yyyy") + " - " + item.Name));

            return nodes;
        }
    }
}