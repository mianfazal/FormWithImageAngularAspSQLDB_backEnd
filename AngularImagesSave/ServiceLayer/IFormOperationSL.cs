using AngularImagesSave.Model;

namespace AngularImagesSave.ServiceLayer
{
    public interface IFormOperationSL
    {
        public Task<CreateRecordResponse> CreateRecord(CreateRecordRequest request);
        public Task<GetRecordResponse> GetRecordByImageName(string imageName);
    }
}
