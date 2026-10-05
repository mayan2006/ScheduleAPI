using Moq;
using Schedule.Core.Model;
using Schedule.Core.Repositories;
using Schedule.Service;

namespace Schedule.Tests;

public class ClassServiceTests
{
    [Fact]
    public async Task AssignTeacher_WhenTeacherMissing_ReturnsNullAndDoesNotSave()
    {
        var classes = new Mock<IClassRepository>();
        var teachers = new Mock<ITeacherRepository>();
        var manager = new Mock<IRepositoryManager>();

        classes
            .Setup(c => c.GetById(1))
            .ReturnsAsync(new Class { Id = 1, Name = "א1" });
        teachers
            .Setup(t => t.GetById(99))
            .ReturnsAsync((Teacher?)null);

        manager.Setup(m => m.Classes).Returns(classes.Object);
        manager.Setup(m => m.Teachers).Returns(teachers.Object);

        var service = new ClassService(manager.Object);

        var result = await service.AssignTeacher(1, 99);

        Assert.Null(result);
        manager.Verify(m => m.SaveAsync(), Times.Never);
    }

    [Fact]
    public async Task AssignTeacher_WhenBothExist_AddsTeacherAndSaves()
    {
        var schoolClass = new Class { Id = 1, Name = "א1" };
        var teacher = new Teacher
        {
            Id = 7,
            FirstName = "רות",
            LastName = "לוי",
            Subject = "מתמטיקה"
        };

        var classes = new Mock<IClassRepository>();
        var teachers = new Mock<ITeacherRepository>();
        var manager = new Mock<IRepositoryManager>();

        classes.Setup(c => c.GetById(1)).ReturnsAsync(schoolClass);
        teachers.Setup(t => t.GetById(7)).ReturnsAsync(teacher);
        manager.Setup(m => m.Classes).Returns(classes.Object);
        manager.Setup(m => m.Teachers).Returns(teachers.Object);
        manager.Setup(m => m.SaveAsync()).Returns(Task.CompletedTask);

        var service = new ClassService(manager.Object);

        var result = await service.AssignTeacher(1, 7);

        Assert.NotNull(result);
        Assert.Single(result!.Teachers);
        Assert.Equal(7, result.Teachers[0].Id);
        Assert.Single(schoolClass.Teachers);
        manager.Verify(m => m.SaveAsync(), Times.Once);
    }
}
