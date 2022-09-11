using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnionArch.Application.Common.Interfaces.Serivces
{
    public interface IDateTimeProvider
    {
        DateTime UtcNow { get; }
    }
}
