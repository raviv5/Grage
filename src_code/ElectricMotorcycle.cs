namespace Ex03.GarageLogic
{
    internal class ElectricMotorcycle : Motorcycle
    {
        private readonly float k_MaxHoursAccumulator = 2.8f;

        public float HoursLeftInAccumulator { get; set; }

        public ElectricMotorcycle()
        {
            Engine = new ElectricEngine();
            (Engine as ElectricEngine).MaxEnergylevel = k_MaxHoursAccumulator;
            m_tires = new Tire[k_NumberOfTires];
        }

        public float GetMaxAccumulatorHours()
        {
            return k_MaxHoursAccumulator;
        }

        public void ChargeAccumulator(float i_HoursToAdd)
        {
            if (i_HoursToAdd + HoursLeftInAccumulator <= k_MaxHoursAccumulator)
            {
                HoursLeftInAccumulator += i_HoursToAdd;
            }
        }

        public override string ToString()
        {
            return string.Format(
@"{0}
{1}
{2}", base.ToString(), (Engine as ElectricEngine).ToString(), m_tires[0].ToString());
        }
    }
}
