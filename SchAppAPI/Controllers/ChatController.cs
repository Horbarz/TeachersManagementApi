using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using Newtonsoft.Json;
using SchAppAPI.DOA.Requests;
using SchAppAPI.DOA.Responses;
using SchAppAPI.Models;
using SchAppAPI.Models.Chat;
using SchAppAPI.Repository;
using SchAppAPI.Services;
using SchAppAPI.Services.Hubs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace SchAppAPI.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class ChatController : ControllerBase
    {
        public readonly IChatRepository chatRepo;
        private readonly UserManager<User> userManager;
        private readonly IHubContext<ChatHub> hubContext;
        private readonly IMobileMessagingClient messagingClient;
        public ChatController(IChatRepository chatRepo, UserManager<User> usermanager, IHubContext<ChatHub> hubContext, IMobileMessagingClient messagingClient)
        {
            this.chatRepo = chatRepo;
            this.userManager = usermanager;
            this.hubContext = hubContext;
            this.messagingClient = messagingClient;
        }

        [HttpPost]
        [Route("sendmessage")]
        public async Task<IActionResult> SendMessage(SendMessageRequest sendMessageRequest)
        {
            //await this.hubContext.Clients.All.SendAsync("ReceiveMessage", sendMessageRequest.Body);

            if (!ModelState.IsValid) return BadRequest();
            var receipient = await userManager.FindByEmailAsync(sendMessageRequest.ReceipientEmail);
            if (receipient == null) return NotFound("Receipient not found");

            if (!Guid.TryParse(User.Claims.Where(c => c.Type == "Id")
                   .Select(c => c.Value).SingleOrDefault(), out var userId)) return BadRequest("User not found");


            if (userId == Guid.Empty) return BadRequest("User not found");
            if (string.IsNullOrWhiteSpace(sendMessageRequest.Body)) return BadRequest("Empty message");
            var message = new Message
            {
                SenderId = userId.ToString(),
                ReceipientId = receipient.Id,
                Body = Convert.ToString(sendMessageRequest.Body)
            };

            await this.chatRepo.Add(message);
            await this.chatRepo.SaveChangesAsync();

            //todo:: update receipient real time
            //await this.hubContext.Clients.All.SendAsync("ReceiveMessage", sendMessageRequest.Body);

            var username = User.Claims.Where(c => c.Type == ClaimTypes.Name)
                   .Select(c => c.Value).SingleOrDefault();

            if(ConnectedUsers.Users.Contains(receipient.UserName))
            {
                var hubMessage = new Dictionary<string, string>
                    {
                        { "Sender", username },
                        { "Message", message.Body}
                    };

                await this.hubContext.Clients.User(receipient.UserName)
                    .SendAsync("ReceiveMessage", JsonConvert.SerializeObject(hubMessage));
            }
            else
            {
                await messagingClient.SendNotification(receipient.DeviceId, $"New Message From {username}", message.Body);
            }

            return Ok(new { status = true, message = "Message sent Successfully" });

        }

        [HttpPost]
        [Route("markAsRead/{id}")]
        public async Task<IActionResult> MarkMessageAsRead(Guid messageId)
        {
            if (!ModelState.IsValid) return BadRequest();

            var userId = User.Claims.Where(c => c.Type == "Id")
                   .Select(c => c.Value).SingleOrDefault();


            if (string.IsNullOrWhiteSpace(userId)) return BadRequest("User not found");

            var messageFromRepo = (await this.chatRepo.Get(msg => msg.ReceipientId == userId && msg.Id == messageId)).FirstOrDefault();
            if (messageFromRepo == null) return BadRequest("Invalid message");
            messageFromRepo.Read = true;

            this.chatRepo.Update(messageFromRepo);
            await this.chatRepo.SaveChangesAsync();

            //todo:: update sender real time
            return Ok(new { status = true, message = "Message read Successfully" });

        }

        [HttpGet]
        [Route("conversationwith/{email}")]
        public async Task<IActionResult> GetConversationWithUser(string email)
        {
            if (!ModelState.IsValid) return BadRequest();

            var userId = User.Claims.Where(c => c.Type == "Id")
                   .Select(c => c.Value).SingleOrDefault();


            if (string.IsNullOrWhiteSpace(userId)) return BadRequest("User not found");


            var receipient = await userManager.FindByEmailAsync(email);
            if (receipient == null) return NotFound("Receipient not found");
            


            var messagesFromRepo = await this.chatRepo.Get(msg => (msg.ReceipientId == receipient.Id && msg.SenderId == userId) ||
                                                                  (msg.ReceipientId == userId && msg.SenderId == receipient.Id), 
                                                          msg => msg.OrderBy(msg => msg.CreatedOn));

            if (!messagesFromRepo.Any()) return Ok(new List<ConversationResponse>());
            var messageToReturn = messagesFromRepo.Select(msg => new ConversationResponse {
                Id = msg.Id,
                UserIsSender = msg.SenderId == userId,
                Body = msg.Body,
                CreatedOn = msg.CreatedOn,
                Read = msg.Read
            });

            return Ok(messageToReturn);

        }

        [HttpGet]
        [Route("GetUnreadMessage")]
        public async Task<IActionResult> GetUnread()
        {
            if (!ModelState.IsValid) return BadRequest();

            var userId = User.Claims.Where(c => c.Type == "Id")
                   .Select(c => c.Value).SingleOrDefault();


            if (string.IsNullOrWhiteSpace(userId)) return BadRequest("User not found");

            var messagesFromRepo = await this.chatRepo.Get(msg => msg.ReceipientId == userId && msg.Read == false, null, $"{nameof(Message.Sender)}");

            if (!messagesFromRepo.Any()) return Ok(new List<ConversationResponse>());
            var messageToReturn = messagesFromRepo.Select(msg => new UnreadChatsResponse
            {
                Id = msg.Id,
                Sender = msg.Sender.Email,
                Body = msg.Body,
                CreatedOn = msg.CreatedOn,
                Read = msg.Read
            });

            return Ok(messageToReturn);

        }

        [HttpGet]
        [Route("GetChats")]
        public async Task<IActionResult> GetChats()
        {
            if (!ModelState.IsValid) return BadRequest();


            var userId = User.Claims.Where(c => c.Type == "Id")
                   .Select(c => c.Value).SingleOrDefault();


            if (string.IsNullOrWhiteSpace(userId)) return BadRequest("User not found");

            var messagesFromRepo = await this.chatRepo.GetActiveChats(userId);
            if (!messagesFromRepo.Any()) return Ok(new List<string>());

            var messagesToReturn = messagesFromRepo.Select(msg => new ChatListResponse
            {
                User = msg.SenderId == userId ? msg.Receipient.Email : msg.Sender.Email,
                Body = msg.Body,
                Read = msg.Read,
                CreatedOn = msg.CreatedOn
            });

            return Ok(messagesToReturn);
        }

    }
}
