using OpenWeatherMapAPI.Models;

namespace OpenWeatherMapAPI.Shared;

public static class OpenWeatherStorage
{
    public static List<OWPWeatherConditionGroup> GetWeatherConditionGroup()
    {
        return new List<OWPWeatherConditionGroup>()
        {
            new OWPWeatherConditionGroup()
            {
                Id = "2xx",
                Name = "Thunderstorm",
                OrderIndex = 6
            },
            new OWPWeatherConditionGroup()
            {
                Id = "3xx",
                Name = "Drizzle",
                OrderIndex = 5
            },
            new OWPWeatherConditionGroup()
            {
                Id = "5xx",
                Name = "Rain",
                OrderIndex = 3
            },
            new OWPWeatherConditionGroup()
            {
                Id = "6xx",
                Name = "Snow",
                OrderIndex = 7
            },
            new OWPWeatherConditionGroup()
            {
                Id = "7xx",
                Name = "Atmosphere",
                OrderIndex = 4
            },
            new OWPWeatherConditionGroup()
            {
                Id = "800",
                Name = "Clear",
                OrderIndex = 1
            },
            new OWPWeatherConditionGroup()
            {
                Id = "80x",
                Name = "Clouds",
                OrderIndex = 2
            }
        };
    }
    public static List<OWPWeatherCondition> GetWeatherConditions()
    {
        return new List<OWPWeatherCondition>()
        {
            new OWPWeatherCondition()
            {
                Id = 200,
                Main = "Thunderstorm",
                Description = "thunderstorm with light rain",
                Icon = "11d",
                WeatherConditionGroupId = "2xx"
            },
            new OWPWeatherCondition()
            {
                Id = 201,
                Main = "Thunderstorm",
                Description = "thunderstorm with rain",
                Icon = "11d",
                WeatherConditionGroupId = "2xx"
            },
            new OWPWeatherCondition()
            {
                Id = 202,
                Main = "Thunderstorm",
                Description = "thunderstorm with heavy rain",
                Icon = "11d",
                WeatherConditionGroupId = "2xx"
            },
            new OWPWeatherCondition()
            {
                Id = 210,
                Main = "Thunderstorm",
                Description = "light thunderstorm",
                Icon = "11d",
                WeatherConditionGroupId = "2xx"
            },
            new OWPWeatherCondition()
            {
                Id = 211,
                Main = "Thunderstorm",
                Description = "thunderstorm",
                Icon = "11d",
                WeatherConditionGroupId = "2xx"
            },
            new OWPWeatherCondition()
            {
                Id = 212,
                Main = "Thunderstorm",
                Description = "heavy thunderstorm",
                Icon = "11d",
                WeatherConditionGroupId = "2xx"
            },
            new OWPWeatherCondition()
            {
                Id = 221,
                Main = "Thunderstorm",
                Description = "ragged thunderstorm",
                Icon = "11d",
                WeatherConditionGroupId = "2xx"
            },
            new OWPWeatherCondition()
            {
                Id = 230,
                Main = "Thunderstorm",
                Description = "thunderstorm with light drizzle",
                Icon = "11d",
                WeatherConditionGroupId = "2xx"
            },
            new OWPWeatherCondition()
            {
                Id = 231,
                Main = "Thunderstorm",
                Description = "thunderstorm with drizzle",
                Icon = "11d",
                WeatherConditionGroupId = "2xx"
            },
            new OWPWeatherCondition()
            {
                Id = 232,
                Main = "Thunderstorm",
                Description = "thunderstorm with heavy drizzle",
                Icon = "11d",
                WeatherConditionGroupId = "2xx"
            },
            new OWPWeatherCondition()
            {
                Id = 300,
                Main = "Drizzle",
                Description = "light intensity drizzle",
                Icon = "09d",
                WeatherConditionGroupId = "3xx"
            },
            new OWPWeatherCondition()
            {
                Id = 301,
                Main = "Drizzle",
                Description = "drizzle",
                Icon = "09d",
                WeatherConditionGroupId = "3xx"
            },
            new OWPWeatherCondition()
            {
                Id = 302,
                Main = "Drizzle",
                Description = "heavy intensity drizzle",
                Icon = "09d",
                WeatherConditionGroupId = "3xx"
            },
            new OWPWeatherCondition()
            {
                Id = 310,
                Main = "Drizzle",
                Description = "light intensity drizzle rain",
                Icon = "09d",
                WeatherConditionGroupId = "3xx"
            },
            new OWPWeatherCondition()
            {
                Id = 311,
                Main = "Drizzle",
                Description = "drizzle rain",
                Icon = "09d",
                WeatherConditionGroupId = "3xx"
            },
            new OWPWeatherCondition()
            {
                Id = 312,
                Main = "Drizzle",
                Description = "heavy intensity drizzle rain",
                Icon = "09d",
                WeatherConditionGroupId = "3xx"
            },
            new OWPWeatherCondition()
            {
                Id = 313,
                Main = "Drizzle",
                Description = "shower rain and drizzle",
                Icon = "09d",
                WeatherConditionGroupId = "3xx"
            },
            new OWPWeatherCondition()
            {
                Id = 314,
                Main = "Drizzle",
                Description = "heavy shower rain and drizzle",
                Icon = "09d",
                WeatherConditionGroupId = "3xx"
            },
            new OWPWeatherCondition()
            {
                Id = 321,
                Main = "Drizzle",
                Description = "shower drizzle",
                Icon = "09d",
                WeatherConditionGroupId = "3xx"
            },
            new OWPWeatherCondition()
            {
                Id = 500,
                Main = "Rain",
                Description = "light rain",
                Icon = "10d",
                WeatherConditionGroupId = "5xx"
            },
            new OWPWeatherCondition()
            {
                Id = 501,
                Main = "Rain",
                Description = "moderate rain",
                Icon = "10d",
                WeatherConditionGroupId = "5xx"
            },
            new OWPWeatherCondition()
            {
                Id = 502,
                Main = "Rain",
                Description = "heavy intensity rain",
                Icon = "10d",
                WeatherConditionGroupId = "5xx"
            },
            new OWPWeatherCondition()
            {
                Id = 503,
                Main = "Rain",
                Description = "very heavy rain",
                Icon = "10d",
                WeatherConditionGroupId = "5xx"
            },
            new OWPWeatherCondition()
            {
                Id = 504,
                Main = "Rain",
                Description = "extreme rain",
                Icon = "10d",
                WeatherConditionGroupId = "5xx"
            },
            new OWPWeatherCondition()
            {
                Id = 511,
                Main = "Rain",
                Description = "freezing rain",
                Icon = "13d",
                WeatherConditionGroupId = "5xx"
            },
            new OWPWeatherCondition()
            {
                Id = 520,
                Main = "Rain",
                Description = "light intensity shower rain",
                Icon = "09d",
                WeatherConditionGroupId = "5xx"
            },
            new OWPWeatherCondition()
            {
                Id = 521,
                Main = "Rain",
                Description = "shower rain",
                Icon = "09d",
                WeatherConditionGroupId = "5xx"
            },
            new OWPWeatherCondition()
            {
                Id = 522,
                Main = "Rain",
                Description = "heavy intensity shower rain",
                Icon = "09d",
                WeatherConditionGroupId = "5xx"
            },
            new OWPWeatherCondition()
            {
                Id = 522,
                Main = "Rain",
                Description = "ragged shower rain",
                Icon = "09d",
                WeatherConditionGroupId = "5xx"
            },
            new OWPWeatherCondition()
            {
                Id = 600,
                Main = "Snow",
                Description = "light snow",
                Icon = "13d",
                WeatherConditionGroupId = "6xx"
            },
            new OWPWeatherCondition()
            {
                Id = 601,
                Main = "Snow",
                Description = "Snow",
                Icon = "13d",
                WeatherConditionGroupId = "6xx"
            },
            new OWPWeatherCondition()
            {
                Id = 602,
                Main = "Snow",
                Description = "Heavy snow",
                Icon = "13d",
                WeatherConditionGroupId = "6xx"
            },
            new OWPWeatherCondition()
            {
                Id = 611,
                Main = "Snow",
                Description = "Sleet",
                Icon = "13d",
                WeatherConditionGroupId = "6xx"
            },
            new OWPWeatherCondition()
            {
                Id = 612,
                Main = "Snow",
                Description = "Light shower sleet",
                Icon = "13d",
                WeatherConditionGroupId = "6xx"
            },
            new OWPWeatherCondition()
            {
                Id = 613,
                Main = "Snow",
                Description = "Shower sleet",
                Icon = "13d",
                WeatherConditionGroupId = "6xx"
            },
            new OWPWeatherCondition()
            {
                Id = 615,
                Main = "Snow",
                Description = "Light rain and snow",
                Icon = "13d",
                WeatherConditionGroupId = "6xx"
            },
            new OWPWeatherCondition()
            {
                Id = 616,
                Main = "Snow",
                Description = "Rain and snow",
                Icon = "13d",
                WeatherConditionGroupId = "6xx"
            },
            new OWPWeatherCondition()
            {
                Id = 620,
                Main = "Snow",
                Description = "Light shower snow",
                Icon = "13d",
                WeatherConditionGroupId = "6xx"
            },
            new OWPWeatherCondition()
            {
                Id = 621,
                Main = "Snow",
                Description = "Shower snow",
                Icon = "13d",
                WeatherConditionGroupId = "6xx"
            },
            new OWPWeatherCondition()
            {
                Id = 622,
                Main = "Snow",
                Description = "Heavy shower snow",
                Icon = "13d",
                WeatherConditionGroupId = "6xx"
            },
            new OWPWeatherCondition()
            {
                Id = 701,
                Main = "Mist",
                Description = "Mist",
                Icon = "50d",
                WeatherConditionGroupId = "7xx"
            },
            new OWPWeatherCondition()
            {
                Id = 711,
                Main = "Smoke",
                Description = "Smoke",
                Icon = "50d",
                WeatherConditionGroupId = "7xx"
            },
            new OWPWeatherCondition()
            {
                Id = 721,
                Main = "Haze",
                Description = "Haze",
                Icon = "50d",
                WeatherConditionGroupId = "7xx"
            },
            new OWPWeatherCondition()
            {
                Id = 731,
                Main = "Dust",
                Description = "sand/ dust whirls",
                Icon = "50d",
                WeatherConditionGroupId = "7xx"
            },
            new OWPWeatherCondition()
            {
                Id = 741,
                Main = "Fog",
                Description = "fog",
                Icon = "50d",
                WeatherConditionGroupId = "7xx"
            },
            new OWPWeatherCondition()
            {
                Id = 751,
                Main = "Sand",
                Description = "sand",
                Icon = "50d",
                WeatherConditionGroupId = "7xx"
            },
            new OWPWeatherCondition()
            {
                Id = 761,
                Main = "Dust",
                Description = "dust",
                Icon = "50d",
                WeatherConditionGroupId = "7xx"
            },
            new OWPWeatherCondition()
            {
                Id = 762,
                Main = "Ash",
                Description = "volcanic ash",
                Icon = "50d",
                WeatherConditionGroupId = "7xx"
            },
            new OWPWeatherCondition()
            {
                Id = 771,
                Main = "Squall",
                Description = "squalls",
                Icon = "50d",
                WeatherConditionGroupId = "7xx"
            },
            new OWPWeatherCondition()
            {
                Id = 781,
                Main = "Tornado",
                Description = "tornado",
                Icon = "50d",
                WeatherConditionGroupId = "7xx"
            },
            new OWPWeatherCondition()
            {
                Id = 800,
                Main = "Clear",
                Description = "clear sky",
                Icon = "01d",
                WeatherConditionGroupId = "800"
            },
            new OWPWeatherCondition()
            {
                Id = 801,
                Main = "Clouds",
                Description = "few clouds: 11-25%",
                Icon = "02d",
                WeatherConditionGroupId = "80x"
            },
            new OWPWeatherCondition()
            {
                Id = 802,
                Main = "Clouds",
                Description = "scattered clouds: 25-50%",
                Icon = "03d",
                WeatherConditionGroupId = "80x"
            },
            new OWPWeatherCondition()
            {
                Id = 803,
                Main = "Clouds",
                Description = "broken clouds: 51-84%",
                Icon = "04d",
                WeatherConditionGroupId = "80x"
            },
            new OWPWeatherCondition()
            {
                Id = 804,
                Main = "Clouds",
                Description = "overcast clouds: 85-100%",
                Icon = "04d",
                WeatherConditionGroupId = "80x"
            }

        };
    }
    public static List<OWPWeatherCondition> GetWeatherConditions_InNight()
    {
        return new List<OWPWeatherCondition>()
        {
            new OWPWeatherCondition()
            {
                Id = 200,
                Main = "Thunderstorm",
                Description = "thunderstorm with light rain",
                Icon = "11n",
                WeatherConditionGroupId = "2xx"
            },
            new OWPWeatherCondition()
            {
                Id = 201,
                Main = "Thunderstorm",
                Description = "thunderstorm with rain",
                Icon = "11n",
                WeatherConditionGroupId = "2xx"
            },
            new OWPWeatherCondition()
            {
                Id = 202,
                Main = "Thunderstorm",
                Description = "thunderstorm with heavy rain",
                Icon = "11n",
                WeatherConditionGroupId = "2xx"
            },
            new OWPWeatherCondition()
            {
                Id = 210,
                Main = "Thunderstorm",
                Description = "light thunderstorm",
                Icon = "11n",
                WeatherConditionGroupId = "2xx"
            },
            new OWPWeatherCondition()
            {
                Id = 211,
                Main = "Thunderstorm",
                Description = "thunderstorm",
                Icon = "11n",
                WeatherConditionGroupId = "2xx"
            },
            new OWPWeatherCondition()
            {
                Id = 212,
                Main = "Thunderstorm",
                Description = "heavy thunderstorm",
                Icon = "11n",
                WeatherConditionGroupId = "2xx"
            },
            new OWPWeatherCondition()
            {
                Id = 221,
                Main = "Thunderstorm",
                Description = "ragged thunderstorm",
                Icon = "11n",
                WeatherConditionGroupId = "2xx"
            },
            new OWPWeatherCondition()
            {
                Id = 230,
                Main = "Thunderstorm",
                Description = "thunderstorm with light drizzle",
                Icon = "11n",
                WeatherConditionGroupId = "2xx"
            },
            new OWPWeatherCondition()
            {
                Id = 231,
                Main = "Thunderstorm",
                Description = "thunderstorm with drizzle",
                Icon = "11n",
                WeatherConditionGroupId = "2xx"
            },
            new OWPWeatherCondition()
            {
                Id = 232,
                Main = "Thunderstorm",
                Description = "thunderstorm with heavy drizzle",
                Icon = "11n",
                WeatherConditionGroupId = "2xx"
            },
            new OWPWeatherCondition()
            {
                Id = 300,
                Main = "Drizzle",
                Description = "light intensity drizzle",
                Icon = "09n",
                WeatherConditionGroupId = "3xx"
            },
            new OWPWeatherCondition()
            {
                Id = 301,
                Main = "Drizzle",
                Description = "drizzle",
                Icon = "09n",
                WeatherConditionGroupId = "3xx"
            },
            new OWPWeatherCondition()
            {
                Id = 302,
                Main = "Drizzle",
                Description = "heavy intensity drizzle",
                Icon = "09n",
                WeatherConditionGroupId = "3xx"
            },
            new OWPWeatherCondition()
            {
                Id = 310,
                Main = "Drizzle",
                Description = "light intensity drizzle rain",
                Icon = "09n",
                WeatherConditionGroupId = "3xx"
            },
            new OWPWeatherCondition()
            {
                Id = 311,
                Main = "Drizzle",
                Description = "drizzle rain",
                Icon = "09n",
                WeatherConditionGroupId = "3xx"
            },
            new OWPWeatherCondition()
            {
                Id = 312,
                Main = "Drizzle",
                Description = "heavy intensity drizzle rain",
                Icon = "09n",
                WeatherConditionGroupId = "3xx"
            },
            new OWPWeatherCondition()
            {
                Id = 313,
                Main = "Drizzle",
                Description = "shower rain and drizzle",
                Icon = "09n",
                WeatherConditionGroupId = "3xx"
            },
            new OWPWeatherCondition()
            {
                Id = 314,
                Main = "Drizzle",
                Description = "heavy shower rain and drizzle",
                Icon = "09n",
                WeatherConditionGroupId = "3xx"
            },
            new OWPWeatherCondition()
            {
                Id = 321,
                Main = "Drizzle",
                Description = "shower drizzle",
                Icon = "09n",
                WeatherConditionGroupId = "3xx"
            },
            new OWPWeatherCondition()
            {
                Id = 500,
                Main = "Rain",
                Description = "light rain",
                Icon = "10n",
                WeatherConditionGroupId = "5xx"
            },
            new OWPWeatherCondition()
            {
                Id = 501,
                Main = "Rain",
                Description = "moderate rain",
                Icon = "10n",
                WeatherConditionGroupId = "5xx"
            },
            new OWPWeatherCondition()
            {
                Id = 502,
                Main = "Rain",
                Description = "heavy intensity rain",
                Icon = "10n",
                WeatherConditionGroupId = "5xx"
            },
            new OWPWeatherCondition()
            {
                Id = 503,
                Main = "Rain",
                Description = "very heavy rain",
                Icon = "10n",
                WeatherConditionGroupId = "5xx"
            },
            new OWPWeatherCondition()
            {
                Id = 504,
                Main = "Rain",
                Description = "extreme rain",
                Icon = "10n",
                WeatherConditionGroupId = "5xx"
            },
            new OWPWeatherCondition()
            {
                Id = 511,
                Main = "Rain",
                Description = "freezing rain",
                Icon = "13n",
                WeatherConditionGroupId = "5xx"
            },
            new OWPWeatherCondition()
            {
                Id = 520,
                Main = "Rain",
                Description = "light intensity shower rain",
                Icon = "09n",
                WeatherConditionGroupId = "5xx"
            },
            new OWPWeatherCondition()
            {
                Id = 521,
                Main = "Rain",
                Description = "shower rain",
                Icon = "09n",
                WeatherConditionGroupId = "5xx"
            },
            new OWPWeatherCondition()
            {
                Id = 522,
                Main = "Rain",
                Description = "heavy intensity shower rain",
                Icon = "09n",
                WeatherConditionGroupId = "5xx"
            },
            new OWPWeatherCondition()
            {
                Id = 522,
                Main = "Rain",
                Description = "ragged shower rain",
                Icon = "09n",
                WeatherConditionGroupId = "5xx"
            },
            new OWPWeatherCondition()
            {
                Id = 600,
                Main = "Snow",
                Description = "light snow",
                Icon = "13n",
                WeatherConditionGroupId = "6xx"
            },
            new OWPWeatherCondition()
            {
                Id = 601,
                Main = "Snow",
                Description = "Snow",
                Icon = "13n",
                WeatherConditionGroupId = "6xx"
            },
            new OWPWeatherCondition()
            {
                Id = 602,
                Main = "Snow",
                Description = "Heavy snow",
                Icon = "13n",
                WeatherConditionGroupId = "6xx"
            },
            new OWPWeatherCondition()
            {
                Id = 611,
                Main = "Snow",
                Description = "Sleet",
                Icon = "13n",
                WeatherConditionGroupId = "6xx"
            },
            new OWPWeatherCondition()
            {
                Id = 612,
                Main = "Snow",
                Description = "Light shower sleet",
                Icon = "13n",
                WeatherConditionGroupId = "6xx"
            },
            new OWPWeatherCondition()
            {
                Id = 613,
                Main = "Snow",
                Description = "Shower sleet",
                Icon = "13n",
                WeatherConditionGroupId = "6xx"
            },
            new OWPWeatherCondition()
            {
                Id = 615,
                Main = "Snow",
                Description = "Light rain and snow",
                Icon = "13n",
                WeatherConditionGroupId = "6xx"
            },
            new OWPWeatherCondition()
            {
                Id = 616,
                Main = "Snow",
                Description = "Rain and snow",
                Icon = "13n",
                WeatherConditionGroupId = "6xx"
            },
            new OWPWeatherCondition()
            {
                Id = 620,
                Main = "Snow",
                Description = "Light shower snow",
                Icon = "13n",
                WeatherConditionGroupId = "6xx"
            },
            new OWPWeatherCondition()
            {
                Id = 621,
                Main = "Snow",
                Description = "Shower snow",
                Icon = "13n",
                WeatherConditionGroupId = "6xx"
            },
            new OWPWeatherCondition()
            {
                Id = 622,
                Main = "Snow",
                Description = "Heavy shower snow",
                Icon = "13n",
                WeatherConditionGroupId = "6xx"
            },
            new OWPWeatherCondition()
            {
                Id = 701,
                Main = "Mist",
                Description = "Mist",
                Icon = "50n",
                WeatherConditionGroupId = "7xx"
            },
            new OWPWeatherCondition()
            {
                Id = 711,
                Main = "Smoke",
                Description = "Smoke",
                Icon = "50n",
                WeatherConditionGroupId = "7xx"
            },
            new OWPWeatherCondition()
            {
                Id = 721,
                Main = "Haze",
                Description = "Haze",
                Icon = "50n",
                WeatherConditionGroupId = "7xx"
            },
            new OWPWeatherCondition()
            {
                Id = 731,
                Main = "Dust",
                Description = "sand/ dust whirls",
                Icon = "50n",
                WeatherConditionGroupId = "7xx"
            },
            new OWPWeatherCondition()
            {
                Id = 741,
                Main = "Fog",
                Description = "fog",
                Icon = "50n",
                WeatherConditionGroupId = "7xx"
            },
            new OWPWeatherCondition()
            {
                Id = 751,
                Main = "Sand",
                Description = "sand",
                Icon = "50n",
                WeatherConditionGroupId = "7xx"
            },
            new OWPWeatherCondition()
            {
                Id = 761,
                Main = "Dust",
                Description = "dust",
                Icon = "50n",
                WeatherConditionGroupId = "7xx"
            },
            new OWPWeatherCondition()
            {
                Id = 762,
                Main = "Ash",
                Description = "volcanic ash",
                Icon = "50n",
                WeatherConditionGroupId = "7xx"
            },
            new OWPWeatherCondition()
            {
                Id = 771,
                Main = "Squall",
                Description = "squalls",
                Icon = "50n",
                WeatherConditionGroupId = "7xx"
            },
            new OWPWeatherCondition()
            {
                Id = 781,
                Main = "Tornado",
                Description = "tornado",
                Icon = "50n",
                WeatherConditionGroupId = "7xx"
            },
            new OWPWeatherCondition()
            {
                Id = 800,
                Main = "Clear",
                Description = "clear sky",
                Icon = "01n",
                WeatherConditionGroupId = "800"
            },
            new OWPWeatherCondition()
            {
                Id = 801,
                Main = "Clouds",
                Description = "few clouds: 11-25%",
                Icon = "02n",
                WeatherConditionGroupId = "80x"
            },
            new OWPWeatherCondition()
            {
                Id = 802,
                Main = "Clouds",
                Description = "scattered clouds: 25-50%",
                Icon = "03n",
                WeatherConditionGroupId = "80x"
            },
            new OWPWeatherCondition()
            {
                Id = 803,
                Main = "Clouds",
                Description = "broken clouds: 51-84%",
                Icon = "04n",
                WeatherConditionGroupId = "80x"
            },
            new OWPWeatherCondition()
            {
                Id = 804,
                Main = "Clouds",
                Description = "overcast clouds: 85-100%",
                Icon = "04n",
                WeatherConditionGroupId = "80x"
            }

        };
    }
}
