using Moq;
using NetCore.FirstStep.Core;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Text;

namespace NetCore.FirstStep.Testing.UnitTest
{
    [TestFixture]
    public class ActivityTests
    {
        public Mock<IActivity<TestIntent, TestModel>> ActivityMock { get; private set; }

        [SetUp]
        public void Init()
        {
            ActivityMock = new Mock<IActivity<TestIntent, TestModel>>();

            ActivityMock
                .Setup(x => x.Run(new TestIntent() { Argument = "intent stuff" }))
                .Returns(ActivityMock.Object);

        }

        [Test]
        public void GenericActivity_Should_Set_Result_When_Running()
        {
            var activity = Activity<TestIntent, TestModel>.Create(intent => 
                new TestModel() { Length = intent.Argument?.Length ?? 0 } );

            activity.Run(new TestIntent() { Argument = "INTENT" });

            Assert.IsNotNull(activity.Result?.Content, "null result", activity);
            Assert.IsTrue(activity.Result.IsSuccessful);
            Assert.AreEqual(5, activity.Result.Content.Length, "5 is expected", activity);
            Assert.AreEqual(activity.Intent, "intent stuff");
        }

        [Test]
        public void GenericActivity_Should_Execute_Then_Method()
        {
            var sut = Activity<TestIntent, TestModel>.Create(intent =>
                new TestModel() { Length = intent.Argument?.Length ?? 0 });

            sut.Run(new TestIntent() { Argument = "INTENT" })
                .Then(model =>
                {
                    // crazy handling...
                    return intent => { model.Length = 12; return model; };
                });

           Assert.IsNotNull(sut?.Result?.Content);
           Assert.IsTrue(sut.Result.IsSuccessful);
           Assert.AreEqual(12, sut.Result.Content.Length);
        }

        [Test]
        public void Activity_Should_Execute_Then_Method()
        {
            var sut = Activity<TestIntent, TestModel>.Create(intent =>
                new TestModel() { Length = intent.Argument?.Length ?? 0 });

            sut.Run(new TestIntent() { Argument = "INTENT" })
                .Then(ActivityMock.Object);

            Assert.IsNotNull(sut?.Result?.Content);
            Assert.IsTrue(sut.Result.IsSuccessful);
            Assert.AreEqual(12, sut.Result.Content.Length);
        }
    }
}
