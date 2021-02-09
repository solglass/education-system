using EducationSystem.API.Models.InputModels;
using EducationSystem.Data.Models;
using System.Globalization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EducationSystem.API.Utils;
using EducationSystem.API.Models.OutputModels;
using EducationSystem.API.Models;

namespace EducationSystem.API.Mappers
{
    public class PaymentMapper
    {
        public PaymentDto ToDto(PaymentInputModel inputModel)
        {
            if (string.IsNullOrEmpty(inputModel.Date))
            {
                throw new Exception("Ошибка! Не было передано значение Date");
            }

            var (isPaymentDateParsed, Data) = Converters.StrToDateTime(inputModel.Date);

            if (!isPaymentDateParsed)
            {
                throw new Exception("Ошибка! Неверный формат PaymentDate");
            }

            return new PaymentDto
            {
                Id = inputModel.Id,
                ContractNumber = inputModel.ContractNumber,
                Amount = inputModel.Amount,
                Date = DateTime.ParseExact(inputModel.Date, "dd.MM.yyyy", CultureInfo.InvariantCulture),
                Period=inputModel.Period
            };
        }

        public List<PaymentDto> ToDtos(List< PaymentInputModel> inputModels)
        {
            List<PaymentDto> payments = new List<PaymentDto>();
            foreach (PaymentInputModel inputModel in inputModels)
            {

                payments.Add(ToDto(inputModel));
            }

            return payments;
        }

        public PaymentOutputModel FromDto(PaymentDto dto)
        {

            return new PaymentOutputModel
            {
                Id = dto.Id,
                ContractNumber = dto.ContractNumber,
                Amount = dto.Amount,
                Date = Converters.DateTimeToStr(dto.Date),
                Period = dto.Period
            };
        
        }

        public List<PaymentOutputModel> FromDtos(List<PaymentDto> dtos)
        {
            var outputModels = new List<PaymentOutputModel>();

            foreach (var dto in dtos)
            {
                outputModels.Add(FromDto(dto));
            }
            return outputModels;
        }
    }
}
