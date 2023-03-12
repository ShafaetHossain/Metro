using Dapper;
using Metro.Application.Contracts.Repositories.Query;
using Metro.Application.Queries.Stations;
using Metro.Core.Entities;
using Metro.Core.Models;
using Metro.Infrastructure.Configs;
using Metro.Infrastructure.Repository.Query.Base;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;

namespace Metro.Infrastructure.Repository.Query
{
    public class StationQueryRepository : MultipleResultQueryRepository<Station>, IStationQueryRepository
    {
        public StationQueryRepository(IConfiguration configuration, IOptions<MetroSettings> settings) : base(configuration, settings) 
        {
        }

        public async Task<(long, IEnumerable<Station>)> GetAllAsync(GetAllStationQuery queryParams)
        {
            int pageSize = queryParams.PageSize;
            int offset = (queryParams.PageNumber - 1) * pageSize;    //offset = (pageNumber -1) * pageSize
            string? stationName = queryParams.StationName;
            DynamicParameters parameters = new DynamicParameters();
            parameters.Add("@offset", offset);
            parameters.Add("@pageSize", pageSize);
            parameters.Add("@StationName", stationName);

            string query = @"declare @totalCount bigint
                             SELECT @totalCount = count(st.Id)
                             FROM Stations as st
                             WHERE st.IsDeleted = 0

                             SELECT st.Id, st.StationName,
                             @totalCount as TotalCount FROM Stations
                             AS st WHERE st.IsDeleted = 0";

            string filter = "";

            if (!string.IsNullOrWhiteSpace(stationName))
            {
                filter += " AND StationName Like '%' +@stationName+ '%' ";
            }

            //filter query condition is implemented if filter string length is greater than 0
            if (filter.Length > 0) query += filter;

            //order by 
            query += " ORDER BY st.CreatedDate DESC, st.LastModifiedDate DESC, st.stationName ASC ";

            //pagination condition is implemented only if pageSize and offset value is valid
            if (pageSize > 1 && offset >= 0)
            {
                query += " OFFSET @offset ROWS FETCH NEXT @pageSize ROWS ONLY";
            }
            var _totalCount = 0;
            var stationList = await base.GetAsync<Station, TotalDataCount, Station>(query,
                (station, count) =>
                {
                    _totalCount = count.TotalCount;
                    return station;
                }, parameters, splitters: "TotalCount", false);


            return (_totalCount, stationList);
        }

        public async Task<Station> GetByIdAsync(Guid id)
        {
            DynamicParameters parameters = new DynamicParameters();
            parameters.Add("@Id", id);

            string query = "SELECT st.Id, st.StationName " +
                "FROM Station AS st WHERE Id = @Id " +
                "AND IsDeleted = 0";

            return await SingleAsync(query, parameters);
        }
    }
}
