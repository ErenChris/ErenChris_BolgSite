﻿using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace BolgSite.Data.FileManager
{
    public interface IFileManager
    {
        FileStream imageStream(string image);
        Task<string> SaveImage(IFormFile Image);
    }
}
