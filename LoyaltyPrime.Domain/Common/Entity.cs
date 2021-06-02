using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace LoyaltyPrime.Domain.Common
{
    public class Entity
    {
        [Key]
        [DatabaseGeneratedAttribute(DatabaseGeneratedOption.Identity)]
        [Column(Order = 1)]
        public int Id { get; protected set; }

        [Column(Order = 100)]
        public DateTime CreatedDateTime { get; set; }

        [Column(Order = 101)]
        public DateTime UpdatedDateTime { get; set; }
    }
}
