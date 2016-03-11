using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace CaseMVC.Models
{
    [Table("condition")]
    public class condition
    {
        [Key]
        public int cc_id { get; set; }

        [Display(Name = "所屬Case")]
        public int c_id { get; set; }

        [Display(Name = "描述")]
        public string condition_description { get; set; }
    }
}