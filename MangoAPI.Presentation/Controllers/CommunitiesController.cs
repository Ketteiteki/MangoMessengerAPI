﻿using System;
using System.Threading;
using System.Threading.Tasks;
using MangoAPI.BusinessLogic.ApiCommands.Communities;
using MangoAPI.BusinessLogic.ApiQueries.Communities;
using MangoAPI.BusinessLogic.Responses;
using MangoAPI.Presentation.Extensions;
using MangoAPI.Presentation.Interfaces;
using MediatR;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace MangoAPI.Presentation.Controllers
{
    /// <summary>
    /// Controller responsible for CommunityType Entity.
    /// </summary>
    [ApiController]
    [Route("api/communities")]
    [Produces("application/json")]
    [Authorize(Roles = "User", AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class CommunitiesController : ApiControllerBase, ICommunitiesController
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="CommunitiesController"/> class.
        /// </summary>
        /// <param name="mediator">Mediator instance.</param>
        public CommunitiesController(IMediator mediator)
            : base(mediator)
        {
        }

        /// <summary>
        /// Gets all current user's chats. Requires role: User.
        /// </summary>
        /// <param name="cancellationToken">Cancellation token instance.</param>
        /// <returns>Possible codes: 200, 400, 409.</returns>
        [HttpGet]
        [SwaggerOperation(Description = "Gets all current user's chats. Requires role: User.",
            Summary = "Gets all user's chats.")]
        [ProducesResponseType(typeof(GetCurrentUserChatsResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status409Conflict)]
        public async Task<IActionResult> GetChats(CancellationToken cancellationToken)
        {
            var userId = HttpContext.User.GetUserId();

            var request = new GetCurrentUserChatsQuery
            {
                UserId = userId
            };

            return await RequestAsync(request, cancellationToken);
        }

        /// <summary>
        /// Creates new group of specified type: Private Channel (3), Public Channel (4), Readonly Channel (5).
        /// Requires role: User.
        /// </summary>
        /// <param name="request">CreateChannelRequest instance.</param>
        /// <param name="cancellationToken">Cancellation token instance.</param>
        /// <returns>Possible codes: 200, 400, 409.</returns>
        [HttpPost("channel")]
        [SwaggerOperation(Description =
            "Creates new group of specified type: Private Channel (3), Public Channel (4), Readonly Channel (5). " +
            "Requires role: User.",
            Summary = "Creates new group of specified type.")]
        [ProducesResponseType(typeof(CreateCommunityResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status409Conflict)]
        public async Task<IActionResult> CreateChannelAsync([FromBody] CreateChannelRequest request,
            CancellationToken cancellationToken)
        {
            var userId = HttpContext.User.GetUserId();
            var command = request.ToCommand(userId);
            return await RequestAsync(command, cancellationToken);
        }

        /// <summary>
        /// Creates new direct chat with specified user. User is fetched by parameter user ID.
        /// Requires role: User.
        /// </summary>
        /// <param name="request">CreateChatRequest instance.</param>
        /// <param name="cancellationToken">Cancellation token instance.</param>
        /// <returns>Possible codes: 200, 400, 409.</returns>
        [HttpPost("chat")]
        [SwaggerOperation(Description =
            "Creates new chat with specified user. Chat types: Direct Chat (1), Secret Chat (2). " +
            "Requires role: User.",
            Summary = "Creates new chat with specified user.")]
        [ProducesResponseType(typeof(CreateCommunityResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status409Conflict)]
        public async Task<IActionResult> CreateChatAsync([FromBody] CreateChatRequest request,
            CancellationToken cancellationToken)
        {
            var userId = HttpContext.User.GetUserId();

            var command = request.ToCommand(userId);

            return await RequestAsync(command, cancellationToken);
        }

        /// <summary>
        /// Gets chat by ID. Requires role: User.
        /// </summary>
        /// <param name="id">User ID of colleague, UUID.</param>
        /// <param name="cancellationToken">Cancellation token instance.</param>
        /// <returns>Possible codes: 200, 400, 409.</returns>
        [HttpGet("{id:guid}")]
        [SwaggerOperation(Description = "Gets chat by ID. Requires role: User.",
            Summary = "Gets chat by ID.")]
        [ProducesResponseType(typeof(GetCommunityByIdResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status409Conflict)]
        public async Task<IActionResult> GetCommunityById([FromRoute] Guid id, CancellationToken cancellationToken)
        {
            var currentUserId = HttpContext.User.GetUserId();

            var query = new GetCommunityByIdQuery
            {
                UserId = currentUserId,
                ChatId = id,
            };

            return await RequestAsync(query, cancellationToken);
        }


        /// <summary>
        /// Searches chats by display name. Requires role: User.
        /// </summary>
        /// <param name="displayName">Display name of the chat, string.</param>
        /// <param name="cancellationToken">Cancellation token instance.</param>
        /// <returns>Possible codes: 200, 400, 409.</returns>
        [HttpGet("searches")]
        [SwaggerOperation(Description = "Searches chats by display name. Requires role: User.",
            Summary = "Searches chats by display name.")]
        [ProducesResponseType(typeof(SearchCommunityResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status409Conflict)]
        public async Task<IActionResult> SearchAsync([FromQuery] string displayName,
            CancellationToken cancellationToken)
        {
            var userId = HttpContext.User.GetUserId();

            var command = new SearchCommunityQuery
            {
                DisplayName = displayName,
                UserId = userId,
            };

            return await RequestAsync(command, cancellationToken);
        }

        /// <summary>
        /// Returns secret chat public key. Required role: User
        /// </summary>
        /// <param name="displayName">ID of the chat, string.</param>
        /// <param name="cancellationToken">Cancellation token instance.</param>
        /// <returns>Possible codes: 200, 400, 409.</returns>
        [HttpGet("chats/public-key/{chatId:guid}")]
        [SwaggerOperation(Description = "Returns secret chat public key. Required role: User",
            Summary = "Returns secret chat public key.")]
        [ProducesResponseType(typeof(GetSecretChatPublicKeyResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status409Conflict)]
        public async Task<IActionResult> GetSecretChatPublicKey([FromRoute] Guid chatId, CancellationToken cancellationToken)
        {
            var userId = HttpContext.User.GetUserId();

            var query = new GetSecretChatPublicKeyQuery
            {
                ChatId = chatId,
                UserId = userId
            };

            return await RequestAsync(query, cancellationToken);
        }
    }
}