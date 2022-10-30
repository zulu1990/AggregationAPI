using AggregationAPI.Models.DTO;
using AggregationAPI.Models.Enums;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace AggretationApp.Repository
{
    public interface IRecordRepository
    {
        IDictionary<string, List<RecordDTO>> GetAllRecordsByDate(int date);
        IEnumerable<RecordDTO> RecordsByTimeAndTinklas(int date, string tinklas);
        IEnumerable<RecordDTO> GetFilteredData();
    }
}
