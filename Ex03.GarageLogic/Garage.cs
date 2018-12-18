using System;
using System.Collections.Generic;
using System.Text;

namespace Ex03.GarageLogic
{
     public class Garage
     {
          public class VehicleInfo
          {
               private Vehicle m_Vehicle;
               private string m_OwnerName;
               private string m_OwnerPhoneNumber;
               private Garage.eVehicleStatus m_VehicleStatus;

               public string OwnerName
               {
                    get { return m_OwnerName; }
                    set { m_OwnerName = value; }
               }

               public string OwnerPhoneNumber
               {
                    get { return m_OwnerPhoneNumber; }
                    set { m_OwnerPhoneNumber = value; }
               }

               public Vehicle Vehicle
               {
                    get { return m_Vehicle; }
                    set { m_Vehicle = value; }
               }

               public Garage.eVehicleStatus VehicleStatus
               {
                    get { return m_VehicleStatus; }
                    set { m_VehicleStatus = value; }
               }

               public VehicleInfo(Vehicle i_Vehicle, string i_OwnerName, string i_OwnerPhoneNumber)
               {
                    m_Vehicle = i_Vehicle;
                    m_OwnerName = i_OwnerName;
                    m_OwnerPhoneNumber = i_OwnerPhoneNumber;
                    m_VehicleStatus = eVehicleStatus.InRepair;
               }

               public override string ToString()
               {
                    StringBuilder str = new StringBuilder();
                    str.AppendFormat("Owner Name: {0}{1}", m_OwnerName, Environment.NewLine);
                    str.AppendFormat("Phone Number: {0}{1}", m_OwnerPhoneNumber, Environment.NewLine);
                    str.AppendFormat("Vehicle status: {0}{1}", m_VehicleStatus, Environment.NewLine);
                    str.AppendFormat("{0} {1}", m_Vehicle.ToString(), Environment.NewLine);

                    return str.ToString();
               }
          }
     
          private const string k_NoSuitableVehicleMassage = "Entered vehicle is not in the garage, Try again";
          private const string k_NotLegalFuel = "Cannot fuel vehicle";
          private const string k_NotLegalCharge = "Cannot charge vehicle";
          private readonly Dictionary<string, VehicleInfo> r_VehiclesInfo = new Dictionary<string, VehicleInfo>();

          public Dictionary<string, VehicleInfo> VehiclesInfo
          {
               get { return r_VehiclesInfo; }
          }

          public void EnterNewVehicle(Vehicle i_NewVehicleToEnter, VehicleEntranceForm i_VehicleForm)
          {
               VehicleInfo newVehicleInfo = new VehicleInfo(i_NewVehicleToEnter, i_VehicleForm.OwnerName, i_VehicleForm.OwnerPhone);
               r_VehiclesInfo.Add(newVehicleInfo.Vehicle.LicenseNumber, newVehicleInfo);
          }

          public string DisplayAllLicenseNumberOfVehicles()
          {
               StringBuilder allLicenseNumberOfVehicles = new StringBuilder();
               foreach (VehicleInfo vehicle in r_VehiclesInfo.Values)
               {
                    allLicenseNumberOfVehicles.AppendLine(vehicle.Vehicle.LicenseNumber);
               }

               return allLicenseNumberOfVehicles.ToString();
          }

          public string DisplayLicenseNumberOfVehiclesByStatus(eVehicleStatus i_RequestedStatusToFilter)
          {
               StringBuilder licenseNumberOfVehiclesByStatus = new StringBuilder();
               foreach (VehicleInfo vehicleInfo in r_VehiclesInfo.Values)
               {
                    if (vehicleInfo.VehicleStatus == i_RequestedStatusToFilter)
                    {
                         licenseNumberOfVehiclesByStatus.AppendLine(vehicleInfo.Vehicle.LicenseNumber);
                    }
               }

               return licenseNumberOfVehiclesByStatus.ToString();
          }

          public void ChangeVehicleStatus(string i_LicenseNumber, eVehicleStatus i_RecievedNewStatus)
          {
               if (r_VehiclesInfo.ContainsKey(i_LicenseNumber))
               {
                    r_VehiclesInfo[i_LicenseNumber].VehicleStatus = i_RecievedNewStatus;
               }
               else
               {
                    throw new ArgumentException(k_NoSuitableVehicleMassage);
               }
          }

          public void InflateWheelsToMax(string i_LicenseNumber)
          {
               if (r_VehiclesInfo.ContainsKey(i_LicenseNumber))
               {
                    r_VehiclesInfo[i_LicenseNumber].Vehicle.Inflate();
               }
               else
               {
                    throw new ArgumentException(k_NoSuitableVehicleMassage);
               }
          }

          public void Fuel(string i_LicenseNumber, GasolineEngine.eFuelType i_fuelToAdd, float i_amountToAdd)
          {
               if (r_VehiclesInfo.ContainsKey(i_LicenseNumber))
               {
                    GasolineEngine gasolineEngine = r_VehiclesInfo[i_LicenseNumber].Vehicle.Engine as GasolineEngine;
                    if (gasolineEngine != null)
                    {
                         gasolineEngine.Fuel(i_amountToAdd, i_fuelToAdd);
                    }
                    else
                    {
                         throw new ArgumentException(k_NotLegalFuel);
                    }
               }
               else
               {
                    throw new ArgumentException(k_NoSuitableVehicleMassage);
               }
          }

          public void Charge(string i_LicenseNumber, float i_minutesAmountToAdd)
          {
               if (r_VehiclesInfo.ContainsKey(i_LicenseNumber))
               {
                    ElectricEngine electricEngine = r_VehiclesInfo[i_LicenseNumber].Vehicle.Engine as ElectricEngine;
                    if (electricEngine != null)
                    {
                         electricEngine.Recharge(i_minutesAmountToAdd / 60);
                    }
                    else
                    {
                         throw new ArgumentException(k_NotLegalCharge);
                    }
               }
               else
               {
                    throw new ArgumentException(k_NoSuitableVehicleMassage);
               }
          }

          public string GetSpecificVehicleInfo(string i_LicenseNumber)
          {
               string specificVehicleInfo;

               if (r_VehiclesInfo.ContainsKey(i_LicenseNumber))
               {
                    specificVehicleInfo = r_VehiclesInfo[i_LicenseNumber].ToString();
               }
               else
               {
                    throw new ArgumentException(k_NoSuitableVehicleMassage);
               }

               return specificVehicleInfo;
          }

          public bool IsExistInGarage(string i_LicenseNumber)
          {
               return r_VehiclesInfo.ContainsKey(i_LicenseNumber);
          }

          public enum eFilter
          {
               InRepair = 1,
               Fixed,
               Payed,
               All
          }

          public enum eVehicleStatus
          {
               InRepair = 1,
               Fixed,
               Payed
          }
     }
}
