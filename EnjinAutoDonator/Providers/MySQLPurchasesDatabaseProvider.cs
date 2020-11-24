using Dapper;
using MySql.Data.MySqlClient;
using EnjinAutoDonator.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EnjinAutoDonator.Providers
{
    public class MySQLPurchasesDatabaseProvider : IPurchasesDatabaseProvider
    {
        private EnjinAutoDonatorPlugin pluginInstance => EnjinAutoDonatorPlugin.Instance;

        private string connectionString => string.Format("Server={0};Port={1};Database={2};Uid={3};Pwd={4};",
            pluginInstance.Configuration.Instance.DatabaseAddress,
            pluginInstance.Configuration.Instance.DatabasePort,
            pluginInstance.Configuration.Instance.DatabaseName,
            pluginInstance.Configuration.Instance.DatabaseUsername,
            pluginInstance.Configuration.Instance.DatabasePassword);
        private MySqlConnection connection => new MySqlConnection(connectionString);

        private string Query(string sql) => sql.Replace("PurchasesTable", pluginInstance.Configuration.Instance.PurchasesTableName);

        public void Initialize()
        {
            const string sql = "CREATE TABLE IF NOT EXISTS PurchasesTable (Id INT AUTO_INCREMENT NOT NULL, PurchaseDate INT NOT NULL, SteamId VARCHAR(255) NOT NULL, " + 
                "SteamName NVARCHAR(255) NULL, ItemId INT NOT NULL, ItemName NVARCHAR(255) NULL, ServerId VARCHAR(55) NOT NULL, CreateDate DATETIME NOT NULL, " +
                "CONSTRAINT PK_PurchasesTable PRIMARY KEY(Id), CONSTRAINT UK_PurchasesTable UNIQUE KEY(PurchaseDate, ItemId, ServerId));";
            connection.Execute(Query(sql));
        }

        public IEnumerable<FinishedPurchase> GetPurchases(int sinceEpoch, string serverId)
        {
            const string sql = "SELECT * FROM PurchasesTable WHERE ServerId = @serverId AND PurchaseDate > @sinceEpoch;";
            return connection.Query<FinishedPurchase>(Query(sql), new { sinceEpoch, serverId });
        }

        public void FinishPurchase(FinishedPurchase purchase)
        {
            const string sql = "INSERT INTO PurchasesTable (Id, PurchaseDate, SteamId, SteamName, ItemId, ItemName, ServerId, CreateDate) " +
                "VALUES (@Id, @PurchaseDate, @SteamId, @SteamName, @ItemId, @ItemName, @ServerId, @CreateDate);";
            connection.Execute(Query(sql), purchase);
        }

        public bool ContainsPurchase(int epoch, int itemId)
        {
            const string sql = "SELECT Count(*) > 0 FROM PurchasesTable WHERE PurchaseDate = @epoch AND ItemId = @itemId;";
            return connection.ExecuteScalar<bool>(Query(sql), new { epoch, itemId });
        }
    }
}
