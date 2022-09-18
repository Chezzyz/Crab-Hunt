using Services.Tutorial.Tasks;

namespace Services.Tutorial
{
    public struct State
    {
        public readonly string Text;
        public readonly string TaskDescription;
        public readonly AbstractTask Task;

        public State(string text, string taskDescription, AbstractTask task)
        {
            Text = text;
            TaskDescription = taskDescription;
            Task = task;
        }
    }
}