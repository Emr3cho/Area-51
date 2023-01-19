using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Channels;
using System.Threading.Tasks;
using Area_51.Models.Enums;
using Area_51.Models.Generators;

namespace Area_51.Models
{
    public class Elevator
    {
        private const int capacity = 1;
        private Floor currentFloor;
        private Agent agentInElevator;

        Semaphore semaphore;

        public Elevator()
        {
            currentFloor = new Floor(FloorTypes.G);
            semaphore = new Semaphore(capacity, capacity);
        }

        public void WorkDay()
        {
            Console.WriteLine("Area 51 is open.");
        }

        public bool TryEnter(Agent agent)
        {
            Console.WriteLine(agent.Id + ". " + agent.FirstName + " " + agent.LastName + " is waiting!");
            if (semaphore.WaitOne())
            {
                Console.WriteLine($"Agent {agent} entered in floor {agent.CurrentFloor}");
                agentInElevator = agent;
                currentFloor.FloorType = agentInElevator.CurrentFloor;
                return true;
            }
            else
            {
                Console.WriteLine(agentInElevator + " is waiting!");
                return false;
            }
        }

        private void Leave()
        {
            if (currentFloor.CanAccess(agentInElevator))
            {
                Console.WriteLine($"Agent {agentInElevator} leaving elevator in floor {currentFloor.FloorType}!");
                semaphore.Release();
            }
            else
            {
                Console.WriteLine($"(CurrentFloor - {currentFloor.FloorType}) Access denied!");
                Console.WriteLine("Changing where to go!");
                Move(AgentGenerator.FloorGenerator(agentInElevator.SecurityLevel));

            }
        }

        public void Move(FloorTypes to)
        {
            int difference;
            Console.WriteLine($"Agent {agentInElevator} with security level {agentInElevator.SecurityLevel} wants to go to floor {to}");
            if (currentFloor.FloorType > to)
            {
                difference = currentFloor.FloorType - to;
                for (int i = 0; i < difference; i++)
                {
                    Console.WriteLine($"(CurrentFloor - {currentFloor.FloorType} ) Agent {agentInElevator} is going down!");
                    GoDown();
                }
            }else if (currentFloor.FloorType < to)
            {
                difference = to - currentFloor.FloorType;
                for (int i = 0; i < difference; i++)
                {
                    Console.WriteLine($"(CurrentFloor - {currentFloor.FloorType} ) Agent {agentInElevator} is going up!");
                    GoUp();
                }
            }
            Leave();
        }

        private void GoUp()
        {
            Thread.Sleep(1000);
            switch (currentFloor.FloorType)
            {
                case FloorTypes.G: currentFloor.FloorType = FloorTypes.S; break;
                case FloorTypes.S: currentFloor.FloorType = FloorTypes.T1; break;
                case FloorTypes.T1: currentFloor.FloorType = FloorTypes.T2; break;
                case FloorTypes.T2: break;
            }
        }

        private void GoDown()
        {
            Thread.Sleep(1000);
            switch (currentFloor.FloorType)
            {
                case FloorTypes.T2: currentFloor.FloorType = FloorTypes.T1; break;
                case FloorTypes.T1: currentFloor.FloorType = FloorTypes.S; break;
                case FloorTypes.S: currentFloor.FloorType = FloorTypes.G; break;
                case FloorTypes.G: break;
            }
        }
    }
}
