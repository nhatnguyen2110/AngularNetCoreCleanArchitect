using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenWeatherMapAPI.Models;

namespace CleanArchitecture.Application.GetWeatherConditions.Queries.GetWeatherConditions;

public class WeatherConditionCollectionDto
{
    public List<OWPWeatherConditionGroup> WeatherConditionGroups { get; set; } = new List<OWPWeatherConditionGroup>();
    public List<OWPWeatherCondition> WeatherConditions { get; set; } = new List<OWPWeatherCondition>();
}
