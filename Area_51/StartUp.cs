
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

        for (int i = 0; i < agentsCount; i++)
        {
            Agent agent = agents[i];
            agent.addElevator(elevator);
            Thread t = new Thread(agent.StartWorkDay);
            t.Start();
            agentThreads.Add(t);
        }

        Thread e = new Thread(elevator.WorkDay);
        e.Start();

        foreach (var t in agentThreads) t.Join();
    }
}