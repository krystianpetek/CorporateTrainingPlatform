using GarageGenius.Shared.Abstractions.Queries.Query;

namespace GarageGenius.Modules.Customers.Shared.Queries.GetUserIdByCustomerId;

public record GetUserIdByCustomerIdQuery(Guid Id) : IQuery<GetUserIdByCustomerIdDto>;
