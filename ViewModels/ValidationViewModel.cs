using GalaSoft.MvvmLight.Messaging;
using Models;
using System;
using System.ComponentModel.DataAnnotations;

namespace ViewModels
{
    public class ValidationViewModel : BaseViewModel<ValidationViewModel>
    {
        /// <summary>
        /// A token for a messaging channel.
        /// </summary>
        public static readonly Guid MessageToken = Guid.NewGuid();

        private string _phoneNumber;
        [Display(Name = "手機號碼")]
        [Required]
        [MaxLength(10)]
        [RegularExpression(@"^09[0-9]{8}$", ErrorMessage = "手機格式錯誤。")]
        public string PhoneNumber
        {
            get
            {
                if (_phoneNumber == null) _phoneNumber = "test binding";
                return _phoneNumber;
            }
            set => Set(ref _phoneNumber, value);
        }

        public ValidationViewModel()
        {
            // Receive MvvmViewModel Message:
            // 只執行 Send 對應的 Register，其他 Register 不會執行。
            // Register 後，再 Send 的訊息才接得到。
            MessengerInstance.Register<NotificationMessage>(this, MvvmViewModel.MessageToken, message =>
            {
                string notification = message.Notification;
            });
            MessengerInstance.Register<NotificationMessage<string>>(this, MvvmViewModel.MessageToken, message =>
            {
                string notification = message.Notification;
                string content = message.Content;
            });
            MessengerInstance.Register<PropertyChangedMessage<Users>>(this, message =>
            {
                string propertyName = message.PropertyName;
                var sender = message.Sender;
                //if (sender is MvvmViewModel)
                //{
                //    string senderName = nameof(MvvmViewModel);
                //}
            });
        }

    }
}
