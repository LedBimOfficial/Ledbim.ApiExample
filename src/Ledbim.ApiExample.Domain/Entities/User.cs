using Ledbim.Core.Base.Entities;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Ledbim.ApiExample.Domain.Entities
{
    public class User : BaseEntity
    {
        [Column(Order = 1), Required, StringLength(64)]
        public string FullName { get; set; } = null!;

        [Column(Order = 2), Required, StringLength(64)]
        public string Mail { get; set; } = null!;

        [Column(Order = 3), Required, StringLength(64)]
        public string Password { get; set; } = null!;
    }
}
