var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();



// AddDistributedMemoryCache, verilerin bellek içinde (RAM) geçici olarak saklanmasını sağlayan bir dağıtılmış önbellek uygulamasını yapılandırır.
builder.Services.AddDistributedMemoryCache();

#region Description
/*

options.Cookie.Name = "CodeAcademy.Session";: Bu, oturum çerezi için kullanılacak ismi belirler. Bu örnekte, çerezin adı "CodeAcademy.Session" olarak belirlenmiştir.

options.IdleTimeout = TimeSpan.FromSeconds(10);: Bu, oturumun ne kadar süreyle (bu örnekte 10 saniye) etkin kalacağını belirler. Kullanıcı 10 saniye boyunca hiçbir işlem yapmazsa oturum otomatik olarak sona erer.

options.Cookie.HttpOnly = true;: Bu ayar, çerezin yalnızca HTTP istekleri üzerinden erişilebilir olmasını sağlar. JavaScript gibi istemci taraflı betikler bu çereze erişemez, bu da güvenlik açısından önemlidir.

options.Cookie.IsEssential = true;: Bu ayar, çerezin "Essential" (temel) olarak işaretlenmesini sağlar. Bu, çerezin web sitesinin düzgün çalışması için gerekli olduğu ve bu nedenle kullanıcının açık izni olmadan kullanılabileceği anlamına gelir.
*/
#endregion
builder.Services.AddSession(options =>
{
    options.Cookie.Name = "CodeAcademy.Session";
    options.IdleTimeout = TimeSpan.FromSeconds(10);
    options.Cookie.HttpOnly = true;
    options.Cookie.IsEssential = true;
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseSession();
app.UseAuthorization();

app.MapControllers();

app.Run();