using System;

namespace Recipes.Application.Exceptions
{
    public abstract class ExceptionWithValue: Exception
    {
        public string Value { get; }

        protected ExceptionWithValue(string message): base(message)
        {
            Value = message;
        }
    }
}