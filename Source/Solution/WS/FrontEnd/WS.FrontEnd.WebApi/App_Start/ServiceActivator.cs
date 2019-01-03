using System;
using System.Net.Http;
using System.Web.Http.Controllers;
using System.Web.Http.Dispatcher;
using WS.IoC.Container;

namespace WS.FrontEnd.WebApi.App_Start
{
    public class ServiceActivator : IHttpControllerActivator
    {
        public IHttpController Create(HttpRequestMessage request, HttpControllerDescriptor controllerDescriptor, Type controllerType)
        {
            var controller = CWrapper.Container.GetInstance(controllerType) as IHttpController;
            return controller;
        }
    }
}