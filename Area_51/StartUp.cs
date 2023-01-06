
using Area_51.Models;
using Area_51.Models.Enums;
using Area_51.Models.Generators;

class Program
{
    public static void Main(string[] args)
    {
        Console.Write("How many agents to have: ");
        int agentsCount = int.Parse(Console.ReadLine());
        List<Agent> agents = AgentGenerator.CreateAgents(agentsCount).ToList();

        Elevator elevator = new Elevator();

        var agentThreads = new List<Thread>();

        Thread e = new Thread(elevator.WorkDay);

        for (int i = 0; i < agentsCount; i++)
        {
            Agent agent = agents[i];
            Thread t = new Thread(agent.StartWorkDay);
            t.Start(elevator);
            agentThreads.Add(t);
        }

        e.Start();

        foreach (var t in agentThreads) t.Join();
    }
}