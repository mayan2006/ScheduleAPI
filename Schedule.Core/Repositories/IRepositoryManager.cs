namespace Schedule.Core.Repositories
{
    public interface IRepositoryManager
    {
        IStudentRepository Students { get; }
        IClassRepository Classes { get; }
        ITeacherRepository Teachers { get; }
        IUniformRepository Uniforms { get; }
        Task SaveAsync();
    }
}
