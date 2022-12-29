using ImageConversionService;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.IO;
using System.Net.Http;
using System.Net;
using System.Threading.Tasks;
using System.Net.Http.Headers;
using Imagination.Helpers;
using Imagination.Filters;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Imagination.Controllers
{
    [Route("api/image-converter")]
    [ApiController]
    public class ImageConvertController : ControllerBase
    {
        public readonly IImageConverter _imageConverter;

        public ImageConvertController(IImageConverter imageConverter)
        {
            _imageConverter = imageConverter ?? throw new ArgumentNullException(nameof(imageConverter));
        }

        /// <summary>
        /// Converts the uploaded image to a JPEG image file.
        /// </summary>
        /// <param name="imageToConvert"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("convert")]
        [TypeFilter(typeof(CustomExceptionFilter))]
        [RequestFormLimits(MultipartBodyLengthLimit = 26214400)] // Max 25 MB file size
        [ProducesResponseType(typeof(FileContentResult), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> ConvertImage(IFormFile imageToConvert)
        {
            if(imageToConvert is null)
            {
                return BadRequest("Please upload the image that you want to convert");
            }
            else if (!FileFormatHelper.IsValidFileType(imageToConvert.ContentType))
            {
                return BadRequest("Invalid file. We accept common web image formats like .png, .bmp, .jpeg, etc.");
            }
            else if (imageToConvert.Length <= 1000)
            {
                return BadRequest("Image file size is too small. Please upload images that are between 100 KB to 25 MB");
            }

            using MemoryStream memoryStream = new MemoryStream();
            imageToConvert.CopyTo(memoryStream);
            byte[] fileBytes = await _imageConverter.ConvertImageToJpeg(memoryStream);
            
            return File(fileBytes, "image/jpeg");
        }
    }
}
