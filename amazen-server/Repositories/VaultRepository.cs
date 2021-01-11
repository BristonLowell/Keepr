using System;
using System.Collections.Generic;
using System.Data;
using amazen_server.Models;
using Dapper;

namespace amazen_server.Repositories
{
  public class VaultRepository
  {
    private readonly IDbConnection _db;

    private readonly string populateCreator = "SELECT vault.*, profile.* FROM vaults vault INNER JOIN profiles profile ON vault.creatorId = profile.id ";
    public VaultRepository(IDbConnection db)
    {
      _db = db;
    }




    public IEnumerable<Vault> GetAll()
    {
      string sql = populateCreator + "WHERE isPrivate = 1";
      return _db.Query<Vault, Profile, Vault>(sql, (vault, profile) => { vault.Creator = profile; return vault; }, splitOn: "id");
    }


    public IEnumerable<Vault> getVaultsByProfile(string profId)
    {
      string sql = @"
        SELECT
        vault.*,
        p.*
        FROM vaults vault
        JOIN profiles p ON vault.creatorId = p.id
        WHERE vault.creatorId = @profId; ";
      return _db.Query<Vault, Profile, Vault>(sql, (vault, profile) => { vault.Creator = profile; return vault; }, new { profId }, splitOn: "id");
    }

    public Vault Create(Vault newVault)
    {
      string sql = @"INSERT INTO vaults
      (name, description, isPrivate, creatorId)
      VALUES
      (@Name, @Description, @IsPrivate, @CreatorId);
      SELECT LAST_INSERT_ID();";
      newVault.Id = _db.ExecuteScalar<int>(sql, newVault);
      return newVault;
    }

    public Vault GetById(int id)
    {
      string sql = "SELECT * FROM vaults WHERE id = @Id";
      return _db.QueryFirstOrDefault<Vault>(sql, new { id });
    }

    public bool Delete(int id)
    {
      string sql = "DELETE FROM vaults WHERE id = @Id LIMIT 1";
      int affectedRows = _db.Execute(sql, new { id });
      return affectedRows > 0;
    }

    // public Vault Edit(int id, Vault editedVault)
    // {
    //   string sql = @"UPDATE vaults SET name = @Name, meat = @Meat, buns = @Buns, sauce = @sauce WHERE id = @Id;
    //   SELECT * FROM burger WHERE id = @Id";
    //   return _db.QueryFirstOrDefault<Vault>(sql, new { id });
    // }
  }
}