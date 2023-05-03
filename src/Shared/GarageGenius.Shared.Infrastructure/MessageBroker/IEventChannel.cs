using System.Threading.Channels;
using GarageGenius.Shared.Abstractions.Events;

namespace GarageGenius.Shared.Infrastructure.MessageBroker;

internal interface IEventChannel
{
    ChannelReader<IEvent> Reader { get; }
    ChannelWriter<IEvent> Writer { get; }
}