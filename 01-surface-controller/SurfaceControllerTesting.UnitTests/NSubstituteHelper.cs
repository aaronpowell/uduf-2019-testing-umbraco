using System.Threading.Tasks;

namespace SurfaceControllerTesting.UnitTests
{
    public static class NSubstituteHelper
    {
        public static void IgnoreAwaitForNSubstituteAssertion(this Task task)
        {
        }
    }
}
