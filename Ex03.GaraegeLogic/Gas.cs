using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ex03.GaraegeLogic
{
    public class Gas : VehicleResource
    {
        private eTypeOfGas m_TypeOfGas;

        internal Gas()
        {
        }

        public eTypeOfGas TypeOfGas
        {
            get { return m_TypeOfGas; }
            set { m_TypeOfGas = value; }
        }

        internal void RefuelingVehicle(float i_AmountGasToAdd, eTypeOfGas i_TypeOfGas)
        {
            float totalAmoutOfGas = CurrentAmountOfResource + i_AmountGasToAdd;
            if (m_TypeOfGas.Equals(i_TypeOfGas))
            {
                if(totalAmoutOfGas <= MaximalAmountOfResource && i_AmountGasToAdd >= Constants.k_MinimalValue)
                {
                    CurrentAmountOfResource += i_AmountGasToAdd;
                }
                else
                {
                    throw new ValueOutOfRangeException(Constants.k_MinimalValue, MaximalAmountOfResource - CurrentAmountOfResource);
                }
            }
            else
            {
                throw new ArgumentException("This type of gas is not matching");
            }
        }
    }
}