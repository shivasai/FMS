using NUnit.Framework;
using Moq;
using System;
using System.Collections.Generic;
using System.Text;
using EmailApp.Controllers;
using EmailApp.Repository;
using EmailApp.Models;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace EmailAppTests
{
    class EmailAppTest
    {
        private MailServiceController mailServiceController;
        private readonly Mock<IEmailRepository> _emailRepository = new Mock<IEmailRepository>();
        [SetUp]
        public void Setup()
        {
            mailServiceController = new MailServiceController(_emailRepository.Object);
        }

        [Test]
        public async Task GenerateEmails()
        {
            Event evnt = new Event();
            evnt.Id = 2;
            _emailRepository.Setup(x => x.GenerateEmails(evnt)).ReturnsAsync(true);
            ActionResult res = await mailServiceController.Post(evnt);
            Assert.That(res, Is.InstanceOf<OkResult>());
        }
        [Test]
        public async Task SendReport()
        {
            Report report = new Report();
            report.Email = "test@gmail.com";            
            _emailRepository.Setup(x => x.SendReport(report.Email)).ReturnsAsync(true);
            ActionResult res = await mailServiceController.SendReport(report);
            Assert.That(res, Is.InstanceOf<OkResult>());
        }
    }
}
