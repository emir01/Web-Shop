using System;
using System.Data.Entity;
using WS.Database.Access.Core.Repos;
using WS.Database.Access.Results;

namespace WS.Database.Access.Interface
{
    public interface IWsUnitOfWork : IDisposable
    {
        #region Core 

        DbContext _context { get; set; }

        #endregion

        #region Repositories

        ProductRepository ProductRepository { get; }

        CategoryRepository CategoryRepository { get; }

        TagTypeRepository TagTypeRepository { get; }

        ManufacturerRepository ManufacturerRepository { get; }

        AppImageRepository AppImageRepository { get; }

        #endregion

        #region Commit

        SaveChangesResult Commit();

        #endregion
    }
}
