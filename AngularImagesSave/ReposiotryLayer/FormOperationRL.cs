using AngularImagesSave.Model;
using AngularImagesSave.ServiceLayer;
using System.Data;
using System.Data.SqlClient;
using System.Net.Mime;

namespace AngularImagesSave.ReposiotryLayer
{
    public class FormOperationRL : IFormOperationRL
    {
        public readonly IConfiguration _configuration;
        public readonly SqlConnection _sqlConnection;

        public FormOperationRL(IConfiguration configuration)
        {
            _configuration = configuration;
            _sqlConnection = new SqlConnection(_configuration.GetValue<string>("ConnectionStrings:DbSettingConnectioSQL"));
        }

        public async Task<CreateRecordResponse> CreateRecord(CreateRecordRequest request)
        {
            CreateRecordResponse response = new CreateRecordResponse();
            try
            {
                
                SqlCommand cmd = new SqlCommand("saveImageData", _sqlConnection);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@Image_Name", request.ImageName);
                cmd.Parameters.AddWithValue("@Image_Path", request.ImagePath);
                cmd.Parameters.Add("@Response", SqlDbType.VarChar, 50);
                cmd.Parameters["@Response"].Direction = ParameterDirection.Output;
             

                _sqlConnection.Open();
                
                await cmd.ExecuteNonQueryAsync();

                    string message = (string)cmd.Parameters["@Response"].Value;
                    if (!String.IsNullOrEmpty(message)){
                        response.IsSuccess = true;
                        response.Message = request.ImageName;
                    }
                    else
                    {
                        response.IsSuccess = false;
                        response.Message = "No output from procedure after execute";
                    }
                  
                

            }
            catch (Exception ex)
            {
                response.IsSuccess = false;
                response.Message = ex.Message;
            }
            finally
            {
                _sqlConnection.Close();
            }
            return response;
        }

        public async Task<GetRecordResponse> GetRecordByImageName(string imageName)
        {
            GetRecordResponse response = new GetRecordResponse();
            try
            {

                SqlCommand cmd = new SqlCommand("retrieveImage", _sqlConnection);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@Image_Name", imageName);
             
                cmd.Parameters.Add("@Response", SqlDbType.VarChar, 255);
                cmd.Parameters["@Response"].Direction = ParameterDirection.Output;


                _sqlConnection.Open();
                int status = await cmd.ExecuteNonQueryAsync();

                    string message = (string)cmd.Parameters["@Response"].Value;
                    if (!String.IsNullOrEmpty(message))
                    {
                        response.IsSuccess = true;
                        response.Path = message;
                    }
                    else
                    {
                        response.IsSuccess = false;
                        response.Path = "No output from procedure after execute";
                    }

            }
            catch (Exception ex)
            {
                response.IsSuccess = false;
                response.Path = ex.Message;
            }
            finally
            {
                _sqlConnection.Close();
            }
            return response;
        }
    }
}
