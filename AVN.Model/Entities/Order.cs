﻿using AVN.Common.Enums;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace AVN.Model.Entities
{
    public class Order : BaseEntity
    {
        [Required]
        [DisplayName("Вид приказа")]
        public OrderType OrderType { get; set; }

        [Required]
        [DisplayName("Дата")]
        public DateTime Date { get; set; }

        [Required]
        [DisplayName("Номер приказа")]
        public string Number { get; set; }

        [Required]
        [DisplayName("Примечание")]
        public string? Note { get; set; }

        public int StudentId { get; set; }
        public Student Student { get; set; }

    }
}
