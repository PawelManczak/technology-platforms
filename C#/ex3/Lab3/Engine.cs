using System.Xml.Serialization;

namespace Lab3
{
    public class Engine
    {
        public double displacment { get; set; }
        public double power { get; set; }
        [XmlAttribute]
        public string model { get; set; }

        public Engine(double dis, int power, string model)
        {
            this.displacment = dis;
            this.power = power;
            this.model = model;
        }
        public Engine() { 
            this.displacment = 0;
            this.power = 0;
            this.model = "";
        }
    }
}