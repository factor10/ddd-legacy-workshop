
namespace factor10.TimeRegistration.DomainModel
{
    public class Registration
    {
        public Duration Duration { get; private set; }
        public string Activity { get; private set; }
        public ProjectSnapshot ProjectSnapshot { get; private set; }

        public Registration(Duration duration, string activity, Project project)
        {
            Duration = duration;
            Activity = activity;
            ProjectSnapshot = project.TakeSnapshot();
        }
        
        private Registration() {} //For persistence reasons
    }
}
