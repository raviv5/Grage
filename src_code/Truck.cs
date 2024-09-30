namespace Ex03.GarageLogic
{
    internal class Truck : Vehicle
    {
        public bool CarryinghazardousMaterials {  get; set; }
        public float CargoVolume {  get; set; }

        private readonly int k_NumberOfTires = 12;
        private readonly int k_MaxAirPressure = 28;

        private readonly eFuelTypes k_FuelType = eFuelTypes.Soler;
        private readonly float k_MaxTankVolume = 110f;

        public Truck()
        {
            Engine = new FuelEngine();
            (Engine as FuelEngine).FuelType = k_FuelType;
            (Engine as FuelEngine).MaxEnergylevel = k_MaxTankVolume;
            m_tires = new Tire[k_NumberOfTires];
        }
        public override int GetNumberOfTires()
        { 
            return k_NumberOfTires;
        }

        public override int GetMaxAirPressureOfTire()
        {
            return k_MaxAirPressure;
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
            string str = string.Format(
@"{0}
Is carrying hazardous materials: {1}
Cargo volume: {2}
{3}
{4}", base.ToString(), CarryinghazardousMaterials, CargoVolume, (Engine as FuelEngine).ToString() , m_tires[0].ToString());

            return str;
        }
    }
}
