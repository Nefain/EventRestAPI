namespace MyFeature.Feature.MyFeature
{
    public class ImageService
    {
        public async Task<bool> ImageExistsAsync(Guid idImage ,string imageName)
        {
            // Проверяем, существует ли файл с указанным именем в папке с изображениями
            string imagePath = Path.Combine("Images", imageName);
            bool exists = await Task.Run(() => File.Exists(imagePath));
            return exists;
        }
    }
}
