using System;
using System.Collections.Generic;
using System.Text;

namespace EducationSystem.Core.Enums
{
    public static class FriendlyNames
    {
        public static string GetFriendlyAttachmentTypeName(AttachmentType attachmentType)
        {

            String FriendlyName = attachmentType switch
            {
                AttachmentType.File => "Файл",
                AttachmentType.Link => "Ссылка",
                _ => "Некорректный тип файла"
            };
            return FriendlyName;

        }
        public static string GetFriendlyGroupStatusName(GroupStatus groupStatus)
        {
            String FriendlyName = groupStatus switch
            {
                GroupStatus.Recruitment => "Ведётся набор",
                GroupStatus.ReadyToStart => "Ждёт начала обучения",
                GroupStatus.InProgress => "Идёт обучение",
                GroupStatus.Finished => "Завершила обучение",
                _ => "Статус группы не найден"
            };
            return FriendlyName;

        }

        public static string GetFriendlyUnderstandingLevelName(UnderstandingLevel understandingLevel)
        {
            String FriendlyName = understandingLevel switch
            {
                UnderstandingLevel.Bad => "Плохо",
                UnderstandingLevel.Medium => "Средне",
                UnderstandingLevel.Good => "Хорошо",
                _ => "Уровень понимания не найден"
            };
            return FriendlyName;

        }
    }
}
