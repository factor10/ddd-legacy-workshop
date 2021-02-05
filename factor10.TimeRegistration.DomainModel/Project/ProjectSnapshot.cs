namespace factor10.TimeRegistration.DomainModel
{
    public class ProjectSnapshot
    {
        public string Name { get; private set; }

        public ProjectSnapshot(string name)
        {
            Name = name;
        }
        
        public ProjectSnapshot() {} //For persistence reasons
    }
}
