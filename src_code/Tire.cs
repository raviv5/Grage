using System.Security.Policy;

namespace Ex03.GarageLogic
{
    internal class Tire
    {
        public string Manufacturer {  get; set; }
        public float CurrentAirPressure { get; set; }
        public float MaxAirPressure { get; set; }

        public void InflateTire(float i_AmountAirToAdd)
        {
            if (i_AmountAirToAdd + CurrentAirPressure <= MaxAirPressure && i_AmountAirToAdd > 0)
            {
                CurrentAirPressure += i_AmountAirToAdd;
            }
            else
            {
                throw new ValueOutOfRangeException(MaxAirPressure, 0);
            }
        }

        public override string ToString()
        {
            return string.Format(
@"Tires manufacturer: {0}
Tires current air pressure: {1}", Manufacturer, CurrentAirPressure);
        }
    }
}
