using System.Collections.Generic;
using System.IO;
using Npgsql;

namespace mvc_project{
    public class TaskService{
        public TaskService(){
            
        }
        public List<Task> GetAll(){
            List<Task> listTasks = new List<Task>();

            using (var cmd = new NpgsqlCommand("SELECT * FROM tasks", DataBase.Connection()))
            using (var reader =  cmd.ExecuteReader())
                while (reader.Read())
                    listTasks.Add(new Task(reader.GetInt32(0), reader.GetString(1), reader.GetBoolean(2), reader.GetInt32(3)));

            return listTasks;
        }

        public Task GetById(int id){
            Task task;

            using (var cmd = new NpgsqlCommand($"SELECT * FROM tasks WHERE id=(@id);", DataBase.Connection())){
                cmd.Parameters.AddWithValue("id", id);
                using (var reader =  cmd.ExecuteReader()){
                    reader.Read();
                    task = new Task(reader.GetInt32(0), reader.GetString(1), reader.GetBoolean(2), reader.GetInt32(3));
                }
            }

            return task;
        }

        public static List<Task> GetByIdList(int id){
            List<Task> listTasks = new List<Task>();

            using (var cmd = new NpgsqlCommand($"SELECT * FROM tasks WHERE groupid=(@groupid);", DataBase.Connection())){
                cmd.Parameters.AddWithValue("groupid", id);
                using (var reader =  cmd.ExecuteReader()){
                    while (reader.Read())
                        listTasks.Add(new Task(reader.GetInt32(0), reader.GetString(1), reader.GetBoolean(2), reader.GetInt32(3)));
                }
            }
            
            return listTasks;
        }

        public void Add(Task task){
            using (var cmd = new NpgsqlCommand("INSERT INTO tasks (name, complited, groupid) VALUES (@name, @complited, @groupid);", DataBase.Connection()))
            {
                if (task.Name == "") task.Name = "Unknown";
                cmd.Parameters.AddWithValue("name", task.Name);
                if (task.Complited == null) task.Complited = false;
                cmd.Parameters.AddWithValue("complited", task.Complited);
                if (task.GroupId == null) task.GroupId = 0;
                cmd.Parameters.AddWithValue("groupid", task.GroupId);
                cmd.ExecuteNonQuery();
            }
        }
        public Task Put(Task task){      
            using (var cmd = new NpgsqlCommand("UPDATE tasks SET name=(@name), complited=(@complited), groupid=(@groupid) WHERE id=(@id);", DataBase.Connection())){
                cmd.Parameters.AddWithValue("id", task.Id);
                if (task.Name == "") task.Name = "Unknown";
                cmd.Parameters.AddWithValue("name", task.Name);
                if (task.Complited == null) task.Complited = false;
                cmd.Parameters.AddWithValue("complited", task.Complited);
                if (task.GroupId == null) task.GroupId = 0;
                cmd.Parameters.AddWithValue("groupid", task.GroupId);
                cmd.ExecuteNonQuery();
            }

            return task;
        }
        public Task Patch(Task task){
            Task oldTask = GetById(task.Id);

            using (var cmd = new NpgsqlCommand("UPDATE tasks SET name=(@name), complited=(@complited), groupid=(@groupid) WHERE id=(@id);", DataBase.Connection())){
                cmd.Parameters.AddWithValue("id", oldTask.Id);
                if (task.Name == "") task.Name = oldTask.Name;
                cmd.Parameters.AddWithValue("name", task.Name);
                if (task.Complited == null) task.Complited = oldTask.Complited;
                cmd.Parameters.AddWithValue("complited", task.Complited);
                if (task.GroupId == null) task.GroupId = oldTask.GroupId;
                cmd.Parameters.AddWithValue("groupid", task.GroupId);
                cmd.ExecuteNonQuery();
            }

            return task;
        }
        public void Delete(int id){
            using (var cmd = new NpgsqlCommand($"DELETE FROM tasks WHERE id=(@id);", DataBase.Connection())){
                cmd.Parameters.AddWithValue("id", id);
                cmd.ExecuteNonQuery();
            }
        }
    }
}