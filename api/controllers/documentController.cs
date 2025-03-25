using common.utils.pdf;
using entities.models;
using Microsoft.AspNetCore.Mvc;

namespace api.controllers
{
    [ApiController]
    [Route("[controller]")]
    public class documentController : Controller
    {
        private readonly ILogger<documentController> _logger;

        public documentController(ILogger<documentController> logger)
        {
            _logger = logger;
        }

        [HttpPost("generatePdf")]
        public async Task<IActionResult> generatePdf([FromBody] pdfRequestModel request)
        {
            try
            {
                var pdfBytes = new pdfHelper().generatePdf(request.documents!, request.sections!, request.contents!, request.horizontalFooterSeparator!, request.verticalSeparator!, request.horizontalSeparator!);
                if (pdfBytes != null && pdfBytes.Length > 0)
                    return File(pdfBytes, "application/pdf", "generated.pdf");

                return BadRequest("The PDF could not be generated.");
            }
            catch (Exception e)
            {
                return StatusCode(500, $"Internal Error: {e.Message}");
            }
        }
    }
}