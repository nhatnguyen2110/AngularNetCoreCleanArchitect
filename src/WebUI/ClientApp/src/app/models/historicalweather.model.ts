import { ChartData } from "chart.js";
import { DailyForecastWeatherDto } from "../web-api-client";

export interface HistoricalWeather {
    chartData: ChartData<"bar">;
    historicalData: DailyForecastWeatherDto[];
  }