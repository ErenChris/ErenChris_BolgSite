using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace BolgSite.Data.FileManager
{
    public class FileManager : IFileManager
    {
        private readonly string _ImagePath = "";

        public FileManager(IConfiguration cfg)
        {
            _ImagePath = cfg["Path:Images"];
        }

        async Task<string> IFileManager.SaveImage(IFormFile Image)
        {
            try
            {
                var SavePath = Path.Combine(_ImagePath);
                if (!Directory.Exists(SavePath))
                {
                    Directory.CreateDirectory(SavePath);
                }

                var mime = Image.FileName.Substring(Image.FileName.LastIndexOf("."));
                var FileName = $"img_{DateTime.Now.ToString("dd_MM_yyyy_HH_mm_ss")}";

                using (FileStream fileStream = new FileStream(Path.Combine(SavePath, FileName), FileMode.Create))
                {
                    await Image.CopyToAsync(fileStream);
                }

                return FileName;
            }
            catch(Exception e)
            {
                Console.WriteLine(e.Message);
                return "ErrorInFileManager";
            }

        }
    }
}
