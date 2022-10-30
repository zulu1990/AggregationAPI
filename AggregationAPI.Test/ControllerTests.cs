using AggregationAPI.Controllers;
using AggregationAPI.Models.DTO;
using AggregationAPI.Repository;
using AggretationApp.Repository;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;
using Record = AggregationAPI.Models.Data.Record;

namespace AggregationAPI.Test
{

    public class TestController
    {
        [Theory]
        [MemberData(nameof(GetLatest_Data))]
        public void GetLatest_Returns(List<Record> records, int months, Dictionary<string, List<RecordDTO>> expected)
        {
            var options = new DbContextOptionsBuilder<DataContext>()
                .UseInMemoryDatabase("Dataset")
                .Options;
            var context = new DataContext(options);

            context.Records.AddRange(records);
            context.SaveChanges();

            var repository = new RecordRepository(context);

            var controller = new DatasetController(repository);
            var result = controller.GetLatest(months);

            Assert.Equal(result.Count, expected.Count);
            Assert.Equal(result.Keys, result.Keys);
            foreach(var rec in result)
            {
                for (int i = 0; i < rec.Value.Count; i++)
                {
                    Assert.True(rec.Value[i].Equals(result[rec.Key][i]));
                }
            }

        }


        [Theory]
        [MemberData(nameof(GetFiltered_Data))]
        public void GetFilteredData_Returns_Empty(List<Record> records, int months, Dictionary<string, List<RecordDTO>> expected)
        {
            var options = new DbContextOptionsBuilder<DataContext>()
                .UseInMemoryDatabase("Dataset")
                .Options;
            var context = new DataContext(options);

            context.Records.AddRange(records);
            context.SaveChanges();


            var repository = new RecordRepository(context);

            var controller = new DatasetController(repository);
            var result = controller.GetLatest(months);

            Assert.Equal(result.Count, expected.Count);
            Assert.Equal(result.Keys, result.Keys);

        }


        public static IEnumerable<object[]> GetLatest_Data =>
    new List<object[]>
    {
            new object[] {
                new List<Record>()
                {
                    new Record("Klaipėdos regiono tinklas", "Namas", "G", 316972, 0.2597, DateTime.Parse("2022-04-30 00:00:00"), 0.0),
                    new Record("Alytaus regiono tinklas", "Butas", "Ne GV", 35282, 0.006, DateTime.Parse("2022-04-30 00:00:00"), 0.0),
                    new Record("Klaipėdos regiono tinklas", "Butas", "N", 4873840, 1.3193,  DateTime.Parse("2022-05-31 00:00:00"), 0.0)
                },
                5,
                new Dictionary<string, List<RecordDTO>>()
                {
                    {
                        "Klaipėdos regiono tinklas", new List<RecordDTO>(){ new RecordDTO("Klaipėdos regiono tinklas", "Butas", "N", 4873840, 1.3193, DateTime.Parse("2022-05-31 00:00:00"), 0.0) }
                    },
                }
            },
            new object[] {
                new List<Record>()
                {
                    new Record("Klaipėdos regiono tinklas", "Namas", "G", 316972, 0.2597, DateTime.Parse("2022-04-30 00:00:00"), 0.0),
                    new Record("Alytaus regiono tinklas", "Butas", "Ne GV", 35282, 0.006, DateTime.Parse("2022-04-30 00:00:00"), 0.0),
                    new Record("Klaipėdos regiono tinklas", "Butas", "N", 4873840, 1.3193,  DateTime.Parse("2022-05-31 00:00:00"), 0.0)
                },
                2,
                new Dictionary<string, List<RecordDTO>>()
                {

                }

            }
    };


        public static IEnumerable<object[]> GetFiltered_Data =>
    new List<object[]>
    {
            new object[] {
                new List<Record>()
                {
                    new Record("Klaipėdos regiono tinklas", "Namas", "G", 316972, 0.2597, DateTime.Parse("2022-04-30 00:00:00"), 0.0),
                    new Record("Alytaus regiono tinklas", "Butas", "Ne GV", 35282, 0.006, DateTime.Parse("2022-04-30 00:00:00"), 0.0),
                    new Record("Klaipėdos regiono tinklas", "Butas", "N", 4873840, 1.3193,  DateTime.Parse("2022-05-31 00:00:00"), 0.0),
                    new Record("Klaipėdos regiono tinklas", "Namas", "N", 4873840, 1.3193,  DateTime.Parse("2022-05-31 00:00:00"), 0.0)
                },
                5,
                new Dictionary<string, List<RecordDTO>>()
                {
                    {
                        "Klaipėdos regiono tinklas", new List<RecordDTO>(){ new RecordDTO("Klaipėdos regiono tinklas", "Butas", "N", 4873840, 1.3193, DateTime.Parse("2022-05-31 00:00:00"), 0.0) }
                    },
                }
            },
            new object[] {
                new List<Record>()
                {
                    new Record("Klaipėdos regiono tinklas", "Namas", "G", 316972, 0.2597, DateTime.Parse("2022-04-30 00:00:00"), 0.0),
                    new Record("Alytaus regiono tinklas", "Butas", "Ne GV", 35282, 0.006, DateTime.Parse("2022-04-30 00:00:00"), 0.0),
                    new Record("Klaipėdos regiono tinklas", "Butas", "N", 4873840, 1.3193,  DateTime.Parse("2022-05-31 00:00:00"), 0.0)
                },
                2,
                new Dictionary<string, List<RecordDTO>>()
                {

                }

            }
    };

    }
}