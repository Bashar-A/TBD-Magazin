using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel.DataAnnotations.Schema;

namespace TBD_Magazin.DbSets
{
    [Table("Клиент")]
    public class Client
    {
        public int id { get; set; }

        [Column("ФИО")]
        public string FullName { get; set; }

        [Column("Электронная_почта")]
        public string Email { get; set; }

        [Column("Телефон")]
        public string PhoneNumber { get; set; }

    }
}
