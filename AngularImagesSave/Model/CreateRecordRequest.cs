using System.ComponentModel.DataAnnotations;

namespace AngularImagesSave.Model
{
    public class CreateRecordRequest
    {
            public string ImageName { get; set; }
            public string? ImagePath { get; set; }
        public List<IFormFile> ProfilePicture { get; set; }
    }

    public class CreateRecordResponse
    {
        public bool IsSuccess { get; set; }
        public string Message { get; set; }


    }
}
