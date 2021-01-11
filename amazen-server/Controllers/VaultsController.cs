using amazen_server.Models;
using amazen_server.Services;
using CodeWorks.Auth0Provider;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace amazen_server.Controllers
{

  [ApiController]
  [Route("api/[controller]")]
  public class VaultsController : ControllerBase
  {
    private readonly VaultService _vs;
    private readonly VaultKeepService _vks;

    public VaultsController(VaultService vs, VaultKeepService vks)
    {
      _vs = vs;
      _vks = vks;
    }


    [HttpGet]

    public ActionResult<IEnumerable<Vault>> GetAll()
    {
      try
      {
        return Ok(_vs.GetAll());
      }
      catch (System.Exception err)
      {

        return BadRequest(err);
      }
    }

    [HttpGet("{id}")]

    public async Task<ActionResult<Vault>> GetById(int id)
    {
      try
      {
        Profile userInfo = await HttpContext.GetUserInfoAsync<Profile>();

        return Ok(_vs.GetById(id, userInfo.Id));
      }
      catch (System.Exception err)
      {

        return BadRequest(err);
      }
    }

    [HttpGet("{id}/keeps")]
    public ActionResult<IEnumerable<Keep>> Get(int id)
    {
      try
      {
        return Ok(_vks.GetKeepsByVaultId(id));
      }
      catch (System.Exception e)
      {
        return BadRequest(e.Message);
      }
    }

    [HttpPost]
    [Authorize]

    public async Task<ActionResult<Vault>> Create([FromBody] Vault newVault)
    {
      try
      {
        Profile userInfo = await HttpContext.GetUserInfoAsync<Profile>();
        newVault.CreatorId = userInfo.Id;
        return Ok(_vs.Create(newVault));
      }
      catch (System.Exception err)
      {

        return BadRequest(err);
      }
    }

    [HttpDelete("{id}")]

    public ActionResult<string> Delete(int id)
    {
      try
      {
        return Ok(_vs.Delete(id));
      }
      catch (System.Exception err)
      {

        return BadRequest(err);
      }
    }

    // [HttpPut("{id}")]

    // public ActionResult<Vault> Edit(int id, [FromBody] Vault editedVault)
    // {
    //   try
    //   {
    //     return Ok(_vs.Edit(id, editedVault));
    //   }
    //   catch (System.Exception err)
    //   {

    //     return BadRequest(err);
    //   }
    // }


  }
}