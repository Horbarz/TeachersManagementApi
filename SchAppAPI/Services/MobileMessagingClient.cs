using System.Collections.Generic;
using System.Threading.Tasks;
using FirebaseAdmin;
using FirebaseAdmin.Messaging;
using Google.Apis.Auth.OAuth2;

namespace SchAppAPI.Services
{
    public class MobileMessagingClient : IMobileMessagingClient
    {

        public Message CreateNotification(string title, string notificationBody, string token)
        {
            return new Message()
            {
                Token = token,
                Notification = new Notification()
                {
                    Body = notificationBody,
                    Title = title
                }
            };
        }

        public Task SendNotification(string token, string title, string body)
        {
            return FirebaseMessaging.DefaultInstance.SendAsync(CreateNotification(title, body, token));
        }

        public Message CreateNotifications(string title, string notificationBody, string token)
        {
            return new Message()
            {
                Token = token,
                Notification = new Notification()
                {
                    Body = notificationBody,
                    Title = title
                }
            };
        }

        public MulticastMessage CreateNotifications(string title, string notificationBody, List<string> tokens)
        {
            return new MulticastMessage()
            {
                Tokens = tokens,
                Notification = new Notification()
                {
                    Body = notificationBody,
                    Title = title
                }
                // Data = new Dictionary<string, string>()
                // {
                //    {"title",title},
                //    {"body",notificationBody}
                // }
            };
        }

        public Task SendMultipleNotifications(List<string> tokens, string title, string body)
        {
            return FirebaseMessaging.DefaultInstance.SendMulticastAsync(CreateNotifications(title, body, tokens));
        }
    }

}