using System;
using System.Collections.Generic;
using System.Text;

namespace EducationSystem.Data.Models
{
    public class NotificationDto : ICloneable
    {
        public int Id { get; set; }
        public UserDto User { get; set; }
        public UserDto Author { get; set; }
        public string Message { get; set; }
        public DateTime Date { get; set; }
        public bool IsRead { get; set; }

        public object Clone()
        {
            return new NotificationDto
            {
                User = User != null ? (UserDto) User.Clone() : null,
                Author = User != null ? (UserDto)Author.Clone() : null,
                Message = Message,
                Date = Date,
                IsRead = IsRead
            };
        }

        public override bool Equals(object obj)
        {
            if (obj == null)
                return false;

            if (!(obj is NotificationDto))
                return false;

            NotificationDto notification = (NotificationDto)obj;
            return (Id == notification.Id
                && Message == notification.Message
                && Date.Equals(notification.Date)
                && IsRead == notification.IsRead);
        }
    }
}
