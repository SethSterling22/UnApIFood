using Microsoft.AspNetCore.Mvc;
using UnApIFood.Attributes;

namespace UnApIFood.Controllers
{
    [Route("API/v1/[controller]")]
    [ApiKey]
    [ApiController]
    public class BaseController : ControllerBase
    {

    }
}
