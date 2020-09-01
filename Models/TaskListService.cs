using System.Collections.Generic;
using System.IO;
using Npgsql;

namespace mvc_project{
    public class TaskListService{
        public TaskListService(){
            
        }
        public List<TaskList> GetAll(){
            List<TaskList> listTasks = new List<TaskList>();

            using (var cmd = new NpgsqlCommand("SELECT * FROM tasklists", DataBase.Connection()))
            using (var reader =  cmd.ExecuteReader())
                while (reader.Read())
                    listTasks.Add(new TaskList(reader.GetInt32(0), reader.GetString(1)));

            return listTasks;
        }

        public TaskList GetById(int id){
            TaskList taskList;

            using (var cmd = new NpgsqlCommand($"SELECT * FROM tasklists WHERE id=(@id);", DataBase.Connection())){
                cmd.Parameters.AddWithValue("id", id);
                using (var reader =  cmd.ExecuteReader()){
                    reader.Read();
                    taskList = new TaskList(reader.GetInt32(0), reader.GetString(1));
                }
            }

            return taskList;
        }
        public List<Task> GetByIdList(int id){
            List<Task> listTasks = TaskService.GetByIdList(id);

            return listTasks;
        }

        public void Add(TaskList taskList){
            using (var cmd = new NpgsqlCommand("INSERT INTO tasklists (name) VALUES (@name);", DataBase.Connection())){
                if (taskList.Name == "") taskList.Name = "Unknown";
                cmd.Parameters.AddWithValue("name", taskList.Name);
                cmd.ExecuteNonQuery();
            }
        }
        public TaskList Put(TaskList taskList){      
            using (var cmd = new NpgsqlCommand("UPDATE tasklists SET name=(@name) WHERE id=(@id);", DataBase.Connection())){
                cmd.Parameters.AddWithValue("id", taskList.Id);
                if (taskList.Name == "") taskList.Name = "Unknown";
                cmd.Parameters.AddWithValue("name", taskList.Name);
                cmd.ExecuteNonQuery();
            }

            return taskList;
        }
        public TaskList Patch(TaskList taskList){
            TaskList oldTask = new TaskList();

            using (var cmd = new NpgsqlCommand($"SELECT * FROM tasklists WHERE id=(@id);", DataBase.Connection())){
                cmd.Parameters.AddWithValue("id", taskList.Id);
                using (var reader =  cmd.ExecuteReader()){
                    reader.Read();
                    oldTask = new TaskList(reader.GetInt32(0), reader.GetString(1));
                }
            }

            using (var cmd = new NpgsqlCommand("UPDATE tasklists SET name=(@name) WHERE id=(@id);", DataBase.Connection())){
                cmd.Parameters.AddWithValue("id", oldTask.Id);
                if (taskList.Name == "") taskList.Name = oldTask.Name;
                cmd.Parameters.AddWithValue("name", taskList.Name);
                cmd.ExecuteNonQuery();
            }

            return taskList;
        }
        public void Delete(int id){
            using (var cmd = new NpgsqlCommand($"DELETE FROM tasklists WHERE id=(@id);", DataBase.Connection())){
                cmd.Parameters.AddWithValue("id", id);
                cmd.ExecuteNonQuery();
            }
            using (var cmd = new NpgsqlCommand($"DELETE FROM tasks WHERE groupid=(@groupid);", DataBase.Connection())){
                cmd.Parameters.AddWithValue("groupid", id);
                cmd.ExecuteNonQuery();
            }
        }
    }
}