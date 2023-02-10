using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using System;
using Microsoft.AspNetCore.Hosting;

namespace BlazorAppEcom.Server.Controllers
{
        [ApiController]
        [Route("api/[controller]")]
        public class UploadController : ControllerBase
        {
            private readonly IWebHostEnvironment env;

            public UploadController(IWebHostEnvironment env)
            {
                this.env = env;
            }

            [HttpPost]
            public async Task Post([FromBody] ImageFile[] files)
            {
                foreach (var file in files)
                {
                    var buf = Convert.FromBase64String(file.base64data);
                    var name = Guid.NewGuid().ToString("N") + "-" + file.fileName;
					await System.IO.File.WriteAllBytesAsync(env.ContentRootPath + System.IO.Path.DirectorySeparatorChar +"/image/"+ name, buf);
                }
            }
        }
    
}
