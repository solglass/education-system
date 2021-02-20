using System;
using System.Collections.Generic;
using System.Text;

namespace EducationSystem.Core.Enums
{
    public static class FriendlyNames
    {
        public static string GetFriendlyAttachmentTypeName(AttachmentType attachmentType)
        {
            switch (attachmentType)
            {
                case AttachmentType.File:
                    {
                        return "Файл";
                    }
                case AttachmentType.Link:
                    {
                        return "Ссылка";
                    }
                default:
                    {
                        return "Некорректный тип файла";
                    }
            }

        }
    }
}
