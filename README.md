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

## Loyihani ishga tushirish

### 1. Talab qilinadigan dasturlar
- [.NET 8 SDK](https://dotnet.microsoft.com/download)
- [Docker Desktop](https://www.docker.com/products/docker-desktop)
- [PostgreSQL](https://www.postgresql.org/download)

### 2. Docker orqali ishga tushirish
docker-compose up --build

API: http://localhost:8080/swagger

### 3. Lokal ishga tushirish
cd src/ELibrary.API
dotnet run

API: https://localhost:7109/swagger

## API Endpoints

### Auth
| Method | Endpoint | Tavsif |
|--------|----------|--------|
| POST | `/api/auth/register` | Ro'yxatdan o'tish |
| POST | `/api/auth/login` | Kirish (JWT token) |

### Books
| Method | Endpoint | Tavsif |
|--------|----------|--------|
| GET | `/api/books` | Barcha kitoblar (filter + pagination) |
| GET | `/api/books/{id}` | Kitob ma'lumoti (Redis kesh) |
| POST | `/api/books` | Kitob qo'shish |
| PUT | `/api/books/{id}` | Kitobni tahrirlash |
| DELETE | `/api/books/{id}` | Kitobni o'chirish |

### Transactions
| Method | Endpoint | Tavsif |
|--------|----------|--------|
| POST | `/api/transactions/buy/{bookId}` | Kitob sotib olish |

## Arxitektura
ELibrary.API        - Controllers, Middleware
ELibrary.Business   - Services, Validators, Mapping
ELibrary.DataAccess - DbContext, Configurations, Migrations
ELibrary.Core       - Entities, Interfaces, Enums

## Muhit o'zgaruvchilari
Database__ConnectionString  - PostgreSQL ulanish
JwtSettings__SecretKey      - JWT maxfiy kalit
Redis__Configuration        - Redis ulanish

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