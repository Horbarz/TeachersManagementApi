using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using SchAppAPI.DOA;
using SchAppAPI.Models;
using SchAppAPI.Repository;
using SchAppAPI.Services;

namespace SchAppAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class NotificationController : ControllerBase
    {
        private readonly IMobileMessagingClient messagingClient;
        private readonly UserManager<User> userManager;
        private readonly INotificationRepository notificationRepository;
        public readonly IUserRepository userRepository;

        public NotificationController(IMobileMessagingClient messagingClient,
                                INotificationRepository notificationRepository,
                                UserManager<User> userManager)
        {
            this.messagingClient = messagingClient;
            this.notificationRepository = notificationRepository;
            this.userManager = userManager;
        }

        [HttpPost]
        [Route("sendNotification")]
        public async Task<IActionResult> SendPushNotification(CreateNotificationRequest notification)
        {
            var notificationToCreate = new Notification
            {
                Title = notification.Title,
                Subject = notification.Subject,
                Content = notification.Content
            };

            string token = "f1mCeFOHRJylzuxDeplRGQ:APA91bEYO7RqMcTW5-eEQpDq2gD92Ee9wRawq8Pvrh23vfwpL1Nkwt7OJd45lbUye5e-5GwT2goY9D0fqFJCRDJQ_70ouKOdPnencG5KZDcmk4zO6xdrhzx0Mc9J612fr271BtFRymMf"; //get device fcm token and pass here
            string token2 = "caHdDt5XRrKVGIyeH22zTR:APA91bGkwl7bLwFwpQwi7qhQBMT2iaeSGr85Y68S2fimyGA1KLkKmQ5Ms2bSpb0ehmFH5K_LxXLKSr4-FFgZQbjwOvLvFv5lOLlANpsKBiH6lO10bT-hypldZ3LR7C9eQOAYYqbNN8Ce";
            //save to database
            await this.notificationRepository.Add(notificationToCreate);
            await this.notificationRepository.SaveChangesAsync();

            var result = messagingClient.SendNotification(token2, notification.Title, notification.Content);

            return Ok(new { Status = "Successful" });
        }

        [HttpPost]
        [Route("pushNotifications")]
        public async Task<IActionResult> SendMultipleNotifications(CreateNotificationRequest notification)
        {
            var notificationToCreate = new Notification
            {
                Title = notification.Title,
                Subject = notification.Subject,
                Content = notification.Content
            };
            var registrationTokens = new List<string>() { "", "" };
            string token = "f1mCeFOHRJylzuxDeplRGQ:APA91bEYO7RqMcTW5-eEQpDq2gD92Ee9wRawq8Pvrh23vfwpL1Nkwt7OJd45lbUye5e-5GwT2goY9D0fqFJCRDJQ_70ouKOdPnencG5KZDcmk4zO6xdrhzx0Mc9J612fr271BtFRymMf"; //get device fcm token and pass here

            //save to database
            await this.notificationRepository.Add(notificationToCreate);
            await this.notificationRepository.SaveChangesAsync();

            var result = messagingClient.SendNotification(token, notification.Title, notification.Content);

            return Ok(new { Status = "Successful" });
        }
    }
}