using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using SVS.Invoice.Models.Dtos;
using SVS.Invoice.Models.Entities;
using SYS.Invoice.BLL.Infrastructure;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SVS.Invoice.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class InvoiceHeadersController : ControllerBase
    {
        private readonly IBaseRepo _baseRepo;
        private readonly IMapper _mapper;
        private readonly ILogger<InvoiceHeadersController> _logger;
        public InvoiceHeadersController(IBaseRepo baseRepo, IMapper mapper, ILogger<InvoiceHeadersController> logger)
        {
           _baseRepo = baseRepo;
           _mapper = mapper;
           _logger = logger;
        }

        /// <summary>
        /// Get all invoices headers
        /// </summary>
        /// <param name="offset">Default : 0</param>
        /// <param name="limit">Default : 10</param>
        /// <returns></returns>
        [HttpGet]
        public async Task<ActionResult<List<InvoiceHeaderDto>>> GetHeaders(int offset, int limit)
        {
            try
            {

                List<InvoiceHeader> headers = await _baseRepo.InvoiceHeadersRepo.GetList(offset, limit);

                List<InvoiceHeaderDto> headerDtos = _mapper.Map<List<InvoiceHeader>,List <InvoiceHeaderDto>>(headers);


                return Ok(headerDtos);
            }
            catch (Exception e)
            {
                _logger.LogError(e.Message);
                return BadRequest();
            }

        }

        /// <summary>
        /// Get details of invoice headers by given id.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("{id}")]
        public async Task<ActionResult<InvoiceHeaderDetailDto>> GetHeadersDetail(string id)
        {
            try
            {

                InvoiceHeader header = await _baseRepo.InvoiceHeadersRepo.Get(id);

                InvoiceHeaderDetailDto headerDetailDto = _mapper.Map<InvoiceHeader, InvoiceHeaderDetailDto>(header);


                return Ok(headerDetailDto);
            }
            catch (Exception e)
            {
                _logger.LogError(e.Message);
                return BadRequest();
            }

        }
    }
}
