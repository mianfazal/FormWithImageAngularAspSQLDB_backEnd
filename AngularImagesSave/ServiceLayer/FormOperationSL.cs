using AngularImagesSave.Model;
using AngularImagesSave.ReposiotryLayer;

namespace AngularImagesSave.ServiceLayer
{
    public class FormOperationSL : IFormOperationSL
    {
        public readonly IFormOperationRL _formOperationRL;
        public FormOperationSL(IFormOperationRL formOperationRL)
        {
            _formOperationRL = formOperationRL;
        }

        public Task<CreateRecordResponse> CreateRecord(CreateRecordRequest request)
        {
            return _formOperationRL.CreateRecord(request);
        }
        public Task<GetRecordResponse> GetRecordByImageName(string imageName)
        {
            return _formOperationRL.GetRecordByImageName(imageName);
        }
    }
}
