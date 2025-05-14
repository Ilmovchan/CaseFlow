using System.Diagnostics.CodeAnalysis;
using CaseFlow.BLL.Exceptions;

namespace CaseFlow.BLL.Common;

public static class EntityValidationExtensions
{
    public static T EnsureExists<T>
        ([NotNull] this T entity, string entityName, int id)
        where T : class?
        => entity ?? throw new EntityNotFoundException(entityName, id);
}