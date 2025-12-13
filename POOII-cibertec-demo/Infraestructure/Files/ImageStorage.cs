namespace POOII_cibertec_demo.Infrastructure.Files
{
    public class ImageStorage
    {
        private readonly IWebHostEnvironment _env;

        public ImageStorage(IWebHostEnvironment env)
        {
            _env = env;
        }

        public async Task<string> SaveAsync(IFormFile image)
        {
            if (image == null || image.Length == 0)
                return null;

            var uploads = Path.Combine(_env.WebRootPath, "images");
            if (!Directory.Exists(uploads))
                Directory.CreateDirectory(uploads);

            var fileName = Guid.NewGuid() + Path.GetExtension(image.FileName);
            var filePath = Path.Combine(uploads, fileName);

            using var stream = new FileStream(filePath, FileMode.Create);
            await image.CopyToAsync(stream);

            return fileName;
        }
    }
}
