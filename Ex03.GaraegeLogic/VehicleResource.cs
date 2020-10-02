using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ex03.GaraegeLogic
{
    public abstract class VehicleResource
    {
        private float m_CurrentAmountOfResource;
        private float m_MaximalAmountOfResource;

        public float CurrentAmountOfResource
        {
            get { return m_CurrentAmountOfResource; }
            set { m_CurrentAmountOfResource = value; }
        }

        public float MaximalAmountOfResource
        {
            get { return m_MaximalAmountOfResource; }
            set { m_MaximalAmountOfResource = value; }
        }
    }
}