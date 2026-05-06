# ELibrary - Kitob Savdosi va Ijarasi Tizimi

## Loyiha haqida
ELibrary - foydalanuvchilar kitoblarni ko'rishi, sotib olishi va ijara asosida o'qishi mumkin bo'lgan backend API.

## Texnologiyalar
- **C# / ASP.NET Core 8**
- **PostgreSQL** - ma'lumotlar bazasi
- **Redis** - keshlash
- **Docker** - konteynerizatsiya
- **JWT** - autentifikatsiya
- **Serilog** - loglash
- **Sentry** - xato monitoring

## Loyihani ishga tushirish

### 1. Talab qilinadigan dasturlar
- [.NET 8 SDK](https://dotnet.microsoft.com/download)
- [Docker Desktop](https://www.docker.com/products/docker-desktop)
- [PostgreSQL](https://www.postgresql.org/download)

### 2. Reponi clone qiling
```bash
git clone https://github.com/esanboyevjavohir/ELibrary.git
cd ELibrary
```

### 3. appsettings.Development.json yarating
`src/ELibrary.API/` papkasida `appsettings.Development.json` fayl yarating:
```json
{
  "Database": {
    "ConnectionString": "Host=localhost;Port=5432;Database=elibrary;Username=postgres;Password=SIZNING_PAROLINGIZ;"
  },
  "JwtSettings": {
    "SecretKey": "SIZNING_SECRET_KEYINGIZ"
  },
  "Redis": {
    "Configuration": "localhost:6379"
  }
}
```

### 4. Docker orqali ishga tushirish
```bash
docker-compose up --build
```
API: `http://localhost:8080/swagger`

### 5. Migration (birinchi marta)
```bash
cd src/ELibrary.API
dotnet ef database update --project ../ELibrary.DataAccess
```

### 6. Lokal ishga tushirish
```bash
cd src/ELibrary.API
dotnet run
```
API: `https://localhost:7109/swagger`

## API Endpoints

### Auth
| Method | Endpoint | Tavsif |
|--------|----------|--------|
| POST | `/Api/User/register` | Ro'yxatdan o'tish |
| POST | `/Api/User/login` | Kirish (JWT token) |

### Books
| Method | Endpoint | Tavsif |
|--------|----------|--------|
| GET | `/Api/Book` | Barcha kitoblar (filter + pagination) |
| GET | `/Api/Book/{id}` | Kitob ma'lumoti (Redis kesh) |
| POST | `/Api/Book` | Kitob qo'shish |
| PUT | `/Api/Book/{id}` | Kitobni tahrirlash |
| DELETE | `/Api/Book/{id}` | Kitobni o'chirish |

### Transactions
| Method | Endpoint | Tavsif |
|--------|----------|--------|
| POST | `/Api/Transaction/buy/{bookId}` | Kitob sotib olish |

## Arxitektura
    ELibrary.API        - Controllers, Middleware
    ELibrary.Business   - Services, Validators, Mapping
    ELibrary.DataAccess - DbContext, Configurations, Migrations
    ELibrary.Core       - Entities, Interfaces, Enums

## Muhit o'zgaruvchilari
| Variable | Tavsif |
|----------|--------|
| `Database__ConnectionString` | PostgreSQL ulanish |
| `JwtSettings__SecretKey` | JWT maxfiy kalit |
| `Redis__Configuration` | Redis ulanish |

## Postman Collection
Barcha API testlari uchun `ELibrary.postman_collection.json` faylini Postman ga import qiling:
1. Postman oching
2. **Import** bosing
3. `ELibrary.postman_collection.json` faylini tanlang
4. Barcha endpointlar tayyor!

## Monitoring
Sentry orqali xatoliklar kuzatiladi. Har qanday crash yuz berganda avtomatik xabar keladi.

## Live URL
- **API:** https://elibrary-m167.onrender.com
- **Swagger:** https://elibrary-m167.onrender.com/swagger