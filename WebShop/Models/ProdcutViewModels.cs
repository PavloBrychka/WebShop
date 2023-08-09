

namespace WebShop.Models
{
    /// <summary>
    /// Загрузка фото на сервер
    /// </summary>
    public class ProdcutUploadImageViewModel
    {
        /// <summary>
        /// Передача фото у вигляді файлу
        /// </summary>
        public IFormFile Image { get; set; }
    }

    /// <summary>
    /// Вертамає інформацію про 1 фото товару
    /// </summary>
    public class ProdcutImageItemViewModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
    }

    public class ProductCreateViewModel
    {
        public string Name { get; set; }
        public int Price { get; set; }
        public List<int> ImagesID { get; set; }
        public string Description { get; set; }
        public int categoryId { get; set; }
    }

    public class ProductImageItemViewModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
    }

    public class ProductGetViewModel
    {
        public string Name { get; set; }
        public decimal Price { get; set; }
        public List<ProductImageItemViewModel> Images { get; set; }
        public string Description { get; set; }
        public string CategoryName { get; set; }
        public int categoryId { get; set; }
    }


}