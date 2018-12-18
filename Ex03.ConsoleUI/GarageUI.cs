using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;
using System.Text.RegularExpressions;
using Ex03.GarageLogic;

namespace Ex03.ConsoleUI
{
     public class GarageUI
     {
          private const string k_VehicleAlreadyInGarageMassage = "Vehicle is already in garage, changing status to In Repair";
          private const string k_WheelsInflatedMassage = "Wheels inflated to maximum";
          private const string k_VehicleFueledMassage = "Vehicle fueled";
          private const string k_VehicleChargedMassage = "Vehicle charged";
          private const string k_VehicleStatusChangedMassage = "Vehicle's status changed";
          private const string k_VehicleEnteredMassage = "Vehicle entered to the garage";
          private const string k_IllegalInputMassage = "Illegal input, Try again";
          private Garage m_Garage = new Garage();

          public void Run()
          {
               bool isProgramActive = true;
               while (isProgramActive == true)
               {
                    PrintGarageMenu();
                    try
                    {
                         HandleKeyPress(Console.ReadLine(), ref isProgramActive);
                    }
                    catch (FormatException ex)
                    {
                         Console.WriteLine(ex.Message.ToString());
                    }
                    catch (Exception ex)
                    {
                         Console.WriteLine(ex.Message.ToString());
                    }
               }
          }

