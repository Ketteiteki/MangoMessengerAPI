﻿using MangoAPI.BusinessLogic.Responses;
using MangoAPI.Domain.Constants;

namespace MangoAPI.BusinessLogic.ApiQueries.Communities
{
    public record GetSecretChatPublicKeyResponse : ResponseBase<GetSecretChatPublicKeyResponse>
    {
        public int PublicKey { get; init; }

        public static GetSecretChatPublicKeyResponse FromSuccess(int publicKey)
        {
            return new()
            {
                Message = ResponseMessageCodes.Success,
                Success = true,
                PublicKey = publicKey,
            };
        }
    }
}
