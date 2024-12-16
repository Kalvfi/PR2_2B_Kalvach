using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OOP_Dedicnost_Test
{
    internal class Gadgets
    {
        public List<ElectricDevice> Devices;
        public int MaxVoltage { get; private set; }

        public Gadgets(ElectricDevice[] devices)
        {
            Devices = devices.ToList();
            MaxVoltage = Devices.Max(d => d.Voltage);
        }

        public void TurnAllOn()
        {
            foreach (ElectricDevice d in Devices)
            {
                Console.WriteLine(d.TurnOn());
            }
        }
        public void TurnAllOff()
        {
            foreach (ElectricDevice d in Devices)
            {
                Console.WriteLine(d.TurnOff());
            }
        }
    }
}
