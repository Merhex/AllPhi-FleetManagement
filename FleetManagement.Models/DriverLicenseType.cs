using System;

namespace FleetManagement.Models
{
    [Flags]
    public enum DriverLicenseType
    {
        None = 0,
        AM = 1 << 1,
        A1 = 1 << 2,
        A2 = 1 << 3,
        A = 1 << 4,
        B = 1 << 5,
        C1 = 1 << 6,
        C = 1 << 7,
        D1 = 1 << 8,
        D = 1 << 9,
        BE = 1 << 10,
        C1E = 1 << 11,
        CE = 1 << 12,
        D1E = 1 << 13,
        DE = 1 << 14,
        G = 1 << 15
    }
}
