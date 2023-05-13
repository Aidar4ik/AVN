﻿using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace AVN.Model.Entities;

public class Student : BaseEntity
{
    [Required]
    [DisplayName("ФИО")]
    [MaxLength(100)]
    public string FullName { get; set; }

    [Required]
    [DisplayName("Статус")]
    [MaxLength(500)]
    public string Status { get; set; }

    [Required]
    [DisplayName("Дата рождения")]
    [MaxLength(100)]
    public DateTime DateOfBirth { get; set; }

    [Required]
    [DisplayName("Форма обучения")]
    [MaxLength(100)]
    public string StudingForm { get; set; }

    [Required]
    [DisplayName("Линия обучения")]
    [MaxLength(100)]
    public string EducationalLine { get; set; }

    [Required]
    [DisplayName("Номер зачетной книжки")]
    [MaxLength(100)]
    public string GradeBookNumber { get; set; }

    [Required]
    [DisplayName("Пол")]
    [MaxLength(20)]
    public string Gender { get; set; }

    [Required]
    [DisplayName("Гражданство")]
    [MaxLength(100)]
    public string Citizenship { get; set; }

    [Required]
    [DisplayName("Адрес")]
    [MaxLength(300)]
    public string Address { get; set; }

    [Required]
    [DisplayName("Номер телефона")]
    [MaxLength(30)]
    public string PhoneNumber { get; set; }

    [Required]
    [DisplayName("Приказы")]
    [MaxLength(1000)]
    public string Orders { get; set; }
    
    public int GroupId { get; set; }
    public Group Group { get; set; }

}