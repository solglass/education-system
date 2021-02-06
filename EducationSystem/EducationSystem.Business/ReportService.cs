using EducationSystem.Data;
using EducationSystem.Data.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace EducationSystem.Business
{
   public class ReportService
    {
        private GroupRepository  _groupReportRepository;

        public ReportService()
        {
            _groupReportRepository = new GroupRepository();
        }

        public List<GroupReportDto> GetAttachments() { return _groupReportRepository.GenerateReport(); }

    }
}
