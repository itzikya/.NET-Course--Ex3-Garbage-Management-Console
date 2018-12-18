using System;
using System.Collections.Generic;
using System.Text;

namespace Ex03.GarageLogic
{
     public class Motorcycle : Vehicle
     {
          private const int k_NumberOfWheelsInMotorcycle = 2;
          private const int k_MaximumAirPressure = 30;
          private const GasolineEngine.eFuelType k_MotorcycleFuelType = GasolineEngine.eFuelType.Octan96;
          private const float k_MotorcycleVolumeOfFuelTank = 6;
          private const float k_MaximumBatteryLifeHours = 1.8f;
          private int m_EngineCapacity;
          private eLicenseType m_LicenseType;

          public int EngineCapacity
          {
               get { return m_EngineCapacity; }
          }

          public eLicenseType LicenseType
          {
               get { return m_LicenseType; }
          }

          public Motorcycle(string i_licenseNumber)
               : base(i_licenseNumber)
          {       
          }

          public override void FulfillVehicleDetails(VehicleEntranceForm i_VehicleEntranceForm)
          {
               Model = i_VehicleEntranceForm.VehicleModel;
               m_LicenseType = i_VehicleEntranceForm.MotorcycleLicenseType;
               m_EngineCapacity = i_VehicleEntranceForm.MotorcycleEngineCapacity;
               if (i_VehicleEntranceForm.VehicleType == VehicleFactory.eVehicleType.ElectricMotorcycle)
               {
                    Engine = new ElectricEngine(i_VehicleEntranceForm.RemainingBatteryHours, k_MaximumBatteryLifeHours);
               }
               else
               {
                    Engine = new GasolineEngine(k_MotorcycleFuelType, i_VehicleEntranceForm.CurrentFuelAmount, k_MotorcycleVolumeOfFuelTank);
               }

               for (int i = 0; i < k_NumberOfWheelsInMotorcycle; i++)
               {
                    Wheel WheelToAdd = new Wheel(
                         i_VehicleEntranceForm.WheelManufacturer,
                         i_VehicleEntranceForm.WheelCurrentAirPressure,
                         k_MaximumAirPressure);
                    Wheels.Add(WheelToAdd);
               }
          }

          public override string ToString()
          {
               StringBuilder str = new StringBuilder();
               str.AppendLine(base.ToString());
               str.AppendLine("Motorcycle Properties:");
               str.AppendFormat("License Type: {0}{1}", m_LicenseType, Environment.NewLine);
               str.AppendFormat("Engine Capacity: {0}{1}", m_EngineCapacity, Environment.NewLine);
               return str.ToString();
          }

          public enum eLicenseType
          {
               A = 1,
               A1,
               B1,
               B2
          }
     }
}
