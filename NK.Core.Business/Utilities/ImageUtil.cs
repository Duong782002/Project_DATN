using Microsoft.AspNetCore.Http;

namespace NK.Core.Business.Utilities
{
    public class ImageUtil
    {
        public static async Task<byte[]> GetImageData(IFormFile image)
        {
            using (MemoryStream ms = new MemoryStream())
            {
                await image.CopyToAsync(ms);
                return ms.ToArray();
            }
        }
    }
}
