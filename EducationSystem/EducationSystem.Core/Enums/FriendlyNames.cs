using System;
using System.Collections.Generic;
using System.Text;

namespace EducationSystem.Core.Enums
{
    public static class FriendlyNames
    {
        public static string GetFriendlyAttachmentTypeName(AttachmentType attachmentType)
        {

            string friendlyName = attachmentType switch
            {
                AttachmentType.File => "Файл",
                AttachmentType.Link => "Ссылка",
                _ => "Некорректный тип файла"
            };
            return friendlyName;

        }
        public static string GetFriendlyGroupStatusName(GroupStatus groupStatus)
        {
            string friendlyName = groupStatus switch
            {
                GroupStatus.Recruitment => "Ведётся набор",
                GroupStatus.ReadyToStart => "Ждёт начала обучения",
                GroupStatus.InProgress => "Идёт обучение",
                GroupStatus.Finished => "Завершила обучение",
                _ => "Статус группы не найден"
            };
            return friendlyName;

        }

        public static string GetFriendlyUnderstandingLevelName(UnderstandingLevel understandingLevel)
        {
            string FriendlyName = understandingLevel switch
            {
                UnderstandingLevel.Bad => "Плохо",
                UnderstandingLevel.Medium => "Средне",
                UnderstandingLevel.Good => "Хорошо",
                _ => "Уровень понимания не найден"
            };
            return FriendlyName;

        }

        public static string GetFriendlyHomeworkAttemptStatusName(HomeworkAttemptStatus attemptStatus)
        {

            string FriendlyName = attemptStatus switch
            {
                HomeworkAttemptStatus.ToDo => "Назначено",
                HomeworkAttemptStatus.Expired => "Просрочено",
                HomeworkAttemptStatus.ToCheck => "Ждет проверки",
                HomeworkAttemptStatus.Passed => "Ждет исправления",
                HomeworkAttemptStatus.PassedLate=> "Сдано",
                HomeworkAttemptStatus.ToFix => "Сдано с опозданием",
                _ => "Некорректный статус"
            };
            return FriendlyName;

        }

        public static string GetFriendlyRoleName(Role role)
        {

            string FriendlyName = role switch
            {
                Role.Admin => "Администратор",
                Role.Manager => "Менеджер",
                Role.Methodist => "Методист",
                Role.Student => "Студент",
                Role.Teacher => "Преподаватель",
                Role.Tutor => "Тьютор",
                _ => "Роль не существует"
            };
            return FriendlyName;

        }
    }
}
