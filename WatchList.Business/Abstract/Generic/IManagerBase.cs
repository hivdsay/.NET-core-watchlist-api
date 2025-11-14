using AutoMapper;
using WatchList.DataAccess.Abstract.UnitOfWorkApp;

namespace WatchList.Business.Abstract.Generic;

public interface IManagerBase
{
    public IUnitOfWorkApp _UnitOfWorkApp { get; set; }
    public IMapper _IMapper { get; set; }
}