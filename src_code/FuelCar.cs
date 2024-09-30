namespace Ex03.GarageLogic
{
    internal class FuelCar : Car 
    {
        public readonly eFuelTypes k_FuelType = eFuelTypes.Octan95;
        
        public readonly float k_MaxTankVolume = 58f;

        public FuelCar()
        {
            Engine = new FuelEngine();
            (Engine as FuelEngine).FuelType = k_FuelType;
            (Engine as FuelEngine).MaxEnergylevel = k_MaxTankVolume;
            m_tires = new Tire[k_NumberOfTires];
        }

        public override string ToString()
        {
            return string.Format(
@"{0}
{1}
{2}", base.ToString(), (Engine as FuelEngine).ToString(), m_tires[0].ToString());
        }
    }
}
