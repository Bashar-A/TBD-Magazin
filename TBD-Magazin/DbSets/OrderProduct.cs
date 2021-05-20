using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel.DataAnnotations.Schema;

namespace TBD_Magazin.DbSets
{
    [Table("ЗаказТовар")]
    public class OrderProduct
    {
        [Column("Цена_за_шт")]
        public int Price { get; set; }

        [Column("Количество")]
        public int Quantity { get; set; }

        [Column("Заказ_id")]
        public int OrderId { get; set; }
        public Order Order { get; set; }

        [Column("Товар_id")]
        public int ProductId { get; set; }
        public Product Product { get; set; }


    }
}
