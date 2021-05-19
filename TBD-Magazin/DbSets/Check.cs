using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel.DataAnnotations.Schema;

namespace TBD_Magazin.DbSets
{
    [Table("Чек")]
    public class Check
    {
        public int id { get; set; }

        [Column("Дата")]
        public DateTime Date { get; set; }

        [Column("Сумма")]
        public int Sum { get; set; }

        [Column("Заказ_id")]
        public Order CheckOrder { get; set; }
    }
}
