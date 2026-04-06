using System.Security.Claims;
using Microsoft.AspNetCore.Mvc;

namespace ECommerceApi.Controllers;

public abstract class BaseAuthController : ControllerBase
{
    protected int GetUserId()
    {
        var claim = User.FindFirst(ClaimTypes.NameIdentifier);
        if (claim == null || !int.TryParse(claim.Value, out var userId))
            throw new UnauthorizedAccessException("Invalid or missing user identity.");

        return userId;
    }
}
