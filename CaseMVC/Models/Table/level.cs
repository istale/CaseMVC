using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace CaseMVC.Models
{
    [Table("level")]
    public class level
    {
        [Key]
        public int l_id { get; set; }

        public string l_name { get; set; }

        public int l_value { get; set; }
    }
}