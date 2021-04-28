using System.Collections.Generic;
using System.Threading.Tasks;
using FirebaseAdmin;
using FirebaseAdmin.Messaging;
using Google.Apis.Auth.OAuth2;

namespace SchAppAPI.Services
{
    public class MobileMessagingClient : IMobileMessagingClient
    {
        private readonly FirebaseMessaging messaging;

        public MobileMessagingClient()
        {
            var app = FirebaseApp.Create(new AppOptions() { Credential = GoogleCredential.FromFile("ihsapi.json").CreateScoped("https://www.googleapis.com/auth/firebase.messaging") });
            messaging = FirebaseMessaging.GetMessaging(app);
        }

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
            return messaging.SendAsync(CreateNotification(title, body, token));
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
            };
        }

        public Task SendMultipleNotifications(List<string> tokens, string title, string body)
        {
            return messaging.SendMulticastAsync(CreateNotifications(title, body, tokens));
        }
    }

}