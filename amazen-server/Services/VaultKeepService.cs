using System;
using System.Collections.Generic;
using amazen_server.Models;
using amazen_server.Repositories;

namespace amazen_server.Services
{
  public class VaultKeepService
  {

    private readonly VaultKeepRepository _repo;

    public VaultKeepService(VaultKeepRepository repo)
    {
      _repo = repo;
    }

    public VaultKeep Create(VaultKeep newCb)
    {
      newCb.Id = _repo.Create(newCb);
      return newCb;
    }

    public IEnumerable<Keep> GetKeepsByVaultId(int vaultId)
    {
      return _repo.GetKeepsByVaultId(vaultId);

    }

    public string Delete(int id, string userId)
    {
      VaultKeep original = _repo.GetById(id);
      if (original == null) { throw new Exception("Bad Id"); }
      if (original.CreatorId != userId)
      {
        throw new Exception("Not the User : Access Denied");
      }
      if (_repo.Remove(id))
      {
        return "deleted succesfully";
      }
      return "did not remove succesfully";
    }
  }
}