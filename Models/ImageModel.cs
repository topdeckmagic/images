namespace images.Models
{
    public class ImageAPI
    {
        public string ImageData { get; set; }
        public Transform [] Transforms { get; set; }
    }

/// <summary>
/// This is the API which will return a customer based on id
/// </summary>
/// <remarks>
/// How to call:
/// 
///     GET /api/customer/1
///     
/// </remarks>
/// <param name="id">Customer Id</param>
/// <response code="200">A valid customer</response>
/// <response code="400">Invalid customer id</response>
/// <returns>A Customer</returns>
    public class Transform
    {
        public string Command {get; set; }
        public string [] Args {get; set; }
    }
}