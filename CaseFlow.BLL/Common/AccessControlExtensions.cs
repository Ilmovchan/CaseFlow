using CaseFlow.BLL.Exceptions;
using CaseFlow.DAL.Data;
using CaseFlow.DAL.Models;
using Microsoft.EntityFrameworkCore;

namespace CaseFlow.BLL.Common;

public static class AccessControlExtensions
{
    public static Case EnsureCaseAccess
        (this Case entityCase, int detectiveId)
    {
        if (entityCase.DetectiveId != detectiveId)
            throw new AccessDeniedException("Case", detectiveId);

        return entityCase;
    }

    public static Client EnsureClientAccess
        (this Client entityClient, IEnumerable<Case> cases, int detectiveId)
    {
        var hasAccess = cases.Any(c => c.ClientId == entityClient.Id && c.DetectiveId == detectiveId);

        if (!hasAccess)
            throw new AccessDeniedException("Client", detectiveId);

        return entityClient;
    }

    public static Evidence EnsureEvidenceAccess
        (this Evidence entityEvidence, IEnumerable<CaseEvidence> caseEvidences, int detectiveId)
    {
        var hasAccess = caseEvidences
            .Any(ce => ce.EvidenceId == entityEvidence.Id && ce.Case.DetectiveId == detectiveId);
        
        if (!hasAccess)
            throw new AccessDeniedException("Evidence", detectiveId);

        return entityEvidence;
    }
}