using System;
using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using SVS.Invoice.Models.Dtos;
using SVS.Invoice.Models.Entities;
using SVS.Invoice.Models.Enums;

namespace SVS.Invoice.Models.Profiles
{
    public class InvoiceProfile : Profile
    {
        private const string _dateStringFormat = "dd-MM-yyyy";
        public InvoiceProfile()
        {
            CreateMap<InvoiceHeader, InvoiceHeaderDetailDto>()
                .ForMember(dto => dto.Date, model => model.MapFrom(props => props.Date.ToString(_dateStringFormat)))
                .ForMember(dto => dto.InvoiceLine, model => model.MapFrom(props => props.InvoiceLines.ToList()));

            CreateMap<InvoiceHeader, InvoiceHeaderDto>()
                .ForMember(dto => dto.Date, model => model.MapFrom(props => props.Date.ToString(_dateStringFormat)))
                .ReverseMap()
                .ForMember(model => model.Date, dto => dto.MapFrom(props => DateTime.Parse(props.Date)));

            CreateMap<InvoiceLine, InvoiceLineDto>()
                .ForMember(dto => dto.UnitCode, model => model.MapFrom(props => ChooseUnitCodeAsString(props.UnitCode)))
                .ReverseMap()
                    .ForMember(model => model.UnitCode, dto => dto.MapFrom(props => ChooseUnitCodeAsEnum(props.UnitCode)));
        }

        private InvoiceLineUnitCodeEnum ChooseUnitCodeAsEnum(string unitCodeAsString)
        {
            switch (unitCodeAsString)
            {
                case "Litre":
                    return InvoiceLineUnitCodeEnum.Litre;
                case "Kilogram":
                    return InvoiceLineUnitCodeEnum.Kilogram;
                default:
                    return InvoiceLineUnitCodeEnum.Adet;
            }
        }
        private string ChooseUnitCodeAsString(InvoiceLineUnitCodeEnum unitCodeAsEnum)
        {
            switch (unitCodeAsEnum)
            {
                case InvoiceLineUnitCodeEnum.Litre:
                    return "Litre";
                case InvoiceLineUnitCodeEnum.Kilogram:
                    return "Kilogram";
                default:
                    return "Adet";
            }
        }
    }
}