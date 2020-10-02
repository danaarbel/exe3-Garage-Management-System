using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ex03.GaraegeLogic
{
    public static class NewVehicle
    {
        public static Vehicle VehicleInstance(string i_ModelName, string i_LicenseNumber, eVehicleType i_TypeOfVeicle)
        {
            Vehicle vehicle = null;
            switch (i_TypeOfVeicle)
            {
                case eVehicleType.Car:
                    {
                        vehicle = new Car(i_ModelName, i_LicenseNumber);
                        break;
                    }

                case eVehicleType.Motorcycle:
                    {
                        vehicle = new Motorcycle(i_ModelName, i_LicenseNumber);
                        break;
                    }

                case eVehicleType.Truck:
                    {
                        vehicle = new Truck(i_ModelName, i_LicenseNumber);
                        break;
                    }
            }

            return vehicle;
        }
    }
}