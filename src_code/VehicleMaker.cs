namespace Ex03.GarageLogic
{
    internal class VehicleMaker
    {
        public static Vehicle newVehicle(eVehicleType vehicleType)
        {
            Vehicle newVehicle = null;

            switch (vehicleType)
            {
                case eVehicleType.FuelMotorcycle:
                    newVehicle = new FuelMotorcycle();
                    break;
                case eVehicleType.ElectricMotorcycle:
                    newVehicle = new ElectricMotorcycle();
                    break;
                case eVehicleType.FuelCar:
                    newVehicle = new FuelCar();
                    break;
                case eVehicleType.ElectricCar:
                    newVehicle = new ElectricCar();
                    break;
                case eVehicleType.Truck:
                    newVehicle = new Truck();
                    break;
                default:
                    break;
            }

            newVehicle.Engine.CurrentEnergyLevel = 0;
            initTires(newVehicle);

            return newVehicle;
        }

        public static void initTires(Vehicle io_vehicle)
        {
            int i = 0;

            foreach (Tire tire in io_vehicle.m_tires)
            {
                io_vehicle.m_tires[i] = new Tire();
                io_vehicle.m_tires[i].MaxAirPressure = io_vehicle.GetMaxAirPressureOfTire();
                i++;
            }
        }
    }
}
