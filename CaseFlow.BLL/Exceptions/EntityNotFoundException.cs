namespace CaseFlow.BLL.Exceptions;

public class EntityNotFoundException : Exception
{
    public EntityNotFoundException(string entityName, int id)
        : base($"{entityName} with id {id} not found") { }
    
    public EntityNotFoundException(Type entityType, int id)
        : base($"{entityType.Name} with id {id} not found") { }
}