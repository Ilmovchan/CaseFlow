namespace CaseFlow.BLL.Exceptions;

public class EntityConflictException : Exception
{
    public EntityConflictException(int entityId, IEnumerable<int> connectedEntitiesIds)
        : base($"Cannot delete entity with id: {entityId}, because it is connected to cases: {String.Join(",", connectedEntitiesIds)}") { }
}