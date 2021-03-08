namespace images.Models
{
    public class ImageAPI
    {
        public string ImageData { get; set; }
        public Transform [] Transforms { get; set; }
    }

    public class Transform
    {
        public string Command {get; set; }
        public string [] Args {get; set; }
    }
}