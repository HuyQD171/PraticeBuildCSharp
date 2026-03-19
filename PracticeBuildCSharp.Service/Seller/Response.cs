namespace PraticeBuildCSharp.Service.Seller;

public class Response
{
    public class GetSellerResponse: User.Response.GetUserResponse
    {
        public string? CompanyName { get; set; }
        
        public string? TaxCode { get; set; }
    }
}