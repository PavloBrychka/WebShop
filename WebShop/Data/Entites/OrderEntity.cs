using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using WebShop.Data.Entites;
using WebShop.Data.Entities;

namespace WebShop.Data.Entites
{
    [Table("Orders")]
    public class OrderEntity
    {
        [Key]
        public int id { get; set; }

        [ForeignKey("User")]
        public int UserId { get; set; }
        public string name { get; set; }
        public int phonenumber { get; set; }
        public int count { get; set; }
        public virtual ICollection<ProductEntity> product { get; set; }
        public DateTime? orderdate { get; set; }
        public DateTime? dateofarrivale { get; set; }

    }
}
