using CleanArchitecture.Domain;

namespace OpenWeatherMapAPI.Models;

public class OWPWeatherCondition
{
    public int Id { get; set; }
    public string? Main { get; set; }
    public string? Description { get; set; }
    public string? Icon { get; set; }
    public string IconUrl
    {
        get
        {
            return String.Format(Constants.ImageStorageOPMFormat, this.Icon);
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
