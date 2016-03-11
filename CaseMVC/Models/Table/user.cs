using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace CaseMVC.Models
{
    [Table("user")]
    public class user
    {
        [Key]
        public int u_id { get; set; }

        [Required]
        [Display(Name ="帳號")]
        public string acc { get; set; }

        [Required]
        [Display(Name = "密碼")]
        public string pwd { get; set; }

        [Required]
        [Display(Name = "等級")]
        public int level { get; set; }
    }
}