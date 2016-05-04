using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LMSystemState
{
    public abstract class SystemState
    {
        public abstract void CheckCurrentState(LMSystem lmSystem);        
    }

    public class DoorOutOfRange:SystemState
    {
        public override void CheckCurrentState(LMSystem lmSystem)
        {
            if(lmSystem.DoorInRange==true)
            {
                lmSystem.SetState(new DoorInRange());
            }                       
        }
    }

    public class DoorInRange:SystemState
    {
        public override void CheckCurrentState(LMSystem lmSystem)
        {
            if(lmSystem.DoorInRange==false)
            {
                lmSystem.SetState(new DoorOutOfRange());
            }
            else if (lmSystem.DoorIsLock==true)
            {
                lmSystem.SetState(new DoorIsLocked());
            }  
        }
    }

    public class DoorIsLocked:SystemState
    {
        public override void CheckCurrentState(LMSystem lmSystem)
        {
            if(lmSystem.DoorIsLock==false || lmSystem.MarkingIsFinished==true)
            {
                lmSystem.SetState(new DoorInRange());
                lmSystem.MarkingIsFinished = false;
            }

        }
    }
}
