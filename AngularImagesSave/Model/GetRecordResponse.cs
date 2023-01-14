namespace AngularImagesSave.Model
{
    public class GetRecordResponse
    {
        public bool IsSuccess { get; set; }
        public string Path { get; set; }
        public FileStream ProfilePicture { get; set; }
    }
}
