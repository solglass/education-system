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
    }
}
