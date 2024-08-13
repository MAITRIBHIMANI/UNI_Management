using AspNetCoreHero.ToastNotification.Abstractions;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UNIManagement.Entities.DataModels;

namespace UNIManagement.Repositories.CommanHelper
{
    public class Helper
    {   
        public static string UploadFile(IFormFile UploadFile, int ID, string rootPath, string filename)
        {
            if (UploadFile != null)
            {
                string FilePath = "wwwroot\\Documents\\" + rootPath + "\\" + ID;
                string path = Path.Combine(Directory.GetCurrentDirectory(), FilePath);

                if (!Directory.Exists(path))
                {
                    Directory.CreateDirectory(path);
                }
             
                string newfilename = filename;
                string fileNameWithPath = Path.Combine(path, newfilename);
                string uploadPath = FilePath.Replace("wwwroot\\Documents\\" + rootPath + "\\" + ID , "/"+ rootPath + "/") + "/" + newfilename;

                using (FileStream stream = new FileStream(fileNameWithPath, FileMode.Create))
                {
                    UploadFile.CopyTo(stream);
                }

                return filename;
            }

            return null;
        }       
    }
}
