using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel.DataAnnotations.Schema;

namespace TBD_Magazin.DbSets
{
    [Table("Доставка")]
    public class Delivery
    {
        public int id { get; set; }

        [Column("Адрес")]
        public string Address { get; set; }

        [Column("Дата")]
        public DateTime? Date { get; set; }

        [Column("Доставлено")]
        public bool Deliveried { get; set; }

        [Column("Заказ_id")]
        public int OrderId { get; set; }
        public Order Order { get; set; }

        [Column("Курьер_id")]
        public int? CourierId { get; set; }
        public Worker Courier { get; set; }

        [Column("Менеджер_id")]
        public int? ManagerId { get; set; }
        public Worker Manager { get; set; }
    }
}
