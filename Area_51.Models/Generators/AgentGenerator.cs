using Area_51.Models.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Area_51.Models.Generators
{
    public class AgentGenerator
    {
        public static IEnumerable<Agent> CreateAgents(int count)
        {
            List<Agent> agents = new List<Agent>();

            for (int i = 0; i < count; i++)
            {
                int id = i + 1;
                string fullName = RandomFullNameGenerator();
                string firstName = fullName.Split(" ")[0];
                string lastName = fullName.Split(" ")[1];
                int age = AgeGenerator();
                SecurityLevels securityLevel = SecurityLevelGenerator();
                FloorTypes currentFloor = FloorGenerator(securityLevel);
                Agent agent = new Agent(id, firstName, lastName, age, currentFloor, securityLevel);

                agents.Add(agent);
            }

            return agents;
        }

        private static string RandomFullNameGenerator()
        {
            string[] agentNames = new string[]
            {
                "Nayden Vitanov",
                "Deyan Tanev",
                "Desislav Petkov",
                "Dyakon Hristov",
                "Milen Todorov",
                "Aleksander Kishishev",
                "Ilian Stoev",
                "Milen Balkanski",
                "Kostadin Nakov",
                "Petar Strashilov",
                "Bozhidara Valova",
                "Lyubina Kostova",
                "Radka Antonova",
                "Vladimira Blagoeva",
                "Bozhidara Rysinova",
                "Borislava Dimitrova",
                "Anelia Velichkova",
                "Violeta Kochanova",
                "Lyubov Ivanova",
                "Blagorodna Dineva",
                "Desislav Bachev",
                "Mihael Borisov",
                "Ventsislav Petrova",
                "Hristo Kirilov",
                "Penko Dachev",
                "Nikolai Zhelyaskov",
                "Petar Tsvetanov",
                "Spas Dimitrov",
                "Stanko Popov",
                "Miro Kochanov",
                "Pesho Stamatov",
                "Roger Porter",
                "Jeffrey Snyder",
                "Louis Coleman",
                "George Powell",
                "Jane Ortiz",
                "Randy Morales",
                "Lisa Davis",
            };

            return agentNames[Random.Shared.Next(0, agentNames.Length - 1)];
        }

        private static int AgeGenerator() => Random.Shared.Next(18, 65);

        private static SecurityLevels SecurityLevelGenerator()
        {
            List<SecurityLevels> securityLevels = Enum.GetValues(typeof(SecurityLevels))
                .Cast<SecurityLevels>()
                .ToList();

            return securityLevels[Random.Shared.Next(0, securityLevels.Count)];
        }

        public static FloorTypes FloorGenerator(SecurityLevels securityLevel)
        {
            List<FloorTypes> securityLevels = Enum.GetValues(typeof(FloorTypes))
                .Cast<FloorTypes>()
                .ToList();

            FloorTypes currentFloorType = securityLevels[0];

            if (securityLevel == SecurityLevels.Secret)
            {
                currentFloorType = securityLevels[Random.Shared.Next(0, 2)];
            }
            else if (securityLevel == SecurityLevels.TopSecret)
            {
                currentFloorType = securityLevels[Random.Shared.Next(0, 4)];
            }

            return currentFloorType;
        }

        public static FloorTypes RandomFloorType()
        {
            return Enum.GetValues(typeof(FloorTypes))
                .Cast<FloorTypes>()
                .ToList()[Random.Shared.Next(0,4)];
        }
    }
}
