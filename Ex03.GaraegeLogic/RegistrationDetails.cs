using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ex03.GaraegeLogic
{
    public class RegistrationDetails
    {
        private string m_OwnerName;
        private string m_OwnerPhoneNumber;

        internal RegistrationDetails()
        {
        }

        internal string OwnerName
        {
            get { return m_OwnerName; }
            set { m_OwnerName = value; }
        }

        internal string OwnerPhoneNumber
        {
            get { return m_OwnerPhoneNumber; }
            set { m_OwnerPhoneNumber = value; }
        }
    }
}