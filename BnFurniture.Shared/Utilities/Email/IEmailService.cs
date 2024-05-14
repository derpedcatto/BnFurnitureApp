using BnFurniture.Domain.Responses;

namespace BnFurniture.Shared.Utilities.Email;

public interface IEmailService
{
    public Task<StatusResponse> SendNewPasswordEmailAsync(string userEmail, string newPassword, string? userFullName, CancellationToken cancellationToken);
}
