using System;
using System.Collections.Generic;
using System.Reflection;
using amazen_server.Models;
using amazen_server.Repositories;

namespace amazen_server.Services
{
  public class VaultService
  {
    private readonly VaultRepository _vr;

    public VaultService(VaultRepository vr)
    {
      _vr = vr;
    }
    public IEnumerable<Vault> GetAll()
    {
      return _vr.GetAll();
    }

    public Vault GetById(int id)
    {
      return _vr.GetById(id);
    }

    public Vault Create(Vault newVault)
    {
      return _vr.Create(newVault);
    }

    public string Delete(int id)
    {
      string Deleted = "Vault Deleted";
      bool deletedVault = _vr.Delete(id);
      if (deletedVault)
      {
        return Deleted;
      };
      throw new Exception("Not a valid Vault");

    }

    public Vault Edit(int id, Vault editedVault)
    {
      Vault oldVault = _vr.GetById(id);


      //NOTE looping through an object and compares

      if (editedVault.Title != null)
      {
        oldVault.Title = editedVault.Title;
      }
      if (editedVault.Description != null)
      {
        oldVault.Description = editedVault.Description;
      }
      if (editedVault.Img != null)
      {
        oldVault.Img = editedVault.Img;
      }

      // foreach (PropertyInfo prop in oldVault.GetType().GetProperties())
      // {
      //   object oldValue = prop.GetValue(oldVault, null);
      //   object newValue = prop.GetValue(editedVault, null);
      //   if (!object.Equals(oldValue, newValue) && newValue != null)
      //   {
      //     oldValue = newValue;
      //     prop.SetValue(oldVault, newValue);
      //   }
      // }
      // oldVault = editedVault;

      return _vr.Edit(id, oldVault);
    }
  }
}
