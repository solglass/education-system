using EducationSystem.Data;
using EducationSystem.Data.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace EducationSystem.Business
{
	public class AttachmentService
	{

		private AttachmentRepository _attachmentRepository;

		public AttachmentService()
		{
			_attachmentRepository = new AttachmentRepository();
		}

		public AttachmentDto GetAttachmentById(int id)
		{
			return _attachmentRepository.GetAttachmentById(id);
		}
		public int ModifyAttachment(AttachmentDto attachmentDto)
		{
			return _attachmentRepository.ModifyAttachment(attachmentDto);
		}
		public int AddAttachment(AttachmentDto attachmentDto)
		{
			return _attachmentRepository.AddAttachment(attachmentDto);
		}
		public int DeleteAttachmentById(int id)
		{
			return _attachmentRepository.DeleteAttachmentById(id);
		}

		public int AddAttachmentToHomeworkAttempt(AttachmentDto attachmentDto, int homeworkAttemptId)
		{
			return _attachmentRepository.AddAttachmentToHomeworkAttempt(attachmentDto, homeworkAttemptId);
		}
		public int AddAttachmentToComment(AttachmentDto attachmentDto, int commentId)
		{
			return _attachmentRepository.AddAttachmentToComment(attachmentDto, commentId);
		}

	}
}
