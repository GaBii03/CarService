
using CarService.BL.Interfaces;
using CarService.BL.Services;
using CarService.DL.Kafka;
using CarService.Models.Entities;
using CarService.Models.Messages;
using CarService.Models.Responses;
using Moq;

namespace CarService.Tests
{
    public class SellCarTests
    {
        private readonly Mock<ICustomerService> _customerServiceMock;
        private readonly Mock<ICarService> _carServiceMock;
        private readonly Mock<IKafkaProducer<SellCarMessage>> _kafkaProducerMock;

        public SellCarTests()
        {
            _customerServiceMock = new Mock<ICustomerService>();
            _carServiceMock = new Mock<ICarService>();
            _kafkaProducerMock = new Mock<IKafkaProducer<SellCarMessage>>();
            _kafkaProducerMock
                .Setup(p => p.ProduceAsync(It.IsAny<string>(), It.IsAny<SellCarMessage>()))
                .Returns(Task.CompletedTask);
        }

        [Fact]
        public void SellCar_ApplyDiscount_Ok()
        {
            // Arrange
            var carId = Guid.NewGuid();
            var customerId = Guid.NewGuid();

            _carServiceMock.Setup(cs => cs.GetCarById(It.IsAny<Guid>())).Returns(() => new Car
            {
                Id = carId,
                Model = "TestModel",
                Year = 2020,
                BasePrice = 20000
            });
            _customerServiceMock.Setup(cs => cs.GetById(It.IsAny<Guid>())).Returns(() => new Customer
            {
                Id = customerId,
                Name = "Test Customer",
                Email = "test@example.com",
                Discount = 5000
            });

            // Act
            var sellCarService = new SellCarService(
                _customerServiceMock.Object,
                _carServiceMock.Object,
                _kafkaProducerMock.Object);

            var result = sellCarService.SellCar(carId, customerId);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(carId, result.Car.Id);
            Assert.Equal(customerId, result.Customer.Id);
            Assert.Equal(25000, result.SalePrice);
        }
    }
}