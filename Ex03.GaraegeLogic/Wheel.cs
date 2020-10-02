using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ex03.GaraegeLogic
{
    public class Wheel
    {
        private readonly float r_MaximalAirPresure;
        private string m_ManufacturerName;
        private float m_CurrentAirPressure;

        internal Wheel(float i_MaximalAirPresure)
        {
            m_ManufacturerName = string.Empty;
            m_CurrentAirPressure = 0f;
            r_MaximalAirPresure = i_MaximalAirPresure;
        }

        public string ManufacturerName
        {
            get { return m_ManufacturerName; }
            set { m_ManufacturerName = value; }
        }

        public float CurrentAirPressure
        {
            get { return m_CurrentAirPressure; }
            set { m_CurrentAirPressure = value; }
        }

        public float MaximalAirPresure
        {
            get { return r_MaximalAirPresure; }
        }

        internal void InflatingWheel(float i_AmountAirToAdd)
        {
            float totalAirPressure = m_CurrentAirPressure + i_AmountAirToAdd;
            if (totalAirPressure <= r_MaximalAirPresure && i_AmountAirToAdd > Constants.k_MinimalValue)
            {
                 m_CurrentAirPressure += i_AmountAirToAdd;
            }
            else
            {
                throw new ValueOutOfRangeException(Constants.k_MinimalValue, r_MaximalAirPresure - m_CurrentAirPressure);
            }
        }

        public override string ToString()
        {
            return string.Format(
                @"Wheel current air pressure: {0}  Wheel maximal air pressure: {1}  Manufacturer name: {2}", 
                m_CurrentAirPressure, 
                r_MaximalAirPresure, 
                m_ManufacturerName);
        }
    }
}