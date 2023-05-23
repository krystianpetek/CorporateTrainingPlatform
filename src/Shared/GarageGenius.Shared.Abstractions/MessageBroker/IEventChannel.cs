using GarageGenius.Shared.Abstractions.Events;
using System.Threading.Channels;

namespace GarageGenius.Shared.Abstractions.MessageBroker;

public interface IEventChannel
{
    ChannelReader<IEvent> Reader { get; }
    ChannelWriter<IEvent> Writer { get; }
}