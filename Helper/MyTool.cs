using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Reflection;

namespace ECommerce.Project.Helper
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
            catch (Exception ex)
            {
                return string.Empty;
            }
        }

        public static List<T> ConvertDataTable<T>(DataTable dt)
        {
            List<T> data = new List<T>();
            foreach (DataRow row in dt.Rows)
            {
                T item = GetItem<T>(row);
                data.Add(item);
            }
            return data;
        }

        public static T GetItem<T>(DataRow dr)
        {
            Type temp = typeof(T);
            T obj = Activator.CreateInstance<T>();

            foreach (DataColumn column in dr.Table.Columns)
            {
                foreach (PropertyInfo pro in temp.GetProperties())
                {
                    if (pro.Name == column.ColumnName)
                        pro.SetValue(obj, dr[column.ColumnName], null);
                    else
                        continue;
                }
            }
            return obj;
        }
    }
}