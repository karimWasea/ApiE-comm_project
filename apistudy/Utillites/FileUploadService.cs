using Microsoft.AspNetCore.Hosting;

namespace apistudy.Utillites
{
    public class FileUploadService
    {
        private IWebHostEnvironment _webHostEnvironment;
        /// <summary>
        /// Initializes a new instance of the <see cref="FileUploadService"/> class.
        /// </summary>
        /// <param name="webHostEnvironment">The hosting environment.</param>

        public FileUploadService(IWebHostEnvironment webHostEnvironment)
        {
            _webHostEnvironment = webHostEnvironment;

        }
        public string SaveImage(IFormFile image)
        {
            var imageName = Guid.NewGuid().ToString() + Path.GetExtension(image.FileName);
            var imagePath = Path.Combine(_webHostEnvironment.WebRootPath, "images", imageName);

            using (var stream = new FileStream(imagePath, FileMode.Create))
            {
                image.CopyToAsync(stream);
            }

            return "images/" + imageName;
        }

        public void DeleteImage(string imagePath)
        {
            var fullPath = Path.Combine(_webHostEnvironment.WebRootPath, imagePath);
            if (System.IO.File.Exists(fullPath))
            {
                System.IO.File.Delete(fullPath);
            }
        }





        /// <summary>
        /// Uploads a collection of files to a specified directory.
        /// </summary>
        /// <param name="files">The collection of files to upload.</param>
        /// <returns>A list of URLs for the uploaded files.</returns>
        public List<string> UploadFiles(IFormFileCollection files)
        {
            try
            {
                // Get the path where uploaded files will be stored.
                var uploads = Path.Combine(_webHostEnvironment.WebRootPath, "images");

                // Create the directory if it doesn't exist.
                if (!Directory.Exists(uploads))
                    Directory.CreateDirectory(uploads);

                // List to store URLs of uploaded files.
                var uploadedFileUrls = new List<string>();

                foreach (var file in files)
                {
                    if (file.Length > 0)
                    {
                        // Generate a unique file name using a GUID and the original file's extension.
                        var fileName = Guid.NewGuid().ToString() + Path.GetExtension(file.FileName);

                        // Combine the directory path and file name to get the full file path.
                        var filePath = Path.Combine(uploads, fileName);

                        // Copy the file data to the specified location.
                        using (var fileStream = new FileStream(filePath, FileMode.Create))
                        {
                            file.CopyToAsync(fileStream);
                        }

                        // Add the URL of the uploaded file to the list.
                        uploadedFileUrls.Add(Path.Combine("/images", fileName));
                    }
                }

                // Return the list of uploaded file URLs.
                return uploadedFileUrls;
            }
            catch (Exception ex)
            {
                // Handle any exceptions that may occur during file upload.
                // You might want to log the exception or handle it differently.
                throw ex;
            }
        }
    }
}
