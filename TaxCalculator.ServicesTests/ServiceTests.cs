using Microsoft.VisualStudio.TestTools.UnitTesting;
using TaxCalculator.Repositories.Interfaces;
using TaxCalculator.Services;
using TaxCalculator.Domain.Entities;
using NSubstitute;
using System.Collections.Generic;
using System;


namespace TaxCalculator.ServicesTests;

[TestClass]
public class TaxCalculatorServicesTests {
    [TestMethod]
    public void ServiceIsCalculatingCorrectly() {
        // Use NSubstitute to build the service
        var repository = Substitute.For<ITaxCalculatorRepository>();
        var service = new TaxCalculatorService(repository);

        var possibleValues = new List<Tuple<double, int, double>>() {
            new Tuple<double, int, double>(100.0, 5, 105.10),
            new Tuple<double, int, double>(150.0, 5, 157.65),
            new Tuple<double, int, double>(50.0, 3, 51.52),
        };

        // Configure the default return for the repository
        var tax = new Tax(0.01);
        _ = repository.GetInterestRateAsync().ReturnsForAnyArgs(tax);


        foreach (var possibility in possibleValues) {
            // Calculate
            var result = service.CalculateTaxAsync(initialValue: possibility.Item1, months: possibility.Item2);

            // Assert
            Assert.AreEqual(possibility.Item3.ToString("F2"), result.Result.ToString("F2"));
        }
    }
}
