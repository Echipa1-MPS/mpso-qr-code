using QR_Presence.Models;
using QR_Presence.Models.APIModels;
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

            await db.CreateTableAsync<User>();
        }

        public static async Task AddUser(User user)
        {
            await Init();
            await db.InsertAsync(user);
        }

        public static async Task UpdateUser(User user)
        {
            await Init();
            await db.UpdateAsync(user);
        }

        public static async Task DeleteUser(User user)
        {
            await Init();
            await db.DeleteAsync(user);
        }

        public static async Task DeleteAllUsers()
        {
            await Init();
            await db.DeleteAllAsync<User>();
        }

        public static async Task<User> GetUser()
        {
            await Init();
            var users = await db.Table<User>().ToListAsync();

            return users.Count == 0 ? new User(): users[0];
        }

        public static async Task<bool> ExistUser()
        {
            await Init();
            var users = await db.Table<User>().ToListAsync();
            return users.Count == 0 ? false : true;
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
