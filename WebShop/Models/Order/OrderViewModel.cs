using WebShop.Data.Entities;

namespace WebShop.Models.Order
{
    public class OrderViewModel
    {
        public int id { get; set; }
        public int UserId { get; set; }
        public string name { get; set; }
        public int number { get; set; }
        public int count { get; set; }
        public virtual ICollection<ProductEntity> product { get; set; }
        public DateTime? orderdate { get; set; }
        public DateTime? dateofarrivale { get; set; }
   

    }

    public class OrderCreateViewModel
    {
       
        public int UserId { get; set; }
        public string name { get; set; }
        public int number { get; set; }
        public int count { get; set; }
        public virtual ICollection<ProductEntity> product { get; set; }
        public DateTime? orderdate { get; set; }
        public DateTime? dateofarrivale { get; set; }


    }
}
