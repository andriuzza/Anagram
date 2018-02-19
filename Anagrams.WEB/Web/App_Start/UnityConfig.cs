using Anagrams.Interfaces;
using Anagrams.Interfaces.DtoModel;
using Anagrams.Interfaces.EntityInterfaces;
using Anagrams.Interfaces.FactoryInterface;
using Anagrams.Interfaces.WebServices;
using Anagrams_Repositories.EntitiesRepositories;
using System;
using System.Web.Configuration;
using Unity;
using Unity.Injection;
using Unity.Lifetime;

namespace Web
{
    /// <summary>
    /// Specifies the Unity configuration for the main container.
    /// </summary>
    public static class UnityConfig
    {
        #region Unity Container
        private static Lazy<IUnityContainer> container =
          new Lazy<IUnityContainer>(() =>
          {
              var container = new UnityContainer();
              RegisterTypes(container);
              return container;
          });

        /// <summary>
        /// Configured Unity Container.
        /// </summary>
        public static IUnityContainer Container => container.Value;
        #endregion

        /// <summary>
        /// Registers the type mappings with the Unity container.
        /// </summary>
        /// <param name="container">The unity container to configure.</param>
        /// <remarks>
        /// There is no need to register concrete types such as controllers or
        /// API controllers (unless you want to change the defaults), as Unity
        /// allows resolving a concrete type even if it was not previously
        /// registered.
        /// </remarks>
        public static void RegisterTypes(IUnityContainer container)
        {

            var path = WebConfigurationManager.AppSettings["directoryPath"];

            var connectionSrting = WebConfigurationManager.AppSettings["connectionString"];

            container.RegisterType<IAnagramFactoryManager, AnagramFactoryManager>
               (new ContainerControlledLifetimeManager(),
                    new InjectionConstructor());

            container.RegisterType<IWordRepository<string>, Anagrams_Repositories.EFRepository>
                (new ContainerControlledLifetimeManager(),
                     new InjectionConstructor());

            container.RegisterType<IDictionaryRepository<WordDto>, WordEFRepository>
                (new ContainerControlledLifetimeManager(),
                     new InjectionConstructor());

            container.RegisterType<IAdditionalSearchService, AdditionalSearchService>
                (new ContainerControlledLifetimeManager(),
                     new InjectionConstructor(new ClickEFRepository()));


            //container.RegisterType<ICookiesManager, CookiesManager>(new InjectionConstructor(HttpContext.Current.Request));

            // NOTE: To load from web.config uncomment the line below.
            // Make sure to add a Unity.Configuration to the using statements.
            // container.LoadConfiguration();

            // TODO: Register your type's mappings here.
            // container.RegisterType<IProductRepository, ProductRepository>();
        }
    }
}
