﻿using System.Threading;
using System.Threading.Tasks;
using MangoAPI.DTO.Commands.Auth;
using MangoAPI.DTO.Responses.Auth;
using MangoAPI.Infrastructure.Interfaces;
using MediatR;

namespace MangoAPI.Infrastructure.CommandHandlers
{
    public class LogoutCommandHandler : IRequestHandler<LogoutCommand, LogoutResponse>
    {
        private readonly ICookieService _cookieService;
        private readonly IJwtRefreshService _jwtRefreshService;

        public LogoutCommandHandler(ICookieService cookieService, IJwtRefreshService jwtRefreshService)
        {
            _cookieService = cookieService;
            _jwtRefreshService = jwtRefreshService;
        }

        public async Task<LogoutResponse> Handle(LogoutCommand request, CancellationToken cancellationToken)
        {
            var validationResult =
                await _jwtRefreshService.VerifyUserRefreshTokenAsync(request.RefreshTokenId, request.UserAgent, request.FingerPrint, request.IpAddress, cancellationToken);

            if(validationResult.IsSuspicious)
                return LogoutResponse.SuspiciousLogout;
            
            if (!validationResult.Success)
                return LogoutResponse.RefreshTokenNotValidated;

            var res = await _jwtRefreshService.RevokeRefreshTokenAsync(validationResult.RefreshToken.Id, cancellationToken);
            return res.Success ? LogoutResponse.SuccessResponse : LogoutResponse.RefreshTokenNotFoundResponse;
        }
    }
}