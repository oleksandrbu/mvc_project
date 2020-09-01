using System.IO;
using Npgsql;

namespace mvc_project{
    public class DataBase{
        private static NpgsqlConnection connection;
        static DataBase(){
            string[] initString = File.ReadAllLines("conf/db_user.txt");
            connection = new NpgsqlConnection(initString[0]);
            connection.Open();
            /*using (var cmd = new NpgsqlCommand("DROP TABLE tasks", connection))
                cmd.ExecuteNonQuery();
            using (var cmd = new NpgsqlCommand("CREATE TABLE tasks(id SERIAL NOT NULL PRIMARY KEY, name TEXT, complited BOOLEAN, groupid INTEGER);", connection))
                cmd.ExecuteNonQuery();
            using (var cmd = new NpgsqlCommand("DROP TABLE tasklists", connection))
                cmd.ExecuteNonQuery();
            using (var cmd = new NpgsqlCommand("CREATE TABLE tasklists(id SERIAL PRIMARY KEY, name TEXT);", connection))
                cmd.ExecuteNonQuery();*/
        }

        public static NpgsqlConnection Connection(){
            return connection;
        }
    }
}