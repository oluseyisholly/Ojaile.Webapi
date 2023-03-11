using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ojaile.Core1.Model
{
    public class PropertyImageViewModel
    {
        public int Id { get; set; }
        public int? PropertyUnitId { get; set; }
        public string? ImageUrl { get; set; }

        public byte[]? ImageData { get; set; }
        public string? Decription { get; set; }
        public IFormFile? PropertyMedia { get; set; }
    }
}
