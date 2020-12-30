using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Threading.Tasks;

namespace ShopBanHang.Models
{
     
    
    public class CT_WarrantyTime
    {
        public long ID { get; set; }       
        public string Code { get; set; }         
        public string Name { get; set; }
        public bool Active { get; set; }
        public DateTime? UpdateDate { get; set; }
        public DateTime? CreateDate { get; set; }
        public string CreateBy { get; set; }
        public string UpdateBy { get; set; }
    }
  
    public class CT_Material
    {
        public long ID { get; set; }
        public string Code { get; set; }
        public string Name { get; set; }
        public bool Active { get; set; }
        public DateTime? UpdateDate { get; set; }
        public DateTime? CreateDate { get; set; }
        public string CreateBy { get; set; }
        public string UpdateBy { get; set; }
    }
   
    public class CT_Color
    {
        public int ID { get; set; }     
        public string Code { get; set; }     
        public string Name { get; set; }
        public bool Active { get; set; }     
        public string CSS { get; set; }
        public DateTime? UpdateDate { get; set; }
        public DateTime? CreateDate { get; set; }
        public string CreateBy { get; set; }
        public string UpdateBy { get; set; }
    }
  
    public class CT_Size
    {
        public int ID { get; set; }
        public string Code { get; set; } 
        public string Name { get; set; }
        public DateTime? UpdateDate { get; set; }
        public DateTime? CreateDate { get; set; }
        public string CreateBy { get; set; }
        public string UpdateBy { get; set; }
    }

}
