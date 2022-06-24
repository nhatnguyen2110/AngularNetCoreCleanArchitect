using OpenWeatherMapAPI.Models;

namespace CleanArchitecture.Application.GetWeatherConditions.Queries.GetWeatherConditions;

public class WeatherConditionCollectionDto
{
    public List<OWPWeatherConditionGroup> WeatherConditionGroups { get; set; } = new List<OWPWeatherConditionGroup>();
    public List<OWPWeatherCondition> WeatherConditions { get; set; } = new List<OWPWeatherCondition>();
    public List<OWPWeatherCondition> WeatherConditionsInNight { get; set; } = new List<OWPWeatherCondition>();
}
