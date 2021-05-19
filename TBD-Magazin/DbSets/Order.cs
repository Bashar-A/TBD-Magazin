using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel.DataAnnotations.Schema;

namespace TBD_Magazin.DbSets
{
    [Table("Заказ")]
    public class Order
    {
        public int id { get; set; }

        [Column("Дата")]
        public DateTime Date { get; set; }

        [Column("Стоимость_заказа")]
        public int OrderSum { get; set; }

        [Column("Клиент_id")]
        public Client OrderClient { get; set; }

        [Column("Продавец_id")]
        public Worker OrderSeller { get; set; }
    }
}
