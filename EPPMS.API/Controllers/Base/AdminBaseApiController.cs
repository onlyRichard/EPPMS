using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace EPPMS.API.Controllers.Base
{
    [ApiController]
    [Route("api/v1/admin/[controller]")]
    public class AdminBaseApiController : ControllerBase
    {
    }
}
