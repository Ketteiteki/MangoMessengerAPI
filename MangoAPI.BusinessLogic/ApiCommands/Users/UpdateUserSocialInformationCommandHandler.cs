﻿using System;
using System.Threading;
using System.Threading.Tasks;
using MangoAPI.BusinessLogic.Responses;
using MangoAPI.DataAccess.Database;
using MangoAPI.DataAccess.Database.Extensions;
using MangoAPI.Domain.Constants;
using MediatR;

namespace MangoAPI.BusinessLogic.ApiCommands.Users
{
    public class UpdateUserSocialInformationCommandHandler 
        : IRequestHandler<UpdateUserSocialInformationCommand, Result<ResponseBase>>
    {
        private readonly MangoPostgresDbContext _postgresDbContext;
        private readonly ResponseFactory<ResponseBase> _responseFactory;

        public UpdateUserSocialInformationCommandHandler(MangoPostgresDbContext postgresDbContext,
            ResponseFactory<ResponseBase> responseFactory)
        {
            _postgresDbContext = postgresDbContext;
            _responseFactory = responseFactory;
        }

        public async Task<Result<ResponseBase>> Handle(UpdateUserSocialInformationCommand request,
            CancellationToken cancellationToken)
        {
            var user = await _postgresDbContext.Users.FindUserByIdIncludeInfoAsync(request.UserId, cancellationToken);

            if (user is null)
            {
                const string errorMessage = ResponseMessageCodes.UserNotFound;
                var details = ResponseMessageCodes.ErrorDictionary[errorMessage];

                return _responseFactory.ConflictResponse(errorMessage, details);
            }

            user.UserInformation.Facebook = StringIsValid(request.Facebook) 
                ? request.Facebook 
                : user.UserInformation.Facebook;

            user.UserInformation.Twitter = StringIsValid(request.Twitter) 
                ? request.Twitter 
                : user.UserInformation.Twitter;

            user.UserInformation.Instagram = StringIsValid(request.Instagram) 
                ? request.Instagram 
                : user.UserInformation.Instagram;

            user.UserInformation.LinkedIn = StringIsValid(request.LinkedIn) 
                ? request.LinkedIn 
                : user.UserInformation.LinkedIn;
            
            user.UserInformation.UpdatedAt = DateTime.UtcNow;

            _postgresDbContext.UserInformation.Update(user.UserInformation);
            await _postgresDbContext.SaveChangesAsync(cancellationToken);

            return _responseFactory.SuccessResponse(ResponseBase.SuccessResponse);
        }

        private static bool StringIsValid(string str)
        {
            return !string.IsNullOrEmpty(str) && !string.IsNullOrWhiteSpace(str);
        }
    }
}