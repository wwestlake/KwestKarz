using System;
using System.Linq;

namespace KwestKarz.Services
{
    public interface ITripEarningsService
    {
        int ImportTripEarnings(Stream csvStream);
    }
}
