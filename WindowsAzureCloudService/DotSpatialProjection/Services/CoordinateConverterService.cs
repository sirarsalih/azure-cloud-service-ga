using System.Collections.Generic;
using DotSpatialProjection.Services.Contracts;

namespace DotSpatialProjection.Services
{
    public class CoordinateConverterService : ICoordinateConverterService
    {
        public List<double> ConvertToArmaCoordinates(double latitude, double longitude)
        {
            var centerLocation = new[] { 39.878880, 25.231705 };
            const int metersX = 14306;
            const int metersY = 13048;
            const double scale = 0.75;

            var latLngProjection = DotSpatial.Projections.KnownCoordinateSystems.Geographic.World.WGS1984;
            var metersProjection = DotSpatial.Projections.KnownCoordinateSystems.Projected.World.WebMercator;
            DotSpatial.Projections.Reproject.ReprojectPoints(centerLocation, null, latLngProjection, metersProjection, 0, 1);
            var latLong = new [] { latitude, longitude };
            DotSpatial.Projections.Reproject.ReprojectPoints(latLong, null, latLngProjection, metersProjection, 0, 1);
            var armaCoordinateList = new List<double>
            {
                (latLong[0] - centerLocation[0])*scale + metersY,
                (latLong[1] - centerLocation[1])*scale + metersX
            };
            return armaCoordinateList;
        }
    }
}
