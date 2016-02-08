using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;


namespace LaserMarking.Protocol
{
    class SerialNumberForASG
    {

        public SerialNumberForASG(string SN1,string SN2)
        {
            this.SerialNumber1 = SN1;
            this.SerialNumber2 = SN2;
        }

        private string _serialNumber1="";
        public string SerialNumber1
        {
            get { return _serialNumber1; }
            set {  _serialNumber1 = value; }
        }

        private string _serialNumber2="";
        public string SerialNumber2
        {
            get { return _serialNumber2; }
            set { _serialNumber2 = value; }
        }

        private string _project="";
        public string Project
        {
            get { return _project; }
            set
            {
                if (value.Length == 5) _project = value;
                else MessageBox.Show("Invalid ProjectNumber!!");
            }
        }

        private string _productionPlace="";
        public string ProductionPlace
        {
            get { return _productionPlace; }
            set { _productionPlace = value; }
        }

        private string _productionLine = "";
        public string ProductionLine
        {
            get { return _productionLine; }
            set { _productionLine = value; }
        }

        private string _moldNumber="";
        public string MoldNumber
        {
            get { return _moldNumber; }
            set { _moldNumber = value; }
        }

        private string _cavityNumber="";
        public string CavityNumber
        {
            get { return _cavityNumber; }
            set { _cavityNumber = value; }
        }

        private string _productionYear="";
        public string ProductionYear
        {
            get { return _productionYear; }
            set
            {
                int result;
                if (int.TryParse(value, out result) && result >= 1900 && result < 2100)
                { _productionYear = value; }
                else
                    MessageBox.Show("Invalid Year! Please input year between 1900-2099");
            }
        }

        private string _productionWeek="";
        public string ProductionWeek
        { 
            get { return _productionWeek; }
            set { _productionWeek = value; }
        }

        private string _serialNumber="";
        public string SerialNumber
        { 
            get { return _serialNumber; }
            set { _serialNumber = value; }
        }

        public void GeneratePropertyFromSerialNumber()
        {
            //Assign the value to properties based on Serial Number1
            Project = "40" + SerialNumber1.Substring(0,3);
            ProductionPlace = SerialNumber1.Substring(3, 1);
            ProductionLine = SerialNumber1.Substring(4, 1);
            MoldNumber = SerialNumber1.Substring(5, 1);
            CavityNumber = SerialNumber1.Substring(6, 1);


            //Assign the value to properties based on Serial Number2
            ProductionYear = "20" + SerialNumber2.Substring(0, 2);
            ProductionWeek = SerialNumber2.Substring(2, 2);
            SerialNumber = SerialNumber2.Substring(4);
        }


        public void GenerateNewSerialNumber()
        {
            try
            {
                SerialNumber1 = Project.Substring(2, 3) + ProductionPlace + ProductionLine + MoldNumber + CavityNumber;
                SerialNumber2 = ProductionYear.Substring(2,2) + ProductionWeek + SerialNumber;
            }

            catch(ArgumentOutOfRangeException)
            { MessageBox.Show("Invalid value!! \r\nPlease check the value!"); }

            catch(Exception ex)
            {
                MessageBox.Show(ex.ToString());                
            }
        
        }
    }
}
