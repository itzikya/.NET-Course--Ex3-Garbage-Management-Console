using System;
using System.Collections.Generic;
using System.Text;

namespace Ex03.GarageLogic
{
     public class EnergySource
     {
          protected const int k_ToDecimalPrecentage = 100;
          private float m_EnergyPercentage;

          protected float EnergyPercentage
          {
               get { return m_EnergyPercentage; }
               set { m_EnergyPercentage = value; }
          }

          public EnergySource(float i_EnergyPercentage)
          {
               m_EnergyPercentage = i_EnergyPercentage;
          }

          public override string ToString()
          {
               StringBuilder str = new StringBuilder();
               str.AppendFormat("Remaining energy: {0}%", m_EnergyPercentage);
               return str.ToString();
          }

          public enum eEngineType
          {
               Gasoline,
               Electric
          }
     } 
}
