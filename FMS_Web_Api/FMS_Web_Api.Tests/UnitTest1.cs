using FMS_Web_Api.Controllers;
using FMS_Web_Api.Repository;
using Moq;
using NUnit.Framework;

namespace FMS_Web_Api.Tests
{
    public class Tests
    {
        DashboardController dashboardController;
        private Mock<DashboardRepository> _dashboardRepository;
        [SetUp]
        public void Setup()
        {
            _dashboardRepository = new Mock<DashboardRepository>();
            dashboardController = new DashboardController(_dashboardRepository.Object);
        }

        [Test]
        public void Test1()
        {

             
            var res = dashboardController.Get();

            Assert.Pass();
        }
    }
}