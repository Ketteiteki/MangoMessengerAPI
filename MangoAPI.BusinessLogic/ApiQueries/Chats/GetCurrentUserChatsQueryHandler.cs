﻿using MangoAPI.DataAccess.Database.Extensions;

namespace MangoAPI.BusinessLogic.ApiQueries.Chats
{
    using System.Linq;
    using System.Threading;
    using System.Threading.Tasks;
    using BusinessExceptions;
    using DataAccess.Database;
    using Domain.Constants;
    using MediatR;
    using Microsoft.EntityFrameworkCore;

    public class
        GetCurrentUserChatsQueryHandler : IRequestHandler<GetCurrentUserChatsQuery, GetCurrentUserChatsResponse>
    {
        private readonly MangoPostgresDbContext _postgresDbContext;

        public GetCurrentUserChatsQueryHandler(MangoPostgresDbContext postgresDbContext)
        {
            _postgresDbContext = postgresDbContext;
        }

        public async Task<GetCurrentUserChatsResponse> Handle(
            GetCurrentUserChatsQuery request,
            CancellationToken cancellationToken)
        {
            var currentUser = await _postgresDbContext.Users.FindUserByIdAsync(request.UserId, cancellationToken);

            if (currentUser == null)
            {
                throw new BusinessException(ResponseMessageCodes.UserNotFound);
            }

            var chats = await _postgresDbContext.UserChats.FindUserChatsByIdIncludeMessagesAsync(currentUser.Id, cancellationToken);

            return GetCurrentUserChatsResponse.FromSuccess(chats);
        }
    }
}