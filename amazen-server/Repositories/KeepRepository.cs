using System;
using System.Collections.Generic;
using System.Data;
using amazen_server.Models;
using Dapper;

namespace amazen_server.Repositories
{
  public class KeepRepository
  {
    //NOTE referencing the database
    private readonly IDbConnection _db;

    private readonly string populateCreator = "SELECT keep.*, profile.* FROM keeps keep INNER JOIN profiles profile ON keep.creatorId = profile.id ";
    public KeepRepository(IDbConnection db)
    {
      _db = db;
    }




    public IEnumerable<Keep> GetAll()
    {
      string sql = populateCreator;
      return _db.Query<Keep, Profile, Keep>(sql, (keep, profile) => { keep.Creator = profile; return keep; }, splitOn: "id");
    }

    public IEnumerable<Keep> getKeepsByProfile(string profId)
    {
      string sql = @"
        SELECT
        keep.*,
        p.*
        FROM keeps keep
        JOIN profiles p ON keep.creatorId = p.id
        WHERE keep.creatorId = @profId; ";
      return _db.Query<Keep, Profile, Keep>(sql, (keep, profile) => { keep.Creator = profile; return keep; }, new { profId }, splitOn: "id");
    }

    // public IEnumerable<Keep> GetAll()
    // {
    //   string sql = "SELECT * FROM keeps";
    //   return _db.Query<Keep>(sql);
    // }

    public Keep Create(Keep newKeep)
    {
      string sql = @"INSERT INTO keeps
      (name, description, img, views, shares, keeps, creatorId)
      VALUES
      (@Name, @Description, @Img,  @Views, @Shares, @Keeps, @CreatorId);
      SELECT LAST_INSERT_ID();";
      newKeep.Id = _db.ExecuteScalar<int>(sql, newKeep);
      return newKeep;
    }

    public Keep GetById(int id)
    {
      string sql = "SELECT * FROM keeps WHERE id = @Id";
      return _db.QueryFirstOrDefault<Keep>(sql, new { id });
    }

    public bool Delete(int id)
    {
      string sql = "DELETE FROM keeps WHERE id = @Id LIMIT 1";
      int affectedRows = _db.Execute(sql, new { id });
      return affectedRows > 0;
    }

    public void Edit(Keep editData)
    {
      string sql = @"
        UPDATE keeps
        SET
        name = @Name,
        description = @Description,
        shares = @Shares,
        views = @Views,
        keeps = @Keeps,
        img = @Img,
        creatorId = @CreatorId
        WHERE id = @Id;";
      _db.Execute(sql, editData);
    }
  }
}