namespace Ex03.GarageLogic
{
    internal abstract class Vehicle
    {
        public const int k_LicenseNumberMaxDigits = 20;

        public string m_Model;
        public string m_LicenseNumber;

        public abstract int GetNumberOfTires();

        public abstract int GetMaxAirPressureOfTire();

        public float CurrentTankLevel 
        {  
            get 
            { 
                return Engine.CurrentEnergyLevel; 
            } 
            set
            {
                Engine.CurrentEnergyLevel = value;
            }
        }

        public float m_EnergyPrecentage 
        { 
            get
            {
                return (Engine.CurrentEnergyLevel / Engine.MaxEnergylevel) * 100; 
            } 
        }

        public Engine Engine { get; set; }

        public Tire[] m_tires;

        public override string ToString()
        {
            string str;

            str = string.Format(
@"Model: {0}
License number: {1}
Energy precentage: {2}%", m_Model, m_LicenseNumber, m_EnergyPrecentage);

            return str;
        }
    }
}
