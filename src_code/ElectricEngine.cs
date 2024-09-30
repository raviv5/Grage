namespace Ex03.GarageLogic
{
    internal class ElectricEngine: Engine
    {
        public void ChargeAccumulator(float i_HoursToAdd)
        {
            if (i_HoursToAdd + CurrentEnergyLevel <= MaxEnergylevel && i_HoursToAdd > 0)
            {
                CurrentEnergyLevel += i_HoursToAdd;
            }
            else
            {
                throw new ValueOutOfRangeException(MaxEnergylevel - CurrentEnergyLevel, 0);
            }
        }
    }
}
