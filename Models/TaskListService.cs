using System.Collections.Generic;
using System.IO;
using Npgsql;
using System;

namespace mvc_project{
    public class TaskListService{
        private NpgsqlConnection connection;
        public TaskListService(){
            string[] initString = File.ReadAllLines("conf/db_user.txt");
            connection = new NpgsqlConnection(initString[0]);
            connection.Open();
            using (var cmd = new NpgsqlCommand("DROP TABLE tasklists", connection))
                cmd.ExecuteNonQuery();
            using (var cmd = new NpgsqlCommand("CREATE TABLE tasklists(id SERIAL NOT NULL PRIMARY KEY, name TEXT);", connection))
                cmd.ExecuteNonQuery();
        }
        public List<TaskList> GetAll(){
            List<TaskList> listTasks = new List<TaskList>();

            using (var cmd = new NpgsqlCommand("SELECT * FROM tasklists", connection))
            using (var reader =  cmd.ExecuteReader())
                while (reader.Read())
                    listTasks.Add(new TaskList(reader.GetInt32(0), reader.GetString(1)));

            return listTasks;
        }

        public List<TaskList> GetById(int id){
            List<TaskList> listTasks = new List<TaskList>();

            using (var cmd = new NpgsqlCommand($"SELECT * FROM tasks WHERE groupid=(@groupid);", connection)){
                cmd.Parameters.AddWithValue("groupid", id);
                using (var reader =  cmd.ExecuteReader())
                    while (reader.Read())
                        listTasks.Add(new TaskList(reader.GetInt32(0), reader.GetString(1)));
            }

            return listTasks;
        }

        public void Add(TaskList taskList){
            using (var cmd = new NpgsqlCommand("INSERT INTO tasklists (name) VALUES (@name);", connection)){
                if (taskList.Name == "") taskList.Name = "Unknown";
                cmd.Parameters.AddWithValue("name", taskList.Name);
                cmd.ExecuteNonQuery();
            }
        }
        public TaskList Put(TaskList taskList){      
            using (var cmd = new NpgsqlCommand("UPDATE tasklists SET name=(@name) WHERE id=(@id);", connection)){
                cmd.Parameters.AddWithValue("id", taskList.Id);
                if (taskList.Name == "") taskList.Name = "Unknown";
                cmd.Parameters.AddWithValue("name", taskList.Name);
                cmd.ExecuteNonQuery();
            }

            return taskList;
        }
        public TaskList Patch(TaskList taskList){
            Task oldTask = new Task();

            using (var cmd = new NpgsqlCommand($"SELECT * FROM tasks WHERE id=(@id);", connection)){
                cmd.Parameters.AddWithValue("id", taskList.Id);
                using (var reader =  cmd.ExecuteReader()){
                    reader.Read();
                    oldTask = new Task(reader.GetInt32(0), reader.GetString(1), reader.GetBoolean(2), reader.GetInt32(3));
                }
            }

            using (var cmd = new NpgsqlCommand("UPDATE tasklists SET name=(@name) WHERE id=(@id);", connection)){
                cmd.Parameters.AddWithValue("id", oldTask.Id);
                if (taskList.Name == "") taskList.Name = oldTask.Name;
                cmd.Parameters.AddWithValue("name", taskList.Name);
                cmd.ExecuteNonQuery();
            }

            return taskList;
        }
        public void Delete(int id){
            using (var cmd = new NpgsqlCommand($"DELETE FROM tasklists WHERE id=(@id);", connection)){
                cmd.Parameters.AddWithValue("id", id);
                cmd.ExecuteNonQuery();
            }
            using (var cmd = new NpgsqlCommand($"DELETE FROM tasks WHERE groupid=(@groupid);", connection)){
                cmd.Parameters.AddWithValue("groupid", id);
                cmd.ExecuteNonQuery();
            }
        }
    }
}