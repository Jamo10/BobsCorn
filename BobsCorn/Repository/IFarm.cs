namespace BobsCorn.Repository
{
    public interface IFarm
    {
        Task<int> GetCornAsync();
        Task<int> GetTotalClientCornAsync(string client);
        Task<int> UpdateClientCornAsync(string client);
    }
}
