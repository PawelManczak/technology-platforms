namespace Lab3
{
    internal class Engine
    {
        public double displacment { get; set; }
        public double power { get; set; }
        public string model { get; set; }

        public Engine(double dis, int power, string model)
        {
            this.displacment = dis;
            this.power = power;
            this.model = model;
        }
    }
}