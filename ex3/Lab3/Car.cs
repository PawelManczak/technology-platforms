using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab3
{
    internal class Car
    {
        public string model { get; set; }
        public int year { get; set; }
        public Engine motor { get; set; }

        public Car(string model, Engine engine, int year)
        {
            this.model = model;
            motor = engine;
            this.year = year;
        }
    }
}
