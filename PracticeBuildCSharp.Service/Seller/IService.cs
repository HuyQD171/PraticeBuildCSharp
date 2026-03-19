namespace PraticeBuildCSharp.Service.Seller;

public interface IService
{
    public Task<Base.Response.PageResult<Response.GetSellerResponse>> GetAllSeller(
        string? searchTerm,
        int pageIndex,
        int pageSize);
}