namespace CaseFlow.BLL.Exceptions;

public class EntityDeleteConflictException : Exception
{
    public EntityDeleteConflictException(string entityName, int entityId, IEnumerable<int> connectedEntitiesIds)
        : base($"Cannot delete {entityName} with id: {entityId}, because it is connected to cases: {String.Join(",", connectedEntitiesIds)}") { }
    
    public EntityDeleteConflictException(string entityName, int entityId)
        : base($"Cannot delete {entityName} with id: {entityId} because it is connected to other cases") { }
    
    public EntityDeleteConflictException(string message)
        : base(message) { }
}

public class EntityUpdateConflictException : Exception
{
    public EntityUpdateConflictException(string entityName, int entityId)
        : base($"Cannot update {entityName} with id: {entityId} because it is connected to other cases") { }

    public EntityUpdateConflictException(string message)
        : base(message) { }
}