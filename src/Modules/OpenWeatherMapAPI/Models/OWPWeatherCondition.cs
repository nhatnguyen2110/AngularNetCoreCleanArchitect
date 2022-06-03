namespace OpenWeatherMapAPI.Models;

public class OWPWeatherCondition
{
    public int Id { get; set; }
    public string? Main { get; set; }
    public string? Description { get; set; }
    public string? Icon { get; set; }
    public string IconUrl { get
        {
            return $"https://openweathermap.org/img/wn/{Icon}.png";
        } 
    }
    public string? WeatherConditionGroupId { get; set; }
}

public class OWPWeatherConditionGroup
{
    public string? Id { get; set; }
    public string? Name { get; set; }
    public int OrderIndex { get; set; }
}
