﻿using System.Threading;

namespace Recipes.WebApi.Tests.Tests.AuthController
{
    public static class UserDataProvider
    {
        private static int _id;
        
        public static string GetValidLogin()
        {
            return "Login" + Interlocked.Increment(ref _id);
        }

        public static string GetValidName()
        {
            return "TestName";
        }

        public static string GetValidPassword()
        {
            return "AB23dd44";
        }
    }
}