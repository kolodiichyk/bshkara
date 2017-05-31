using System;
using System.Configuration;
using System.IO;
using System.Web.Mvc;
using Bshkara.Web.Helpers;

namespace Bshkara.Web.Controllers
{
    [Authorize]
    public class FileController : Controller
    {
        #region Actions

        [HttpPost]
        public ActionResult UploadFile()
        {
            var myFile = Request.Files[0];
            var isUploaded = false;
            var error = string.Empty;

            var tempFolderName = ConfigurationManager.AppSettings["ImageTempFolderName"];

            if (myFile != null && myFile.ContentLength != 0)
            {
                var tempFolderPath = Server.MapPath("~/" + tempFolderName);

                if (FileHelper.CreateFolderIfNeeded(tempFolderPath))
                {
                    try
                    {
                        myFile.SaveAs(Path.Combine(tempFolderPath, myFile.FileName));
                        isUploaded = true;
                    }
                    catch (Exception ex)
                    {
                        error = ex.Message;
                        /*TODO: You must process this exception.*/
                    }
                }
            }

            var filePath = string.Concat("/", tempFolderName, "/", myFile.FileName);
            return Json(new {isUploaded, filePath, error}, "text/html");
        }

        #endregion
    }
}