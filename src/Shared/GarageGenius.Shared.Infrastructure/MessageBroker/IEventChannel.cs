using GarageGenius.Shared.Abstractions.Events;
using System.Threading.Channels;

namespace GarageGenius.Shared.Infrastructure.MessageBroker;

internal interface IEventChannel
{
    ChannelReader<IEvent> Reader { get; }
    ChannelWriter<IEvent> Writer { get; }
}