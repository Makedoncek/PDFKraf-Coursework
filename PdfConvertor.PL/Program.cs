using IronPdf;
using PdfConvertor.BLL.Services;
using PdfConvertor.BLL.Services.Interfaces;

License.LicenseKey = "IRONSUITE.123456V64.GMAIL.COM.26614-2BAD8A1309-C55HP-QJVQPSIGSTPA-V5TB4Q64WGNB-HCYCZY57CSBP-JZN2R5BZ3XNW-36S5BWG7Z2IM-JJTA6GLFMDMW-O7FZMN-TBRWCVVAKS6LUA-DEPLOYMENT.TRIAL-3AFLMI.TRIAL.EXPIRES.18.FEB.2024";


var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddScoped<IPdfToBinaryConverter,PdfToBinaryConverter>();
builder.Services.AddScoped<ICompressPdfService, CompressPdfService>();
builder.Services.AddScoped<IMergePdfService, MergePdfService>();
builder.Services.AddScoped<ISplitPdfService, SplitPdfService>();
builder.Services.AddScoped<IWatermarkPdfService, WatermarkPdfService>();
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddCors(options =>
{
    options.AddPolicy("MyCorsPolicy", builder =>
        builder.WithOrigins("http://localhost:4200") // Замените на порт вашего фронтенда
            .AllowAnyHeader()
            .AllowAnyMethod());
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseRouting();

app.UseCors("MyCorsPolicy");

app.UseAuthorization();

app.UseEndpoints(endpoints =>
{
    endpoints.MapControllers(); // Не нужно RequireCors здесь, так как уже применено в UseCors
});

app.Run();