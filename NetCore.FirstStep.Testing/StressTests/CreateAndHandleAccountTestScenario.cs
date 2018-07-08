using NetCore.FirstStep.Core;
using NetCore.FirstStep.ViewModels;
using Newtonsoft.Json;
using NUnit.Framework;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Net.Http;
using System.Runtime.Serialization.Formatters.Binary;
using System.Runtime.Serialization.Json;
using System.Text;
using NetCore.FirstStep.ViewArguments;
using System;
using System.Threading.Tasks;
using System.Linq;
using System.Diagnostics;
using System.Threading;

namespace NetCore.FirstStep.Testing.UnitTests
{
    [TestFixture]
    public class CreateAndHandleAccountTestScenario
    {
        private List<AccountViewModel> _accounts;  
        public CreateAndHandleAccountTestScenario()
        {
            _accounts = new List<AccountViewModel>();
            Uri = "http://localhost:5000/api/";
        }

        [Test]
        //[TestCase(10, 5)]
        //[TestCase(50, 5)]
        //[TestCase(100, 5)]
        public void ExecuteDonationScenario(/*int concurrentCalls, int repeat*/)
        {
            var concurrentCalls = 50;
            var repeat = 5;

            var tasks = new Task[concurrentCalls];
            var stopWatch = new Stopwatch();
            stopWatch.Start();

            while(repeat-- > 0)
            {
                for (int i = 0; i < concurrentCalls; i++)
                {
                    tasks[i] = Task.Run(() => RunScenario());
                    Thread.Sleep(50);
                }

                Task.WaitAll(tasks);
                stopWatch.Stop();

                TestContext.Out.WriteLine($"processed {concurrentCalls} concurrent calls in {stopWatch.ElapsedMilliseconds}ms");

            }
        }


        private void RunScenario()
        {
            var accountPath = Uri.Insert(Uri.Length, "account");
            var transactionPath = Uri.Insert(Uri.Length, "transaction");

            var account = CreateResponse<TestResult<AccountViewModel>>(accountPath, "POST");

            account = CreateGetResponse<TestResult<AccountViewModel>>($"{accountPath}/{account.Content.Key}");

            var donatorKey = "27e8a0c9-06ae-4d3c-ace9-a9335aca5435";
            var transactionCount = new Random().Next(1, 3);

            var sum = new Random().Next(1000, 2000);

            var response = CreateResponse<TestResult<TransactionViewModel>>(
                    transactionPath,
                    "POST",
                    new SubmitTransactionViewArgument() { SenderKey = donatorKey, RecipientKey = account.Content.Key, Sum = sum });

            var someOthers = _accounts.Take(5);

            var tasks = someOthers.Select(acc => Task.Run(() =>
                    CreateResponse<TestResult<TransactionViewModel>>(
                        transactionPath,
                        "POST",
                        new SubmitTransactionViewArgument()
                        {
                            SenderKey = account.Content.Key,
                            RecipientKey = acc.Key,
                            Sum = sum / 10
                        })));

            Task.WaitAll(tasks.ToArray());

            var validationTasks = tasks.Select(acc => Task.Run(() =>
                CreateResponse<TestResult<TransactionViewModel>>(
                transactionPath,
                "PUT",
                    new ValidateTransactionViewArgument()
                    {
                        TransactionId = acc.Result.Content.Id
                    })));

            Task.WaitAll(validationTasks.ToArray());

            _accounts.Add(account.Content);
        }

        public string Uri { get; set; }

        private T CreateResponse<T>(string actionUri, string method, object content = null)
        {
            using (var client = new HttpClient())
            {
                var result = client.SendAsync(
                    new HttpRequestMessage(new HttpMethod(method), actionUri)
                    {
                        Content = new StringContent(
                            JsonConvert.SerializeObject(content),
                            Encoding.UTF8,
                            "application/json")
                    }).Result;

                var stringResult = result.Content.ReadAsStringAsync().Result;
                return JsonConvert.DeserializeObject<T>(stringResult);
            }
        }

        private T CreateGetResponse<T>(string actionUri)
        {
            var request = HttpWebRequest.Create(actionUri);
            request.Method = "GET";

            var response = request.GetResponse();
    
            using (var stream = response.GetResponseStream())
            {
                using (var reader = new StreamReader(stream))
                {
                    var content = reader.ReadToEnd();
                    return JsonConvert.DeserializeObject<T>(content);
                }
            }
        }
    }
}
