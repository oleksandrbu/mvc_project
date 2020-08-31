namespace mvc_project{
    public class TaskList{
        public TaskList(){
            Id = 0;
            Name = "";
        }
        public TaskList(int id, string name)
        {   
            Id = id;
            Name = name;
        }   

        public int Id { get; set; }
        public string Name { get; set; }
    }
}