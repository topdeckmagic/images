# Image API

This is an implementaiton of a stateless server side component for processing images and applying user provided modifications over HTTPS.  The API supports images and commands in a single JSON payload and returns the transformed image.

## High Level Architecture

<img src="docs\HighLevel.svg">

The architecture is simple, a client server based approach with bundling of the image with the commands required to make the transformation.  While a client can do transformations one at a time, the approach supports minimizing round trips for image processing.

The server is a .NET Core API deployed in Azure at <https://topdeckmagic-images.azurewebsites.net/api/Image>.  The server is self contained and holds the image in memory for processing, once returned the image is gone from server context.

## Architecture Justification

While more complex solutions could have been pursued the nature of the stated problem is such that less is more.  By building a simple stateless solution this component is reusable for image processing in multiple scenarios.  Additionally the decision to bundle the image and commands together avoids additional round trips or the requirement to store the image server side.  This reduces security, privacy and other risks for the solution.

## API Specification

There is a single API endpoint for the solution that support HTTPS POST with the following JSON Schema.  The client is required to encode the image as a Base64 encoded string.

### POST Schema

Posts to the API must be made using the following schema:

```
{
    "imageData": "<Base 64 String >",
    "Transforms": [
        {"Command": "<CommandName>", "Args": [<String Array of Optional Arguments>]}
        ...
    ]
}
```

### Commands and Arguments for Transforms

The below table includes the valid Command names for the service along with the supported arguments.

| Command   | Arguments                   | Description                                          |
|-----------|-----------------------------|------------------------------------------------------|
| FlipH     | None                        | Flips the image on the Horizontal Axis               |
| FlipV     | None                        | Flips the image on the Vertical Axis                 |
| Rotate    | Int                         | Rotates the picture that number of degrees clockwise |
| Rotate    | "Left" or "Right"           | Rotates the picture 90 degress in that direction     |
| GrayScale | None                        | Converts the image to grayscale                      |
| Resize    | Int \<Height>, Int \<Width> | Resized the image to the specificed Height and Width |
| Thumbnail | None                        | Converts the image to a 30X30 pixel Thumbnai         |

## Design Patterns

The application is built using the MVC model however with no implemented UI it is better called a MC implementation.  Both the overall schema and the schema for Transforms are heald in the ImageModel.cs with all relevant application logic in ImageController.cs.

## Testing the Solution

The solution can be validated with Postman!

[![Run in Postman](https://run.pstmn.io/button.svg)](https://app.getpostman.com/run-collection/d5b4a3a2e8fb7565334c)