using ImageMagick;

namespace ImageConversionService
{
    public interface IImageConverter
    {
        /// <summary>
        /// Converts the image in the memory stream to a JPEG file.
        /// </summary>
        /// <param name="memoryStream">MemoryStream</param>
        /// <returns>Byte Array</returns>
        Task<byte[]> ConvertImageToJpeg(MemoryStream memoryStream);
    }


    public class ImageConverter : IImageConverter
    {
        public async Task<byte[]> ConvertImageToJpeg(MemoryStream memoryStream)
        {
            memoryStream.Position = 0;
            using var image = new MagickImage(memoryStream);
            
            // Sets the output format to jpeg
            image.Format = MagickFormat.Jpeg;

            // Create byte array that contains a jpeg file
            byte[] imageBytes = image.ToByteArray();

            // Save as jpeg to server location. This is optional and can be omitted.
            //await image.WriteAsync(@$"C:\path\to\folder\{DateTime.Now:yyyy-MMM-dd_HH-mm-ss-fff}.jpeg");

            return imageBytes;
        }
    }
}