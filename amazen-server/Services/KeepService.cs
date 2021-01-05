using System;
using System.Collections.Generic;
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

    // public Keep GetById(int id)
    // {
    //   return _kr.GetById(id);
    // }

    public Keep Create(Keep newKeep)
    {
      return _kr.Create(newKeep);
    }

    // public string Delete(int id)
    // {
    //   string Deleted = "Keep Deleted";
    //   bool deletedKeep = _kr.Delete(id);
    //   if (deletedKeep)
    //   {
    //     return Deleted;
    //   };
    //   throw new Exception("Not a valid Keep");

    // }

    // public Keep Edit(int id, Keep editedKeep)
    // {
    //   Keep oldKeep = _kr.GetById(id);


    //   //NOTE looping through an object and compares

    //   if (editedKeep.Title != null)
    //   {
    //     oldKeep.Title = editedKeep.Title;
    //   }
    //   if (editedKeep.Description != null)
    //   {
    //     oldKeep.Description = editedKeep.Description;
    //   }
    //   if (editedKeep.Img != null)
    //   {
    //     oldKeep.Img = editedKeep.Img;
    //   }

    //   // foreach (PropertyInfo prop in oldKeep.GetType().GetProperties())
    //   // {
    //   //   object oldValue = prop.GetValue(oldKeep, null);
    //   //   object newValue = prop.GetValue(editedKeep, null);
    //   //   if (!object.Equals(oldValue, newValue) && newValue != null)
    //   //   {
    //   //     oldValue = newValue;
    //   //     prop.SetValue(oldKeep, newValue);
    //   //   }
    //   // }
    //   // oldKeep = editedKeep;

    //   return _kr.Edit(id, oldKeep);
    // }
  }
}
