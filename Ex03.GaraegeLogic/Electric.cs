using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ex03.GaraegeLogic
{
    public class Electric : VehicleResource
    {
        internal Electric()
        {
        }

        internal void LoadingVehicle(float i_HoursForLoading)
        {
            float totalAmoutOfElecticity = CurrentAmountOfResource + i_HoursForLoading;
            if (totalAmoutOfElecticity <= MaximalAmountOfResource && i_HoursForLoading >= Constants.k_MinimalValue)
            {
                CurrentAmountOfResource += i_HoursForLoading;
            }
            else
            {
                throw new ValueOutOfRangeException(Constants.k_MinimalValue, (MaximalAmountOfResource - CurrentAmountOfResource) * Constants.k_MinutesInHour);
            }
        }
    }
}