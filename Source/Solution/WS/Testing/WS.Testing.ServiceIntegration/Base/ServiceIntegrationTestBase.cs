using System;
using System.Data.Entity;
using NUnit.Framework;
using StructureMap;
using StructureMap.Pipeline;
using WS.Core.Toolbox.Utilities;
using WS.Database.Bootstrap.Context;
using WS.Database.Domain.Base;
using WS.IoC.Container;

namespace WS.Testing.ServiceIntegration.Base
{
    /// <summary>
    /// Base class for integration tests for specific Web Shop services
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class ServiceIntegrationTestBase<T>
    {
        public T ServiceInstance { get; set; }

        protected IContainer _container;

        public ServiceIntegrationTestBase()
        {
            // we are keeping an internal reference of the IoC Container for test usage
            _container = CWrapper.Container;
        }
        
        public T GetServiceInstance(bool setupContext = true)
        {
            // configure the container to return a new instance of a 
            // context
            if (setupContext)
            {
                _container.Configure(c => c.For<DbContext>().Use(new WsContext()).SetLifecycleTo(new UniquePerRequestLifecycle()));
            }
            
            var serviceInstance = _container.GetInstance<T>();
            
            if (serviceInstance == null)
            {
                throw new InvalidOperationException($"Could not create an instance for service {typeof(T).Name}");
            }

            return serviceInstance;
        }

        /// <summary>
        /// Creates an instance of a service that is to serve as an utility for testing the core 
        /// instance under test with the integration base.
        /// </summary>
        /// <typeparam name="TUtility"></typeparam>
        /// <returns></returns>
        public TUtility GetUtilityService<TUtility>()
        {
            _container.Configure(c => c.For<DbContext>().Use(new WsContext()).SetLifecycleTo(new TransientLifecycle()));

            var utilityServiceInstance = _container.GetInstance<TUtility>();

            return utilityServiceInstance;
        }

        /// <summary>
        /// Creates a unique name based on a given base and a suffix length.
        /// 
        /// Suffix is generated from Guids.
        /// </summary>
        /// <param name="baseName"></param>
        /// <param name="suffixLength"></param>
        /// <returns></returns>
        public string GetUniqueString(string baseName, int suffixLength = 30)
        {
            var guidString = Guid.NewGuid().ToString();

            if (suffixLength >= guidString.Length)
            {
                suffixLength = guidString.Length - 1;
            }

            return baseName + guidString.Substring(0, suffixLength);
        }

        /// <summary>
        /// Will run asserts on properties related to creation of entities
        /// </summary>
        /// <typeparam name="TEntity"></typeparam>
        /// <param name="entity"></param>
        /// <param name="assertCreatedBy"></param>
        /// <param name="assertExactTimeCreated">Flag indicating if we want to assert the time created within a time range or just that the time created date is set</param>
        public void AssertEntityCreated<TEntity>(TEntity entity, bool assertCreatedBy = false, bool assertExactTimeCreated = true) where TEntity : Entity
        {
            // get the current date time used to run comparisons on date related meta fields
            var when = DateTime.Now;

            Assert.IsNotNull(entity.DateCreated, "The DateCreated Field on the entitiy must be set when creating entities");

            if (assertExactTimeCreated)
            {
                Assert.IsTrue(DateCompareUtilities.CompareWithinRange(when, entity.DateCreated.Value), "The DateCreated field must be set within a range of the current time when the test was run");
            }

            if (assertCreatedBy)
            {
                Assert.IsTrue(!string.IsNullOrWhiteSpace(entity.CreatedBy), "The CreatedBy field for the entitiy must be set");
            }

            Assert.IsNotNull(entity.Status, "The Status field for entities must be set when creating new entities");
            Assert.IsTrue(entity.Status.Value, "The Status field for newly created entities must be set to True");
        }

        public void AssertEntityUpdated<TEntity>(TEntity entity) where TEntity : Entity
        {
            // When are we running the assert - required to assert the times on
            // meta fields
            var when = DateTime.Now;

            Assert.IsNotNull(entity.DateModified, "The DateModified field on the entity must be set when updating entities");
            Assert.IsTrue(DateCompareUtilities.CompareWithinRange(when, entity.DateModified.Value), "The DateModified field must be set within a range of the current time when the test has run");
        }
    }
}
