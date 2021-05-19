using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel.DataAnnotations.Schema;

namespace TBD_Magazin.DbSets
{
    [Table("Товар")]
    public class Product
    {
        public int id { get; set; }

        [Column("Наименование")]
        public string Name { get; set; }

        [Column("Мощность")]
        public int Power { get; set; }

        [Column("Срок_гарантии")]
        public int Garanty { get; set; }

        [Column("Цена")]
        public int Price { get; set; }

        [Column("Количество_на_складе")]
        public int Quantity { get; set; }

        [Column("Производитель_id")]
        public Manufacturer ProductManufacturer { get; set; }

        [Column("Категория_id")]
        public Category ProductCategory { get; set; }


    }
}
