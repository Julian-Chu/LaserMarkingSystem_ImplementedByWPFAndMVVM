using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;
using System.IO;
using System.Windows;
using ProgramNoSetting.Controller;
using BlockConditionsWindow.Model;


namespace LaserMarking
{
    public class SerializationAndDeserialzation
    {
        public CommonMarkingConditionsWithSerialPort commonMarkingConditions;
        public List<BlockConditionsWithSerialPort> blockConditionsList;

        public SerializationAndDeserialzation(CommonMarkingConditionsWithSerialPort commonMarkingConditions,
             List<BlockConditionsWithSerialPort> blockConditions)
        {
            this.commonMarkingConditions = commonMarkingConditions;
            this.blockConditionsList = blockConditions;
        }

        public SerializationAndDeserialzation() //constructor without args for serialization
        {}


        public void Serialize(CommonMarkingConditionsWithSerialPort commonMarkingConditions, List<BlockConditionsWithSerialPort> blockConditions)
        {
            string fileName = "LaserMarking.xml";
            string path = Path.Combine(Environment.CurrentDirectory, @"Data\", fileName);
            XmlSerializer serializer3 = new XmlSerializer(typeof(SerializationAndDeserialzation));
            using (TextWriter writer = new StreamWriter(path))
            {
                try
                {
                    SerializationAndDeserialzation _serialize = new LaserMarking.SerializationAndDeserialzation(commonMarkingConditions, blockConditions);
                    serializer3.Serialize(writer, _serialize);
                }
                catch (Exception ex)
                { MessageBox.Show(ex.ToString()); }
            }
        }

        public SerializationAndDeserialzation Deserialize()
        {
            object obj;
            XmlSerializer deserializer = new XmlSerializer(typeof(SerializationAndDeserialzation));
            string fileName = "LaserMarking.xml";
            string path = Path.Combine(Environment.CurrentDirectory, @"Data\", fileName);
            using (StreamReader reader = new StreamReader(path))
            {
                obj = deserializer.Deserialize(reader);            
            }
                SerializationAndDeserialzation s = (SerializationAndDeserialzation)obj;
                return s;
        }
    }
}
