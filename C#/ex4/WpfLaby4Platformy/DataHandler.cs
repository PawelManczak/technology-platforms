using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WpfLaby4Platformy
{
    internal class DataHandler
    {
        public static List<Car> myCars = new List<Car>(){
                new Car("E250", new Engine(1.8, 204, "CGI"), 2009),
                new Car("E350", new Engine(3.5, 292, "CGI"), 2009),
                new Car("A6", new Engine(2.5, 187, "FSI"), 2012),
                new Car("A6", new Engine(2.8, 220, "FSI"), 2012),
                new Car("A6", new Engine(3.0, 295, "TFSI"), 2012),
                new Car("A6", new Engine(2.0, 175, "TDI"), 2011),
                new Car("A6", new Engine(3.0, 309, "TDI"), 2011),
                new Car("S6", new Engine(4.0, 414, "TFSI"), 2012),
                new Car("S8", new Engine(4.0, 513, "TFSI"), 2012)
        };


        public static void exercise1()
        {
            var methodBasedSyntaxQuery = myCars
                .Where(c => c.model == "A6")
                .Select(car =>
                    new
                    {
                        engineType = car.motor.model == "TDI" ? "Diesel" : "Petrol",
                        hppl = car.motor.power / car.motor.displacment,
                    })
                    .GroupBy(elem => elem.engineType)
                    .Select(elem => new
                    {
                        name = elem.First().engineType.ToString(),
                        value = elem.Average(s => s.hppl).ToString()
                    })
                    .OrderByDescending(t => t.value)
                    .Select(e => e);
 

            var queryExpresionSyntax = from elem
                                        in (from car in myCars
                                            where car.model == "A6"
                                            select new
                                            {
                                                engineType = car.motor.model == "TDI" ? "Diesel" : "Petrol",
                                                hppl = car.motor.power / car.motor.displacment,
                                            })
                                       group elem by elem.engineType into elemGrouped
                                       select new
                                       {
                                           name = elemGrouped.First().engineType.ToString(),
                                           value = elemGrouped.Average(s => s.hppl).ToString()
                                       } into elemSelected
                                       orderby elemSelected.value descending
                                       select elemSelected;


            Console.WriteLine("Method-based Syntax:");
            foreach (var e in methodBasedSyntaxQuery) Console.WriteLine(e);
            Console.WriteLine("Query Expression Syntax:");
            foreach (var e in queryExpresionSyntax) Console.WriteLine(e);

        }
        private static int CompareCarsPowers(Car car1, Car car2)
        {
            if (car1.motor.power >= car2.motor.power)
                return 1;
            else
                return -1;
            
        }
        private static bool IsTDI(Car car)
        {
            return car.motor.model == "TDI";
        }

        private static void arg3MessageBox(Car car)
        {
            DialogResult result = MessageBox.Show(car.ToString(), "ex3", MessageBoxButtons.OK);
        }

        delegate int CompareCarsPowerDelegate(Car car1, Car car2);
        public static void exercise2()
        {

            Console.WriteLine("\n\n exercise 2 a\n");


            List <Car> myCarsCopy = new List<Car>(myCars);
            CompareCarsPowerDelegate arg1 = CompareCarsPowers;

            myCarsCopy.Sort(new Comparison<Car>(arg1));
            foreach (var e in myCarsCopy) Console.WriteLine(e.ToString());

            Console.WriteLine("\n\n exercise 2 b\n");
            Predicate<Car> arg2 = IsTDI;
            foreach (var e in myCarsCopy.FindAll(arg2)) Console.WriteLine(e.ToString());

            Console.WriteLine("\n\n exercise 2 c\n");

            Action<Car> arg3 = arg3MessageBox;
            myCars.ForEach(arg3);

        }
    }

    
}
    

