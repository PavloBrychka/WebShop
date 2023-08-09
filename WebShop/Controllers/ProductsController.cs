using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System.Drawing;
using WebShop.Data;
using WebShop.Data.Entities;
//using WebShop.Helpers;
using WebShop.Models;
using static System.Net.Mime.MediaTypeNames;

namespace WebShop.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        private readonly AppEFContext _appContext;
        private readonly IConfiguration _configuration;
        private readonly IMapper _mapper;
        public ProductsController(AppEFContext appEFContext, IConfiguration configuration, IMapper mapper)
        {
            _appContext = appEFContext;
            _configuration = configuration;
            _mapper = mapper;

        }

        [HttpGet("list")]
        public async Task<IActionResult> List()
        {
            var model = await _appContext.Products
                .Include(x => x.Category)
                .Include(x => x.ProductImages)
                .Select(x => new ProductGetViewModel
                {
                    Name = x.Name,
                    Description = x.Description,
                    Price = x.Price,
                    categoryId = x.CategoryId,
                    CategoryName = x.Category.Name,
                    Images =
                        x.ProductImages
                        .Select(x =>
                            new ProductImageItemViewModel { Id = x.Id, Name = x.Name })
                        .ToList(),
                })
                .ToListAsync();
            return Ok(model);
        }

        [HttpPost("AddProduct")]
        public async Task<IActionResult> AddProduct([FromForm] ProductCreateViewModel model)
        {
            if (model.Name != null)
            {
                var product = new ProductEntity
                {
                    Name = model.Name,
                    Description = model.Description,
                    Price = model.Price,
                    DateCreated = DateTime.UtcNow,
                    CategoryId = model.categoryId
                };
                _appContext.Add(product);
                _appContext.SaveChanges();
                foreach (var idImg in model.ImagesID)
                {
                    var image = await _appContext.ProductImages.SingleOrDefaultAsync(x => x.Id == idImg);
                    image.ProductId = product.Id;
                }
                _appContext.SaveChanges();
                return Ok(product);
            }
            return BadRequest(404);
        }

        [HttpPost("uploadImage")]
        public async Task<IActionResult> UploadImage([FromForm] ProdcutUploadImageViewModel model)
        {

            string imageName = string.Empty;
            if (model.Image != null)
            {
                var fileExp = Path.GetExtension(model.Image.FileName);
                var dirSave = Path.Combine(Directory.GetCurrentDirectory(), "images");
                imageName = Path.GetRandomFileName() + fileExp;
                using (var ms = new MemoryStream())
                {
                    await model.Image.CopyToAsync(ms);
                    var bmp = new Bitmap(System.Drawing.Image.FromStream(ms));
                    string[] sizes = ((string)_configuration.GetValue<string>("ImageSizes")).Split(" ");
                    foreach (var s in sizes)
                    {
                        int size = Convert.ToInt32(s);
                        var saveImage = ImageWorker.CompressImage(bmp, size, size, false);
                        saveImage.Save(Path.Combine(dirSave, s + "_" + imageName));
                    }
                }
                var entity = new ProductImageEntity();
                entity.Name = imageName;
                entity.DateCreated = DateTime.SpecifyKind(DateTime.Now, DateTimeKind.Utc);
                _appContext.ProductImages.Add(entity);
                _appContext.SaveChanges();
                return Ok(_mapper.Map<ProdcutImageItemViewModel>(entity));

            }
            return BadRequest();
        }

        [HttpDelete("RemoveImage/{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var image = await _appContext.ProductImages.SingleOrDefaultAsync(x => x.Id == id);
            if (image == null)
                return NotFound();

            var dirSave = Path.Combine("images");
            string[] sizes = ((string)_configuration.GetValue<string>("ImageSizes")).Split(" ");
            foreach (var s in sizes)
            {
                var imgDelete = Path.Combine(dirSave, s + "_" + image.Name);
                if (System.IO.File.Exists(imgDelete))
                {
                    System.IO.File.Delete(imgDelete);
                }
            }
            _appContext.ProductImages.Remove(image);
            await _appContext.SaveChangesAsync();
            return Ok();
        }

    }
}