using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.IO.Ports;
using System.Threading;

namespace BlockConditionsWindow.Model
{
    public class BlockConditionsWithSerialPort : Protocol.BlockConditions
    {
        SerialPort sp;

        public void SetSerialPort(SerialPort sp)
        {this.sp = sp;}

        public BlockConditionsWithSerialPort(SerialPort sp)
        {this.sp = sp;}

        public SerialPort GetCurrentSerialPort()
        {return this.sp;}

        public BlockConditionsWithSerialPort() { }

        public void DownloadBlockConditions()
        {
            try{
                sp.Open();
            // header: K3
            sp.WriteLine(HeaderToRequestBlockCondition + "," + this.ProgramNo + "," + this.BlockNo + Delimiter);
            Thread.Sleep(200);
            string ReturnBlockCondition = sp.ReadExisting();
            string[] BlockConditions = ReturnBlockCondition.Split(',');

            if (BlockConditions[1] == "0")
            {
                SortBlockConditions(ReturnBlockCondition);
            }
            else
                throw new Exception("Error");                
            }
            catch (System.IO.IOException ex) { throw ex; }
            catch (Exception ex) { throw ex; }
            finally{
                sp.Close();
            }
        }

        public void UploadBlockConditions(){
            try
            {
                sp.Open();
                sp.WriteLine(HeaderToSetBlockCondition + "," + this.ProgramNo + "," + this.BlockNo + "," + Setting + "," + Delimiter);
            }
            catch (System.IO.IOException ex) { throw ex; }
            catch (Exception ex) { throw ex; }
            finally
            {
                sp.Close();
            }
        }



    }
}
