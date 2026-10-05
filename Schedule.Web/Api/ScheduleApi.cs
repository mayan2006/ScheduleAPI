using System.Text.Json;
using Schedule.Core.DTOs;

namespace Schedule.Web.Api
{
    public class ScheduleApi
    {
        private static readonly JsonSerializerOptions JsonOptions = new(JsonSerializerDefaults.Web);

        private readonly HttpClient _http;

        public ScheduleApi(HttpClient http)
        {
            _http = http;
        }

        public Task<PagedResult<StudentDto>?> GetStudentsPagedAsync(int page, int pageSize)
        {
            return _http.GetFromJsonAsync<PagedResult<StudentDto>>(
                $"api/Student?page={page}&pageSize={pageSize}", JsonOptions);
        }

        public Task<HttpResponseMessage> CreateStudentAsync(StudentCreateDto student)
        {
            return _http.PostAsJsonAsync("api/Student", student, JsonOptions);
        }

        public Task<HttpResponseMessage> DeleteStudentAsync(int id)
        {
            return _http.DeleteAsync($"api/Student/{id}");
        }

        public Task<List<ClassDto>?> GetClassesAsync()
        {
            return _http.GetFromJsonAsync<List<ClassDto>>("api/Class", JsonOptions);
        }

        public Task<HttpResponseMessage> CreateClassAsync(ClassCreateDto schoolClass)
        {
            return _http.PostAsJsonAsync("api/Class", schoolClass, JsonOptions);
        }

        public Task<HttpResponseMessage> DeleteClassAsync(int id)
        {
            return _http.DeleteAsync($"api/Class/{id}");
        }
    }
}
