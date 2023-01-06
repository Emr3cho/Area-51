using Area_51.Models.Enums;
using Area_51.Models.Generators;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Area_51.Models
{
    public class Agent
    {
        public int Id { get; private set; }
        public string FirstName { get; private set; }
        public string LastName { get; private set; }
        public int Age { get; private set; }
        public SecurityLevels SecurityLevel { get; private set; }
        public FloorTypes CurrentFloor { get; set; }

        public Agent(int id,string firstName, string lastName, int age, FloorTypes currentFloor, SecurityLevels securityLevel)
        {
            this.Id = id;
            this.FirstName = firstName;
            this.LastName = lastName;
            this.Age = age;
            this.CurrentFloor = currentFloor;
            this.SecurityLevel = securityLevel;
        }

        public void StartWorkDay(object elevator)
        {
            Elevator elv = (Elevator) elevator;
            
            elv.TryEnter(this);

            while (true) 
            { 
                var randomFloorToGo = AgentGenerator.RandomFloorType();
                elv.Move(randomFloorToGo);
                break;
            }
        }

        public override string ToString()
        {
            return Id + ". " + FirstName + " " + LastName;
        }
    }
}
