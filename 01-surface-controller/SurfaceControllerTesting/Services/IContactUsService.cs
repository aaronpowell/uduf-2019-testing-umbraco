using System.Threading.Tasks;

namespace SurfaceControllerTesting.Services
{
    public interface IContactUsService
    {
        Task<int> AddFeedbackAsync(string name, string email, string message);
    }
}