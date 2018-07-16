#r ".\TestDummies.dll"
using TestDummies;
using TestDummies.Policies;

LoggerConfiguration
    .Destructure.ToMaximumDepth(maximumDestructuringDepth: 3)
    .Destructure.ToMaximumStringLength(maximumStringLength: 3)
    .Destructure.ToMaximumCollectionCount(maximumCollectionCount: 3)
    .Destructure.AsScalar(typeof(System.Version))
    .Destructure.With(new CustomPolicy());
