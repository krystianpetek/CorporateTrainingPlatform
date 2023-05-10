using GarageGenius.Shared.Abstractions.Events;
using System.Threading.Channels;

namespace GarageGenius.Shared.Infrastructure.MessageBroker;

internal sealed class EventChannel : IEventChannel
{
    private readonly Channel<IEvent> _messages = Channel.CreateUnbounded<IEvent>();

    public ChannelReader<IEvent> Reader => _messages.Reader;
    public ChannelWriter<IEvent> Writer => _messages.Writer;
}