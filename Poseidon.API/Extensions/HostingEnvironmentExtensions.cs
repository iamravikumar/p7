using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;

namespace Poseidon.API.Extensions
{
    public static class HostingEnvironmentExtensions
    {
        private const string TestEnvironment = "Test";

        public static bool IsTest(this IWebHostEnvironment webHostEnvironment)
        {
            return webHostEnvironment.IsEnvironment(TestEnvironment);
        }
    }
}