using FirebaseAdmin.Messaging;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MessagingLab
{
    public class HomeController : Controller
    {
        // Fields
        private readonly FirebaseMessaging _firebaseMessaging = null;


        // Constructors
        public HomeController(FirebaseMessaging firebaseMessaging)
        {
            #region Contracts

            if (firebaseMessaging == null) throw new ArgumentException();

            #endregion

            // Default
            _firebaseMessaging = firebaseMessaging;
        }


        // Methods
        public ActionResult<SendNotificationResultModel> SendNotification([FromBody] SendNotificationActionModel actionModel)
        {
            #region Contracts

            if (actionModel == null) throw new ArgumentException(nameof(actionModel));

            #endregion

            // Message
            var message = new MulticastMessage()
            {
                Notification = new Notification()
                {
                    Title = actionModel.Title,
                    Body = actionModel.Body,
                },
                Tokens = actionModel.RegistrationTokenList,
            };

            // Send
            var response = _firebaseMessaging.SendMulticastAsync(message).GetAwaiter().GetResult();
            if (response == null) throw new InvalidOperationException($"{nameof(response)}=null");

            // Return
            return (new SendNotificationResultModel()
            {
               SuccessCount = response.SuccessCount,
               FailureCount = response.FailureCount
            });
        }


        // Class
        public class SendNotificationActionModel
        {
            // Properties
            public string Title { get; set; }

            public string Body { get; set; }

            public List<string> RegistrationTokenList { get; set; }
        }

        public class SendNotificationResultModel
        {
            // Properties
            public int SuccessCount { get; set; }

            public int FailureCount { get; set; }
        }
    }
}
