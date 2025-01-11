using JCB_Cinema.Application.Interfaces;
using JCB_Cinema.Application.Requests.Create;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace JCB_Cinema.WebAPI.Controllers
{
    /// <summary>
    /// Controller for managing photos in the cinema.
    /// </summary>
    [Route("api/photos")]
    [ApiController]
    public class PhotosController : ControllerBase
    {
        private IPhotoService _photoService;

        /// <summary>
        /// Initializes a new instance of the <see cref="PhotosController"/> class.
        /// </summary>
        /// <param name="photoService">Service to handle photo operations.</param>
        public PhotosController(IPhotoService photoService)
        {
            _photoService = photoService;
        }

        /// <summary>
        /// Retrieves a photo based on its description.
        /// </summary>
        /// <param name="description">The description of the photo to retrieve.</param>
        /// <returns>
        ///   * Status200OK (with data): If the photo is found, the method returns a 200 OK response with the photo.
        ///   * Status404NotFound (no data): If no photo is found with the specified description.
        /// </returns>
        [HttpGet("{description}")]
        public async Task<IActionResult> GetPhoto(string description)
        {
            try
            {
                var result = await _photoService.Get(description);
                if (result == null || result.Bytes == null)
                {
                    return NotFound();
                }

                var mimeType = GetMimeType(result.FileExtension);

                Response.Headers.Append("X-Photo-Description", result.Description ?? "No description");
                Response.Headers.Append("X-Photo-Size", result.Size?.ToString() ?? "0");
                Response.Headers.Append("X-Photo-FileExtension", result.FileExtension);

                var fileExtension = result.FileExtension.StartsWith('.') ? result.FileExtension : "." + result.FileExtension;
                return File(
                        result.Bytes,
                        mimeType,
                        $"photo_{result.Description ?? DateTime.Now.ToString("dd-MM-yyyy")}{fileExtension}"
                    );
            }
            catch (Exception)
            {
                throw;
            }
        }

        /// <summary>
        /// Uploads a new photo to the system.
        /// </summary>
        /// <param name="uploadPhoto">The photo file to be uploaded.</param>
        /// <returns>
        ///   * Status201Created (with data): If the photo is successfully uploaded, the method returns a 201 Created response with the photo description.
        ///   * Status401Unauthorized (no data): If the user is not authorized to upload a photo.
        ///   * Status409Conflict (no data): If there is a conflict during the upload.
        ///   * Status400BadRequest (no data): If there is an error while uploading the photo.
        /// </returns>
        [Authorize(Roles = "Admin")]
        [HttpPost("upload")]
        public async Task<IActionResult> UploadPhoto([FromForm] UploadPhoto uploadPhoto)
        {
            try
            {
                var result = await _photoService.UploadPhoto(uploadPhoto);
                if (result == null)
                {
                    return Conflict();
                }

                return CreatedAtAction(nameof(GetPhoto), new { description = result.Description }, result.Description);
            }
            catch (UnauthorizedAccessException)
            {
                return Unauthorized();
            }
            catch (NullReferenceException)
            {
                return ValidationProblem("Wysłano niepoprawny plik.");
            }
            catch (Exception)
            {
                return BadRequest();
            }
        }

        /// <summary>
        /// Updates an existing photo in the system.
        /// </summary>
        /// <param name="updatePhoto">The updated photo details.</param>
        /// <returns>
        ///   * Status204NoContent (no data): If the photo is successfully updated.
        ///   * Status401Unauthorized (no data): If the user is not authorized to update the photo.
        ///   * Status400BadRequest (no data): If there is an error while updating the photo.
        /// </returns>
        [Authorize(Roles = "Admin")]
        [HttpPut("update")]
        public async Task<IActionResult> UpdatePhoto([FromForm] UpdatePhoto updatePhoto)
        {
            try
            {
                await _photoService.Update(updatePhoto);
                return NoContent();
            }
            catch (UnauthorizedAccessException)
            {
                return Unauthorized();
            }
            catch (NullReferenceException)
            {
                return ValidationProblem("Wysłano niepoprawny plik.");
            }
            catch (Exception)
            {
                return BadRequest();
            }
        }

        /// <summary>
        /// Deletes a photo based on its ID.
        /// </summary>
        /// <param name="id">The ID of the photo to be deleted.</param>
        /// <returns>
        ///   * Status204NoContent (no data): If the photo is successfully deleted.
        ///   * Status401Unauthorized (no data): If the user is not authorized to delete the photo.
        ///   * Status404NotFound (no data): If the photo with the specified ID is not found.
        /// </returns>
        [Authorize(Roles = "Admin")]
        [HttpDelete("delete/{id}")]
        public async Task<IActionResult> DeletePhoto(int id)
        {
            try
            {
                await _photoService.Delete(id);
                return NoContent();
            }
            catch (UnauthorizedAccessException)
            {
                return Unauthorized();
            }
            catch (NullReferenceException)
            {
                return ValidationProblem("Wysłano niepoprawny plik.");
            }
            catch (Exception)
            {
                return BadRequest();
            }
        }

        #region Mime

        /// <summary>
        /// Determines the MIME type based on the file extension.
        /// </summary>
        /// <param name="fileExtension">The file extension of the photo.</param>
        /// <returns>The MIME type corresponding to the file extension.</returns>
        private string GetMimeType(string fileExtension)
        {
            return fileExtension.ToLower() switch
            {
                ".jpg" or ".jpeg" => "image/jpeg",
                ".png" => "image/png",
                ".gif" => "image/gif",
                ".bmp" => "image/bmp",
                ".pdf" => "application/pdf",
                _ => "application/octet-stream" // Default MIME type
            };
        }

        #endregion
    }
}
