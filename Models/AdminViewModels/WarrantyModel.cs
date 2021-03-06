﻿using System;
using System.ComponentModel.DataAnnotations;

namespace ECommerce.Project.Areas.Admin.Models
{
    public class WarrantyModel
    {
        public long ID { get; set; }

        [Required]
        [MaxLength(5)]
        public string Code { get; set; }

        [MaxLength(20)]
        public string Name { get; set; }

        public bool Active { get; set; }
        public DateTime? UpdateDate { get; set; }
        public DateTime? CreateDate { get; set; }
        public string CreateBy { get; set; }
        public string UpdateBy { get; set; }
    }
}