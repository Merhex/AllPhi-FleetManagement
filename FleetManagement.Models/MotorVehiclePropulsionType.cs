using System;

namespace FleetManagement.Models
{
    [Flags]
    public enum MotorVehiclePropulsionType
    {
        Gasoline = 1 << 1,
        Diesel = 1 << 2,
        Electric = 1 << 3,
        HybridGasoline = 1 << 4,
        HybridDiesel = 1 << 5,
        Other = 1 << 6,
    }
}
