using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using System.Threading.Tasks;
using Ex03.GaraegeLogic;

namespace Ex03.GaraegeLogic
{
    public abstract class Vehicle
    {
        private readonly string r_ModelName;
        private readonly string r_LicenseNumber;
        private readonly List<Wheel> r_WheelCollection = new List<Wheel>();
        private float m_PercentageResourceLeft;
        private eVehicleStatus m_VehicleStatus;
        private eVehicleResource m_TypeOfResource;
        protected VehicleResource m_Resourse;
        protected readonly RegistrationDetails r_RegistrationDetails = new RegistrationDetails();
        protected float m_MaximalAirPresure;
        protected int m_NumberOfWheels;
        protected bool m_SupportGas;
        protected bool m_SupportElectric;

        protected Vehicle(string i_ModelName, string i_LicenseNumber)
        {
            r_ModelName = i_ModelName;
            r_LicenseNumber = i_LicenseNumber;
        }

        public string ModelName
        {
            get { return r_ModelName; }
        }

        public string LicenseNumber
        {
            get { return r_LicenseNumber; }
        }

        public float PercentageResourceLeft
        {
            get { return m_PercentageResourceLeft; }
        }

        public eVehicleStatus VehicleStatus
        {
            get { return m_VehicleStatus; }
            set { m_VehicleStatus = value; }
        }

        public VehicleResource Resourse
        {
            get { return m_Resourse; }
        }

        public eVehicleResource TypeOfResource
        {
            get { return m_TypeOfResource; }
            set { m_TypeOfResource = value; }
        }

        public RegistrationDetails RegistrationDetails
        {
            get { return r_RegistrationDetails; }
        }

        public List<Wheel> WheelCollection
        {
            get { return r_WheelCollection; }
        }

        public bool SupportGas
        {
            get { return m_SupportGas; }
        }

        public bool SupportElectric
        {
            get { return m_SupportElectric; }
        }
        
        public void SetVehicleResource(eVehicleResource i_VehicleResource)
        {
            if(eVehicleResource.Gas.Equals(i_VehicleResource))
            {
                m_Resourse = new Gas();
                Gas gas = m_Resourse as Gas;
                UpdateTypeOfGas(gas);
            } 
            else if (eVehicleResource.Electric.Equals(i_VehicleResource))
            {
                m_Resourse = new Electric();
            }
        }

        internal virtual void UpdateTypeOfGas(Gas i_Gas)
        { 
        }

        public virtual void UpdateGasMaximalAmount() 
        { 
        }

        public virtual void UpdateElectricMaximalAmount() 
        {
        }

        public void UpdateCurrentAmountOfResource(float i_CurrentAmountOfResource)
        {
            m_Resourse.CurrentAmountOfResource = i_CurrentAmountOfResource;
        }

        public virtual void SetMessagesInDictionary() 
        {
        }

        public abstract bool UpdateUniqueProperties(string i_Message, int i_PropertyNumber);

        public abstract Dictionary<int, string> ReceiveDictionary();

        public void UdatePercentageResourceLeft(float i_CurrentAmountOfResource, float i_MaximalAmountOfResource)
        {
            m_PercentageResourceLeft = (i_CurrentAmountOfResource / i_MaximalAmountOfResource) * 100;
        }

        internal void AddGasToVehicle(float i_AmountGassToAdd, eTypeOfGas i_TypeOfGass)
        {
            try
            {
                Gas gas = m_Resourse as Gas;
                gas.RefuelingVehicle(i_AmountGassToAdd, i_TypeOfGass);
                UdatePercentageResourceLeft(gas.CurrentAmountOfResource, gas.MaximalAmountOfResource);
            }
            catch (InvalidCastException)
            {
                throw new ArgumentException("Vehicle resourse isn't gas");
            }
        }

        internal void AddElectricToVehicle(float i_AmountGassToAdd)
        {
            try
            {
                Electric electric = m_Resourse as Electric;
                electric.LoadingVehicle(i_AmountGassToAdd);
                UdatePercentageResourceLeft(electric.CurrentAmountOfResource, electric.MaximalAmountOfResource);
            }
            catch (InvalidCastException)
            {
                throw new ArgumentException("Vehicle resourse isn't electicity");
            }   
        }

        public void SetRegistrationDetails(string i_OwnerName, string i_OwnerPhoneNumber)
        {
            r_RegistrationDetails.OwnerName = i_OwnerName;
            r_RegistrationDetails.OwnerPhoneNumber = i_OwnerPhoneNumber;
        }

        public void SetVehicleStatus()
        {
            m_VehicleStatus = eVehicleStatus.InRepair;
        }

        internal void AddWheels()
        {
            for (int i = 0; i < m_NumberOfWheels; i++)
            {
                r_WheelCollection.Add(new Wheel(m_MaximalAirPresure));
            }
        }

        internal abstract string UniquePropertiesString();

        private string Printwheels()
        {
            StringBuilder wheels = new StringBuilder();
            int NumberOfWheel = 0;
            foreach (Wheel wheel in r_WheelCollection)
            {
                NumberOfWheel++;
                wheels.Append(string.Format(
                    @"
Wheel number: {0} 
{1}", 
                 NumberOfWheel, 
                 wheel.ToString()));   
            }

            string stringOfWheels = wheels.ToString();

            return stringOfWheels;
        }

        private string ResourceInformationString()
        {
            string stringResourceInformation;
            if (m_Resourse is Gas)
            {
                stringResourceInformation = string.Format(
                    @"current amount of gas : {0}
Type of gas: {1}
", 
              m_Resourse.CurrentAmountOfResource, 
              (m_Resourse as Gas).TypeOfGas);
            }
            else
            {
                stringResourceInformation = string.Format("current amount of electric : {0}", m_Resourse.CurrentAmountOfResource);
            }

            return stringResourceInformation;
        }

        public override string ToString()
        {
            return string.Format(
                @"License number: {0}
Model name: {1}
Owner name: {2}
Vehicle status: {3}{4}
{5}
{6}
", 
                r_LicenseNumber, 
                r_ModelName, 
                r_RegistrationDetails.OwnerName,
                m_VehicleStatus.ToString(), 
                Printwheels(), 
                ResourceInformationString(), 
                UniquePropertiesString()); 
        }
    }
}