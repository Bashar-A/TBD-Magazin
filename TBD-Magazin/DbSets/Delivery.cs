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
        public DateTime Date { get; set; }

        [Column("Заказ_id")]
        public Order DeliveryOrder { get; set; }

        [Column("Курьер_id")]
        public Worker DeliveryCourier { get; set; }

        [Column("Менеджер_id")]
        public Worker DeliveryManager { get; set; }
    }
}
