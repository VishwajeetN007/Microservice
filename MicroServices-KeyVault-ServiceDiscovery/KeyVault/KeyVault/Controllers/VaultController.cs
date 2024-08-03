using KeyVault.Services;
using Microsoft.AspNetCore.Mvc;

namespace KeyVault.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class VaultController : ControllerBase
    {
        IKeyVaultService _vaultService;
        public VaultController(IKeyVaultService vaultService)
        {
            _vaultService = vaultService;
        }

        [HttpGet("{key}")]
        public async Task<IActionResult> Get(string key)
        {
            try
            {
                if (string.IsNullOrEmpty(key))
                {
                    return BadRequest();
                }

                string secretValue = await _vaultService.GetSecret(key);

                if (!string.IsNullOrEmpty(secretValue))
                {
                    return Ok(secretValue);
                }
                else
                {
                    return NotFound("Secret key not found.");
                }
            }
            catch
            {
                return BadRequest("Error: Unable to read secret");
            }
        }

        [HttpPost]
        public async Task<IActionResult> Post(string key, string value)
        {
            try
            {
                if (string.IsNullOrEmpty(key) || string.IsNullOrEmpty(value))
                {
                    return BadRequest();
                }

                string secretValue = await _vaultService.SetSecret(key, value);

                if (!string.IsNullOrEmpty(secretValue))
                {
                    return Ok(secretValue);
                }
                else
                {
                    return NotFound("Secret key not found.");
                }
            }
            catch
            {
                return BadRequest("Error: Unable to create secret");
            }
        }

        [HttpDelete("{key}")]
        public async Task<IActionResult> Delete(string key)
        {
            try
            {
                if (string.IsNullOrEmpty(key))
                {
                    return BadRequest();
                }

                string secretValue = await _vaultService.DeleteSecret(key);

                if (!string.IsNullOrEmpty(secretValue))
                {
                    return Ok(secretValue);
                }
                else
                {
                    return NotFound("Secret key not found.");
                }
            }
            catch
            {
                return BadRequest("Error: Unable to delete secret");
            }
        }
    }
}
