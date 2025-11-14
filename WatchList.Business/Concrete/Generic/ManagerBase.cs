using AutoMapper;
using WatchList.Business.Abstract.Generic;
using WatchList.DataAccess.Abstract.UnitOfWorkApp;

namespace WatchList.Business.Concrete.Generic;

public class ManagerBase : IManagerBase
{
    public ManagerBase(IUnitOfWorkApp unitOfWorkApp, IMapper IMapper)
    {
        _UnitOfWorkApp = unitOfWorkApp;
        _IMapper = IMapper;
    }
    public IUnitOfWorkApp _UnitOfWorkApp { get; set; }
    public IMapper _IMapper { get; set; }
}