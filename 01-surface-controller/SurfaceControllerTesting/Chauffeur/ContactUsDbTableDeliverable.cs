using Chauffeur;
using System.IO;
using System.Threading.Tasks;
using Umbraco.Core.Persistence;
using static SurfaceControllerTesting.Services.ContactUsService;

namespace SurfaceControllerTesting.Chauffeur
{
    [DeliverableName("create-contact-us-table")]
    public class ContactUsDbTableDeliverable : Deliverable
    {
        private readonly DatabaseSchemaHelper databaseHelper;

        public ContactUsDbTableDeliverable(
            TextReader reader,
            TextWriter writer,
            DatabaseSchemaHelper databaseHelper) : base(reader, writer)
        {
            this.databaseHelper = databaseHelper;
        }

        public override Task<DeliverableResponse> Run(string command, string[] args)
        {
            databaseHelper.CreateTable<ContactUsPoco>();

            return Task.FromResult(DeliverableResponse.Continue);
        }
    }
}