using EasyNetQ.Consumer;

namespace EasyNetQ.Tests.ConsumeTests;

public class When_consume_is_called_with_auto_ack : ConsumerTestBase
{
    protected override void AdditionalSetUp()
    {
        StartConsumer((_, _, _) => AckStrategies.Ack, true);
    }

    [Fact]
    public void Should_create_a_consumer()
    {
        MockBuilder.Consumers.Count.Should().Be(1);
    }

    [Fact]
    public void Should_create_a_channel_to_consume_on()
    {
        MockBuilder.Channels.Count.Should().Be(1);
    }

    [Fact]
    public void Should_invoke_basic_consume_on_channel()
    {
        MockBuilder.Channels[0].Received().BasicConsume(
            Arg.Is("my_queue"),
            Arg.Is(true),
            Arg.Is(ConsumerTag),
            Arg.Is(true),
            Arg.Is(false),
            Arg.Is((IDictionary<string, object>)null),
            Arg.Is(MockBuilder.Consumers[0])
        );
    }
}
