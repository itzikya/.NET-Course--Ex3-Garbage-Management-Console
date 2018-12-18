using System;
using System.Collections.Generic;
using System.Text;

namespace Ex03.GarageLogic
{
     public class GasolineEngine : EnergySource
     {
          private const string k_NotLegalFuelType = "Not compatible fuel type";
          private readonly float r_MaxFuelAmount;
          private readonly eFuelType r_FuelType;
          private float m_CurrentFuelAmount;

          public eFuelType FuelType
          {
               get { return r_FuelType; }
          }

          public float CurrentFuelAmount
          {
               get { return m_CurrentFuelAmount; }
          }

          public float MaxFuelAmount
          {
               get { return r_MaxFuelAmount; }
          }

          public void Fuel(float i_FuelAmountToAdd, eFuelType i_fuelType)
          {
               if (i_fuelType != r_FuelType)
               {
                    throw new ArgumentException(k_NotLegalFuelType);
               }

               if (m_CurrentFuelAmount + i_FuelAmountToAdd <= r_MaxFuelAmount)
               {
                    m_CurrentFuelAmount += i_FuelAmountToAdd;
                    EnergyPercentage = m_CurrentFuelAmount / r_MaxFuelAmount * k_ToDecimalPrecentage;
               }
               else
               {
                    throw new ValueOutOfRangeException(0, r_MaxFuelAmount - m_CurrentFuelAmount);
               }
          }

          public GasolineEngine(eFuelType i_FuelType, float i_CurrentFuelAmount, float i_MaxFuelAmount)
               : base(i_CurrentFuelAmount / i_MaxFuelAmount * k_ToDecimalPrecentage)
          {
               r_FuelType = i_FuelType;
               r_MaxFuelAmount = i_MaxFuelAmount;
               if (i_CurrentFuelAmount <= r_MaxFuelAmount)
               {
                    m_CurrentFuelAmount = i_CurrentFuelAmount;
               }
               else
               {
                    throw new ValueOutOfRangeException(0, r_MaxFuelAmount);
               }
          }

          public enum eFuelType
          {
               Soler = 1,
               Octan95,
               Octan96,
               Octan98
          }

          public override string ToString()
          {
               StringBuilder str = new StringBuilder();
               str.AppendLine("Engine Properties:");
               str.AppendFormat("Engine Type {0} {1}", eEngineType.Gasoline, Environment.NewLine);
               str.AppendLine(base.ToString());
               str.AppendFormat("remaining battery status {0}/{1} {2}", m_CurrentFuelAmount, r_MaxFuelAmount, Environment.NewLine);
               return str.ToString();
          }
     }
}
