using System;
using _3DTrackingProducts.Api.Core.Repositories;

namespace _3DTrackingProducts.Api.Core
{
    public interface IUnitOfWork : IDisposable
    {
        IUsersRepository Users { get; }
        ILogRepository Logs { get; }
        ITagRepository Tags { get; }
        ITagPositionRepository TagPositions { get; }
        IRoomRepository Rooms { get; }
        IPairAntennaRepository PairAntennas { get; }
        ICategoryRepository Category { get; }
        IControlTagRepository ControlTag { get; }
        ITag3DPositionRepository Tag3DPosition {get;}
        int Complete();
    }
}

