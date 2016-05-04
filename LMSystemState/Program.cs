using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LMSystemState
{
    class Program
    {
        static void Main(string[] args)
        {
            LMSystem laserMarkingSystem = new LMSystem();
            Console.WriteLine("{0}\n",laserMarkingSystem.ReturnCurrentState());

            laserMarkingSystem.DoorInRange = true;
            laserMarkingSystem.CheckCurrentState();
            Console.WriteLine("{0}\n", laserMarkingSystem.ReturnCurrentState());

            laserMarkingSystem.DoorInRange = false;
            laserMarkingSystem.CheckCurrentState();
            Console.WriteLine("{0}\n", laserMarkingSystem.ReturnCurrentState());

            laserMarkingSystem.DoorIsLock = true;
            laserMarkingSystem.CheckCurrentState();
            Console.WriteLine("{0}\n", laserMarkingSystem.ReturnCurrentState());

            laserMarkingSystem.DoorInRange = true;
            laserMarkingSystem.CheckCurrentState();
            Console.WriteLine("{0}\n", laserMarkingSystem.ReturnCurrentState());

            Console.ReadKey();


        }
    }
}
