using System;
using System.Collections.Generic;
using System.Text;

namespace Ex03.GarageLogic
{
     public abstract class Vehicle
     {
          private readonly string r_LicenseNumber;
          private readonly List<Wheel> r_Wheels = new List<Wheel>();
          private string m_Model;
          private EnergySource m_Engine;

          public Vehicle(string i_LicenseNumber)
          {
               r_LicenseNumber = i_LicenseNumber;
          }

          public override string ToString()
          {
               StringBuilder str = new StringBuilder();
               str.AppendLine("Vehicle properties:");
               str.AppendFormat("Vehicle model: {0}{1}", m_Model, Environment.NewLine);
               str.AppendFormat("Licende Number: {0}{1}", r_LicenseNumber, Environment.NewLine);
               str.AppendFormat(m_Engine.ToString());
               str.AppendFormat(r_Wheels[0].ToString());
               return str.ToString();
          }

          public string Model
          {
               get { return m_Model; }
               set { m_Model = value; }
          }

          public string LicenseNumber
          {
               get { return r_LicenseNumber; }
          }

          public EnergySource Engine
          {
               get { return m_Engine; }
               set { m_Engine = value; }
          }

          public List<Wheel> Wheels
          {
               get { return r_Wheels; }
          }

          public void Inflate()
          {
               foreach (Wheel wheel in Wheels)
               {
                    wheel.InflateWheelToFull();
               }
          }

          public abstract void FulfillVehicleDetails(VehicleEntranceForm i_VehicleEntranceForm);
     }
}
