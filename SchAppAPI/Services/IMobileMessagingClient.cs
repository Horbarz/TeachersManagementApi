using System.Collections.Generic;
using System.Threading.Tasks;
using FirebaseAdmin.Messaging;

namespace SchAppAPI.Services
{
    public interface IMobileMessagingClient
    {
        Task SendNotification(string token, string title, string body);
        Message CreateNotification(string title, string notificationBody, string token);
        MulticastMessage CreateNotifications(string title, string notificationBody, List<string> tokens);

        Task SendMultipleNotifications(List<string> tokens, string title, string body);
    }
}