using MetricsSample;
using Prometheus.Client.AspNetCore;
using Prometheus.Client.DependencyInjection;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// registrace služby Prometheus
builder.Services.AddMetricFactory();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

// přidání middleware Prometheus
app.UsePrometheusServer(opt =>
{
    // nastavení URL routy možno změnit
    opt.MapPath = "/metrics";
    // vypnutí výchozích telemtrických údajů
    opt.UseDefaultCollectors = false;
});

app.UseCustomApi();

app.Run();
