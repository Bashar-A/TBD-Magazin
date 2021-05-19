using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel.DataAnnotations.Schema;

namespace TBD_Magazin.DbSets
{
    [Table("Поставка")]
    public class Supply
    {
        public int id { get; set; }

        [Column("Дата")]
        public DateTime Date { get; set; }

        [Column("Менеджер_id")]
        public Worker SupplyManager { get; set; }

        [Column("Поставщик_id")]
        public Providor SupplyProvider { get; set; }
    }
}
