﻿using Model.Entities.Base;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;
using Microsoft.EntityFrameworkCore;


namespace Model.Entities
{
    [Table("Inventory", Schema = "dbo")]
    [Index(nameof(MakeId), Name = "IX_Inventory_MakeId")]
    public partial class Car : BaseEntity
    {
        private bool? _isDrivable;

        [DisplayName("Is Drivable")]
        public bool IsDrivable
        {
            get => _isDrivable ?? false;
            set => _isDrivable = value;
        }

        [Required]
        [DisplayName("Make")]
        public int MakeId { get; set; }

        [NotMapped]
        public string MakeName => MakeNavigation?.Name ?? "Unknown";

        [ForeignKey(nameof(MakeId))]
        [InverseProperty(nameof(Make.Cars))]
        public Make? MakeNavigation { get; set; }

        [Required]
        [StringLength(50)]
        public string Color { get; set; } = "Gold";

        [Required]
        [StringLength(50)]
        [DisplayName("Pet Name")]
        public string PetName { get; set; } = "My Precious";

        [JsonIgnore]
        [InverseProperty(nameof(Order.CarNavigation))]
        public IEnumerable<Order> Orders { get; set; } = new List<Order>();

        public override string ToString() => $"{PetName ?? "**No Name**"} is {Color} {MakeNavigation?.Name} with ID {Id}";
    }
}
