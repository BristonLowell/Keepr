using amazen_server.Models;
using amazen_server.Services;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace amazen_server.Controllers
{

  [ApiController]
  [Route("api/[controller]")]
  public class VaultController : ControllerBase
  {
    private readonly VaultService _vs;

    public VaultController(VaultService vs)
    {
      _vs = vs;
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

    public ActionResult<Vault> GetById(int id)
    {
      try
      {
        return Ok(_vs.GetById(id));
      }
      catch (System.Exception err)
      {

        return BadRequest(err);
      }
    }


    [HttpPost]

    public ActionResult<Vault> Create([FromBody] Vault newVault)
    {
      try
      {
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

    [HttpPut("{id}")]

    public ActionResult<Vault> Edit(int id, [FromBody] Vault editedVault)
    {
      try
      {
        return Ok(_vs.Edit(id, editedVault));
      }
      catch (System.Exception err)
      {

        return BadRequest(err);
      }
    }


  }
}