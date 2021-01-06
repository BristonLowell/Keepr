using System.Threading.Tasks;
using CodeWorks.Auth0Provider;
using amazen_server.Models;
using amazen_server.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace amazen_server.Controllers
{
  [ApiController]
  [Route("api/[controller]")]
  public class VaultKeepController : ControllerBase
  {
    private readonly VaultKeepService _vks;

    public VaultKeepController(VaultKeepService vks)
    {
      _vks = vks;
    }

    [HttpPost]
    [Authorize]
    public async Task<ActionResult<VaultKeep>> Post([FromBody] VaultKeep newVaultKeep)
    {
      try
      {
        Profile userInfo = await HttpContext.GetUserInfoAsync<Profile>();
        newVaultKeep.CreatorId = userInfo.Id;
        return Ok(_vks.Create(newVaultKeep));
      }
      catch (System.Exception e)
      {
        return BadRequest(e.Message);
      }
    }

    [Authorize]
    [HttpDelete("{id}")]
    public async Task<ActionResult<string>> Delete(int id)
    {
      try
      {
        Profile userInfo = await HttpContext.GetUserInfoAsync<Profile>();
        return Ok(_vks.Delete(id, userInfo.Id));
      }
      catch (System.Exception e)
      {

        return BadRequest(e.Message);

      }
    }

  }
}