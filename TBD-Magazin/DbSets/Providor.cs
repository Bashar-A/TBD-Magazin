using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel.DataAnnotations.Schema;

namespace TBD_Magazin.DbSets
{
    [Table("Поставщик")]
    public class Providor
    {
        public int id { get; set; }

        [Column("Наименование")]
        public string Name { get; set; }
    }
}
