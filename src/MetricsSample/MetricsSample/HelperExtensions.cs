using Prometheus.Client;

namespace MetricsSample;

public static class HelperExtensions
{
    public static IApplicationBuilder UseCustomApi(this WebApplication app)
    {
        var rnd = new Random();

        var summaries = new[]
        {
            "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
        };

        app.MapGet("/weatherforecast", (IMetricFactory metricFactory) =>
            {
                var counter = metricFactory.CreateCounter("metricssample_weatherforecast_total", "Celkovy pocet volani metody GetWeatherForecast");
                counter.Inc();

                var forecast = Enumerable.Range(1, 5).Select(index =>
                        new WeatherForecast
                        (
                            DateOnly.FromDateTime(DateTime.Now.AddDays(index)),
                            Random.Shared.Next(-20, 55),
                            summaries[Random.Shared.Next(summaries.Length)]
                        ))
                    .ToArray();
                return forecast;
            })
            .WithName("GetWeatherForecast")
            .WithOpenApi();

        app.MapGet("/long-running", async (IMetricFactory metricFactory) =>
            {
                var rndValue = rnd.Next(500, 5000);

                var gauge = metricFactory.CreateGauge("metricssample_longrunning_gauge", "Gauge pro metodu LongRunning");
                gauge.Set(rndValue);

                var histogram = metricFactory.CreateHistogram("metricssample_longrunning_histogram", "Histogram pro metodu LongRunning",  ValueTuple.Create("params1"));
                histogram.WithLabels("start").Observe(1);

                await Task.Delay(rndValue);
            })
            .WithName("LongRunning")
            .WithOpenApi();

        return app;
    }
}

record WeatherForecast(DateOnly Date, int TemperatureC, string? Summary)
{
    public int TemperatureF => 32 + (int)(TemperatureC / 0.5556);
}
