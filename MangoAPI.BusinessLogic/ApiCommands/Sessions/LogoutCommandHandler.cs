﻿using MangoAPI.DataAccess.Database.Extensions;

namespace MangoAPI.BusinessLogic.ApiCommands.Sessions
{
    using System.Threading;
    using System.Threading.Tasks;
    using BusinessExceptions;
    using DataAccess.Database;
    using Domain.Constants;
    using MediatR;
    using Microsoft.EntityFrameworkCore;

    public class LogoutCommandHandler : IRequestHandler<LogoutCommand, LogoutResponse>
    {
        private readonly MangoPostgresDbContext _postgresDbContext;

        public LogoutCommandHandler(MangoPostgresDbContext postgresDbContext)
        {
            _postgresDbContext = postgresDbContext;
        }

        public async Task<LogoutResponse> Handle(LogoutCommand request, CancellationToken cancellationToken)
        {
            var session = await _postgresDbContext.Sessions
                .GetSessionByRefreshTokenAsync(request.RefreshToken, cancellationToken);

            if (session is null)
            {
                throw new BusinessException(ResponseMessageCodes.InvalidOrExpiredRefreshToken);
            }

            var user = await _postgresDbContext.Users.FindUserByIdAsync(session.UserId, cancellationToken);

            if (user is null || session.UserId != user.Id)
            {
                throw new BusinessException(ResponseMessageCodes.UserNotFound);
            }

            _postgresDbContext.Sessions.Remove(session);
            await _postgresDbContext.SaveChangesAsync(cancellationToken);

            return LogoutResponse.SuccessResponse;
        }
    }
}