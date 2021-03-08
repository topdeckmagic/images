using System;
using Microsoft.AspNetCore.Mvc;
using images.Models;
using SixLabors.ImageSharp;
using SixLabors.ImageSharp.Processing;
using System.IO;

namespace images.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ImageController : ControllerBase
    {
            
        // POST: api/Image
        [HttpPost]
        public FileContentResult PostImage(ImageAPI image)
        {
            var imageUnpacked = Convert.FromBase64String(image.ImageData);
            var img = Image.Load(new MemoryStream(imageUnpacked));

            foreach (Transform t in image.Transforms)
            {
                switch (t.Command)
                {
                    case "FlipH":
                        FlipH(img);
                        break;
                    case "FlipV":
                        FlipV(img);
                        break;
                    case "Rotate":
                        Rotate(img, t.Args);
                        break;
                    case "GrayScale":
                        GrayScale(img);
                        break;
                    case "Resize":
                        Resize(img, t.Args);
                        break;
                    case "Thumbnail":
                        Thumbnail(img);
                        break;
                    default:
                        break;                     
                } 
            }

            var output = new MemoryStream();
            img.SaveAsPng(output);

            return File(output.ToArray(), "image/png");
        }

        private void FlipH(Image img)
        {
            img.Mutate(x => x.Flip(FlipMode.Horizontal));
        }

        private void FlipV(Image img)
        {
            img.Mutate(x => x.Flip(FlipMode.Vertical));
        }

        private void Rotate(Image img, string [] args)
        {
            int degrees = 0;
            switch (args[0])
            {
                case "Right":
                    degrees = 90;
                    break;
                case "Left":
                    degrees = -90;
                    break;
                default:
                    degrees = int.Parse(args[0]);
                    break;
            }
            img.Mutate(x => x.Rotate(degrees));
        }

        private void GrayScale(Image img)
        {
            img.Mutate(x => x.Grayscale());
        }

        private void Resize(Image img, string [] args)
        {
            var h = int.Parse(args[0]);
            var w = int.Parse(args[1]);

            img.Mutate(x => x.Resize(w, h));
        }

        private void Thumbnail(Image img)
        {
            string[] thumbnailSize = new string[] {"30", "30"};
            Resize(img, thumbnailSize);
        }
    }
}