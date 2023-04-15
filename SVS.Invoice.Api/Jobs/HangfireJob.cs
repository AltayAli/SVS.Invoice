using AutoMapper;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using SVS.Invoice.Models.Dtos;
using SVS.Invoice.Models.Entities;
using SVS.Invoice.Models.Enums;
using SYS.Invoice.BLL.HelperServices;
using SYS.Invoice.BLL.Infrastructure;
using SYS.Invoice.BLL.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace SVS.Invoice.Api.Jobs
{
    public class HangfireJob
    {
        private readonly IBaseRepo _baseRepo;
        private readonly IMapper _mapper;
        private readonly ILogger<HangfireJob> _logger;
        private readonly IEmailSender _emailSender;
        public HangfireJob(IBaseRepo baseRepo,
                                        IMapper mapper,
                                        ILogger<HangfireJob> logger,
                                        IEmailSender emailSender)
        {
            _baseRepo = baseRepo;
            _mapper = mapper;
            _logger = logger;
            _emailSender = emailSender;
        }

        public void LoadRecords()
        {
            List<InvoiceRecord> records = _baseRepo.InvoiceRecordsRepo.GetList(InvoiceRecordStatusEnum.Waiting).GetAwaiter().GetResult();

            foreach (InvoiceRecord record in records)
            {
                try
                {
                    InvoiceDto invoice = JsonConvert.DeserializeObject<InvoiceDto>(record.Record);

                    InvoiceHeader invoiceHeader = _mapper.Map<InvoiceHeader>(invoice.InvoiceHeader);
                    List<InvoiceLine> invoiceLines = _mapper.Map<List<InvoiceLine>>(invoice.InvoiceLine);
                    _baseRepo.InvoiceHeadersRepo.Insert(invoiceHeader).GetAwaiter().GetResult();
                    _baseRepo.InvoiceLinesRepo.Insert(invoiceHeader.Id, invoiceLines).GetAwaiter().GetResult();

                    record.Status = InvoiceRecordStatusEnum.Added;
                    _baseRepo.InvoiceRecordsRepo.Update(record);
                    _baseRepo.SaveChanges().GetAwaiter().GetResult();

                    SendMail(invoice.InvoiceHeader);
                }
                catch (Exception ex)
                {
                    _logger.LogError(ex.Message);
                    record.Status = InvoiceRecordStatusEnum.InCorrect;
                    _baseRepo.InvoiceRecordsRepo.Update(record).GetAwaiter().GetResult();
                    _baseRepo.SaveChanges().GetAwaiter().GetResult();
                }

            }

        }

        private void SendMail(InvoiceHeaderDto invoiceHeader)
        {
            StringBuilder mailBodySb = new();

            mailBodySb.Append($"ID : {invoiceHeader.Id}.</br>" + 
                            $"Sender : {invoiceHeader.SenderTitle}.</br>" +
                            $"Receiver : {invoiceHeader.ReceiverTitle}.</br>" +
                            $"Date : {invoiceHeader.Date}.</br>");

            try
            {
                _emailSender.SendEmailAsync(new MailRequest
                {
                    Subject = "Invoice succefully added",
                    Body = mailBodySb.ToString()
                }).GetAwaiter().GetResult();
            }
            catch (Exception ex)
            {

                _logger.LogError(ex.Message);
            }
        }
    }
}
