using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel.DataAnnotations.Schema;

namespace TBD_Magazin.DbSets
{
    [Table("ПоставкаТовар")]
    public class SupplyProduct
    {
        [Column("Цена_за_шт")]
        public int Price { get; set; }

        [Column("Количество")]
        public int Quantity { get; set; }

        [Column("Поставка_id")]
        public Supply SupplyID { get; set; }

        [Column("Товар_id")]
        public Product ProductID { get; set; }


    }
}
