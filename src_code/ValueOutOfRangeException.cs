using System;

namespace Ex03.GarageLogic
{
    public class ValueOutOfRangeException : Exception
    {
        public float MaxValue { get; private set; }
        public float MinValue { get; private set; }

        public ValueOutOfRangeException(float i_MaxValue, float i_MinValue)
        {
            MaxValue = i_MaxValue;
            MinValue = i_MinValue;
        }
    }
}
