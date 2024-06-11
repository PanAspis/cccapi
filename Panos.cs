using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using QBM.CompositionApi.ApiManager;
using QBM.CompositionApi.Definition;
using QBM.CompositionApi.PlugIns;
using VI.Base;

// This attribute will automatically assign all methods defined by this DLL
// to the CCC module.
[assembly: Module("CCC")]

namespace QBM.CompositionApi
{
    public class Panos : IMethodSetProvider
    {
        private readonly MethodSet _project;

        public Panos(IResolve resolver)
        {
            _project = new MethodSet
            {
                AppId = "Panos"
            };

            var svc = resolver.Resolve<IExtensibilityService>();

            // Configure all API providers that implement IApiProviderFor<CustomApiProject>
            var apiProvidersByAttribute = svc.FindAttributeBasedApiProviders<Panos>();
            _project.Configure(resolver, apiProvidersByAttribute);

            var authConfig = new Session.SessionAuthDbConfig
            {
                AuthenticationType = Config.AuthType.AllManualModules,
                Product = null,
                SsoAuthentifiers =
                {
                    // Add the names of any single-sign-on authentifiers here
                },
                ExcludedAuthentifiers =
                {
                    // Add the names of any excluded authentifiers here
                }
            };

            // To explicitly set the list allowed authentication modules,
            // set the AuthenticationType to AuthType.Default and set
            // the list of ManualAuthentifiers.

            _project.SessionConfig = authConfig;
        }

        public Task<IEnumerable<IMethodSet>> GetMethodSetsAsync(CancellationToken ct = new CancellationToken())
        {
            return Task.FromResult<IEnumerable<IMethodSet>>(new[] { _project });
        }
    }

    public class CustomApiPlugin : IPlugInMethodSetProvider
    {
        public IMethodSetProvider Build(IResolve resolver)
        {
            return new Panos(resolver);
        }
    }


}