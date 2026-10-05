# Schedule

מערכת בית־ספרית: תלמידות, מורות, כיתות ומדים.

שכבות נפרדות, DTOs במקום Entities ב־JSON, קשרים ב־EF Core, ולידציה, טיפול שגיאות מרכזי, pagination, בדיקות יחידה, וממשק Blazor Server.

## טכנולוגיות

- ASP.NET Core 6 Web API (EF Core 6, SQL Server, Swagger)
- Blazor Server (Schedule.Web, net10)
- xUnit + Moq

## מבנה הפתרון

```
Schedule.sln
├── Schedule.API        # Controllers, Swagger, Exception Middleware
├── Schedule.Core       # מודלים, DTOs, ממשקים, ולידציה
├── Schedule.Service    # לוגיקה עסקית
├── Schedule.Data       # DbContext, Repositories, Migrations, Unit of Work
├── Schedule.Web        # Blazor Server — תלמידות וכיתות
└── Schedule.Tests      # xUnit + Moq על ה-Services
```

הזרימה: **Blazor → Controller → Service → Repository → SQL Server**.

השמירה עוברת דרך `IRepositoryManager.SaveAsync` (Unit of Work) — פעם אחת בסוף, לא בכל Repository.

## מודל הנתונים

| קשר | פירוט |
|---|---|
| One-to-Many | כיתה → תלמידות (`Student.ClassId`, SetNull במחיקה) |
| Many-to-Many | כיתה ↔ מורות (`ClassTeacher`) |
| One-to-One | תלמידה ↔ מדים (`Uniform.StudentId`) |

ה־API מחזיר DTOs. Entities עם Navigation Properties לא יוצאים החוצה — כדי לא לשבור JSON במעגלים.

## API

Swagger בפיתוח: https://localhost:7209/swagger

CRUD מלא: `/api/Student`, `/api/Class`, `/api/Teacher`, `/api/Uniform`.

דוגמאות לקשרים:

| פעולה | נתיב |
|---|---|
| תלמידות של כיתה | `GET /api/Class/{id}/students` |
| מורות של כיתה | `GET /api/Class/{id}/teachers` |
| שיבוץ מורה | `POST /api/Class/{id}/teachers/{teacherId}` |
| כיתות של מורה | `GET /api/Teacher/{id}/classes` |

Pagination לתלמידות:

- בלי query: `GET /api/Student` — כל הרשימה
- עם עמוד: `GET /api/Student?page=1&pageSize=10` — `PagedResult` (`items`, `totalCount`, `totalPages`)

`Skip`/`Take` רצים ב־SQL, לא אחרי `ToList`.

## ממשק

Blazor Server ב־https://localhost:7210 (או http://localhost:5210).

- תלמידות: הוספה, מחיקה, בחירת כיתה, דפדוף
- כיתות: הוספה ומחיקה

הפרונט מדבר עם ה־API ב־HTTP (`ApiBaseUrl` ב־`Schedule.Web/appsettings.json`). אין CORS — הקריאות יוצאות מהשרת.

## בדיקות

```bash
dotnet test --project Schedule.Tests
```

בודקים Services בלי דאטהבייס: `ClassId = 0` נשמר כ־`null`, שיבוץ מורה נכשל בלי `SaveAsync`.

## הרצה

נדרש SQL Server מקומי ומסד `ScheduleDB`. מחרוזת החיבור ב־`appsettings.json` (לא ב־`Program.cs`):

```
Server=localhost;Database=ScheduleDB;TrustServerCertificate=True;Trusted_Connection=True
```

```bash
dotnet restore
dotnet ef database update --project Schedule.Data --startup-project Schedule.API
dotnet run --project Schedule.API
dotnet run --project Schedule.Web
```

שני תהליכים: קודם API (7209), אחר כך Web (7210).

אם הדפדפן מתריע על תעודת HTTPS — Advanced → Proceed, או להיכנס ב־HTTP לפורט 5210.
