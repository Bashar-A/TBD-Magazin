using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel.DataAnnotations.Schema;

namespace TBD_Magazin.DbSets
{
    [Table("Производитель")]
    public class Manufacturer
    {
        public int id { get; set; }

        [Column("Наименование")]
        public string Name { get; set; }
    }
}
