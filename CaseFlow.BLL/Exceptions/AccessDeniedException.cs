namespace CaseFlow.BLL.Exceptions;

public class AccessDeniedException : Exception
{
    public AccessDeniedException(string entityName, int detectiveId)
        : base($"Access to {entityName} denied for Detective with ID {detectiveId}.") { }
}
