namespace AspnetIdentityRoleBasedTutorial.Services
{
	public class FileService : IFileService
	{
		private readonly IWebHostEnvironment _environment;

		public FileService(IWebHostEnvironment environment)
		{
			_environment = environment;
		}

		public (int, string) SaveImage(IFormFile imageFile)
		{
			try
			{
				// Define path to store uploaded files
				var uploadPath = Path.Combine(_environment.WebRootPath, "Uploads");

				// Create the directory if it doesn't exist
				if (!Directory.Exists(uploadPath))
				{
					Directory.CreateDirectory(uploadPath);
				}

				// Validate file extension
				var allowedExtensions = new[] { ".jpg", ".png", ".jpeg" };
				var fileExtension = Path.GetExtension(imageFile.FileName).ToLower();
				if (!allowedExtensions.Contains(fileExtension))
				{
					var errorMessage = $"Only {string.Join(", ", allowedExtensions)} extensions are allowed.";
					return (0, errorMessage);
				}

				// Generate unique file name
				var uniqueFileName = $"{Guid.NewGuid()}{fileExtension}";
				var filePath = Path.Combine(uploadPath, uniqueFileName);

				// Save file to disk
				using (var fileStream = new FileStream(filePath, FileMode.Create))
				{
					imageFile.CopyTo(fileStream);
				}

				return (1, uniqueFileName);
			}
			catch (Exception)
			{
				return (0, "An error occurred while uploading the image.");
			}
		}

		public bool DeleteImage(string imageFileName)
		{
			try
			{
				var filePath = Path.Combine(_environment.WebRootPath, "Uploads", imageFileName);

				// Delete file if it exists
				if (File.Exists(filePath))
				{
					File.Delete(filePath);
					return true;
				}

				return false;
			}
			catch (Exception)
			{
				return false;
			}
		}
	}
}
