using Dapper;
using Metro.Application.Contracts.Repositories.Query;
using Metro.Application.Queries.Schedules;
using Metro.Core.Entities;
using Metro.Core.Models;
using Metro.Infrastructure.Configs;
using Metro.Infrastructure.Repository.Query.Base;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;

namespace Metro.Infrastructure.Repository.Query
{
    public class ScheduleQueryRepository : MultipleResultQueryRepository<Schedule>, IScheduleQueryRepository
    {
        public ScheduleQueryRepository(IConfiguration configuration, IOptions<MetroSettings> settings) : base(configuration, settings) 
        { 
        }

        public async Task<(long, IEnumerable<Schedule>)> GetAllAsync(GetAllScheduleQuery queryParams)
        {
            int pageSize = queryParams.PageSize;
            int offset = (queryParams.PageNumber - 1) * pageSize;    //offset = (pageNumber -1) * pageSize
            DynamicParameters parameters = new DynamicParameters();
            parameters.Add("@offset", offset);
            parameters.Add("@pageSize", pageSize);

            string query = @"declare @totalCount bigint
                            SELECT @totalCount = count(sch.Id)
                            FROM Schedules AS sch
                            WHERE sch.IsDeleted = 0

                            SELECT sch.Id, sch.StationFromId,
                            sch.StationToId, sch.DepartureTime,
                            sch.TotalSeat, sch.SeatBooked, sch.Price,
                            @totalCount AS TotalCount FROM Schedules
                            AS sch WHERE sch.IsDeleted = 0";

            string filter = "";

            //filter query condition is implemented if filter string length is greater than 0
            if (filter.Length > 0) query += filter;

            //order by 
            query += " ORDER BY sch.CreatedDate DESC, sch.LastModifiedDate DESC ";

            //pagination condition is implemented only if pageSize and offset value is valid
            if (pageSize > 1 && offset >= 0)
            {
                query += " OFFSET @offset ROWS FETCH NEXT @pageSize ROWS ONLY";
            }
            var _totalCount = 0;
            var scheduleList = await base.GetAsync<Schedule, TotalDataCount, Schedule>(query,
                (schedule, count) =>
                {
                    _totalCount = count.TotalCount;
                    return schedule;
                }, parameters, splitters: "TotalCount", false);

            return (_totalCount, scheduleList);
        }

        public async Task<Schedule> GetByIdAsync(Guid id)
        {
            DynamicParameters parameters = new DynamicParameters();
            parameters.Add("@Id", id);

            string query = "SELECT sch.Id, sch.StationFromId, " +
                            "sch.StationToId, sch.DepartureTime, " +
                            "sch.TotalSeat, sch.SeatBooked, sch.Price " +
                            "FROM Schedules AS sch WHERE Id = @Id " +
                            "AND IsDeleted = 0";

            return await SingleAsync(query, parameters);
        }
    }
}
