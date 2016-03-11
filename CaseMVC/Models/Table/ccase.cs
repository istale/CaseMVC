using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace CaseMVC.Models
{
    [Table("case")]
    public class ccase
    {
        [Key]
        [Display(Name ="編號")]
        public int c_id { get; set; }

        [Display(Name = "描述")]
        public string description { get; set; }

        [Display(Name = "等級")]
        public int l_value { get; set; }

        [Display(Name = "部門")]
        public string d_id { get; set; }  // m:生產, l:配銷, f:財務

        [Display(Name = "功能")]
        public int f_id { get; set; }
    }
}