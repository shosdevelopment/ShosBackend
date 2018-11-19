using System;
using System.Web.Mvc;
using System.Web.Routing;
using Castle.Windsor;

namespace ECommerceService.App_Start.IoC.Plumbing
{
	public class WindsorControllerFactory : DefaultControllerFactory
	{
		readonly IWindsorContainer _container;

		public WindsorControllerFactory(IWindsorContainer container)
		{
			_container = container;
		}

		protected override IController GetControllerInstance(RequestContext requestContext, Type controllerType)
		{
			if (controllerType != null && _container.Kernel.HasComponent(controllerType))
				return (IController)_container.Resolve(controllerType);

			return base.GetControllerInstance(requestContext, controllerType);
		}

		public override void ReleaseController(IController controller)
		{
			_container.Release(controller);
		}
	}
}