using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ex03.GaraegeLogic;

namespace Ex03.ConsoleUI
{
    public class ConsoleUI
    {
        private readonly Garage r_Garage = new Garage();
        private Vehicle m_NewVehicle;

        public void GarageManager()
        {
            Console.WriteLine("Welcome to the garage!!");
            bool stayInTheGarage = true;
            while (stayInTheGarage)
            {
                try
                {
                    GarageMenu();
                    eGarageMenu userChoice = (eGarageMenu)Enum.Parse(typeof(eGarageMenu), Console.ReadLine());
                    switch (userChoice)
                    {
                        case eGarageMenu.One:
                            InsertVehicleToGarage();
                            break;
                        case eGarageMenu.Two:
                            PresentLicenseNumber();
                            break;
                        case eGarageMenu.Three:
                            ChangingVehicleStatus(ReceiveLicenseNumber());
                            break;
                        case eGarageMenu.Four:
                            InflatingWheelToMaximum(ReceiveLicenseNumber());
                            break;
                        case eGarageMenu.Five:
                            RefuelingVehicle(ReceiveLicenseNumber());
                            break;
                        case eGarageMenu.Six:
                            LoadingVehicle(ReceiveLicenseNumber());
                            break;
                        case eGarageMenu.Seven:
                            PrintInformationOfVehcle(ReceiveLicenseNumber());
                            break;
                        case eGarageMenu.Eight:
                            stayInTheGarage = false;
                            Console.WriteLine("Bye bye! hope to see you again");
                            break;
                        default:
                            Console.WriteLine("Invalid number! Please enter your choice number again");
                            break;
                    }
                }
                catch (ValueOutOfRangeException exception)
                {
                    Console.WriteLine(exception.Message);
                }
                catch (ArgumentNullException exception)
                {
                    Console.WriteLine(exception.Message);
                }
                catch (ArgumentException exception)
                {
                    Console.WriteLine(exception.Message);
                }
                catch (FormatException exception)
                {
                    Console.WriteLine(exception.Message);
                }
                catch (NullReferenceException exception)
                {
                    Console.WriteLine(exception.Message);
                }
                catch (Exception exception)
                {
                    Console.WriteLine(exception.Message);
                }
            }
        }

