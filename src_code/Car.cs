namespace Ex03.GarageLogic
{
    internal class Car : Vehicle
    {
        public eCarColor CarColor {  get; set; }
        
        public eCarDoorsNumber CarDoorsNumber {  get; set; }

        protected readonly int k_NumberOfTires = 5;
        protected readonly int k_MaxAirPressure = 30;

        public override int GetNumberOfTires()
        {
            return k_NumberOfTires;
        }

        public override int GetMaxAirPressureOfTire()
        {
            return k_MaxAirPressure;
        }

        public override string ToString()
        {
            return string.Format(
@"{0}
Car color: {1}        
Number of doors: {2}", base.ToString(), CarColor, CarDoorsNumber);
        }
    }
}
