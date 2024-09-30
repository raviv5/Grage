namespace Ex03.GarageLogic
{
    internal class FuelMotorcycle : Motorcycle
    {
        private readonly eFuelTypes k_FuelType = eFuelTypes.Octan98;

        private readonly float k_MaxTankVolume = 5.8f;
        public float CurrentFuelAmountInLiters { get; set; }
        
        public FuelMotorcycle()
        {
            Engine = new FuelEngine();
            (Engine as FuelEngine).FuelType = k_FuelType;
            (Engine as FuelEngine).MaxEnergylevel = k_MaxTankVolume;
            m_tires = new Tire[k_NumberOfTires];
        }

        public eFuelTypes getFuelType()
        {
            return k_FuelType;
        }

        public float getMaxFuelLevelInLiter()
        {
            return k_MaxTankVolume;
        }

        public override string ToString()
        {
            return string.Format(
@"{0}
{1}
{2}",base.ToString() , (Engine as FuelEngine).ToString() , m_tires[0].ToString());
        }
    }
}
