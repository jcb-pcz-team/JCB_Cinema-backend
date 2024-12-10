using JCB_Cinema.Application.Interfaces;
using JCB_Cinema.Application.Requests.Create;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace JCB_Cinema.WebAPI.Controllers
{
    [Route("api/photos")]
    [ApiController]
    public class PhotosController : ControllerBase
    {
        private IPhotoService _photoService;

        public PhotosController(IPhotoService photoService)
        {
            _photoService = photoService;
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetPhoto(int id)
        {
            try
            {
                var result = await _photoService.Get(id);
                if (result == null)
                {
                    return NotFound();
                }
                if (result.Bytes == null)
                {
                    return NoContent();
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

                return CreatedAtAction(nameof(GetPhoto), new { id = result.Id }, result);
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
        private string GetMimeType(string fileExtension)
        {
            return fileExtension.ToLower() switch
            {
                ".jpg" or ".jpeg" => "image/jpeg",
                ".png" => "image/png",
                ".gif" => "image/gif",
                ".bmp" => "image/bmp",
                ".pdf" => "application/pdf",
                _ => "application/octet-stream" // Domyślny typ MIME
            };
        }
        #endregion
    }
}
