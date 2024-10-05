using NK.Core.Model.Enums;

namespace NK.Core.Business.Utilities
{
    public static class WeatherHelper
    {
        public static Weather GetWeatherByMonth(int month)
        {
            switch (month)
            {
                case 1:
                case 2:
                case 3:
                    return Weather.Xuan;
                case 4:
                case 5:
                case 6:
                    return Weather.Ha;
                case 7:
                case 8:
                case 9:
                    return Weather.Thu;
                case 10:
                case 11:
                case 12:
                    return Weather.Dong;
                default:
                    throw new ArgumentOutOfRangeException(nameof(month), "Invalid month");
            }
        }
    }
}
