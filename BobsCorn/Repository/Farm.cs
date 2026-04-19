using Dapper;
using Microsoft.Data.SqlClient;

namespace BobsCorn.Repository
{
    public class Farm: IFarm
    {
        private readonly string _configuration;

        public Farm(IConfiguration configuration)
        {
            _configuration = configuration.GetConnectionString("DefaultConnection");
        }

        public async Task<int> GetCornAsync()
        {
            using var connection = new SqlConnection(_configuration);
            return await connection.QuerySingleAsync<int>("UPDATE Corn SET Quantity = Quantity - 1 SELECT Quantity FROM Corn");
        }

        public async Task<int> UpdateClientCornAsync(string client)
        {
            using var connection = new SqlConnection(_configuration);
            return await connection.QuerySingleAsync<int>(@"INSERT CornClient(ClientId, Total, CreatedDate)
                                                        VALUES (@client, 1, GETDATE())
                                                        SELECT SUM(Total) FROM CornClient WHERE ClientId = @client", new { client });
        }

        public async Task<int> GetTotalClientCornAsync(string client)
        {
            using var connection = new SqlConnection(_configuration);
            var result = await connection.QuerySingleAsync<int?>("SELECT SUM(Total) FROM CornClient WHERE ClientId = @client", new { client });

            return result??0;
        }
    }
}
