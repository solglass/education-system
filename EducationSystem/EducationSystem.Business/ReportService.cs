using EducationSystem.Data;
using EducationSystem.Data.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace EducationSystem.Business
{
    public class ReportService : IReportService
    {
        private IGroupRepository _groupReportRepository;
        private IPaymentRepository _paymentReportRepository;
        public ReportService(IGroupRepository groupRepository, IPaymentRepository paymentRepository)
        {
            _groupReportRepository = groupRepository;
            _paymentReportRepository = paymentRepository;
        }
        public List<UserDto> GetStudentsByIsPaidInPeriod(string period)
        {
            return _paymentReportRepository.GetListOfStudentsByPeriodWhoHaveNotPaid(period);
        }
        public List<GroupReportDto> GetAttachments() { return _groupReportRepository.GenerateReport(); }

    }
}
