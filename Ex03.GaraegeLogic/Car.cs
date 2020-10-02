using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ex03.GaraegeLogic
{
    public class Car : Vehicle
    {
        private const float k_MaximalBatteryTime = (float)2.1;
        private const eTypeOfGas k_typeOfGas = eTypeOfGas.Octan96;
        private readonly Dictionary<int, string> r_UniquePropertiesOfCar = new Dictionary<int, string>();
        private eColorOfCar m_Color;
        private eNumberOfDoors m_NumberOfDoors;

        internal Car(string i_ModelName, string i_LicenseNumber) : base(i_ModelName, i_LicenseNumber)
        {
            m_MaximalAirPresure = (float)eMaximalAirPresure.ThirtyTwo;
            m_NumberOfWheels = (int)eNumberOfWheels.Four;
            m_SupportGas = true;
            m_SupportElectric = true;
            AddWheels();
        }

        internal eColorOfCar Color
        {
            get { return m_Color; }
            set { m_Color = value; }
        }

        internal eNumberOfDoors NumberOfDoors
        {
            get { return m_NumberOfDoors; }
            set { m_NumberOfDoors = value; }
        }

        public override void UpdateGasMaximalAmount()
        {
            m_Resourse.MaximalAmountOfResource = (float)eGasTankCapacity.Sixty;
        }

        public override void UpdateElectricMaximalAmount()
        {
            m_Resourse.MaximalAmountOfResource = k_MaximalBatteryTime;
        }

        internal override void UpdateTypeOfGas(Gas i_Gas)
        {
            i_Gas.TypeOfGas = k_typeOfGas;
        }

        public override void SetMessagesInDictionary()
        {
            r_UniquePropertiesOfCar.Add((int)eCarProperties.Color, "the color of your car, press:\n0 for Red\n1 for White\n2 for Black\n3 for Silver");
            r_UniquePropertiesOfCar.Add((int)eCarProperties.NumberOfDoors, "the number of doors in your car, press:\n0 for Two\n1 for Three\n2 for Four\n3 for Five");
        }

        public override Dictionary<int, string> ReceiveDictionary()
        {
            return r_UniquePropertiesOfCar;
        }

        public override bool UpdateUniqueProperties(string i_Message, int i_PropertyNumber)
        {
            bool parseSucceed = false;
            if (i_PropertyNumber == (int)eCarProperties.Color)
            {
                int numberOfInputOptions = Enum.GetNames(typeof(eColorOfCar)).Length;
                int color = InputValidation.CorrectUserInput(i_Message, numberOfInputOptions);
                if (color != Constants.k_InValid)
                {
                    m_Color = (eColorOfCar)color;
                    parseSucceed = true;
                }
            }
            else if (i_PropertyNumber == (int)eCarProperties.NumberOfDoors)
            {
                int numberOfInputOptions = Enum.GetNames(typeof(eNumberOfDoors)).Length;
                int doorsNumber = InputValidation.CorrectUserInput(i_Message, numberOfInputOptions);
                if (doorsNumber != Constants.k_InValid)
                {
                    m_NumberOfDoors = (eNumberOfDoors)doorsNumber;
                    parseSucceed = true;
                }
            }

            return parseSucceed;
        }

        internal override string UniquePropertiesString()
        {
            string uniqueProperties = string.Format(
                @"Color of the car: {0}
Number of doors in the car: {1}", 
                m_Color, 
                m_NumberOfDoors);
            return uniqueProperties;
        }
    }
}