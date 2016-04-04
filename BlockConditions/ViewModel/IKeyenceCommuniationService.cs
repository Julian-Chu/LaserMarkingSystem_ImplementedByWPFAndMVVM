using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO.Ports;

namespace BlockConditionsWindow.ViewModel
{
    interface IKeyenceCommuniationService
    {
        void Upload(BlockConditionsWindow.Model.BlockConditions bCs);
        void Download(BlockConditionsWindow.Model.BlockConditions bCs);
    }

    public class KeyenceCommunicationService:IKeyenceCommuniationService
    {
        SerialPort sp;

        public KeyenceCommunicationService(SerialPort sp)
        {
            this.sp = sp;
        }

        public KeyenceCommunicationService()
        {
            this.sp = new SerialPort();
        }

        public void SetSerialPort(SerialPort sp)
        {
            this.sp = sp;
        }
        
        public void Upload(BlockConditionsWindow.Model.BlockConditions bCs)
        {
            try
            {
                sp.Open();
                sp.WriteLine(bCs.HeaderToSetBlockCondition + "," + bCs.ProgramNo + "," + bCs.BlockNo + "," + bCs.Setting + "," + bCs.Delimiter);
            }
            catch (System.IO.IOException ex) { throw ex; }
            catch (Exception ex) { throw ex; }
            finally
            {
                sp.Close();
            }
        }

        public void Download(BlockConditionsWindow.Model.BlockConditions bCs)
        {
            try
            {
                sp.Open();
                sp.WriteLine(bCs.HeaderToRequestBlockCondition + "," + bCs.ProgramNo + "," + bCs.BlockNo + bCs.Delimiter);
                var waitingForResponce=Task.Delay(250);
                waitingForResponce.Wait();
                string ReturnBlockCondition = sp.ReadExisting();
                string[] BlockConditions = ReturnBlockCondition.Split(',');

                if (BlockConditions[1] == "0")
                {
                    bCs.SortBlockConditions(ReturnBlockCondition);
                }
                else
                    throw new Exception("Error");
            }
            catch (System.IO.IOException ex) { throw ex; }
            catch (Exception ex) { throw ex; }
            finally
            {
                sp.Close();
            }
        }

        public void SetSerialport(SerialPort sp)
        { this.sp = sp; }

        public SerialPort GetCurrentSerialPort()
        { return this.sp; }
    }

    public class MockKeyenceCommunicationService : IKeyenceCommuniationService
    {
        public string ReturnBlockCondition;
        public void Upload(Model.BlockConditions bCs)
        {
            
        }

        public void Download(BlockConditionsWindow.Model.BlockConditions bCs)
        {
            string[] BlockConditions = ReturnBlockCondition.Split(',');

            if (BlockConditions[1] == "0")
            {
                bCs.SortBlockConditions(ReturnBlockCondition);
            }
            else
                throw new Exception("Error");
        }

        public MockKeyenceCommunicationService(string returnedBlockCondition)
        {
            this.ReturnBlockCondition = returnedBlockCondition;
        }
    }

}
