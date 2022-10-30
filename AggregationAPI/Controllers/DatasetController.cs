using AggregationAPI.Models.DTO;
using AggretationApp.Repository;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace AggregationAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DatasetController : ControllerBase
    {
        private readonly IRecordRepository _recordsRepository;
        public DatasetController(IRecordRepository recordsRepository)
        {
            _recordsRepository = recordsRepository;
        }


        [HttpGet("GetLatestData/{monthCount}")]
        public IDictionary<string, List<RecordDTO>> GetLatest(int monthCount)
        {
            var data =  _recordsRepository.GetAllRecordsByDate(monthCount);

            return data;
        }

        [HttpGet("GetLatestData/{tinklas}-{monthCount}")]
        public IEnumerable<RecordDTO> GetMonthsByTinklas(string tinklas, int monthCount)
        {
            var data = _recordsRepository.RecordsByTimeAndTinklas(monthCount, tinklas);

            return data;
        }


        [HttpGet("get-filtered-data")]
        public IEnumerable<RecordDTO> GetFilteredData()
        {
            var result = _recordsRepository.GetFilteredData();

            return result;
        }



    }
}
