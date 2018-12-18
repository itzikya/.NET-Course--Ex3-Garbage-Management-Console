using System;
using System.Collections.Generic;
using System.Text;

namespace Ex03.GarageLogic
{
     public class ElectricEngine : EnergySource
     {
          private readonly float r_MaxBatteryHours;
          private float m_RemainingBatteryHours;

          public float RemainingBatteryHours
          {
               get { return m_RemainingBatteryHours; }
          }

          public float MaxBatteryHours
          {
               get { return r_MaxBatteryHours; }
          }

          public ElectricEngine(float i_RemainingBatteryHours, float i_MaxBatteryHours) : 
               base(i_RemainingBatteryHours / i_MaxBatteryHours * k_ToDecimalPrecentage)
          {
               r_MaxBatteryHours = i_MaxBatteryHours;
               if (i_RemainingBatteryHours <= r_MaxBatteryHours)
               {
                    m_RemainingBatteryHours = i_RemainingBatteryHours;
                    EnergyPercentage = m_RemainingBatteryHours / r_MaxBatteryHours * k_ToDecimalPrecentage;
               }
               else
               {
                    throw new ValueOutOfRangeException(0, r_MaxBatteryHours);
               }
          }

          public void Recharge(float i_HoursToCharge)
          {
               if (m_RemainingBatteryHours + i_HoursToCharge <= r_MaxBatteryHours)
               {
                    m_RemainingBatteryHours += i_HoursToCharge;
               }
               else
               {
                    throw new ValueOutOfRangeException(0, r_MaxBatteryHours - m_RemainingBatteryHours);
               }
          }

          public override string ToString()
          {
               StringBuilder str = new StringBuilder();
               str.AppendLine("Engine Properties:");
               str.AppendFormat("Engine Type: {0} {1}", eEngineType.Electric, Environment.NewLine);
               str.AppendLine(base.ToString());
               str.AppendFormat("Remaining battery status: {0}/{1} {2}", m_RemainingBatteryHours, r_MaxBatteryHours, Environment.NewLine);
               return str.ToString();
          }
     }
}
