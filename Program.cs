using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SQLite;

namespace Jasons_SQLite_CS_Console_App3
{
    class Program
    {
        static void Main(string[] args)
        {

            // CREATE NEW TABLE
            string query = @"CREATE TABLE IF NOT EXISTS
                             [albums] (
                             [id] INTEGER NOT NULL PRIMARY KEY AUTOINCREMENT,
                             [title] TEXT,
                             [artist] TEXT)";
            using (SQLiteCommand myCommand = new SQLiteCommand(query, Database.MyConnection()))
            {
                Database.OpenConnection();
                myCommand.CommandText = query;
                myCommand.ExecuteNonQuery();
                Database.CloseConnection();
            }

            // INSERT INTO DATABASE
            query = "INSERT INTO albums (`title`, `artist`) VALUES (@title, @artist)";
            using (SQLiteCommand myCommand = new SQLiteCommand(query, Database.MyConnection()))
            {
                Database.OpenConnection();
                myCommand.Parameters.AddWithValue("@title", "Yellow Submarine");
                myCommand.Parameters.AddWithValue("@artist", "John Lennon");
                var returnValue = myCommand.ExecuteNonQuery();
                Database.CloseConnection();
                Console.WriteLine("Rows Added : {0}", returnValue);
            }

                // SELECT FROM DATABASE
                query = "SELECT * FROM albums";
            using (SQLiteCommand myCommand = new SQLiteCommand(query, Database.MyConnection()))
            {
                Database.OpenConnection();

                SQLiteDataReader result = myCommand.ExecuteReader();
                if (result.HasRows)
                {
                    while (result.Read())
                    {
                        Console.WriteLine("Album: {0} - Artist: {1}", result["title"], result["artist"]);
                    }
                }
                Database.CloseConnection();
            }
            Console.ReadKey();
        }
    }
}
