using System;
using System.Reflection;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using Fabric.AppBase.CSharp.Standard.Infrastructure.Logging;
using Fabric.AppBase.CSharp.Standard.Infrastructure.NHibernateMaps;
using Fabric.AppBase.CSharp.Standard.Web;
using Fabric.Clients.CSharp.Fluent;
using Fabric.Clients.CSharp.Fluent.Session;
using Gallery.Web.Application;
using Gallery.Web.Logic;

namespace Gallery.Web {
	
	/*================================================================================================*/
	public class GalleryMvcApplication : HttpApplication {

		private static Global AppGlobal;
		private static FabricClientConfig AppFabCfg;


		////////////////////////////////////////////////////////////////////////////////////////////////
		/*--------------------------------------------------------------------------------------------*/
		protected void Application_Start() {
			BuildGlobal();
			AppGlobal.OnAppStart();

			ControllerBuilder.Current.SetControllerFactory(AppGlobal.BuildWindsorControllerFactory());
			
			AreaRegistration.RegisterAllAreas();

			RegisterGlobalFilters(GlobalFilters.Filters);
			RegisterRoutes(RouteTable.Routes);
		}

		/*--------------------------------------------------------------------------------------------*/
		protected void Application_End() {
			AppGlobal.OnAppEnd();
		}

		/*--------------------------------------------------------------------------------------------*/
		protected void Application_Error(object pSender, EventArgs pEvt) {
			Exception ex = Server.GetLastError();
			ReflectionTypeLoadException rtl = (ex as ReflectionTypeLoadException);
			Log.Error("Application_Error: "+ex+" // "+rtl);
		}

		/*--------------------------------------------------------------------------------------------*/
		public override void Init() {
			base.Init();
			AppGlobal.OnInit(this);
		}

		/*--------------------------------------------------------------------------------------------*/
		protected void Application_BeginRequest(object pSender, EventArgs pEvt) {
			AppGlobal.OnAppBeginRequest();
		}


		////////////////////////////////////////////////////////////////////////////////////////////////
		/*--------------------------------------------------------------------------------------------*/
		private void BuildGlobal() {
			#if DEBUG
			const string domain = "http://localhost:49316";
			#else
			const string domain = "http://zachkinstner.com";
			#endif

			AppFabCfg = new FabricClientConfig(
				"GalCfg",
				"http://inTheFabric.com/api",
				2,
				"0123456789abcdefghijkLMNOPqrstuv",
				4,
				domain+"/gallery/Home/FabricOauthRedirect",
				FabricSessionContainerProvider
			);

			AppGlobal = new Global(
				new[] { Server.MapPath("~/bin/Gallery.Infrastructure.dll") },
				new AutoPersistenceModelGenerator().Generate(GlobalLogic.GetDomainAssembly()),
				Server.MapPath("~/NHibernate.config"),
				AppFabCfg,
				(() => new GalleryWebSession()),
				Assembly.GetAssembly(typeof(GalleryWebSession)),
				Assembly.GetAssembly(typeof(HomeLogic))
			);
		}

		/*--------------------------------------------------------------------------------------------*/
		private static IFabricSessionContainer FabricSessionContainerProvider(string pConfigKey) {
			return Global.GetFabricSessionContainerProvider(HttpContext.Current.Session, pConfigKey);
		}


		////////////////////////////////////////////////////////////////////////////////////////////////
		/*--------------------------------------------------------------------------------------------*/
		public static void RegisterGlobalFilters(GlobalFilterCollection pFilters) {
			pFilters.Add(new HandleErrorAttribute());
		}

		/*--------------------------------------------------------------------------------------------*/
		public static void RegisterRoutes(RouteCollection pRoutes) {
			pRoutes.IgnoreRoute("{resource}.axd/{*pathInfo}");

			pRoutes.MapRoute("Default", "{controller}/{action}/{id}",
				new { controller="Home", action="Index", id=UrlParameter.Optional });
		}

	}

}