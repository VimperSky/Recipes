using System.Threading;

namespace Recipes.WebApi.Tests.Tests.AuthController
{
    public static class TestAuthProvider
    {
        private static int _id;
        
        public static string GetTestLogin()
        {
            return "Login" + Interlocked.Increment(ref _id);
        }

        public static string GetTestName()
        {
            return "TestName";
        }

        public static string GetTestPassword()
        {
            return "AB23dd44";
        }
    }
}