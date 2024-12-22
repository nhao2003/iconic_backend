using System.Net;
using Microsoft.AspNetCore.Mvc;
using CloudinaryDotNet;
using CloudinaryDotNet.Actions;

namespace API.Controllers;

public class UploadController : BaseApiController
{
    private readonly Cloudinary _cloudinary = new(new Account(
        "devfdx8fs",
        "891586263536798",
        "RzdaT2bvDC4KZ-BeHlm0ZccPcS0"
    ));

    [HttpPost]
    public async Task<ActionResult<UploadResult>> UploadFile(IFormFile file, [FromForm] string folder)
    {
        var uploadParams = new ImageUploadParams()
        {
            File = new FileDescription(file.FileName, file.OpenReadStream()),
            Folder = folder
        };

        var uploadResult = await _cloudinary.UploadAsync(uploadParams);

        if (uploadResult.Error != null)
        {
            return APIErrorResponse(
                Guid.NewGuid(),
                HttpStatusCode.BadRequest,
                "Problem uploading file",
                ["Failed to upload the file."]
            );
        }

        return APISuccessResponse(
            uploadResult,
            "File uploaded successfully"
        );
    }
}