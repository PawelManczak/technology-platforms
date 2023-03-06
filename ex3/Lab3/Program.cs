// See https://aka.ms/new-console-template for more information
using Lab3;

List<Car> myCars = new List<Car>(){
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

//1 a

var results1 = from e in myCars
               where e.model == "A6"
               select new
               {
                   engineType = e.motor.model == "TDI" ? "Diesel" : "Petrol",
                   hppl = e.motor.power / e.motor.displacment
               };

foreach (var result in results1)
{
    Console.WriteLine(result);
}
//1 b

var results2 = from e in results1
               group e.hppl by e.engineType;

foreach (var groupHeader in results2)
{
    int counter = 0;
    double result = 0.0d;
    foreach (var value in groupHeader)
    {
        result += value;
        counter++;
    }

    Console.WriteLine(groupHeader.Key + " " + (result/counter).ToString());
}
