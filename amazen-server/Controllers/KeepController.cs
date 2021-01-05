using amazen_server.Models;
using amazen_server.Services;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace amazen_server.Controllers
{

  [ApiController]
  [Route("api/[controller]")]
  public class KeepController : ControllerBase
  {
    private readonly KeepService _ks;

    public KeepController(KeepService ks)
    {
      _ks = ks;
    }


    [HttpGet]

    public ActionResult<IEnumerable<Keep>> GetAll()
    {
      try
      {
        return Ok(_ks.GetAll());
      }
      catch (System.Exception err)
      {

        return BadRequest(err);
      }
    }

    // [HttpGet("{id}")]

    // public ActionResult<Keep> GetById(int id)
    // {
    //   try
    //   {
    //     return Ok(_ks.GetById(id));
    //   }
    //   catch (System.Exception err)
    //   {

    //     return BadRequest(err);
    //   }
    // }


    [HttpPost]

    public ActionResult<Keep> Create([FromBody] Keep newKeep)
    {
      try
      {
        return Ok(_ks.Create(newKeep));
      }
      catch (System.Exception err)
      {

        return BadRequest(err);
      }
    }

    // [HttpDelete("{id}")]

    // public ActionResult<string> Delete(int id)
    // {
    //   try
    //   {
    //     return Ok(_ks.Delete(id));
    //   }
    //   catch (System.Exception err)
    //   {

    //     return BadRequest(err);
    //   }
    // }

    // [HttpPut("{id}")]

    // public ActionResult<Keep> Edit(int id, [FromBody] Keep editedKeep)
    // {
    //   try
    //   {
    //     return Ok(_ks.Edit(id, editedKeep));
    //   }
    //   catch (System.Exception err)
    //   {

    //     return BadRequest(err);
    //   }
    // }


  }
}