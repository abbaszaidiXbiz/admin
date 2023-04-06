using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using admin.Collections;
using CloudinaryDotNet;
using CloudinaryDotNet.Actions;

namespace admin.Utils
{
    public class CloudinaryHelper
    {
        public Cloudinary _cloudinary { get; set; }
        public CloudinaryHelper(Cloudinary cloudinary)
        {
            _cloudinary = cloudinary;
        }

       
    }
}