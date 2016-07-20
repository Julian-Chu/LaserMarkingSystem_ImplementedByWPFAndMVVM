using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO.Ports;

namespace CommonMarkingConditionsModule.DataAccessLayer
{
    public interface IKeyenceCommunication
    {
        void Upload(CommonMarkingConditionsModule.Model.CommonMarkingConditions cmc);
        void Download(CommonMarkingConditionsModule.Model.CommonMarkingConditions cmc);        
    }

    public class KeyenceCommunicationService_Serialport:IKeyenceCommunication
    {
        SerialPort sp;
        readonly string NoError = "0";
        protected string[] delimiterString = new string[] { "\r", "," };

        public KeyenceCommunicationService_Serialport(SerialPort sp)
        {
            this.sp = sp;
        }

        public void Upload(CommonMarkingConditionsModule.Model.CommonMarkingConditions cmcs)
        {
            try
            {
                sp.Close();
                sp.Open();
                sp.WriteLine(cmcs.HeaderToSetCommonMarkingConditionsInLM + "," + cmcs.ProgramNo + "," + cmcs.SettingToLMController + cmcs.Delimiter);  //(K0,parameters...\r)
                Task.Delay(200).Wait();

                string ReturnedCommonMarkingConditions= sp.ReadExisting();

                string[] CommonMarkingConditions = ReturnedCommonMarkingConditions.Split(delimiterString, System.StringSplitOptions.RemoveEmptyEntries);

                if (CommonMarkingConditions[1] == NoError) //no error
                {
                    System.Windows.MessageBox.Show("Upload is finished!");
                }
                else
                {
                    //For iteration and incremental 2
                    Model.ErrorCode _errorCode = new Model.ErrorCode();
                    _errorCode.CommunicationErrorMsg(ReturnedCommonMarkingConditions);
                    string error = CommonMarkingConditions[2] + _errorCode.CommunicationErrorMsg(ReturnedCommonMarkingConditions);
                    //ErrorLog function............
                }
            }
            catch (Exception ex)
            {
                System.Windows.MessageBox.Show(ex.Message);
            }
            finally
            { sp.Close(); }
        }

        public void Download(CommonMarkingConditionsModule.Model.CommonMarkingConditions cmcs)
        {
            try
            {
                sp.Close();
                sp.Open();
                string command = cmcs.HeaderToRequestCommonMarkingConditionsFromLM + "," + cmcs.ProgramNo + cmcs.Delimiter;
                sp.WriteLine(command);  //(K1,xxxx\r)
                Task.Delay(250).Wait();

                string responseFromPort = sp.ReadExisting();
                //Thread.Sleep(250);

                string[] responses = responseFromPort.Split(delimiterString, System.StringSplitOptions.RemoveEmptyEntries);

                if (responses[1] == NoError) //no error
                {
                    cmcs.SettingFromLMController = responseFromPort;
                }
                else
                {
                    Model.ErrorCode _errorCode = new Model.ErrorCode();
                    _errorCode.NoErrorExists(responseFromPort);
                }
            }
            catch (System.IO.IOException ex) { throw new System.IO.IOException(ex.Message); }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            finally
            {
                sp.Close();
            }
        }
    }    
}
