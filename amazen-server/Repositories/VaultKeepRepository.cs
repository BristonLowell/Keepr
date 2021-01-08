using System;
using System.Collections.Generic;
using System.Data;
using Dapper;
using amazen_server.Models;

namespace amazen_server.Repositories
{
  public class VaultKeepRepository
  {
    private readonly IDbConnection _db;

    public VaultKeepRepository(IDbConnection db)
    {
      _db = db;
    }

    public int Create(VaultKeep newVk)
    {
      string sql = @"
        INSERT INTO vaultkeeps
        (vaultId, keepId, creatorId)
        VALUES
        (@VaultId, @KeepId, @CreatorId);
        SELECT LAST_INSERT_ID();";
      return _db.ExecuteScalar<int>(sql, newVk);
    }

    public IEnumerable<Keep> GetKeepsByVaultId(int vaultId)
    {
      string sql = @"
        SELECT b.*,
        cb.id as VaultKeepId,
        p.*
        FROM vaultkeeps cb
        JOIN keeps b ON b.id = cb.keepId
        JOIN profiles p ON p.id = b.creatorId
        WHERE vaultId = @vaultId;";
      return _db.Query<VaultKeepViewModel, Profile, VaultKeepViewModel>(sql, (keep, profile) => { keep.Creator = profile; return keep; }, new { vaultId }, splitOn: "id");
    }

    public bool Remove(int id)
    {
      string sql = "DELETE from vaultkeeps WHERE id = @Id";
      int valid = _db.Execute(sql, new { id });
      return valid > 0;
    }

    public bool RemoveAll(int id)
    {
      string sql = "DELETE from vaultkeeps WHERE vaultId = @Id";
      int valid = _db.Execute(sql, new { id });
      return valid > 0;
    }

    public VaultKeep GetById(int id)
    {
      string sql = @"SELECT * from vaultkeeps WHERE id = @Id";
      return _db.QueryFirstOrDefault<VaultKeep>(sql, new { id });
    }
  }
}