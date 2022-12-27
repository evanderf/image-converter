using System;
using System.Collections.Generic;

namespace Imagination.Helpers
{
    public static class FileFormatHelper
    {
        /// <summary>
        /// Checks if the content type matches with the whitelisted values.
        /// </summary>
        /// <param name="contentType"></param>
        /// <returns>true if content type matches</returns>
        public static bool IsValidFileType(string contentType)
        {
            List<string> whitelistedContentTypes = new List<string>
            {
                "image/bmp", "image/gif", "image/jpeg", "image/png", "image/tiff", "image/webp"
            };

            return whitelistedContentTypes.Exists(w => w.Equals(contentType, StringComparison.OrdinalIgnoreCase));

        }
    }
}
