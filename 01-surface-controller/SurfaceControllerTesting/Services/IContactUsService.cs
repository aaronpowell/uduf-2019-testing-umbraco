using System.Collections.Generic;
using System.Threading.Tasks;
using static SurfaceControllerTesting.Services.ContactUsService;

namespace SurfaceControllerTesting.Services
{
    public interface IContactUsService
    {
        Task<int> AddFeedbackAsync(string name, string email, string message);
        Task<List<ContactUsPoco>> GetFeedbackAsync();
        Task<ContactUsPoco> GetByIdAsync(int id);
    }
}