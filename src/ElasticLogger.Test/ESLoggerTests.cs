using System;
using System.Diagnostics;
using System.Threading.Tasks;
using ElasticsearchInside;
using ElasticSearch.Extensions.Logging;
using Microsoft.Extensions.Logging;
using Microsoft.VisualStudio.TestPlatform.ObjectModel.Logging;
using Xunit;

namespace ElasticLogger.Test
{
    public class ESLoggerTests
    {
        [Fact]
        public async Task Test1()
        {
            using (var elasticsearch = new ElasticsearchInside.Elasticsearch())
            {
                ////Arrange
                await elasticsearch.Ready();
                //var client = new ElasticClient(new ConnectionSettings(elasticsearch.Url));

                ILoggerFactory loggerFactory = new LoggerFactory()
                    .AddElasticSearch(elasticsearch.Url,
                        LogLevel.Trace);


                var logger = loggerFactory.CreateLogger("xxxxxxx");

                var circularRefObj = new Circle();
                circularRefObj.me = circularRefObj;


                logger.Log(LogLevel.Critical, new EventId(), circularRefObj, null, null);

            }

        }

        [Fact]
        public void Test2()
        {
            bool on = true;

            Assert.True(on);

        }

    }

    public class Circle
    {
        public Circle me { get; set; }
    }
}