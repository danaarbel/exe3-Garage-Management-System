using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ex03.GaraegeLogic
{
    public class Truck : Vehicle
    {
        private const eTypeOfGas k_typeOfGas = eTypeOfGas.Soler;
        private readonly Dictionary<int, string> r_UniquePropertiesOfTruck = new Dictionary<int, string>();
        private bool m_ContainDangerousMaterial;
        private float m_CargoCapacity;

        internal Truck(string i_ModelName, string i_LicenseNumber) : base(i_ModelName, i_LicenseNumber)
        {
            m_MaximalAirPresure = (float)eMaximalAirPresure.TwentyEight;
            m_NumberOfWheels = (int)eNumberOfWheels.Sixteen;
            m_SupportGas = true;
            m_SupportElectric = false;
            AddWheels();
        }

        internal bool ContainDangerousMaterial
        {
            get { return m_ContainDangerousMaterial; }
            set { m_ContainDangerousMaterial = value; }
        }

        internal float CargoCapacity
        {
            get { return m_CargoCapacity; }
            set { m_CargoCapacity = value; }
        }

        public override void UpdateGasMaximalAmount()
        {
            m_Resourse.MaximalAmountOfResource = (float)eGasTankCapacity.HundredAndTwenty;
        }

        internal override void UpdateTypeOfGas(Gas i_Gas)
        {
            i_Gas.TypeOfGas = k_typeOfGas;
        }

        public override void SetMessagesInDictionary()
        {
            r_UniquePropertiesOfTruck.Add((int)eTruckProperties.ContainDangerousMaterial, "True/Flase if you Truck contain dangerous material");
            r_UniquePropertiesOfTruck.Add((int)eTruckProperties.CargoCapacity, "your cargo capacity");
        }

        public override Dictionary<int, string> ReceiveDictionary()
        {
            return r_UniquePropertiesOfTruck;
        }

        public override bool UpdateUniqueProperties(string i_Message, int i_PropertyNumber)
        {
            bool parseSucceed = false;
            if (i_PropertyNumber == (int)eTruckProperties.ContainDangerousMaterial)
            {
                parseSucceed = bool.TryParse(i_Message, out bool containDangerousMaterial);
                if (parseSucceed)
                {
                    m_ContainDangerousMaterial = containDangerousMaterial;
                }
            }
            else if(i_PropertyNumber == (int)eTruckProperties.CargoCapacity)
            {
                float cargoCapacity = 0f;
                bool parseSucces = InputValidation.InputIsFloat(i_Message, ref cargoCapacity);
                if (parseSucces && cargoCapacity > 0)
                {
                    m_CargoCapacity = cargoCapacity;
                    parseSucceed = true;
                }
            }

            return parseSucceed;
        }

        internal override string UniquePropertiesString()
        {
            string uniqueProperties = string.Format(
                @"Contain dangerous material: {0}
Cargo capacity of the truck: {1}", 
                m_ContainDangerousMaterial, 
                m_CargoCapacity);

            return uniqueProperties;
        }
    }
}