using Moq;
using Schedule.Core.DTOs;
using Schedule.Core.Model;
using Schedule.Core.Repositories;
using Schedule.Service;

namespace Schedule.Tests;

public class StudentServiceTests
{
    [Fact]
    public async Task AddStudent_WhenClassIdIsZero_SavesStudentWithoutClass()
    {
        var students = new Mock<IStudentRepository>();
        var manager = new Mock<IRepositoryManager>();
        Student? saved = null;

        manager.Setup(m => m.Students).Returns(students.Object);
        manager.Setup(m => m.SaveAsync()).Returns(Task.CompletedTask);
        students
            .Setup(s => s.AddStudent(It.IsAny<Student>()))
            .Callback<Student>(student => saved = student);

        var service = new StudentService(manager.Object);

        await service.AddStudent(new StudentCreateDto
        {
            FirstName = "מיכל",
            LastName = "שלין",
            Birthdate = new DateTime(2012, 1, 1),
            ClassId = 0
        });

        Assert.NotNull(saved);
        Assert.Null(saved!.ClassId);
        manager.Verify(m => m.SaveAsync(), Times.Once);
    }
}
