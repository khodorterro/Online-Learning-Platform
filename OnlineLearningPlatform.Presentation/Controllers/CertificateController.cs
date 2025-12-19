using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using OnlineLearning.BusinessLayer.Helpers;
using OnlineLearning.BusinessLayer.Interfaces;

namespace OnlineLearningPlatform.Presentation.Controllers
{
    [Route("api/Certificate")]
    [ApiController]
    [Authorize(Roles = "Student")]
    public class CertificateController : ControllerBase
    {
        private readonly ICertificateService _service;

        public CertificateController(ICertificateService service)
        {
            _service = service;
        }

        [HttpPost("{courseId:int}")]
        public async Task<IActionResult> Generate(int courseId)
        {
            int userId = User.GetUserId();
            var certificate = await _service.GenerateAsync(userId, courseId);
            return Ok(certificate);
        }

        [HttpGet("me")]
        public async Task<IActionResult> GetMyCertificates()
        {
            int userId = User.GetUserId();
            return Ok(await _service.GetUserCertificatesAsync(userId));
        }
    }
}
