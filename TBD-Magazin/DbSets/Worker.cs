using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel.DataAnnotations.Schema;

namespace TBD_Magazin.DbSets
{
    [Table("Сотрудник")]
    public class Worker
    {
        public int id { get; set; }

        [Column("ФИО")]
        public string FullName { get; set; }

        [Column("Дата_рождения")]
        public DateTime DateOfBirth { get; set; }

        [Column("Телефон")]
        public string PhoneNumber { get; set; }

        [Column("Адрес")]
        public string Address { get; set; }

        [Column("Паспортные_данные")]
        public string Passport { get; set; }

        [Column("Должность_id")]
        public int RoleId { get; set; }
        public Role Role { get; set; }


    }
}
