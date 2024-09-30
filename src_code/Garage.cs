using System;
using System.Collections.Generic;
using System.Linq;

namespace Ex03.GarageLogic
{
    public class Garage
    {
        private class CustomerInfo //only garage sees this, and all fields are public so garage sees all fields
        {
            public string FirstName;
            public string LastName;
            public string phone;
            public eVehicleStatus status;
            public Vehicle myVehicle;

            public override string ToString()
            {
                string str;

                str = string.Format(
@"Vehicle owner: {0} {1}
Phone number: {2}
Status of vehicle: {3}
{4}", FirstName, LastName, phone, status, myVehicle.ToString());

                return str ;
            }
        }

        private Dictionary<string, CustomerInfo> m_vehiclesInGarage;

        public Garage() 
        {
            m_vehiclesInGarage = new Dictionary<string, CustomerInfo>();
        }

        public bool IsVehicleInGarage(string i_LicenseNumber)
        {
            return m_vehiclesInGarage.ContainsKey(i_LicenseNumber);
        }

        public string GetFullVehicleInfo(string i_License)
        {
            return m_vehiclesInGarage[i_License].ToString();
        }

        public void AddCustomer(string i_FirstName, string i_LastName, string i_Phone,
            eVehicleType i_VehicleType , string i_License)
        {
            CustomerInfo customer = new CustomerInfo();
            
            customer.phone = i_Phone;
            customer.FirstName = i_FirstName;
            customer.LastName = i_LastName;
            customer.myVehicle = VehicleMaker.newVehicle(i_VehicleType);
            customer.status = eVehicleStatus.Repairing;
            customer.myVehicle.m_LicenseNumber = i_License;

            m_vehiclesInGarage.Add(i_License, customer);
        }

        public void InitVehicle(string i_Model, string i_LicenseNumber)
        {
            Vehicle newVehicle = m_vehiclesInGarage[i_LicenseNumber].myVehicle;

            newVehicle.m_Model = i_Model;
            newVehicle.m_LicenseNumber = i_LicenseNumber;
        }

        public void InitTires(string i_License, float i_TireAirPressure, string i_Manufacturer)
        {
            Tire[] tires = m_vehiclesInGarage[i_License].myVehicle.m_tires;

            if (i_TireAirPressure <= tires[0].MaxAirPressure && i_TireAirPressure > 0)
            {
                foreach (Tire tire in tires)
                {
                    tire.CurrentAirPressure = i_TireAirPressure;
                    tire.Manufacturer = i_Manufacturer;
                }
            }
            else
            {
                throw new ValueOutOfRangeException(tires[0].MaxAirPressure, 0);
            }
        }

        public void InitTankLevel(string i_License, float i_CurrentTankLevel) 
        {
            Vehicle vehicle = m_vehiclesInGarage[i_License].myVehicle;

            if (vehicle.Engine is FuelEngine)
            {
                (vehicle.Engine as FuelEngine).Refuel(i_CurrentTankLevel, (vehicle.Engine as FuelEngine).FuelType);
            }
            else if (vehicle.Engine is ElectricEngine)
            {
                (vehicle.Engine as ElectricEngine).ChargeAccumulator(i_CurrentTankLevel);
            }          
        }

        public void SetCar(string i_License, eCarColor i_CarColor, eCarDoorsNumber i_CarDoorsNumber)
        {
            (m_vehiclesInGarage[i_License].myVehicle as Car).CarColor = i_CarColor;
            (m_vehiclesInGarage[i_License].myVehicle as Car).CarDoorsNumber = i_CarDoorsNumber;
        }

        public void SetMotorcycle(string i_License, eMotorcycleLicenseType i_LicenseType, int i_EngineVolume)
        {
            (m_vehiclesInGarage[i_License].myVehicle as Motorcycle).LicenseType = i_LicenseType;
            (m_vehiclesInGarage[i_License].myVehicle as Motorcycle).EngineVolume = i_EngineVolume;
        }

        public void SetTruck(string i_License, bool i_IsCarryinghazardousMaterials, float i_CargoVolume)
        {
            (m_vehiclesInGarage[i_License].myVehicle as Truck).CarryinghazardousMaterials = i_IsCarryinghazardousMaterials;
            (m_vehiclesInGarage[i_License].myVehicle as Truck).CargoVolume = i_CargoVolume;
        }

        public void Refuel(string i_License, float i_AmountFuelToAdd, eFuelTypes i_TypeFuelToAdd)
        {

            (m_vehiclesInGarage[i_License].myVehicle.Engine as FuelEngine).Refuel(i_AmountFuelToAdd, i_TypeFuelToAdd);

        }

        public void ReCharge(string i_License, float i_AmountFuelToAdd)
        {
            (m_vehiclesInGarage[i_License].myVehicle.Engine as ElectricEngine).ChargeAccumulator(i_AmountFuelToAdd);
        }

        public void RefillTiresToMax(string i_License)
        {
            foreach (Tire tire in m_vehiclesInGarage[i_License].myVehicle.m_tires)
            {
                tire.InflateTire(tire.MaxAirPressure - tire.CurrentAirPressure);
            }
        }

        public void ChangeVehicleStatus(string i_License, eVehicleStatus i_NewStaus)
        {
            m_vehiclesInGarage[i_License].status = i_NewStaus;
        }

        public List<string> LicensesByFilter(eVehicleStatus i_Filter)
        {
            List<string> licensesStringByFilter = new List<string>();

            foreach (KeyValuePair<string, CustomerInfo> license in m_vehiclesInGarage)
            {
                if ((i_Filter!=eVehicleStatus.All && license.Value.status == i_Filter) || i_Filter == eVehicleStatus.All)
                {
                    licensesStringByFilter.Add(license.Key);
                }
            }

            return licensesStringByFilter;
        }
    }
}
