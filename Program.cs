using $safeprojectname$.DAO;

var builder = WebApplication.CreateBuilder(args);

// Adiciona suporte a controllers e views (caso ainda precise)
builder.Services.AddControllersWithViews();

var app = builder.Build();

// Configurações do pipeline HTTP
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
}

// Permite servir arquivos estáticos (HTML, CSS, JS, imagens etc.)
app.UseDefaultFiles(); // <- importante: busca automaticamente por index.html
app.UseStaticFiles();

app.UseRouting();
app.UseAuthorization();

// Mapeia controladores de API (se existirem)
app.MapControllers();

// Se nenhuma rota for encontrada, carrega o index.html
app.MapFallbackToFile("index.html");

app.Run();





