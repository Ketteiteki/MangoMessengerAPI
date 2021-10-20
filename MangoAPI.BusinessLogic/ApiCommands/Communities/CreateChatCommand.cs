﻿using System;
using MangoAPI.BusinessLogic.Responses;
using MangoAPI.Domain.Enums;
using MediatR;

namespace MangoAPI.BusinessLogic.ApiCommands.Communities
{
    public record CreateChatCommand : IRequest<GenericResponse<CreateCommunityResponse, ErrorResponse>>
    {
        public Guid PartnerId { get; init; }
        public Guid UserId { get; set; }
        public CommunityType CommunityType { get; init; }
    }
}