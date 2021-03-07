﻿using AutoMapper;
using DTOs;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Hosting;
using ReportModule;
using Request;

namespace DentalClinicAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class ReportController : ControllerBase
    {
        private readonly ReportDSL reportDSL;

        public ReportController(IMapper _mapper, IHostEnvironment hostEnvironment)
        {
            reportDSL = new ReportDSL(_mapper, hostEnvironment);
        }
        [HttpPost]
        [Route("GetExpenseReport")]
        public IActionResult GetReceivedDonationReport([FromBody] RequestedData<ExpenseFilterDTO> requestedData)
        {
            var reportString = reportDSL.GetExpenseReport(requestedData.Entity);
            return File(reportString, System.Net.Mime.MediaTypeNames.Application.Octet, "ExpenseReport" + ".pdf");
        }

        [HttpPost]
        [Route("GetExpenseDetailsLists")]
        public IActionResult GetExpenseDetailsLists([FromBody] RequestedData<ExpenseFilterDTO> requestedData)
        {
            requestedData.DetailsList = reportDSL.GetExpenseDetailsLists();
            return Ok(requestedData);
        }
        
    }
}