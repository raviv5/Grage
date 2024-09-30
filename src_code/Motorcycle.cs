namespace Ex03.GarageLogic
{
    internal class Motorcycle : Vehicle
    {
        public eMotorcycleLicenseType LicenseType {  get; set; }
        public int EngineVolume {  get; set; }

        protected readonly int k_NumberOfTires = 2;
        protected readonly int k_MaxAirPressure = 29;

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
            return  string.Format(
@"{0}
License type: {1}
Engine volume: {2}", base.ToString(), LicenseType, EngineVolume);
        }
    }
}
