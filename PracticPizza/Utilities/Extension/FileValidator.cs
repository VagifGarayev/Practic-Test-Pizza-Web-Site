using Microsoft.AspNetCore.Components.Web;

namespace PracticPizza.Utilities.Extension
{
	public static class FileValidator
	{

		public static bool FileType(this IFormFile file,string type="image/")
		{
			if (file.ContentType.Contains(type))
			{
				return true;
			}
			return false;

		}

		public static bool FileSize(this IFormFile file ,int LimitKB)
		{
			if (file.Length <= LimitKB * 1024)
			{
				return true;
			}
			return false;
		}

		public static async Task<string> CreateFile(this IFormFile file,string roots,params string[] folders)
		{
			string fileName=Guid.NewGuid().ToString() + file.FileName;
			string path = roots;

			for (int i = 0; i < folders.Length; i++)
			{
		       path=Path.Combine(path, folders[i]);

			}
			path= Path.Combine(path, fileName);

			using (FileStream stream = new FileStream(path, FileMode.Create))
			{
				await file.CopyToAsync(stream);
			}
			return fileName;

		}

		public static void Delete(this string fileName, string roots,params string[] folders)
		{
			string path = roots;
			for (int i = 0; i < folders.Length; i++)
			{
				path=Path.Combine (path, folders[i]);
			}
			path= Path.Combine(path, fileName);

			if(File.Exists(path))
			{
				File.Delete(path);
			}


		}
		
	}
}
