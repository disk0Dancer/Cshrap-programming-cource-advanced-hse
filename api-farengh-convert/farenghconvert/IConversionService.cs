namespace farenghconvert;

[ServiceContract(Namespace = "http://fahrenheittocelsiusconversion.azurewebsites.net/")]
public interface IConversionService
{
        [OperationContract]
        double FahrenheitToCelsius(double farenheitDegrees);

        [OperationContract]
        double CelsiusToFahrenheit(double celsiusDegrees);

}