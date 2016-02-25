using System.Linq;
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
