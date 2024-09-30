namespace Ex03.GarageLogic
{
    internal abstract class Engine
    {
        public float CurrentEnergyLevel { get; set; }

        public float MaxEnergylevel { get; set; }

        public override string ToString()
        {
            string str = string.Format(
@"Current Energy Level: {0}", CurrentEnergyLevel);

            return str;
        }
    }
}
