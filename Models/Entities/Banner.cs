using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ECommerce.Project.Models
{
    
    public class Banner
    {
       
     
        public long ID { get; set; }
        
        public string ImagePath { get; set; }     
        
        public int Type { get; set; }
        
        public bool? IsShow { get; set; }
        
        public string Title { get; set; }

        public string Description { get; set; }

        public string LinkTo { get; set; }
    
        public string Position { get; set; }
        
        public int? OrderBy { get; set; }

        public DateTime? UpdateDate { get; set; }
        public DateTime? CreateDate { get; set; }
        public string CreateBy { get; set; }
        public string UpdateBy { get; set; }


    }

}
