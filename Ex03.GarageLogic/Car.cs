using System;
using System.Collections.Generic;
using System.Text;

namespace Ex03.GarageLogic
{
     public class Car : Vehicle
     {
          private const int k_NumberOfWheelsInCar = 4;
          private const int k_MaximumAirPressure = 32;
          private const GasolineEngine.eFuelType k_CarFuelType = GasolineEngine.eFuelType.Octan98;
          private const float k_CarVolumeOfFuelTank = 45;
          private const float k_MaximumBatteryLifeHours = 3.2f;
          private eCarColor m_Color;
          private int m_DoorsNumber;

          public Car(string i_VehicleLicenseNumber)
               : base(i_VehicleLicenseNumber)
          {
          }

          public override void FulfillVehicleDetails(VehicleEntranceForm i_VehicleEntranceForm)
          {
               Model = i_VehicleEntranceForm.VehicleModel;
               m_Color = i_VehicleEntranceForm.CarColor;
               m_DoorsNumber = i_VehicleEntranceForm.CarDoorsNumber;

               if (i_VehicleEntranceForm.VehicleType == VehicleFactory.eVehicleType.ElectricMotorcycle)
               {
                    Engine = new ElectricEngine(i_VehicleEntranceForm.RemainingBatteryHours, k_MaximumBatteryLifeHours);
               }
               else
               {
                    Engine = new GasolineEngine(k_CarFuelType, i_VehicleEntranceForm.CurrentFuelAmount, k_CarVolumeOfFuelTank);
               }

               for (int i = 0; i < k_NumberOfWheelsInCar; i++)
               {
                    Wheel WheelToAdd = new Wheel(
                         i_VehicleEntranceForm.WheelManufacturer,
                         i_VehicleEntranceForm.WheelCurrentAirPressure,
                         k_MaximumAirPressure);
                    Wheels.Add(WheelToAdd);
               }
          }

          public eCarColor Color
          {
               get { return m_Color; }
          }

          public int DoorsNumber
          {
               get { return m_DoorsNumber; }
          }

          public enum eCarColor
          {
               Grey = 1,
               Blue,
               White,
               Black
          }

          public enum eCarDoors
          {
               Two = 2,
               Three,
               Four,
               Five
          }

          public override string ToString()
          {
               StringBuilder str = new StringBuilder();
               str.AppendLine(base.ToString());
               str.AppendLine("Car Properties:");
               str.AppendFormat("Number of Doors: {0}{1}", m_DoorsNumber, Environment.NewLine);
               str.AppendFormat("Car Color: {0}{1}", m_Color, Environment.NewLine);
               return str.ToString();
          }
     }
}
