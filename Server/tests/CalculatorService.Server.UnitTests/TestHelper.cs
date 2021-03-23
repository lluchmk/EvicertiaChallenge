using CalculatorService.Server.Application.Journal.Interfaces;
using Moq;
using System;
using System.Collections.Generic;

namespace CalculatorService.Server.Tests.Unit
{
    public static class TestHelper
    {
        public static int GeneratePositiveNumber()
        {
            var random = new Random();
            return random.Next(1, 100);

        }

        public static IEnumerable<int> GeneratePositiveNumbers(int amount = 5)
        {
            var addends = new List<int>();
            var random = new Random();
            for (var i = 0; i < amount; i++)
            {
                addends.Add(random.Next(1, 100));
            }

            return addends;
        }

        public static IOperationRequest GenerateRequest()
        {
            return new Mock<IOperationRequest>().Object;
        }

        public static IEnumerable<IOperationRequest> GenerateRequests(int amount = 5)
        {
            var requests = new List<IOperationRequest>();
            for (int i = 0; i < amount; i++)
            {
                requests.Add(new Mock<IOperationRequest>().Object);
            }

            return requests;
        }

        public static IOperationResponse GenerateResponse()
        {
            return new Mock<IOperationResponse>().Object;
        }

        public static IEnumerable<IOperationResponse> GenerateResponses(int amount = 5)
        {
            var requests = new List<IOperationResponse>();
            for (int i = 0; i < amount; i++)
            {
                requests.Add(new Mock<IOperationResponse>().Object);
            }

            return requests;
        }
    }
}
