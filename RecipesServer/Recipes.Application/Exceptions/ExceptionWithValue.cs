using System;

namespace Recipes.Application.Exceptions
{
    public abstract class ExceptionWithValue: Exception
    {
        public string Value { get; }
        public ExceptionWithValue(string message): base(message)
        {
            Value = message;
        }
    }
}