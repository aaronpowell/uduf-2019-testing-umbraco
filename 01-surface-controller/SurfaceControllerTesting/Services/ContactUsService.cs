using System;
using System.Threading.Tasks;
using Umbraco.Core.Persistence;
using Umbraco.Core.Persistence.DatabaseAnnotations;

namespace SurfaceControllerTesting.Services
{
    public class ContactUsService : IContactUsService
    {
        private readonly UmbracoDatabase database;

        public ContactUsService(UmbracoDatabase database)
        {
            this.database = database;
        }

        public Task<decimal> AddFeedbackAsync(string name, string email, string message)
        {
            var poco = new ContactUsPoco
            {
                Name = name,
                Email = email,
                Message = message,
                CreatedDate = DateTime.Now
            };

            var result = (decimal)database.Insert(poco);

            return Task.FromResult(result);
        }

        [TableName("ContactUsResponses")]
        [PrimaryKey("Id", autoIncrement = true)]
        public class ContactUsPoco
        {
            [PrimaryKeyColumn(AutoIncrement = true)]
            public int Id { get; set; }
            public string Name { get; set; }
            public string Email { get; set; }
            public string Message { get; set; }
            public DateTime CreatedDate { get; set; }
        }
    }
}