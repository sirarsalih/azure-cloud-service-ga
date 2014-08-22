using System.Collections.Generic;

namespace DotSpatialProjection.Services.Contracts
{
    public interface ICoordinateConverterService
    {
        List<double> ConvertToArmaCoordinates(double latitude, double longitude);
    }
}
