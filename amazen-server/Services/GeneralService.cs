using System;
using System.Collections.Generic;
using System.Reflection;
using amazen_server.Models;
using amazen_server.Repositories;

namespace amazen_server.Services
{
  public class VaultService
  {
    private readonly VaultRepository _gr;

    public VaultService(VaultRepository gr)
    {
      _gr = gr;
    }
    public IEnumerable<Vault> GetAll()
    {
      return _gr.GetAll();
    }

    public Vault GetById(int id)
    {
      return _gr.GetById(id);
    }

    public Vault Create(Vault newVault)
    {
      return _gr.Create(newVault);
    }

    public string Delete(int id)
    {
      string Deleted = "Vault Deleted";
      bool deletedVault = _gr.Delete(id);
      if (deletedVault)
      {
        return Deleted;
      };
      throw new Exception("Not a valid Vault");

    }

    public Vault Edit(int id, Vault editedVault)
    {
      Vault oldVault = _gr.GetById(id);


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

      return _gr.Edit(id, oldVault);
    }
  }
}
