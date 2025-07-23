namespace Day1proj2.Models
{
    public class SupportAgent
    {
        public int AgentId { get; }
        public string Name { get; }
        public string Department { get; }

        public SupportAgent(int agentId, string name, string department)
        {
            AgentId = agentId;
            Name = name;
            Department = department;
        }

        public void DisplayAgent()
        {
            Console.WriteLine($"Agent ID: {AgentId}, Name: {Name}, Department: {Department}");
        }
    }
}