          private void PrintGarageMenu()
          {
               string garageMenu = string.Format(
@"Please choose one of the option below
[1] Enter new vehicle to the garage
[2] Display all license number of vehicles in the garage
[3] Change vehicle status
[4] Inflate wheels to maximum
[5] Fuel vehicle
[6] Charge vehicle
[7] Display vehicle info
[8] Exit");
               Console.WriteLine(garageMenu);
          }

          private void HandleKeyPress(string i_UserInput, ref bool i_IsProgramActive)
          {
               int userChoice = int.Parse(i_UserInput);
               if (Enum.IsDefined(typeof(eFunctionOption), userChoice))
               {
                    switch (userChoice)
                    {
                         case (int)eFunctionOption.EnterNewVehicle:
                              EnterNewVehicleRoutine();
                              break;
                         case (int)eFunctionOption.DisplayLicenseNumberInfo:
                              DisplayAllLicenseNumberInGarageRoutine();
                              break;
                         case (int)eFunctionOption.ChangeStatus:
                              ChangeVehicleStatusRoutine();
                              break;
                         case (int)eFunctionOption.Inflate:
                              InflateWheelsToMaxRoutine();
                              break;
                         case (int)eFunctionOption.Fuel:
                              FuelVehicleRoutine();
                              break;
                         case (int)eFunctionOption.Charge:
                              ChargeVehicleRoutine();
                              break;
                         case (int)eFunctionOption.DisplayVehiclesInfo:
                              DisplayVehicleInfoRoutine();
                              break;
                         case (int)eFunctionOption.Exit:
                              i_IsProgramActive = false;
                              break;
                    }
               }
               else
               {
                    throw new ValueOutOfRangeException((int)eFunctionOption.EnterNewVehicle, (int)eFunctionOption.Exit);
               }
          }

          private void InflateWheelsToMaxRoutine()
          {
               string licenseNumber = GetLicenseNumber();

               m_Garage.InflateWheelsToMax(licenseNumber);
               Console.WriteLine(k_WheelsInflatedMassage);
          }

          private void FuelVehicleRoutine()
          {
               string licenseNumber;
               GasolineEngine.eFuelType fuelType;
               float fuelAmount;

               licenseNumber = GetLicenseNumber();
               fuelType = GetFuelType();
               fuelAmount = GetFuelAmount();

               try
               {
                    m_Garage.Fuel(licenseNumber, fuelType, fuelAmount);
                    Console.WriteLine(k_VehicleFueledMassage);
               }
               catch (ArgumentException ex)
               {
                    Console.WriteLine(ex.Message);
               }
               catch (ValueOutOfRangeException ex)
               {
                    Console.WriteLine(ex.Message);
               }
          }

          private void ChargeVehicleRoutine()
          {
               string licenseNumber = GetLicenseNumber();
               float hoursAmount = GetHoursAmount();

               try
               {
                    m_Garage.Charge(licenseNumber, hoursAmount);
                    Console.WriteLine(k_VehicleChargedMassage);
               }
               catch (ValueOutOfRangeException ex)
               {
                    Console.WriteLine(ex.Message);
               }
               catch (ArgumentException ex)
               {
                    Console.WriteLine(ex.Message);
               }
          }

          private void DisplayVehicleInfoRoutine()
          {
               string licenseNumber = GetLicenseNumber();
               string vehicleInfo = m_Garage.GetSpecificVehicleInfo(licenseNumber);
               Console.WriteLine(vehicleInfo);
          }

          private void ChangeVehicleStatusRoutine()
          {
               string licenseNumber = GetLicenseNumber();
               Garage.eVehicleStatus newStatus = GetStatus();

               m_Garage.ChangeVehicleStatus(licenseNumber, newStatus);
               Console.WriteLine(k_VehicleStatusChangedMassage);    
          }

          private void EnterNewVehicleRoutine()
          {
               VehicleEntranceForm vehicleForm = new VehicleEntranceForm();
               vehicleForm.LicenseNumber = GetLicenseNumber();
               bool vehicleFoundInGarage = m_Garage.IsExistInGarage(vehicleForm.LicenseNumber);

               if (vehicleFoundInGarage)
               {
                    Console.WriteLine(k_VehicleAlreadyInGarageMassage);
                    m_Garage.ChangeVehicleStatus(vehicleForm.LicenseNumber, Garage.eVehicleStatus.InRepair);
               }
               else
               {
                    vehicleForm.VehicleType = GetVehicleType();
                    Vehicle newVehicleToInsert = VehicleFactory.CreateNewVehicle(vehicleForm.LicenseNumber, vehicleForm.VehicleType);
                    GetVehicleInfo(vehicleForm);
                    newVehicleToInsert.FulfillVehicleDetails(vehicleForm);
                    m_Garage.EnterNewVehicle(newVehicleToInsert, vehicleForm);
                    Console.WriteLine(k_VehicleEnteredMassage);
               }
          }

          private void GetVehicleInfo(VehicleEntranceForm i_VehicleEnranceForm)
          {
               i_VehicleEnranceForm.VehicleModel = GetVehicleModel();
               GetSpecificVehicleInfo(i_VehicleEnranceForm);
               GetEngineInfo(i_VehicleEnranceForm);
               GetWheelsInfo(i_VehicleEnranceForm);
               GetOwnerInfo(i_VehicleEnranceForm);
          }

          private void GetSpecificVehicleInfo(VehicleEntranceForm i_VehicleEnranceForm)
          {
               if (i_VehicleEnranceForm.VehicleType == VehicleFactory.eVehicleType.ElectricCar
                         || i_VehicleEnranceForm.VehicleType == VehicleFactory.eVehicleType.GasolineCar)
               {
                    i_VehicleEnranceForm.CarColor = GetCarColor();
                    i_VehicleEnranceForm.CarDoorsNumber = GetCarDoorsNumber();
               }
               else if (i_VehicleEnranceForm.VehicleType == VehicleFactory.eVehicleType.ElectricMotorcycle
                    || i_VehicleEnranceForm.VehicleType == VehicleFactory.eVehicleType.GasolineMotorcycle)
               {
                    i_VehicleEnranceForm.MotorcycleEngineCapacity = GetMotorcycleEngineCapacity();
                    i_VehicleEnranceForm.MotorcycleLicenseType = GetMotorcycleLicenseType();
               }
               else if (i_VehicleEnranceForm.VehicleType == VehicleFactory.eVehicleType.Truck)
               {
                    i_VehicleEnranceForm.TruckTrunkCapacity = GetTruckTrunkCapacity();
                    i_VehicleEnranceForm.IsTruckTrunkCool = GetCoolTruckTrunkSatus();
               }
          }

          private void GetEngineInfo(VehicleEntranceForm i_VehicleEnranceForm)
          {
               if (i_VehicleEnranceForm.VehicleType == VehicleFactory.eVehicleType.ElectricCar
                         || i_VehicleEnranceForm.VehicleType == VehicleFactory.eVehicleType.ElectricMotorcycle)
               {
                    i_VehicleEnranceForm.RemainingBatteryHours = GetRemainingBatteryHours();
               }
               else
               {
                    i_VehicleEnranceForm.CurrentFuelAmount = GetCurrentFuelAmount();
               }
          }

          private void GetWheelsInfo(VehicleEntranceForm i_VehicleEnranceForm)
          {
               i_VehicleEnranceForm.WheelManufacturer = GetWheelsManufacturer();
               i_VehicleEnranceForm.WheelCurrentAirPressure = GetWheelsAirPressure();
          }

          private void GetOwnerInfo(VehicleEntranceForm i_VehicleEnranceForm)
          {
               i_VehicleEnranceForm.OwnerName = GetOwnerName();
               i_VehicleEnranceForm.OwnerPhone = GetOwnerPhone();
          }

          private string GetOwnerPhone()
          {
               Console.WriteLine("Enter owner's phone:");
               string ownerPhone = Console.ReadLine();
               while (ownerPhone == string.Empty)
               {
                    Console.WriteLine(k_IllegalInputMassage);
                    ownerPhone = Console.ReadLine();
               }

               return ownerPhone;
          }

          private string GetOwnerName()
          {
               Console.WriteLine("Enter owner's name:");
               string ownerName = Console.ReadLine();
               while (ownerName == string.Empty)
               {
                    Console.WriteLine(k_IllegalInputMassage);
                    ownerName = Console.ReadLine();
               }

               return ownerName;
          }

          private string GetLicenseNumber()
          {
               Console.WriteLine("Enter vehicle's license number");
               string licenseNumber = Console.ReadLine();
               while (licenseNumber == string.Empty)
               {
                    Console.WriteLine(k_IllegalInputMassage);
                    licenseNumber = Console.ReadLine();
               }

               return licenseNumber;
          }

          private float GetHoursAmount()
          {
               Console.WriteLine("Enter hours amount to charge");
               string hoursAmountString = Console.ReadLine();
               float hoursAmount;
               while (!float.TryParse(hoursAmountString, out hoursAmount))
               {
                    Console.WriteLine(k_IllegalInputMassage);
                    hoursAmountString = Console.ReadLine();
               }

               return hoursAmount;
          }

          private Garage.eVehicleStatus GetStatus()
          {
               Console.WriteLine("Enter vehicle's new status:");
               PrintEnumOptions<Garage.eVehicleStatus>();

               return (Garage.eVehicleStatus)GetEnumInput<Garage.eVehicleStatus>();
          }

          private void PrintEnumOptions<T>()
          {
               foreach (var value in Enum.GetValues(typeof(T)))
               {
                    Console.WriteLine("[{0}] {1}", (int)value, (T)value);
               }
          }

          private int GetEnumInput<T>()
          {
               string userInput = Console.ReadLine();
               int enumOption;
               int.TryParse(userInput, out enumOption);
               bool isLegalInput = false;

               while (!isLegalInput)
               {
                    if (!int.TryParse(userInput, out enumOption) || !Enum.IsDefined(typeof(T), enumOption))
                    {
                         Console.WriteLine(k_IllegalInputMassage);
                         userInput = Console.ReadLine();
                    }
                    else
                    {
                         isLegalInput = true;
                    }
               }

               return enumOption;
          }

          private GasolineEngine.eFuelType GetFuelType()
          {
               Console.WriteLine("Enter vehicle's fuel type");
               PrintEnumOptions<GasolineEngine.eFuelType>();

               return (GasolineEngine.eFuelType)GetEnumInput<GasolineEngine.eFuelType>();
          }

          private float GetFuelAmount()
          {
               Console.WriteLine("Enter gasoline amount to fill");
               string fuelAmountString = Console.ReadLine();
               float fuelAmount;

               while (!float.TryParse(fuelAmountString, out fuelAmount))
               {
                    Console.WriteLine(k_IllegalInputMassage);
                    fuelAmountString = Console.ReadLine();
               }

               return fuelAmount;
          }

          private string GetVehicleModel()
          {
               Console.WriteLine("Enter vehicle's model:");
               string vehicleModel = Console.ReadLine();
               while (vehicleModel == string.Empty)
               {
                    Console.WriteLine(k_IllegalInputMassage);
                    vehicleModel = Console.ReadLine();
               }

               return vehicleModel;
          }

          private Garage.eFilter GetFilter()
          {
               Console.WriteLine("Enter requested filter");
               PrintEnumOptions<Garage.eFilter>();

               return (Garage.eFilter)GetEnumInput<Garage.eFilter>();
          }

          private void DisplayAllLicenseNumberInGarageRoutine()
          {
               Garage.eFilter filterChoice;
               string listToPrint;
               filterChoice = GetFilter();

               if (filterChoice == Garage.eFilter.All)
               {
                    listToPrint = m_Garage.DisplayAllLicenseNumberOfVehicles();
               }
               else
               {
                    listToPrint = m_Garage.DisplayLicenseNumberOfVehiclesByStatus((Garage.eVehicleStatus)filterChoice);
               }

               Console.WriteLine(listToPrint);
          }

          private VehicleFactory.eVehicleType GetVehicleType()
          {
               Console.WriteLine("Enter your vehicle type:");
               PrintEnumOptions<VehicleFactory.eVehicleType>();

               return (VehicleFactory.eVehicleType)GetEnumInput<VehicleFactory.eVehicleType>();
          }

          private Car.eCarColor GetCarColor()
          {
               Console.WriteLine("Choose your car color:");
               PrintEnumOptions<Car.eCarColor>();

               return (Car.eCarColor)GetEnumInput<Car.eCarColor>();
          }

          private int GetCarDoorsNumber()
          {
               Console.WriteLine("Choose your car numbers of doors: ");
               PrintEnumOptions<Car.eCarDoors>();

               return GetEnumInput<Car.eCarDoors>();
          }

          private int GetMotorcycleEngineCapacity()
          {
               Console.WriteLine("Enter engine capacity: ");
               string engineCapacityString = Console.ReadLine();
               int engineCapacity;

               while (!int.TryParse(engineCapacityString, out engineCapacity))
               {
                    Console.WriteLine(k_IllegalInputMassage);
                    engineCapacityString = Console.ReadLine();
               }

               return engineCapacity;
          }

          private Motorcycle.eLicenseType GetMotorcycleLicenseType()
          {
               Console.WriteLine("Choose your license type:");
               PrintEnumOptions<Motorcycle.eLicenseType>();

               return (Motorcycle.eLicenseType)GetEnumInput<Motorcycle.eLicenseType>();
          }

          private float GetRemainingBatteryHours()
          {
               Console.WriteLine("Enter remaining battery hours: ");
               string remainingBatteryHoursString = Console.ReadLine();
               float remainingBatteryHours;

               while (!float.TryParse(remainingBatteryHoursString, out remainingBatteryHours))
               {
                    Console.WriteLine(k_IllegalInputMassage);
                    remainingBatteryHoursString = Console.ReadLine();
               }

               return remainingBatteryHours;
          }

          private float GetCurrentFuelAmount()
          {
               Console.WriteLine("Enter current fuel amount: ");
               string currentFuelString = Console.ReadLine();
               float currentFuelAmount;

               while (!float.TryParse(currentFuelString, out currentFuelAmount))
               {
                    Console.WriteLine(k_IllegalInputMassage);
                    currentFuelString = Console.ReadLine();
               }

               return currentFuelAmount;
          }

          private string GetWheelsManufacturer()
          {
               Console.WriteLine("Enter wheels manufacturer's name: ");
               string wheelsManufacturer = Console.ReadLine();
               while (wheelsManufacturer == string.Empty)
               {
                    Console.WriteLine(k_IllegalInputMassage);
                    wheelsManufacturer = Console.ReadLine();
               }

               return wheelsManufacturer;
          }

          private float GetWheelsAirPressure()
          {
               Console.WriteLine("Enter wheels air pressure: ");
               string wheelsAirPressureString = Console.ReadLine();
               float wheelsAirPressure;

               while (!float.TryParse(wheelsAirPressureString, out wheelsAirPressure))
               {
                    Console.WriteLine(k_IllegalInputMassage);
                    wheelsAirPressureString = Console.ReadLine();
               }

               return wheelsAirPressure;
          }

          private int GetTruckTrunkCapacity()
          {
               Console.WriteLine("Enter trunk's truck capacity: ");
               string trunkCapacityString = Console.ReadLine();
               int trunkCapacity;

               while (!int.TryParse(trunkCapacityString, out trunkCapacity))
               {
                    Console.WriteLine(k_IllegalInputMassage);
                    trunkCapacityString = Console.ReadLine();
               }

               return trunkCapacity;
          }

          private Truck.eTruckTrunkCooling GetCoolTruckTrunkSatus()
          {
               Console.WriteLine("Is your truck's trunk cool?");
               PrintEnumOptions<Truck.eTruckTrunkCooling>();

               return (Truck.eTruckTrunkCooling)GetEnumInput<Truck.eTruckTrunkCooling>();
          }

          private enum eFunctionOption
          {
               EnterNewVehicle = 1,
               DisplayLicenseNumberInfo,
               ChangeStatus,
               Inflate,
               Fuel,
               Charge,
               DisplayVehiclesInfo,
               Exit
          }
     }
}
