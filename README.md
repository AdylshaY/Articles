# Articles

.NET 8 tabanlı, mikroservis mimarisiyle geliştirilen bir makale gönderim ve yayın yönetim sistemi. Proje şu an geliştirme aşamasındadır.

## Yapı

```
src/
├── BuildingBlocks/        # Servisler arasında paylaşılan ortak kütüphaneler
│   ├── Articles.Abstractions
│   ├── Articles.Security
│   ├── Blocks.Core
│   ├── Blocks.Domain
│   ├── Blocks.EntityFramework
│   ├── Blocks.Exceptions
│   └── Blocks.MediatR
└── Services/
    └── Submission/        # Makale gönderim servisi
        ├── Submission.API           # Minimal API endpoint'leri
        ├── Submission.Application   # CQRS (MediatR) ile iş mantığı
        ├── Submission.Domain        # Entity'ler ve domain kuralları
        └── Submission.Persistence   # EF Core ile veri erişimi
```

Submission servisi; makale oluşturma, yazar atama/oluşturma, dosya (manuscript) yükleme ve asset yönetimi gibi işlemleri kapsar.

## Kullanılan teknolojiler

- .NET 8, ASP.NET Core Minimal API
- Entity Framework Core (SQL Server)
- MediatR (CQRS)
- FluentValidation
- Swagger / Swashbuckle

## Çalıştırma

```
dotnet build Articles.sln
dotnet run --project Services/Submission/Submission.API
```

Varsayılan bağlantı dizesi `appsettings.json` içinde `DefaultConnection` altında tanımlıdır; yerel bir SQL Server örneği veya Docker container'ı gerektirir.

## Durum

Aktif geliştirme sürecinde. Veritabanı migration'ları ve seed verisi henüz eklenmedi.
