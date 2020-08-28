using System.Collections.Generic;

namespace mvc_project{
    public class TaskService{
        private List<Task> listTasks;
        private int index;
        public TaskService(){
            index = 0;
            listTasks = new List<Task>();
            Add("vasia", true);
            Add("petya", false);
        }
        public List<Task> GetAll(){
            return listTasks;
        }

        public Task GetById(int id){
            return listTasks[id];
        }

        public void Add(string name, bool done){
            Task task = new Task(index, name, done);
            listTasks.Add(task);
            index++;
        }
    }
}