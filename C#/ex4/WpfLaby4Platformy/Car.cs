using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace WpfLaby4Platformy
{
   
    [XmlType(TypeName = "car")]
    public class Car
    {
        public string model { get; set; }
        public int year { get; set; }

        [XmlElement(ElementName = "Engine")]
        public Engine motor { get; set; }

        public Car(string model, Engine engine, int year)
        {
            this.model = model;
            motor = engine;
            this.year = year;
        }

        public Car()
        {
            this.model = "";
            this.year = 0;
            this.motor = null;
        }

        public override string ToString()
        {
            return $"Model: {model}, Year: {year}, Engine: {motor}";
        }
    }
}
