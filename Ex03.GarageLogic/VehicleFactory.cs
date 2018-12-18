using System;
using System.Collections.Generic;
using System.Text;

namespace Ex03.GarageLogic
{
     public static class VehicleFactory
     {
          public static Vehicle CreateNewVehicle(string i_LicenseNumber, eVehicleType i_VehicleType)
          {
               Vehicle generatedVehicle = null;
               if (i_VehicleType == eVehicleType.ElectricCar
                    || i_VehicleType == eVehicleType.GasolineCar)
               {
                    generatedVehicle = new Car(i_LicenseNumber);
               }
               else if (i_VehicleType == eVehicleType.ElectricMotorcycle
                    || i_VehicleType == eVehicleType.GasolineMotorcycle)
               {
                    generatedVehicle = new Motorcycle(i_LicenseNumber);
               }
               else if (i_VehicleType == eVehicleType.Truck)
               {
                    generatedVehicle = new Truck(i_LicenseNumber);
               }

               return generatedVehicle;
          }

          public enum eVehicleType
          {
               GasolineCar = 1,
               ElectricCar,
               GasolineMotorcycle,
               ElectricMotorcycle,
               Truck
          }
     }
}
