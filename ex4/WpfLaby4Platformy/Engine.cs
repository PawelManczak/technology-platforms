using System;

using System.Xml.Serialization;
namespace WpfLaby4Platformy
{
    public class Engine : IComparable
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

        public override string ToString()
        {
            return $"{model} {displacment} ({power} hp)";
        }

        public int CompareTo(object obj)
        {
            Engine other = (Engine)obj;
            if (model.CompareTo(other.model) != 0)
            {
                return model.CompareTo(other.model);
            }
            else if (displacment.CompareTo(other.displacment) != 0)
            {
                return displacment.CompareTo(other.displacment);
            }
            return power.CompareTo(other.power);
        }
    }
}