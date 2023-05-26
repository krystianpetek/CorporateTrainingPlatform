using GarageGenius.Shared.Abstractions.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GarageGenius.Modules.Vehicles.Core.Exceptions;
internal class VehicleAlreadyExistsException : GarageGeniusException
{
    public VehicleAlreadyExistsException(Guid vehicleId) : base($"Vehicle with ID: {vehicleId} already exists.")
    {
    }
}
