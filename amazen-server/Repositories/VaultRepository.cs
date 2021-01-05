// using System;
// using System.Collections.Generic;
// using System.Data;
// using amazen_server.Models;
// using Dapper;

// namespace amazen_server.Repositories
// {
//   public class VaultRepository
//   {
//     private readonly IDbConnection _db;

//     public VaultRepository(IDbConnection db)
//     {
//       _db = db;
//     }

//     public IEnumerable<Vault> GetAll()
//     {
//       string sql = "SELECT * FROM generals";
//       return _db.Query<Vault>(sql);
//     }

//     public Vault Create(Vault newVault)
//     {
//       string sql = @"INSERT INTO generals
//       (title, description, img)
//       VALUES
//       (@Title, @Description, @Img);
//       SELECT LAST_INSERT_ID();";
//       newVault.Id = _db.ExecuteScalar<int>(sql, newVault);
//       return newVault;
//     }

//     public Vault GetById(int id)
//     {
//       string sql = "SELECT * FROM generals WHERE id = @Id";
//       return _db.QueryFirstOrDefault<Vault>(sql, new { id });
//     }

//     public bool Delete(int id)
//     {
//       string sql = "DELETE FROM generals WHERE id = @Id LIMIT 1";
//       int affectedRows = _db.Execute(sql, new { id });
//       return affectedRows > 0;
//     }

//     public Vault Edit(int id, Vault editedVault)
//     {
//       string sql = @"UPDATE generals SET name = @Name, meat = @Meat, buns = @Buns, sauce = @sauce WHERE id = @Id;
//       SELECT * FROM burger WHERE id = @Id";
//       return _db.QueryFirstOrDefault<Vault>(sql, new { id });
//     }
//   }
// }