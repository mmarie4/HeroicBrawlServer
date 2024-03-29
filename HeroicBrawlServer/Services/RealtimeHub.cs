﻿using HeroicBrawlServer.Services.Abstractions;
using HeroicBrawlServer.Services.Models.Messages;
using HeroicBrawlServer.Services.Models.Messages.PlayerActions;
using HeroicBrawlServer.Shared.Extensions;
using Microsoft.AspNetCore.SignalR;
using System;
using System.Threading.Tasks;

namespace HeroicBrawlServer.Services
{
    /// <summary>
    ///     SignalR custom hub
    /// </summary>
    public class RealtimeHub : Hub
    {

        private readonly IRoomService _roomService;
        private readonly IUserService _userService;

        /// <summary>
        ///     RealtimeHub constructor
        /// </summary>
        /// <param name="roomService"></param>
        public RealtimeHub(IRoomService roomService, IUserService userService)
        {
            _roomService = roomService;
            _userService = userService;
        }

        /// <summary>
        ///     Handles player disconnected to remove from the rooms
        /// </summary>
        /// <param name="exception"></param>
        /// <returns></returns>
        public override Task OnDisconnectedAsync(Exception exception)
        {
            var connectionId = Context.ConnectionId;
            _roomService.RemovePlayerFromRooms(connectionId);

            return base.OnDisconnectedAsync(exception);
        }

        #region SignalR entry points

        /// <summary>
        ///     Adds user to a signalR group
        /// </summary>
        /// <param name="roomId">Id of the room</param>
        /// <param name="heroId">Id of hero</param>
        /// <param name="bearerToken">Access token containing the user id</param>
        /// <returns></returns>
        public async Task JoinRoom(Guid roomId, string heroName, string initialState, string bearerToken)
        {
            var userId = bearerToken.ExtractUserId();
            var user = await _userService.GetByIdAsync(userId);

            _roomService.AddUserToRoom(Context.ConnectionId, userId, roomId, heroName, initialState);
            await Groups.AddToGroupAsync(Context.ConnectionId, roomId.ToString());
            await SendMessage(roomId, new UserJoinedRoomMessage(userId, user.Pseudo, heroName));
        }

        /// <summary>
        ///     Removes a user from a signalR group
        /// </summary>
        /// <param name="roomId">Id of the room</param>
        /// <param name="bearerToken">Access token containing the user id</param>
        /// <returns></returns>
        public async Task LeaveRoom(Guid roomId, Guid connectionId, string bearerToken)
        {
            var userId = bearerToken.ExtractUserId();

            _roomService.RemoveUserFromRoom(Context.ConnectionId, userId, roomId);
            await Groups.RemoveFromGroupAsync(Context.ConnectionId, roomId.ToString());
            await SendMessage(roomId, new UserLeftRoomMessage(userId, connectionId));
        }

        #region player actions

        public void Move(MoveMessage request)
        {
            _roomService.MovePlayer(request.RoomId, request.ConnectionId, request.PositionX, request.PositionY);
        }

        public void SetAnimationState(SetAnimationMessage request)
        {
            _roomService.SetAnimationStatePlayer(request.RoomId, request.ConnectionId, request.AnimationState);
        }

        public void TakeDamage(TakeDamageMessage request)
        {
            _roomService.TakeDamagePlayer(request.RoomId, request.ConnectionId, request.DamageTaken, request.FromConnectionId);
        }

        public void Die(DieMessage request)
        {
            _roomService.Die(request.RoomId, request.ConnectionId, request.FromConnectionId);
        }

        public void Respawn(RespawnMessage request)
        {
            _roomService.RespawnPlayer(request.RoomId, request.ConnectionId, request.X, request.Y);
        }

        #endregion

        #endregion

        #region private functions

        /// <summary>
        ///     Sends a message to all users in the room
        /// </summary>
        /// <param name="roomId">id of the room</param>
        /// <param name="message">message to send</param>
        /// <returns></returns>
        private async Task SendMessage(Guid roomId, BaseMessage message)
        {
            await ((IHubClients)Clients).SendToRoom(roomId, message);
        }

        #endregion

    }
}
