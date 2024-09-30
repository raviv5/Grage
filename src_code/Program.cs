using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using Ex03.GarageLogic;

namespace Ex03.ConsoleUI
{
    public class Program
    {
        const int k_LicenseNumberMaxDigits = 10;
        private static Garage m_garage = new Garage();

        public static void Main()
        {
            run();
        }

        public static void run()
        {
            eOption choice;

            WelcomeMsg();
            do
            {
                Menu();
                choice = GetOption();
                OperateOption(choice);
            }
            while (choice != eOption.Exit);
        }

        public enum eOption
        {
            AddVehicle = 1,
            LicensesShowen,
            ChangeStatues,
            Inflate,
            Refuel,
            Recharge,
            ShowVehicleDetails,
            Exit
        }
        public static void WelcomeMsg()
        {

            Console.WriteLine(
@"Welcome to our garage !
how can we help you ?");
        }
        public static void ExitMsg()
        {
            Console.WriteLine(
@"Bye bye !
See you next time !
");
        }
        public static void Menu()
        {
            string[] options = { "Enter new vehicle to garage", "Show all license numbers",
                                 "Change vehicle status", "Inflate vehicle tires", "Refuel vehicle", "Recharge vehicle", "Show vehicle full details", "EXIT" };
            int i = 1;

            Console.WriteLine(@"
Menu:");
            foreach (string option in options)
            {
                Console.WriteLine($"{i++}.{option}");
            }
        }
        public static eOption GetOption()
        {

            return (eOption)getOptionFromEnumList(typeof(eOption));
        }
        public static void OperateOption(eOption i_SelectOption)
        {
            switch (i_SelectOption)
            {

                case eOption.AddVehicle: 
                    {
                        addVehicle();
                        break;
                    }
                case eOption.LicensesShowen:  
                    {
                        showLicenses();
                        break;
                    }
                case eOption.ChangeStatues:
                    {
                        changeStatus();
                        break;
                    }
                case eOption.Inflate:
                    {
                        inflateTires();
                        break;
                    }
                case eOption.Refuel:
                    {
                        refuel();
                        break;
                    }
                case eOption.Recharge:
                    {
                        recharge();
                        break;
                    }
                case eOption.ShowVehicleDetails:
                    {
                        showFullDetails();
                        break;
                    }
                case eOption.Exit:
                    {
                        ExitMsg();
                        break;
                    }
                default:
                    {
                        break;
                    }
            }
        }


        //from here all should be private
        private static void addVehicle()
        {
            string license = getLicenseNumber();

            if (m_garage.IsVehicleInGarage(license))
            {
                Console.WriteLine("Vehicle already in garage!");
            }
            else
            {
                string CostumerFirstName = getAllLetterName("First Name: ");
                string CostumerLastName = getAllLetterName("Last Name: ");
                string CostumerPhone = getPhoneNumber();
                eVehicleType CostumerVehicle = getVehicleType();

                m_garage.AddCustomer(CostumerFirstName, CostumerLastName, CostumerPhone, CostumerVehicle, license);

                string vehicleModel = getNoneEmptyString("vehicle Model: ");

                m_garage.InitVehicle(vehicleModel, license);
                initTires(license);
                initTank(license);

                switch (CostumerVehicle)
                {
                    case eVehicleType.FuelCar:
                    case eVehicleType.ElectricCar:
                        {
                            eCarColor carColor = getCarColor(); 
                            eCarDoorsNumber numberOfDoors = getNumberOfDoors(); 

                            m_garage.SetCar(license, carColor, numberOfDoors);
                            break;
                        }
                    case eVehicleType.FuelMotorcycle:
                    case eVehicleType.ElectricMotorcycle:
                        {
                            eMotorcycleLicenseType MotorcycleLicense = getMotorcycleLicense(); 
                            int EngineVolume = getEngineVolume(); 

                            m_garage.SetMotorcycle(license, MotorcycleLicense, EngineVolume);
                            break;
                        }
                    case eVehicleType.Truck:
                        {
                            bool isDangerousCargo = isDangerCargo();
                            float cargoVolume = getFloatType("Cargo volume: ");
                            m_garage.SetTruck(license, isDangerousCargo, cargoVolume);
                            break;
                        }
                }

                Console.WriteLine("The vehicle was successfully added!");
            }           
        }

        private static void initTires(string io_License)
        {
            string tireManufacturer = getNoneEmptyString("Tire manufacturer: ");
            float currentAirPressure;
            bool inputInRange;

            do
            {
                inputInRange = true;
                currentAirPressure = getFloatType("Air pressure in tires: ");

                try
                {
                    m_garage.InitTires(io_License, currentAirPressure, tireManufacturer);
                }
                catch (ValueOutOfRangeException e)
                {
                    string errorMessage = string.Format(
$"Tire air pressure out of range. Enter values between {e.MinValue} to {e.MaxValue}.");

                    Console.WriteLine(errorMessage);
                    inputInRange = false;
                }
            } while (!inputInRange);
        }

        private static void initTank(string io_License)
        {
            bool inputInRange;
            float currentEnergyInVehicle;

            do
            {
                inputInRange = true;
                currentEnergyInVehicle = getFloatType("Current amount of energy in vehicle (hours/litres): ");

                try
                {
                    m_garage.InitTankLevel(io_License, currentEnergyInVehicle);
                }
                catch (ValueOutOfRangeException e)
                {
                    string errorMessage = string.Format(
$"Energy level out of range. Enter values between {e.MinValue} to {e.MaxValue}.");

                    Console.WriteLine(errorMessage);
                    inputInRange = false;
                }
            } while (!inputInRange);
        }

        private static string getAllLetterName(string i_msg)
        {
            string name;
            bool validName;

            do
            {
                validName = true;
                Console.Write(i_msg);
                name = Console.ReadLine();
              
                foreach (char letter in name)
                {
                    if (!char.IsLetter(letter))
                    {
                        validName = false;
                        Console.WriteLine("Please contain only letters in your name.");
                        break;
                    }
                }
            }
            while (!validName);

            return name;
        }

        private static string getPhoneNumber()
        {
            bool isValid;
            string format = @"^0\d{2}-\d{7}$";
            string phoneNumber;
            
            do
            {
                isValid = true;
                Console.Write("Phone Number (in this format 0XX-XXXXXXX): ");
                phoneNumber = Console.ReadLine();
                
                if (!Regex.IsMatch(phoneNumber, format))
                {
                    Console.WriteLine("Please make sure your phone number in the correct format !");
                    isValid = false;
                }
            }
            while (!isValid);

            return phoneNumber;
        }
        private static string getLicenseNumber()
        {
            int number;
            string license;
            bool valid;

            do
            {                
                valid = true;
                Console.WriteLine();
                Console.Write("Please enter license number of the vehicle: ");
                license = Console.ReadLine();
                
                if (!(int.TryParse(license, out number)) || license.Length > k_LicenseNumberMaxDigits)
                {
                    Console.WriteLine($"Please enter only digits less then {k_LicenseNumberMaxDigits}.");
                    valid = false;
                }
            }
            while (!valid);

            return license;
        }
        private static eVehicleType getVehicleType()
        {
            string msg = string.Format(
@"
Please choose vehicle type:
1. Fuel Motorcycle
2. Electric Motorcycle
3. Fuel Car
4. Electric Car
5. Truck");
            Console.WriteLine(msg);

            return (eVehicleType)getOptionFromEnumList(typeof(eVehicleType));
        }
        private static int getOptionFromEnumList(Type i_EnumType)
        {
            bool valid;
            int pick = 0;

            do
            {
                valid = true;
                try
                {
                    pick = int.Parse(Console.ReadLine());
                    if (!Enum.IsDefined(i_EnumType, pick))
                    {
                        throw new FormatException();  //should be out of range expesion
                    }
                }
                catch (FormatException)
                {
                    Console.WriteLine("Please choose option from the list.");
                    valid = false;
                }
            }
            while (!valid);

            return pick ;
        }
        private static string getNoneEmptyString(string i_msg)
        {
            bool valid;
            string noneEmpty;

            do
            {
                valid = true;
                Console.Write(i_msg);
                noneEmpty = Console.ReadLine();
                
                if (string.IsNullOrEmpty(noneEmpty))
                {
                    valid = false;
                }
            }
            while (!valid);

            return noneEmpty;
        }
        private static float getFloatType(string i_msg) 
        {
            bool valid;
            string input;
            float value;

            do
            {
                valid = true;
                Console.Write(i_msg);
                input = Console.ReadLine();
                
                if (!float.TryParse(input, out value))
                {
                    valid = false;
                    Console.WriteLine("Please enter a rational number.");
                }
            }
            while (!valid);

            return value;
        }
        private static int getEngineVolume()
        {
                bool valid;
                string engineVolume;
                int volume;

                do
                {
                    valid = true;
                    Console.Write("Engine volume in motorcycle: ");
                    engineVolume = Console.ReadLine();
                    
                if (!int.TryParse(engineVolume, out volume))
                    {
                        valid = false;
                        Console.WriteLine("Please enter a integar number.");
                    }
                }
                while (!valid);

            return volume;
        }
        private static bool isDangerCargo()
        {
            string isDangerous;
            bool valid;

            do
            {
                valid = true;
                Console.Write("Dangerous cargo (yes/no) : ");
                isDangerous = Console.ReadLine();

                if (!(isDangerous == "yes") && !(isDangerous == "no"))
                {
                    valid = false;
                    Console.WriteLine("Please try again !");
                }
                
            }
            while(!valid);

            return isDangerous == "yes";
        }

        private static eCarColor getCarColor()
        {
            string msg = string.Format(
@"
Please choose car color type:
1. Blue
2. White
3. Red
4. Yellow");
            Console.WriteLine(msg);

            return (eCarColor)getOptionFromEnumList(typeof(eCarColor));
        }

        private static eCarDoorsNumber getNumberOfDoors()
        {
            string msg = string.Format(
@"
Please choosenumber of doors:
1. 2 Doors
2. 3 Doors
3. 4 Doors
4. 5 Doors");
            Console.WriteLine(msg);

            return (eCarDoorsNumber)getOptionFromEnumList(typeof(eCarDoorsNumber));
        }

        private static eMotorcycleLicenseType getMotorcycleLicense()
        {
            string msg = string.Format(

@"
Please choose license type:
1. A1
2. A2
3. B1
4. B2");
            Console.WriteLine(msg);
            return (eMotorcycleLicenseType)getOptionFromEnumList(typeof(eMotorcycleLicenseType));
        }
        private static eFuelTypes getFuelType() 
        {
            string msg = string.Format(
@"
Please choose fuel type
1. Octan96
2. Octan95
3. Octan98
4. Soler");
            Console.WriteLine(msg);

            return (eFuelTypes)getOptionFromEnumList(typeof(eFuelTypes));
        }


        private static void showLicenses()
        {
            eVehicleStatus filter = getStatus();  
            List<string> licenses = m_garage.LicensesByFilter(filter);
            int i = 1;

            Console.WriteLine($"Vehicles in status: {filter}");

            foreach (string numberLicense in licenses)
            {               
                Console.WriteLine($"{i++}. '{numberLicense}'");
            }
        }

        private static void changeStatus()
        {
             string license = getLicenseNumber();
             eVehicleStatus newStatus = getStatus();

            try
            {
                m_garage.ChangeVehicleStatus(license, newStatus);
                Console.WriteLine("The vehicle's status was successfully changed.");
            }
            catch(KeyNotFoundException)
            {
                Console.WriteLine("Vehicle is not found.");
            }
            
        } 
        private static void inflateTires()
        {
            string license = getLicenseNumber();

            try
            {
                m_garage.RefillTiresToMax(license);
                Console.WriteLine("The vehicle's tires were successfully inflated.");
            }
            catch(KeyNotFoundException)
            {
                Console.WriteLine("Vehicle is not found!");
            }
        }

        private static void refuel()
        {
            string license = getLicenseNumber();
            float fuelToAdd = getFloatType("Fuel to add in litres: ");
            eFuelTypes fuelType = getFuelType();
            bool valid = false;

            do
            {
                try
                {
                    m_garage.Refuel(license, fuelToAdd, fuelType);
                    Console.WriteLine("The vehicle was successfully refueled!");
                    valid = true;
                }
                catch (KeyNotFoundException)
                {
                    Console.WriteLine("Vehicle is not found!");
                    break;
                }
                catch (ValueOutOfRangeException outOfRange)
                {
                    Console.WriteLine($"Fuel amount is out of range! Enter values from {outOfRange.MinValue} to {outOfRange.MaxValue}");
                    fuelToAdd = getFloatType("Fuel to add in litres: ");
                }
                catch (ArgumentException)
                {
                    Console.WriteLine("Fuel type is not valid");
                    fuelType = getFuelType();
                }
                catch (NullReferenceException)
                {
                    Console.WriteLine("Vehicle is not refuelable!");
                    break;
                }
            }
            while (!valid);
        }

        private static void recharge()
        {
            string license = getLicenseNumber();
            float hoursOfChargeToAdd = getFloatType("Hours to charge:  ");
            bool valid = false;

            do
            {
                try
                {
                    m_garage.ReCharge(license, hoursOfChargeToAdd);
                    Console.WriteLine("The vehicle was successfully recharged!");
                    valid = true;
                }
                catch (KeyNotFoundException)
                {
                    Console.WriteLine("Vehicle is not found!");
                    break;
                }
                catch (ValueOutOfRangeException outOfRange)
                {
                    Console.WriteLine($"Charge amount is out of range! Enter values from {outOfRange.MinValue} to {outOfRange.MaxValue}");
                    hoursOfChargeToAdd = getFloatType("Hours to charge: ");
                }
                catch(NullReferenceException)
                {
                    Console.WriteLine("Vehicle is not electric!");
                    break;
                }
            }
            while (!valid);

        }
        private static void showFullDetails()
        {
            try
            {
                Console.WriteLine(m_garage.GetFullVehicleInfo(getLicenseNumber()));
            }
            catch (KeyNotFoundException) 
            { 
                Console.WriteLine("No such vehicle in garage."); 
            }
        }
        private static eVehicleStatus getStatus()
        {
            string msg = string.Format(
@"
Please choose status:
1. all statuses
2. in repairing
3. fixed
4. paid");
            Console.WriteLine(msg);

            return (eVehicleStatus)getOptionFromEnumList(typeof(eVehicleStatus));
        }
    }
}
