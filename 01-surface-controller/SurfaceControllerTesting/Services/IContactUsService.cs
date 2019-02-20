using System.Threading.Tasks;

namespace SurfaceControllerTesting.Services
{
    public interface IContactUsService
    {
        Task<decimal> AddFeedbackAsync(string name, string email, string message);
    }
}