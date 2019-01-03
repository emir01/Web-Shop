using System;
using System.Data.Entity;
using WS.Database.Access.Core.Repos;
using WS.Database.Access.Interface;
using WS.Database.Access.Results;

namespace WS.Database.Access.Core
{
    public class WsUnitOfWork : IWsUnitOfWork
    {
        #region Context

        public DbContext _context { get; set; }

        #endregion

        #region Repositories

        private ProductRepository _productRepository;

        private CategoryRepository _categoryRepository;

        private ManufacturerRepository _manufacturerRepository;

        private TagTypeRepository _tagTypeRepository;

        private AppImageRepository _appImageRepository;

        #endregion

        #region Ctor

        public WsUnitOfWork(DbContext context)
        {
            _context = context;
        }

        #endregion

        #region Properties

        public ProductRepository ProductRepository => _productRepository ?? (_productRepository = new ProductRepository(_context));

        public CategoryRepository CategoryRepository => _categoryRepository ?? (_categoryRepository = new CategoryRepository(_context));

        public TagTypeRepository TagTypeRepository => _tagTypeRepository ?? (_tagTypeRepository = new TagTypeRepository(_context));

        public ManufacturerRepository ManufacturerRepository => _manufacturerRepository ?? (_manufacturerRepository = new ManufacturerRepository(_context));

        public AppImageRepository AppImageRepository => _appImageRepository ?? (_appImageRepository = new AppImageRepository(_context));

        #endregion

        public void Dispose()
        {
            _context.Dispose();
        }

        public SaveChangesResult Commit()
        {
            try
            {
                _context.SaveChanges();
                return SaveChangesResult.Success();
            }
            catch (Exception)
            {
                return SaveChangesResult.Failed();
            }
        }
    }
}
