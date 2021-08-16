namespace Recipes.Application.Exceptions
{
    public class PermissionException : ExceptionWithValue
    {
        public PermissionException(string message): base(message) {}

        
        public const string NotEnoughPermissionsToModifyResource = "У вас нет прав на модификацию данного ресурса.";
    }
}