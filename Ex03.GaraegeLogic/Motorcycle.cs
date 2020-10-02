using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ex03.GaraegeLogic
{
    public class Motorcycle : Vehicle
    {
        private const float k_MaximalBatteryTime = (float)1.2;
        private const eTypeOfGas k_typeOfGas = eTypeOfGas.Octan95;
        private readonly Dictionary<int, string> r_UniquePropertiesOfMotorcycle = new Dictionary<int, string>();
        private eLicenseType m_LicenseType;
        private int m_EngineCapacity;

        internal Motorcycle(string i_ModelName, string i_LicenseNumber) : base(i_ModelName, i_LicenseNumber)
        {
            m_MaximalAirPresure = (float)eMaximalAirPresure.Thirty;
            m_NumberOfWheels = (int)eNumberOfWheels.Two;
            m_SupportGas = true;
            m_SupportElectric = true;
            AddWheels();
        }

        internal eLicenseType LicenseType
        {
            get { return m_LicenseType; }
            set { m_LicenseType = value; }
        }

        internal int EngineCapacity
        {
            get { return m_EngineCapacity; }
            set { m_EngineCapacity = value; }
        }

        public override void UpdateGasMaximalAmount()
        {
            m_Resourse.MaximalAmountOfResource = (float)eGasTankCapacity.Seven;
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
            r_UniquePropertiesOfMotorcycle.Add((int)eMotorcycleProperties.LicenseType, "your type of license, press:\n0 for A\n1 for A1\n2 for AA\n3 for B");
            r_UniquePropertiesOfMotorcycle.Add((int)eMotorcycleProperties.EngineCapacity, "your engine capacity");
        }

        public override Dictionary<int, string> ReceiveDictionary()
        {
            return r_UniquePropertiesOfMotorcycle;
        }

        public override bool UpdateUniqueProperties(string i_Message, int i_PropertyNumber)
        {
            bool parseSucceed = false;
            if (i_PropertyNumber == (int)eMotorcycleProperties.LicenseType)
            {
                int numberOfInputOptions = Enum.GetNames(typeof(eLicenseType)).Length;
                int licenseType = InputValidation.CorrectUserInput(i_Message, numberOfInputOptions);
                if (licenseType != Constants.k_InValid)
                {
                    m_LicenseType = (eLicenseType)licenseType;
                    parseSucceed = true;
                }
            }
            else if(i_PropertyNumber == (int)eMotorcycleProperties.EngineCapacity)
            {
                parseSucceed = int.TryParse(i_Message, out int engineCapacity);
                if (parseSucceed)
                {
                    m_EngineCapacity = engineCapacity;
                }
            }

            return parseSucceed;
        }

        internal override string UniquePropertiesString()
        {
            string uniqueProperties = string.Format(
                @"License type of the motorcycle: {0}
Engine capacity of the motorcycle: {1}", 
                m_LicenseType, 
                m_EngineCapacity);

            return uniqueProperties;
        }
    }
}