using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Threading.Tasks;
using TerminalManage.Services.Information;

namespace TerminalManange.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class InformationController : ControllerBase
    {
        private readonly IInformationService _informationService;
        private readonly ILogger<InformationController> _logger;

        public InformationController(IInformationService informationService, ILogger<InformationController> logger)
        {
            _informationService = informationService;
            _logger = logger;
        }

        [HttpGet]
        [Route(nameof(GetSummary))]
        public async Task<IActionResult> GetSummary()
        {
            try
            {
                return Ok(await _informationService.GetSummary());
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"logging error in {nameof(GetSummary)}");
                throw;
            }
        }

        [HttpGet]
        [Route(nameof(GetSummaryById))]
        public async Task<IActionResult> GetSummaryById(string id)
        {
            try
            {
                return Ok(await _informationService.GetSummaryById(id));
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"logging error in {nameof(GetSummaryById)}");
                throw;
            }
        }
    }
}
