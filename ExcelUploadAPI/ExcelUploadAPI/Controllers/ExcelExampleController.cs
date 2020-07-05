using ExcelDataReader;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Http;
using ExcelUploadAPI.Models;

namespace ExcelUploadAPI.Controllers
{
    [RoutePrefix("Api/Excel")]
    public class ExcelExampleController : ApiController
    {
        [Route("UploadExcel")]
        [HttpPost]
        public string ExcelUpload()
        {
            string message = "";
            HttpResponseMessage result = null;
            var httpRequest = HttpContext.Current.Request;
            using (AngularDBEntities objEntity = new AngularDBEntities())
            {

                if (httpRequest.Files.Count > 0)
                {
                    HttpPostedFile file = httpRequest.Files[0];
                    Stream stream = file.InputStream;

                    IExcelDataReader reader = null;

                    if (file.FileName.EndsWith(".xls"))
                    {
                        reader = ExcelReaderFactory.CreateBinaryReader(stream);
                    }
                    else if (file.FileName.EndsWith(".xlsx"))
                    {
                        reader = ExcelReaderFactory.CreateOpenXmlReader(stream);
                    }
                    else
                    {
                        message = "This file format is not supported";
                    }

                    DataSet excelRecords = reader.AsDataSet();
                    reader.Close();

                    var finalRecords = excelRecords.Tables[0];
                    for (int i = 0; i < finalRecords.Rows.Count; i++)
                    {
                        UserDetail objUser = new UserDetail();
                        objUser.UserName = finalRecords.Rows[i][0].ToString();
                        objUser.EmailId = finalRecords.Rows[i][1].ToString();
                        objUser.Gender = finalRecords.Rows[i][2].ToString();
                        objUser.Address = finalRecords.Rows[i][3].ToString();
                        objUser.MobileNo = finalRecords.Rows[i][4].ToString();
                        objUser.PinCode = finalRecords.Rows[i][5].ToString();

                        objEntity.UserDetails.Add(objUser);

                    }

                    int output = objEntity.SaveChanges();
                    if (output > 0)
                    {
                        message = "Excel file has been successfully uploaded";
                    }
                    else
                    {
                        message = "Excel file uploaded has fiald";
                    }

                }

                else
                {
                    result = Request.CreateResponse(HttpStatusCode.BadRequest);
                }
            }
            return message;
        }

        [Route("UserDetails")]
        [HttpGet]
        public List<UserDetail> BindUser()
        {
            List<UserDetail> lstUser = new List<UserDetail>();
            using (AngularDBEntities objEntity = new AngularDBEntities())
            {
                lstUser = objEntity.UserDetails.ToList();
            }
            return lstUser;
        }
    }
}
