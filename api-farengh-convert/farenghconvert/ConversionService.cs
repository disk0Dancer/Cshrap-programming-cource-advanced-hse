using CoreWCF;
using System;
using System.Runtime.Serialization;

namespace farenghconvert
{
    public class ConversionService : IConversionService
    {
        public double FahrenheitToCelsius(double farenheitDegrees)
        {
            return 5.0 / 9 * (farenheitDegrees - 32);
        }

        public double CelsiusToFahrenheit(double celsiusDegrees)
        {
            return 9.0 / 5 * celsiusDegrees + 32;
        }
    }
}