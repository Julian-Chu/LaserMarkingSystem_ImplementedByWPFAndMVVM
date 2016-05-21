using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LMSystemState;
using NUnit.Framework;

namespace LMSystemState.Tests
{
    [TestFixture()]
    public class LMSystemTests
    {
        LMSystem lmSystem;
        [Test()]
        public void Constructor_LMSystem()
        {
            lmSystem = new LMSystem();
            lmSystem.CheckCurrentState();

            Assert.IsTrue(lmSystem.ReturnCurrentState() == typeof(DoorOutOfRange));            
        }

        [Test()]
        public void ChangeFromDoorOutOfRangeToDoorInRange()
        {
            lmSystem = new LMSystem();
            lmSystem.DoorInRange = true;
            lmSystem.CheckCurrentState();

            Assert.IsTrue(lmSystem.ReturnCurrentState() == typeof(DoorInRange));
        }

        [Test()]
        public void ChangeFromDoorInRangeToDoorIsLocked()
        {
            lmSystem = new LMSystem();
            lmSystem.DoorInRange = true;
            lmSystem.DoorIsLock = true;
            lmSystem.CheckCurrentState();
            Assert.IsTrue(lmSystem.ReturnCurrentState()==typeof(DoorIsLocked));
        }

        [Test()]
        public void ChangeFromDoorIsLockedToDoorInRange()
        {
            lmSystem = new LMSystem();
            lmSystem.DoorInRange = true;
            lmSystem.DoorIsLock = true;
            lmSystem.CheckCurrentState();
            Assert.IsTrue(lmSystem.ReturnCurrentState() == typeof(DoorIsLocked));

            lmSystem.DoorIsLock = false;
            lmSystem.CheckCurrentState();
            Assert.IsTrue(lmSystem.ReturnCurrentState() == typeof(DoorInRange));
        }

        
    }
}
