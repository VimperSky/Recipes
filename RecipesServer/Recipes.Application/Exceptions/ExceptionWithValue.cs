using System;

namespace Recipes.Application.Exceptions
{
    public abstract class ExceptionWithValue : Exception
    {
        protected ExceptionWithValue(string message) : base(message)
        {
            Value = message;
        }

        public string Value { get; }
    }
}