        private void GarageMenu()
        {
            Console.WriteLine(string.Format(@"Please enter your choice number: 
1. Insert new vehicle to the garage
2. Show the license numbers according to the specific status 
3. Change the vehicle status 
4. Inflate the vehicle wheels to the maximum
5. Refuel the vehicle with gas
6. Load the vehicle with electricity
7. Show the information of the vehicle
8. To QUIT
"));
        }

        public void InsertVehicleToGarage()
        {
            Console.WriteLine("Enter your vehicle license number");
            string licenseNumber = Console.ReadLine();
            bool vehicleIsExist = r_Garage.CheckIfVehicleInGarage(licenseNumber);
            if (vehicleIsExist)
            {
                r_Garage.UpdateVehicleStatus(licenseNumber, eVehicleStatus.InRepair);
                Console.WriteLine("This vehicle is already exist in the garage");
                return;
            }

            CreateVehicle(licenseNumber);
            ReceiveResource();
            m_NewVehicle.SetVehicleResource(m_NewVehicle.TypeOfResource);
            SetMaximalAmountOfResource();
            SetCurrentAmountOfResource();
            m_NewVehicle.UdatePercentageResourceLeft(m_NewVehicle.Resourse.CurrentAmountOfResource, m_NewVehicle.Resourse.MaximalAmountOfResource);
            m_NewVehicle.SetVehicleStatus();
            UpdateRegistrationDetails();
            SetWheelsProperties();
            m_NewVehicle.SetMessagesInDictionary();
            UpdateUniqueProperties(m_NewVehicle.ReceiveDictionary());
            r_Garage.InsertVehicleToGarage(m_NewVehicle);
        }

        public string ReceiveLicenseNumber()
        {
            Console.WriteLine("Enter your vehicle license number");
            string licenseNumber = Console.ReadLine();
            bool isExist = r_Garage.CheckIfVehicleInGarage(licenseNumber);
            if(!isExist)
            {
                throw new ArgumentException("There is no vehicle with this license number in the garage");
            }

            return licenseNumber;
        }

        public void CreateVehicle(string i_LicenseNumber)
        {
            Console.WriteLine("Enter your vehicle model name");
            string modelName = Console.ReadLine();
            int numberOfInputOptions = Enum.GetNames(typeof(eVehicleType)).Length;
            bool validInput = false;
            int opthionNumber = 0;
            while (!validInput)
            {
                foreach(eVehicleType typeOfVehicle in Enum.GetValues(typeof(eVehicleType)))
                {
                    Console.WriteLine(string.Format("For {0} enter {1}", typeOfVehicle.ToString(), opthionNumber));
                    opthionNumber++;
                }
     
                int vehicleType = InputValidation.CorrectUserInput(Console.ReadLine(), numberOfInputOptions);
                if (vehicleType != Constants.k_InValid)
                {
                    validInput = true;
                    m_NewVehicle = NewVehicle.VehicleInstance(modelName, i_LicenseNumber, (eVehicleType)vehicleType);
                }
                else
                {
                    Console.WriteLine("This vehicle type isn't valid!");
                }
            }
        }

        public void ReceiveResource()
        {
            int numberOfInputOptions = Enum.GetNames(typeof(eVehicleResource)).Length;
            bool validInput = false;
            while (!validInput)
            {
                Console.WriteLine(string.Format(@"Choose your vehicle resource, enter: 0 / 1)
0 - Gas
1 - Electric
"));
                int vehicleResource = InputValidation.CorrectUserInput(Console.ReadLine(), numberOfInputOptions);
                if (vehicleResource != Constants.k_InValid)
                {
                    bool isGas = vehicleResource == (int)eVehicleResource.Gas;
                    bool isElectricity = vehicleResource == (int)eVehicleResource.Electric;
                    if ((isGas && !m_NewVehicle.SupportGas) || (isElectricity && !m_NewVehicle.SupportElectric))
                    {
                        Console.WriteLine("This garage doesn't support this type of vehicle with this type of resource");
                        continue;
                    }

                    validInput = true;
                    m_NewVehicle.TypeOfResource = (eVehicleResource)vehicleResource;
                }
                else
                {
                    Console.WriteLine("This vehicle resource isn't valid!");
                }
            }
        }

        public void SetMaximalAmountOfResource()
        {
            if (eVehicleResource.Gas.Equals(m_NewVehicle.TypeOfResource))
            {
                m_NewVehicle.UpdateGasMaximalAmount();
            }
            else
            {
                m_NewVehicle.UpdateElectricMaximalAmount();
            }
        }

        public float ReceiveCurrentAmountOfResource()
        { 
            bool validInput = false;
            float amountOfResource = 0f;
            while (!validInput)
            {
                Console.WriteLine("Enter your current amount of resource");
                bool parseSucces = InputValidation.InputIsFloat(Console.ReadLine(), ref amountOfResource);
                if (parseSucces)
                {
                    float maximalAmountOfResource = m_NewVehicle.Resourse.MaximalAmountOfResource;
                    if (amountOfResource <= maximalAmountOfResource && amountOfResource >= Constants.k_MinimalValue)
                    {
                        validInput = true;
                    }
                    else
                    {
                        Console.WriteLine("The input is out of range!");
                    } 
                }
                else
                {
                    Console.WriteLine("The input isn't a float number!");
                }
            }

            return amountOfResource;
        }

        public void SetCurrentAmountOfResource()
        {
            m_NewVehicle.UpdateCurrentAmountOfResource(ReceiveCurrentAmountOfResource());
        }

        public void UpdateRegistrationDetails()
        {
            Console.WriteLine("Enter your name");
            string OwnerName = Console.ReadLine();
            string OwnerPhoneNumber = string.Empty;
            bool validInput = false;
            while (!validInput)
            {
                Console.WriteLine("Enter your phone number");
                OwnerPhoneNumber = Console.ReadLine();
                if(InputValidation.ContainOnlyDigits(OwnerPhoneNumber))
                {
                    validInput = true;
                }
                else
                {
                    Console.WriteLine("The input must contain only digits!");
                }
            }

            m_NewVehicle.SetRegistrationDetails(OwnerName, OwnerPhoneNumber);
        }

        public void SetWheelsProperties()
        {
            foreach (Wheel wheel in m_NewVehicle.WheelCollection)
            {
                bool validInput = false;
                while(!validInput)
                {
                    Console.WriteLine("Enter the current air pressure of the wheel");
                    float airPressure = 0f;
                    bool parseSucces = InputValidation.InputIsFloat(Console.ReadLine(), ref airPressure);
                    if (parseSucces)
                    {
                        if (airPressure <= wheel.MaximalAirPresure && airPressure >= Constants.k_MinimalValue)
                        {
                            validInput = true;
                            wheel.CurrentAirPressure = airPressure;
                        }
                        else
                        {
                            Console.WriteLine("the input is out of range");
                            continue;
                        }
                    }
                    else
                    {
                        Console.WriteLine("the input isn't a float");
                    }
                }
                
                Console.WriteLine("Enter the manufacturer name of the wheel");
                string manufacturerName = Console.ReadLine();
                wheel.ManufacturerName = manufacturerName;
            }
        }

        public void UpdateUniqueProperties(Dictionary<int, string> i_Messages)
        {
            foreach (KeyValuePair<int, string> pair in i_Messages)
            {
                bool validInput = false;
                while(!validInput)
                {
                    Console.WriteLine(string.Format("Please enter {0}", pair.Value));
                    string userInput = Console.ReadLine();
                    validInput = m_NewVehicle.UpdateUniqueProperties(userInput, pair.Key);
                    if(!validInput)
                    {
                        Console.WriteLine("The value isn't valid!");
                    }
                }
            }
        }

        public void PresentLicenseNumber()
        {
            List<string> licenseNumbers;
            Console.WriteLine(string.Format(@"Choose the vehicles status that you want to see, enter: 0 / 1 / 2 / 3)
0 - Vehicles in repair
1 - Repaired vehicles
2 - Paid vehicles
3 - All vehicles
"));
            int numberOfInputOptions = Enum.GetNames(typeof(eSelectingByStatus)).Length;
            int selectingByStatus = InputValidation.CorrectUserInput(Console.ReadLine(), numberOfInputOptions);
            if (selectingByStatus == Constants.k_InValid)
            {
                throw new FormatException("Format exception has occured");
            }

            licenseNumbers = r_Garage.CreateLicenseNumbersList((eSelectingByStatus)selectingByStatus);
            if (licenseNumbers.Count == 0)
            {
                if (selectingByStatus == (int)eSelectingByStatus.AllVehicles)
                {
                    Console.WriteLine("There is no vehicles in the garage");
                }
                else
                {
                    Console.WriteLine("There is no vehicles in this status");
                }
            }
            else
            {
                Console.WriteLine("The vehicles are:");
                foreach (string licenseNumber in licenseNumbers)
                {
                    Console.WriteLine(licenseNumber);
                }
            }
        }

        public void ChangingVehicleStatus(string i_LicenseNumber)
        {
            Console.WriteLine(string.Format(@"Choose new status for your car, enter: 0 / 1 / 2)
0 - Vehicles in repair
1 - Repaired vehicles
2 - Paid vehicles
"));
            int numberOfInputOptions = Enum.GetNames(typeof(eVehicleStatus)).Length;
            int selectingByStatus = InputValidation.CorrectUserInput(Console.ReadLine(), numberOfInputOptions);
            if (selectingByStatus == Constants.k_InValid)
            {
                throw new FormatException("Format exception has occured");
            }
            
            r_Garage.UpdateVehicleStatus(i_LicenseNumber, (eVehicleStatus)selectingByStatus);
        }

        public void InflatingWheelToMaximum(string i_LicenseNumber)
        {
            r_Garage.InflatingWheelToMaxium(i_LicenseNumber);
        }

        public void RefuelingVehicle(string i_LicenseNumber)
        {
            if(r_Garage.VeliclesInGarage[i_LicenseNumber].Resourse is Electric)
            {
                Console.WriteLine("The vehicle resource isn't gas!");

                return;
            }

            Console.WriteLine("Enter the amount of gas for refueling the vehicle");
            float amountGassToAdd = 0f;
            bool parseSucces = InputValidation.InputIsFloat(Console.ReadLine(), ref amountGassToAdd);
            if (!parseSucces)
            {
                throw new FormatException("Format exception has occured");
            }

            eTypeOfGas typeOfGas = (r_Garage.VeliclesInGarage[i_LicenseNumber].Resourse as Gas).TypeOfGas;
            r_Garage.RefuelingWithGas(i_LicenseNumber, typeOfGas, amountGassToAdd);
        }

        public void LoadingVehicle(string i_LicenseNumber)
        {
            if (r_Garage.VeliclesInGarage[i_LicenseNumber].Resourse is Gas)
            {
                Console.WriteLine("The vehicle resource isn't electicity!");

                return;
            }

            Console.WriteLine("Enter the amount of minutes for loading the vehicle");
            float numberOfMinutes = 0f;
            bool parseSucces = InputValidation.InputIsFloat(Console.ReadLine(), ref numberOfMinutes);
            if (!parseSucces)
            {
                throw new FormatException("Format exception has occured");
            }
           
            float hoursForLoading = (float)(numberOfMinutes / Constants.k_MinutesInHour);
            r_Garage.LoadingWithElectric(i_LicenseNumber, hoursForLoading);
        }

        public void PrintInformationOfVehcle(string i_LicenseNumber)
        {
            Console.WriteLine(r_Garage.VehicleString(i_LicenseNumber));
        }
    }
}