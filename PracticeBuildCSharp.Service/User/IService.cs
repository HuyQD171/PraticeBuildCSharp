namespace PraticeBuildCSharp.Service.User;

public interface IService
{
    public Task<Base.Response.PageResult<Response.GetUserResponse>> GetUser(
        string? searchTerm,
        int pageIndex,
        int pageSize
    );
}