using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Umbraco.Core.Persistence;
using Umbraco.Core.Persistence.DatabaseAnnotations;
using Umbraco.Core.Persistence.SqlSyntax;

namespace SurfaceControllerTesting.Services
{
    public class ContactUsService : IContactUsService
    {
        private readonly UmbracoDatabase database;
        private readonly ISqlSyntaxProvider sqlSyntaxProvider;

        public ContactUsService(UmbracoDatabase database, ISqlSyntaxProvider sqlSyntaxProvider)
        {
            this.database = database;
            this.sqlSyntaxProvider = sqlSyntaxProvider;
        }

        public Task<int> AddFeedbackAsync(string name, string email, string message)
        {
            var poco = new ContactUsPoco
            {
                Name = name,
                Email = email,
                Message = message,
                CreatedDate = DateTime.Now
            };

            var result = database.Insert(poco).ToString();

            return Task.FromResult(int.Parse(result));
        }

        public Task<ContactUsPoco> GetByIdAsync(int id)
        {
            var sql = new Sql()
                .From<ContactUsPoco>(sqlSyntaxProvider)
                .Where<ContactUsPoco>(m => m.Id == id, sqlSyntaxProvider);

            return Task.FromResult(database.SingleOrDefault<ContactUsPoco>(sql));
        }

        public Task<List<ContactUsPoco>> GetFeedbackAsync()
        {
            var sql = new Sql()
                .From<ContactUsPoco>(sqlSyntaxProvider);

            return Task.FromResult(database.Fetch<ContactUsPoco>(sql));
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