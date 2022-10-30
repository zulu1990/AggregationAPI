using AggregationAPI.Models.DTO;
using AggregationAPI.Models.Enums;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;

namespace AggretationApp.Repository
{
    public class RecordRepository : IRecordRepository
    {
        private readonly DataContext _context;
        public RecordRepository(DataContext context)
        {
            _context = context;
        }


        public IEnumerable<RecordDTO> RecordsByTimeAndTinklas(int date, string tinklas)
        {
            return GetAllRecordsByDate(date).FirstOrDefault(x=> x.Key.Contains(tinklas)).Value.ToArray();
        }

        public IDictionary<string, List<RecordDTO>> GetAllRecordsByDate(int date)
        {
            var dataNewerThen = GetDate(date);
                var grouped = _context.Records.Where(x => x.PL_T >= dataNewerThen)
                    .Select(r => new RecordDTO(r.TINKLAS, r.OBT_PAVADINIMAS, r.OBJ_GV_TIPAS, r.OBJ_NUMERIS, r.P_PLUS, r.PL_T, r.P_MINUS))
                    .ToList().GroupBy(x => x.TINKLAS);

                return grouped.ToDictionary(g => g.Key, g => g.ToList());
        }

        public IEnumerable<RecordDTO> GetFilteredData()
        {
            var dataNewerThen = GetDate();

            var data = _context.Records.Where(x => x.PL_T >= dataNewerThen && x.OBT_PAVADINIMAS == "Butas")
                .Select(r => new RecordDTO(r.TINKLAS, r.OBT_PAVADINIMAS, r.OBJ_GV_TIPAS, r.OBJ_NUMERIS, r.P_PLUS, r.PL_T, r.P_MINUS))
                .ToArray();

            return data;
        }

        private DateTime GetDate(int months = 2)
        {
            var now = DateTime.Now;

            var dataNewerThen = new DateTime(now.Year, now.Month - months, 1);

            return dataNewerThen;
        }
    }
}
