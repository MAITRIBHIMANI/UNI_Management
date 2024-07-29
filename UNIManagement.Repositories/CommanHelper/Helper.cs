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
        public static string Documents(IFormFile UploadFile, int EmployeeId, string rootPath, string filename)
        {

            if (UploadFile != null)
            {
                string FilePath = "wwwroot\\Documents\\" + rootPath + "\\" + EmployeeId;
                string path = Path.Combine(Directory.GetCurrentDirectory(), FilePath);

                if (!Directory.Exists(path))
                {
                    Directory.CreateDirectory(path);
                }
                //Directory.CreateDirectory(path);

                string newfilename = filename;
                string fileNameWithPath = Path.Combine(path, newfilename);
                string uploadPath = FilePath.Replace("wwwroot\\Documents\\" + rootPath + "\\" + EmployeeId, "/Employee") + "/" + newfilename;

                using (FileStream stream = new FileStream(fileNameWithPath, FileMode.Create))
                {
                    UploadFile.CopyTo(stream);
                }


                return uploadPath;
            }

            return null;
        }
        
        public static string Files(IFormFile UploadFile, int ProjectId, string rootPath, string filename)
        {

            if (UploadFile != null)
            {
                string FilePath = "wwwroot\\Documents\\" + rootPath + "\\" + ProjectId;
                string path = Path.Combine(Directory.GetCurrentDirectory(), FilePath);

                if (!Directory.Exists(path))
                {
                    Directory.CreateDirectory(path);
                }
                //Directory.CreateDirectory(path);

                string newfilename = filename;
                string fileNameWithPath = Path.Combine(path, newfilename);
                string uploadPath = FilePath.Replace("wwwroot\\Documents\\" + rootPath + "\\" + ProjectId, "/Project/") + "/" + newfilename;

                using (FileStream stream = new FileStream(fileNameWithPath, FileMode.Create))
                {
                    UploadFile.CopyTo(stream);
                }


                return uploadPath;
            }

            return null;
        }

        public static string Doc(IFormFile UploadFile, int NotificationId, string rootPath, string filename)
        {

            if (UploadFile != null)
            {
                string FilePath = "wwwroot\\Documents\\" + rootPath + "\\" + NotificationId;
                string path = Path.Combine(Directory.GetCurrentDirectory(), FilePath);

                if (!Directory.Exists(path))
                {
                    Directory.CreateDirectory(path);
                }
                //Directory.CreateDirectory(path);

                string newfilename = filename;
                string fileNameWithPath = Path.Combine(path, newfilename);
                string uploadPath = FilePath.Replace("wwwroot\\Documents\\" + rootPath + "\\" + NotificationId, "/Notificaation/") + "/" + newfilename;

                using (FileStream stream = new FileStream(fileNameWithPath, FileMode.Create))
                {
                    UploadFile.CopyTo(stream);
                }


                return uploadPath;
            }

            return null;
        }

        public static string UploadClientAdditionalInfo(IFormFile UploadFile, int ClientId, string rootPath, string filename)
        {

            if (UploadFile != null)
            {
                string FilePath = "wwwroot\\Client\\AdditionalInformation\\" + ClientId;
                string path = Path.Combine(Directory.GetCurrentDirectory(), FilePath);

                if (!Directory.Exists(path))
                {
                    Directory.CreateDirectory(path);
                }
                //Directory.CreateDirectory(path);

                string newfilename = filename;
                string fileNameWithPath = Path.Combine(path, newfilename);
                string uploadPath = FilePath.Replace("wwwroot\\Client\\" + rootPath + "\\" + ClientId ,"/Client/") + "/" + newfilename;

                using (FileStream stream = new FileStream(fileNameWithPath, FileMode.Create))
                {
                    UploadFile.CopyTo(stream);
                }


                return uploadPath;
            }

            return null;
        }

        public static string TaskDocument(IFormFile UploadFile, int TaskId, string rootPath, string filename)
        {

            if (UploadFile != null)
            {
                string FilePath = "wwwroot\\Task\\Documents\\" + TaskId;
                string path = Path.Combine(Directory.GetCurrentDirectory(), FilePath);

                if (!Directory.Exists(path))
                {
                    Directory.CreateDirectory(path);
                }
                //Directory.CreateDirectory(path);

                string newfilename = filename;
                string fileNameWithPath = Path.Combine(path, newfilename);
                string uploadPath = FilePath.Replace("wwwroot\\Task\\" + rootPath + "\\" + TaskId ,"/Task/") + "/" + newfilename;

                using (FileStream stream = new FileStream(fileNameWithPath, FileMode.Create))
                {
                    UploadFile.CopyTo(stream);
                }


                return uploadPath;
            }

            return null;
        }


    }
}
