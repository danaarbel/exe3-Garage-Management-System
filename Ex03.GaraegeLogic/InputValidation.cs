using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ex03.GaraegeLogic
{
    public class InputValidation
    {
        public static bool InputIsFloat(string i_UserInput, ref float io_ConvertInput)
        {
            return float.TryParse(i_UserInput, out io_ConvertInput);
        }

        public static bool ContainOnlyDigits(string i_UserInput)
        {
            return int.TryParse(i_UserInput, out int floatInput);
        }

        public static int CorrectUserInput(string i_UserInput, int i_NumberOfInputOptions)
        {
            int validInput = Constants.k_InValid;
            bool parseSucces = int.TryParse(i_UserInput, out int correctNumber);
            if(parseSucces && correctNumber >= Constants.k_MinimalValue && correctNumber < i_NumberOfInputOptions)
            {
                validInput = correctNumber;
            }

            return validInput;
        }
    }
}