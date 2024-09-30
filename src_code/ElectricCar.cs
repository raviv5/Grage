namespace Ex03.GarageLogic
{
    internal class ElectricCar : Car
    {
        private readonly float k_MaxHoursAccumulator = 4.8f;

        public ElectricCar()
        {
            Engine = new ElectricEngine();
            (Engine as ElectricEngine).MaxEnergylevel = k_MaxHoursAccumulator;
            m_tires = new Tire[k_NumberOfTires];
        }

        public float GetMaxAccumulatorHours()
        {
            return k_MaxHoursAccumulator;
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
