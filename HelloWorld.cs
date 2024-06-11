using System;
using System.IO;
using System.Web;
using System.Net;
using System.Threading;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Linq;
using System.Text;
using VI.Base;
using VI.DB;
using VI.DB.Entities;
using QBM.CompositionApi.ApiManager;
using QBM.CompositionApi.Definition;

namespace QBM.CompositionApi
{
    public class HelloWorld : IApiProviderFor<Panos>, IApiProvider
    {
        public void Build(IApiBuilder builder)
        {
            builder.AddMethod(Method.Define("hello_world")
                .AllowUnauthenticated()
                .HandleGet(qr => new DataObject { Message = "Hello world!" }));
               
        }
        public class DataObject
        {
            public string Message { get; set; }

        }
    }
}