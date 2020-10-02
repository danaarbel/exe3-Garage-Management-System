using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ex03.GaraegeLogic
{
    public class ValueOutOfRangeException : Exception
    {
        private readonly float r_MaxValue;
        private readonly float r_MinValue;

		public ValueOutOfRangeException(float i_MinValue, float i_MaxValue) : base(string.Format("The value is out of the range! you can choose value between {0} to {1}", i_MinValue, i_MaxValue))
		{
			r_MinValue = i_MinValue;
			r_MaxValue = i_MaxValue;
		}

		public float MaxValue
		{
			get { return r_MaxValue; }
		}

		public float MinValue
		{
			get { return r_MinValue; }
		}
	}
}
