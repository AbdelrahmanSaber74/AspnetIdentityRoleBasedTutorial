namespace AspnetIdentityRoleBasedTutorial.Services
{
	public interface IFileService
	{
		(int, string) SaveImage(IFormFile imageFile);
		bool DeleteImage(string imageFileName);
	}
}
