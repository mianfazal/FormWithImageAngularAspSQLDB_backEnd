using AngularImagesSave.Model;

namespace AngularImagesSave.ReposiotryLayer
{
    public interface IFormOperationRL
    {
        public Task<CreateRecordResponse> CreateRecord(CreateRecordRequest request);
        public  Task<GetRecordResponse> GetRecordByImageName(string imageName);
    }
}
