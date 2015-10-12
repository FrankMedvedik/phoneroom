using System;
using System.Globalization;
using System.Net;
using System.Threading.Tasks;
using Microsoft.Lync.Model;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Linq;
using PhoneLogic.Model;
using PhoneLogic.Model.Models;


namespace PhoneLogic.Core.Services
{

    public static class PhoneNumberSvc
    {
        public static string GetNumbers(string input)
        {
            return new string(input.Where(c => char.IsDigit(c)).ToArray());
        }

    }
}
