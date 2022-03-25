using System;
using MangoAPI.BusinessLogic.Responses;
using MediatR;

namespace MangoAPI.BusinessLogic.ApiQueries.OpenSslKeyExchange;

public record OpenSslGetKeyExchangeRequestsQuery : IRequest<Result<OpenSslGetKeyExchangeRequestsResponse>>
{
    public Guid UserId { get; init; }
}