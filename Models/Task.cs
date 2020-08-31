namespace mvc_project{
    public class Task{
        public Task(){
            Id = 0;
            Name = "";
            Complited = null;
            GroupId = null;
        }
        public Task(int id, string name, bool complited, int groupid)
        {   
            Id = id;
            Name = name;
            Complited = complited;
            GroupId = groupid;
        }   

        public int Id { get; set; }
        public string Name { get; set; }
        public bool? Complited { get; set; }
        public int? GroupId { get; set; }
    }
}