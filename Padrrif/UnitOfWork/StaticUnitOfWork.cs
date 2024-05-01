namespace Padrrif;

public static class StaticUnitOfWork
{
    public static async Task<string> SaveImageAsync(this IFormFile file, IWebHostEnvironment env)
    {
        if (file == null || file.Length == 0)
            throw new ArgumentException("File is empty or null");

        var uploadsDirectory = Path.Combine(env.WebRootPath, "UserImages");

        if (!Directory.Exists(uploadsDirectory))
            Directory.CreateDirectory(uploadsDirectory);

        var fileName = Guid.NewGuid().ToString() + Path.GetExtension(file.FileName);
        var filePath = Path.Combine(uploadsDirectory, fileName);

        using (var stream = new FileStream(filePath, FileMode.Create))
        {
            await file.CopyToAsync(stream);
        }

        return Path.Combine("UserImages", fileName);
    }
    public static string? GetFileUrl(this string? fileName, IHttpContextAccessor httpContextAccessor)
    {
        if (!string.IsNullOrEmpty(fileName))
        {
            var request = httpContextAccessor?.HttpContext?.Request;
            if (request != null)
            {
                var protocol = request.IsHttps ? "https://" : "http://";
                var host = request.Host.Host;
                var port = request.Host.Port;
                return $"{protocol}{host}:{port}/{fileName}";
            }
        }

        return null;
    }
}
