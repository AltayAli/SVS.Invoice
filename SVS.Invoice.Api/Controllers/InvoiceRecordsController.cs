using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using SVS.Invoice.Models.Entities;
using SYS.Invoice.BLL.HelperServices;
using SYS.Invoice.BLL.Infrastructure;
using SYS.Invoice.BLL.Models;
using System;
using System.IO;
using System.Threading.Tasks;

namespace SVS.Invoice.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class InvoiceRecordsController : ControllerBase
    {
        private readonly IBaseRepo _baseRepo;
        private readonly IMapper _mapper;
        private readonly ILogger<InvoiceHeadersController> _logger;
        private readonly IFileOperationsService _fileOperationsService;
        public InvoiceRecordsController(IBaseRepo baseRepo, 
                                        IMapper mapper, 
                                        ILogger<InvoiceHeadersController> logger,
                                        IFileOperationsService fileOperationsService)
        {
            _baseRepo = baseRepo;
            _mapper = mapper;
            _logger = logger;
            _fileOperationsService = fileOperationsService;
        }

        /// <summary>
        /// Upload invoice record as json
        /// </summary>
        /// <param name="file">Permitted format is json. Other formats will be rejected.</param>
        /// <returns></returns>
        [HttpPost]
        public async Task<IActionResult> Insert(IFormFile file)
        {
            try
            {
                string fileExt = Path.GetExtension(file.FileName);
                if (fileExt != ".json")
                    throw new InvalidDataException("Wrong file format");

                FileDetailModel fileDetail = await _fileOperationsService.Upload(file);

                await _baseRepo.InvoiceRecordsRepo.Insert(new InvoiceRecord()
                {
                    FileName = fileDetail.Name,
                    Record = fileDetail.Text
                });

                await _baseRepo.SaveChanges();

                return Ok();
            }
            catch (Exception e)
            {
                _logger.LogError(e.Message);
                return BadRequest();
            }
        }
    }
}
