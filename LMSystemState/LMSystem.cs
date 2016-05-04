using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LMSystemState
{
    public class LMSystem
    {

        private SystemState currentState;

        bool markingIsFinished=false;
        public bool MarkingIsFinished
        {
            get { return markingIsFinished; }
            set { markingIsFinished = value; }
        }

        bool doorIsLock=false;
        public bool DoorIsLock
        {
            get { return doorIsLock; }
            set { doorIsLock = value; }
        }

        bool doorInRange=false;
        public bool DoorInRange
        {
            get { return doorInRange; }
            set { doorInRange = value; }
        }

        public LMSystem()
        {
            currentState = new DoorInRange();
        }


        public void SetState(SystemState systemState)
        {
            currentState = systemState;
            currentState.CheckCurrentState(this);
        }

        public void CheckCurrentState()
        {
            currentState.CheckCurrentState(this);
        }

        public Type ReturnCurrentState()
        {                        
            return currentState.GetType();           
        }

        
    }
}
