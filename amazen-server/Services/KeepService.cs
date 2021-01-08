using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using amazen_server.Models;
using amazen_server.Repositories;

namespace amazen_server.Services
{
  public class KeepService
  {
    private readonly KeepRepository _kr;

    public KeepService(KeepRepository kr)
    {
      _kr = kr;
    }
    public IEnumerable<Keep> GetAll()
    {
      return _kr.GetAll();
    }

    public Keep GetById(int id)
    {
      return _kr.GetById(id);
    }

    public Keep Create(Keep newKeep)
    {
      return _kr.Create(newKeep);
    }
    public IEnumerable<Keep> GetKeepsByProfile(string profId, string userId)
    {
      return _kr.getKeepsByProfile(profId).ToList().FindAll(b => b.CreatorId == userId || b.CreatorId == profId);
    }

    public string Delete(int id)
    {
      string Deleted = "Keep Deleted";
      bool deletedKeep = _kr.Delete(id);
      if (deletedKeep)
      {
        return Deleted;
      };
      throw new Exception("Not a valid Keep");
    }

    public Keep Edit(Keep editData, string userId)
    {
      Keep original = _kr.GetById(editData.Id);
      if (original == null) { throw new Exception("Bad Id"); }
      if (editData.Name == null)
      {
        editData.Name = original.Name;
      }
      if (editData.Description == null)
      {
        editData.Description = original.Description;
      }
      if (editData.Img == null)
      {
        editData.Img = original.Img;
      }
      if (editData.CreatorId == null)
      {
        editData.CreatorId = original.CreatorId;
      }
      if (editData.Views == 0)
      {
        editData.Views = original.Views;
      }
      if (editData.Shares == 0)
      {
        editData.Shares = original.Shares;
      }
      if (editData.Keeps == 0)
      {
        editData.Keeps = original.Keeps;
      }
      _kr.Edit(editData);

      return _kr.GetById(editData.Id);

    }
  }
}
