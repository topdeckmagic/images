# Image API

This is an implementaiton of a stateless server side component for processing images and applying user provided modifications.  The API supports images and commands in a single JSON payload and returns the resulting image.

## High Level Architectur
<img src="docs\HighLevel.svg">

# API Specification
There is a single API endpoint for the solution that support HTTPS POST with the following JSON Schema.  The client is required to encode the image as a Base64 encoded string.

```
{
    "imageData": "<Base 64 String >",
    "Transforms": [
        {"Command": "<CommandName>", "Args": [<String Array of Optional Arguments>]}
        ...
    ]
}
```

| Command   | Arguments                   | Description                                          |
|-----------|-----------------------------|------------------------------------------------------|
| FlipH     | None                        | Flips the image on the Horizontal Axis               |
| FlipV     | None                        | Flips the image on the Vertical Axis                 |
| Rotate    | Int                         | Rotates the picture that number of degrees clockwise |
| Rotate    | "Left" or "Right"           | Rotates the picture 90 degress in that direction     |
| GrayScale | None                        | Converts the image to grayscale                      |
| Resize    | Int \<Width>, Int \<Height> | Resized the image to the specificed Width and Height |
| Thumbnail | None                        | Converts the image to a 30X30 pixel Thumbnai         |