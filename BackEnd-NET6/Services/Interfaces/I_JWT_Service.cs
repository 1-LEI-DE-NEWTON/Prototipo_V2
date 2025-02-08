namespace BackEnd_NET6.Services.Interfaces
{
    public interface I_JWT_Service
    {
        string GenerateJwtToken(string username);        
    }
}
