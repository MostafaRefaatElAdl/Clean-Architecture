using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace OnionArch.Contracts.Authentication
{
    public record RegisterRequest(
    string FirstName,
    string LastName,
    string Email,
    string Password);
}
