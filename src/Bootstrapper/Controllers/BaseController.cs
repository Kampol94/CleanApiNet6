using Microsoft.AspNetCore.Mvc;

namespace CleanApi.Bootstrapper.Controllers
{
    [ApiController]
    [Route("api/v{version:apiVersion}/[controller]")]
    public class BaseController : ControllerBase
    {
    }
}