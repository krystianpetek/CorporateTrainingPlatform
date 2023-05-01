using GarageGenius.Shared.Abstractions.Commands;
using GarageGenius.Shared.Abstractions.Events;
using GarageGenius.Shared.Abstractions.Queries;

namespace GarageGenius.Shared.Abstractions.Dispatcher;
public interface IDispatcher : ICommandDispatcher, IQueryDispatcher, IEventDispatcher { }
