using System;

namespace Ex03.GarageLogic
{
    internal class FuelEngine: Engine
    {
        public eFuelTypes FuelType { get; set; }

        public void Refuel(float i_AmountFuelToAdd, eFuelTypes i_TypeFuelToAdd)
        {
            if (i_TypeFuelToAdd == FuelType)
            {
                if (i_AmountFuelToAdd + CurrentEnergyLevel <= MaxEnergylevel && i_AmountFuelToAdd > 0)
                {
                    CurrentEnergyLevel += i_AmountFuelToAdd;
                }
                else
                {
                    throw new ValueOutOfRangeException(MaxEnergylevel - CurrentEnergyLevel, 0);
                }
            }
            else
            {
                throw new ArgumentException();
            }
        }

        public override string ToString()
        {
            string str = string.Format(
@"{0}
Fuel Type: {1}", base.ToString(), FuelType);

            return str ;
        }
    }
}
