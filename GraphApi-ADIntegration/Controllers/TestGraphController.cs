using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace GraphApi_ADIntegration.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TestGraphController : ControllerBase
    {
        private readonly IGraphADHelper graphADHelper;
        public TestGraphController(IGraphADHelper graphADHelper)
        {
            this.graphADHelper = graphADHelper;
        }

        [Route("ListUsers")]
        public async Task<IActionResult> GetUsers() {
            return Ok(await graphADHelper.ListUsersAsync());
        }
    }
}
