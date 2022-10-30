using AggregationAPI.Models.DTO;
using AggretationApp.Repository;
using System;
using System.Collections.Generic;
using System.Text;

namespace AggregationAPI.Repository
{
    public class MockRecordRepository : IRecordRepository
    {
        private readonly DataContext _context;
        public MockRecordRepository(DataContext context)
        {

        }

        public IDictionary<string, List<RecordDTO>> GetAllRecordsByDate(int date)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<RecordDTO> GetFilteredData()
        {
            throw new NotImplementedException();
        }

        public IEnumerable<RecordDTO> RecordsByTimeAndTinklas(int date, string tinklas)
        {
            throw new NotImplementedException();
        }


        public DateTime GetDate(int months = 2)
        {
            var now = DateTime.Now;

            var dataNewerThen = new DateTime(now.Year, now.Month - months, 1);

            return dataNewerThen;
        }
    }
}
