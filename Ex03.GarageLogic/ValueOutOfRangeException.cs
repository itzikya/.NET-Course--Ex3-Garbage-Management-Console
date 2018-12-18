using System;
using System.Collections.Generic;
using System.Text;

namespace Ex03.GarageLogic
{
     public class ValueOutOfRangeException : Exception
     {
          private const string k_MassageError = "Value not in range. should be between {0} to {1}";
          private readonly float r_MinValue;
          private readonly float r_MaxValue;

          public ValueOutOfRangeException(float i_MinValue, float i_MaxValue)
               : base(string.Format(k_MassageError, i_MinValue, i_MaxValue))
          {
               r_MaxValue = i_MaxValue;
               r_MinValue = i_MinValue;
          }
     }
}
