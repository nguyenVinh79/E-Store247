using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace ShopBanHang.Helper
{
    public class MyTool
    {
        public static string UploadImage(IFormFile myFile, params string[] path)
        {
            //upload to folder /wwwroot/Hinh/HangHoa
            //MyTool.UploadImage(file, "wwwroot","folder1","subfolder");
            //params is dynamic type, no need to define a new instance
            try
            {
                var fullPath = Directory.GetCurrentDirectory();
                foreach (var item in path)
                {
                    fullPath = Path.Combine(fullPath, item);
                }
                fullPath = Path.Combine(fullPath, myFile.FileName);
                using (var newFile = new FileStream(fullPath, FileMode.Create))
                {
                    myFile.CopyTo(newFile);
                }
                return myFile.FileName;
            }
            catch(Exception ex)
            {
                return string.Empty;
            }
        }
    }
}
