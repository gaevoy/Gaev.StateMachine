using NUnit.Framework;

namespace Gaev.StateMachine.Tests
{
    public class DeliveryTests
    {
        [Test]
        public void CreateNew()
        {
            // When
            var delivery = new Delivery();

            // Then
            Assert.AreEqual("New", delivery.StateName);
        }

        [Test]
        public void NewToSent()
        {
            // Given
            var delivery = new Delivery();

            // When
            delivery.Handle(new Delivery.Send());

            // Then
            Assert.AreEqual("Sent", delivery.StateName);
        }

        [Test]
        public void NewToCancel()
        {
            // Given
            var delivery = new Delivery();

            // When
            delivery.Handle(new Delivery.Cancel());

            // Then
            Assert.AreEqual("Canceled", delivery.StateName);
        }

        [Test]
        public void NewToAny()
        {
            // Given
            var delivery = new Delivery();

            // When
            delivery.Handle(new object());

            // Then
            Assert.AreEqual("New", delivery.StateName);
        }

        [Test]
        public void SentToReceived()
        {
            // Given
            var delivery = new Delivery();
            delivery.Handle(new Delivery.Send());

            // When
            delivery.Handle(new Delivery.Receive());

            // Then
            Assert.AreEqual("Received", delivery.StateName);
        }

        [Test]
        public void SentToAny()
        {
            // Given
            var delivery = new Delivery();
            delivery.Handle(new Delivery.Send());

            // When
            delivery.Handle(new object());

            // Then
            Assert.AreEqual("Sent", delivery.StateName);
        }

        [Test]
        public void ReceivedToAny()
        {
            // Given
            var delivery = new Delivery();
            delivery.Handle(new Delivery.Send());
            delivery.Handle(new Delivery.Receive());

            // When
            delivery.Handle(new object());

            // Then
            Assert.AreEqual("Received", delivery.StateName);
        }
    }
}
