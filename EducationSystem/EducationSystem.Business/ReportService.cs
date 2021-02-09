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
        private PaymentRepository _paymentReportRepository;
        public ReportService()
        {
            _groupReportRepository = new GroupRepository();
        }
        public List<UserDto> GetStudentsByIsPaidInPeriod(string period)
        {
            return _paymentReportRepository.GetStudentsByIsPaidInPeriod(period);
        }
        public List<GroupReportDto> GetAttachments() { return _groupReportRepository.GenerateReport(); }

    }
}
