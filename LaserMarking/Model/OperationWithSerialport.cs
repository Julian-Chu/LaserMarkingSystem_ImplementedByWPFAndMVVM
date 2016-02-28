using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO.Ports;
using System.Windows.Controls;
using System.Timers;


namespace LaserMarking.Model
{
    class OperationWithSerialport:Protocol.Operation
    {
        SerialPort sp;
        object _locker;
        ListBox _listbox;   //listbox to show message

        public OperationWithSerialport(SerialPort sp, ListBox listbox)
        {
            this.sp = sp;
            _locker=new object();
            this._listbox = listbox;            
        }

        public override string RequestREADYStatus()
        {
            return SendCommand(CommandToRequestREADYStatus) ;            
        }

        public override string SwitchProgramNo()
        {
            return SendCommand(CommandToSwitchProgramNo) ;            
        }

        private string SendCommand(string Command)
        {
            try
            {
                sp.Close();              

                if (sp.IsOpen == false)
                    sp.Open();
                sp.WriteLine(Command+'\r');
                //System.Threading.Thread.Sleep(100);
                // Protocol.ErrorCode.IsResponseTimeOut();
                #region
                /*
                string response = sp.ReadExisting();
                sp.Close();
                string[] responseArray = response.Split(',');

                if (responseArray[1] == "0")
                    return "0";
                else
                {
                    Protocol.ErrorCode _error = new Protocol.ErrorCode();
                    _error.NoErrorExists(response);

                    return responseArray[1];
                }
                 * */
                #endregion

                string response = ReadResponse().Result;
                return response;
            }
            catch (IndexOutOfRangeException) { return "Illegal Response or No Response!"; }
            catch (Exception e){return e.Message;}
        }

        async private Task<string> ReadResponse()
        {
            try
            {
                await Task.Delay(200).ConfigureAwait(false);                
                string response = sp.ReadExisting();
                sp.Close();
                string[] responseArray = response.Split(',','\r');

                if (responseArray[1] == "0") return "0";
                else
                {
                    Protocol.ErrorCode _error = new Protocol.ErrorCode();
                    _error.NoErrorExists(response);
                    return responseArray[1];
                }
            }

            catch (IndexOutOfRangeException) { return "Illegal Response or No Response!"; }
            catch (Exception e) {return e.Message;}
        }

        void _timer_Elapsed(object sender, ElapsedEventArgs e)
        {
            System.Windows.MessageBox.Show("Time is up");
        }

        public override void MarkingStart()
        {
            try
            {
                sp.Close();            

                if (sp.IsOpen == false)
                    sp.Open();
                sp.DataReceived += sp_DataReceived;
                sp.WriteLine(MarkingStartCommand);                

                Protocol.ErrorCode.IsResponseTimeOut();

                string response = sp.ReadExisting();
                sp.Close();
                string[] responseArray = response.Split(',');
                /*
                lock (_locker)
                {
                    SentCommandToContoller = false;
                }*/
            }
            catch (Exception e) { Console.Write(e.Message); }
        }

        void sp_DataReceived(object sender, SerialDataReceivedEventArgs e)
        {
            SerialPort sp = sender as SerialPort;
            string response = sp.ReadExisting();

            Protocol.ErrorCode _error = new Protocol.ErrorCode();
            if(!_error.NoErrorExists(response))
            { _listbox.Items.Add("Marking Finished!"); }           
        }                
     
        async public Task StartMarkingProcess()
        {
            var result = RequestREADYStatus();
            switch(result)
            {
                case "0":
                _listbox.Items.Add("READY ON");
                //System.Threading.Thread.Sleep(100);
                _listbox.Items.Add("Marking Start");
                //MarkingStart();
                    break;
                case "1":
                    _listbox.Items.Add("READY OFF(An error has occurred or the controller is under control of terminal block");
                    //btn.IsEnabled = true;
                    break;                  
                case "2":
                    _listbox.Items.Add("READY OFF(Program expansion or marking is in progress)");
                    //btn.IsEnabled = true;
                    break;
                default:
                    _listbox.Items.Add(result);
                    break;
            }
        }
    }
}
