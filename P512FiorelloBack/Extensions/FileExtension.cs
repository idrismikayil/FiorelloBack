using Microsoft.AspNetCore.Http;

namespace P512FiorelloBack.Extensions
{
    public static class FileExtension
    {
        public static bool IsSupported (this IFormFile file, string contentType)
        {
            return file.ContentType.Contains(contentType);
        }

        public static bool IsGreatergivenMb(this IFormFile file, int mb)
        {
            return file.Length > 1024 * 1024 * mb;
        }

        public static bool IsOkay(this IFormFile file)
        {
            return file.ContentType.Contains("image") && file.Length < 1024 * 1024;
        }
    }
}
