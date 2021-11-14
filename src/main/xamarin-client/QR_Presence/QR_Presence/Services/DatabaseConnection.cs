using QR_Presence.Models;
using SQLite;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Essentials;

namespace QR_Presence.Services
{
    public static class DatabaseConnection
    {
        static SQLiteAsyncConnection db;
        static async Task Init()
        {
            if (db != null)
            {
                return;
            }

            var databasePath = Path.Combine(FileSystem.AppDataDirectory, "stateDb.db");
            db = new SQLiteAsyncConnection(databasePath);

            await db.CreateTableAsync<UserModel>();
        }

        public static async Task AddUser(UserModel user)
        {
            await Init();
            await db.InsertAsync(user);
        }

        public static async Task UpdateUser(UserModel user)
        {
            await Init();
            await db.UpdateAsync(user);
        }

        public static async Task DeleteUser(UserModel user)
        {
            await Init();
            await db.DeleteAsync(user);
        }

        public static async Task<UserModel> GetUser()
        {
            await Init();
            var users = await db.Table<UserModel>().ToListAsync();
            return users[0];
        }

        public static async Task<float> GetFunctionResult(string query)
        {
            await Init();
            // "SELECT SUM(Price) FROM \"Transaction\" WHERE Type = \"Income\"" get sum from Income
            // "SELECT SUM(Price) FROM \"Transaction\" WHERE Type = \"Expense\"" get sum from Expense

            var incomeSum = await db.ExecuteScalarAsync<float>(query);
            return incomeSum;
        }

      
    }
}
