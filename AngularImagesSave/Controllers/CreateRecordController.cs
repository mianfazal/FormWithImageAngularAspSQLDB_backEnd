using AngularImagesSave.Model;
using AngularImagesSave.ServiceLayer;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Net;
using System.Reflection;
using System.Xml.Linq;
using static System.Net.Mime.MediaTypeNames;

namespace AngularImagesSave.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CreateRecordController : ControllerBase
    {
        public readonly IFormOperationSL _formOperationSL;
        private readonly IWebHostEnvironment _environment;

        public CreateRecordController(IFormOperationSL formOperationSL, IWebHostEnvironment environment)
        {
            _formOperationSL = formOperationSL;
            _environment = environment;
        }

        [HttpPost]
        [Route("createRecord")]
        public async Task<IActionResult> CreateRecord([FromForm] CreateRecordRequest request)
        {
            CreateRecordResponse response = new CreateRecordResponse();
            try
            {
                foreach (var item in request.ProfilePicture)
                {

                    if (item.FileName == null || item.FileName.Length == 0)
                    {
                        return Content("File not selected");
                    }
      
                    var path = Path.Combine(_environment.ContentRootPath, "Images/", item.FileName);

                    using (FileStream stream = new FileStream(path, FileMode.Create))
                    {
                        await item.CopyToAsync(stream);
                        stream.Close();
                    }
                    var newRequest = new CreateRecordRequest
                    {
                        ImagePath = path,
                        ImageName = request.ImageName                     

                    };
                    response = await _formOperationSL.CreateRecord(newRequest);
                }

               
            }
            catch (Exception ex)
            {
                response.IsSuccess = false;
                response.Message = ex.Message;
            }
            return Ok(response);
        }

        [HttpGet]
        [Route("getRecordByImageName/{imageName}")]
        public async Task<IActionResult> GetRecordByImageName([FromRoute] string imageName = null )
        {
            GetRecordResponse response = new GetRecordResponse();
          
            try
            {
                response = await _formOperationSL.GetRecordByImageName(imageName);

                //response.ProfilePicture = System.IO.File.OpenRead(response.Path);
                Byte[] b = System.IO.File.ReadAllBytes(response.Path);   // You can use your own method over here.         
                string base64String = Convert.ToBase64String(b);
                response.Path = base64String;
                

                //   response.ProfilePicture = System.IO.File.OpenRead(response.Path);

            }
            catch (Exception ex)
            {
                response.IsSuccess = false;
                response.Path = ex.Message;
            }
            return Ok(response);


        }
    }
}
