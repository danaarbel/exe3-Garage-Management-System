using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ex03.GaraegeLogic
{
    public class Garage
    {
        private readonly Dictionary<string, Vehicle> r_VehiclesInGarage = new Dictionary<string, Vehicle>();

        public Dictionary<string, Vehicle> VeliclesInGarage
        {
            get { return r_VehiclesInGarage; }
        }

        public void InsertVehicleToGarage(Vehicle i_Vehicle)
        {
            r_VehiclesInGarage.Add(i_Vehicle.LicenseNumber, i_Vehicle);
        }

        public bool CheckIfVehicleInGarage(string i_LicenseNumber)
        {
            bool isExist = false;
            if (r_VehiclesInGarage.ContainsKey(i_LicenseNumber))
            {
                isExist = true;
            }

            return isExist;
        }

        private Vehicle GetVehicleByLicenseNumber(string i_LicenseNumber)
        {
            bool isExist = CheckIfVehicleInGarage(i_LicenseNumber);
            if (!isExist)
            {
                throw new ArgumentException("This license number doesn't match to any of the garage vehicles");
            }
            
            return r_VehiclesInGarage[i_LicenseNumber];
        }

        public List<string> CreateLicenseNumbersList(eSelectingByStatus i_SelectingByStatus)
        {
            List<string> licenseNumbers = new List<string>();
            foreach(KeyValuePair<string, Vehicle> pair in r_VehiclesInGarage)
            {
                bool statusIsAllVehicles = i_SelectingByStatus.Equals(eSelectingByStatus.AllVehicles);
                if ((int)pair.Value.VehicleStatus == (int)i_SelectingByStatus || statusIsAllVehicles)
                {
                    licenseNumbers.Add(pair.Key);
                }
           }
        
            return licenseNumbers;
        }

        public void UpdateVehicleStatus(string i_LicenseNumber, eVehicleStatus i_VehicleStatus)
        {
            GetVehicleByLicenseNumber(i_LicenseNumber).VehicleStatus = i_VehicleStatus;
        }

        public void InflatingWheelToMaxium(string i_LicenseNumber)
        {
            List<Wheel> wheels = GetVehicleByLicenseNumber(i_LicenseNumber).WheelCollection;
            foreach(Wheel wheel in wheels)
            {
                wheel.InflatingWheel(wheel.MaximalAirPresure - wheel.CurrentAirPressure);
            }
        }

        public void RefuelingWithGas(string i_LicenseNumber, eTypeOfGas i_TypeOfGas, float i_AmountGassToAdd)
        {
            GetVehicleByLicenseNumber(i_LicenseNumber).AddGasToVehicle(i_AmountGassToAdd, i_TypeOfGas);
        }

        public void LoadingWithElectric(string i_LicenseNumber, float i_HoursForLoading)
        {
            GetVehicleByLicenseNumber(i_LicenseNumber).AddElectricToVehicle(i_HoursForLoading);
        }

        public string VehicleString(string i_LicenseNumber)
        {
            return GetVehicleByLicenseNumber(i_LicenseNumber).ToString();
        }
    }
}
