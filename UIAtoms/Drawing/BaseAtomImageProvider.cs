using NeuroSpeech.UIAtoms.Controls;
using NeuroSpeech.UIAtoms.Web;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace NeuroSpeech.UIAtoms.Drawing
{
    public abstract class BaseAtomImageProvider<T>
    {

        public abstract Task<T> LoadAsync(string url);

        private static HttpClient _client;

        internal protected static HttpClient Client
        {
            get
            {
                return _client
                    ??
                    (_client = DependencyService.Get<IWebClient>().Client);
            }
        }

        private static ShortMemoryCache _cache;
        internal protected ShortMemoryCache Cache => 
            _cache ?? (_cache = DependencyService.Get<ShortMemoryCache>());

        internal protected async Task<object> WebFetchAsync(Uri uri) {


            string url = uri.ToString();
            object data;
            //var data = Cache.Get(url);
            //if (data != null)
            //    return data;
            // url format...
            // static://{assemblyName}/{className}.{propertyName}
            // must return a bytearray or string...

            data = await LoadFromUrlAsync(uri);

            //// do not cache web urls...
            //if (!uri.Scheme.StartsWith("http", StringComparison.OrdinalIgnoreCase))
            //{
            //    Cache.Add(url, data);
            //}

            return data;
        }

        private static ConcurrentDictionary<Uri, MemberInfo> staticFieldDictionary
             = new ConcurrentDictionary<Uri, MemberInfo>();

        protected virtual async Task<object> LoadFromUrlAsync(Uri uri)
        {
            if (uri.Scheme.Equals("static", StringComparison.OrdinalIgnoreCase))
            {

                MemberInfo m = staticFieldDictionary.GetOrAdd(uri, u=> ResolveStaticPropertyOrField(u));


                var v = (m as FieldInfo)?.GetValue(null) ?? (m as PropertyInfo)?.GetValue(null);


                if (v is byte[])
                {
                    return v as byte[];
                }

                if (v is string)
                {
                    return await Task.Run(() => AtomStockImages.DecodeDataUri(v as string));
                }

            }

            if (uri.Scheme.Equals("res", StringComparison.OrdinalIgnoreCase)) {



                string assemblyName = uri.Host;
                var a = FindAssembly(assemblyName);

                


                var key = "Res-Images:" + uri.ToString();

                byte[] d = null;
                //d = Cache.Get(key) as byte[];
                //if (d != null)
                //    return d;

                string name = (assemblyName + uri.PathAndQuery).Replace("/", ".");

                string[] tokens = name.Split(new char[] { '?' }, 2);
                name = tokens[0];

                var names = a.GetManifestResourceNames();

                string resName = names.FirstOrDefault(x => string.Equals(x, name, StringComparison.OrdinalIgnoreCase));

                return a.GetManifestResourceStream(resName);

                //using (var s = a.GetManifestResourceStream(resName))
                //{
                //    using (var ms = new MemoryStream())
                //    {
                //        await s.CopyToAsync(ms);
                //        d = ms.ToArray();
                //        //Cache.Add(key, d);
                //    }
                //}
                //return d;
            }

            if (uri.Scheme.Equals("app-res", StringComparison.OrdinalIgnoreCase)) {


                string key = uri.ToString();
                byte[] d = null;
                //d = Cache.Get(key) as byte[];
                //if (d != null)
                //    return d;

                var a = Application.Current.GetType().Assembly;
                
                var names = a.GetManifestResourceNames();
                string name = uri.PathAndQuery.Replace("/", ".");

                string[] tokens = name.Split(new char[] { '?' }, 2);
                name = tokens[0];

                name = names.FirstOrDefault(x => x.EndsWith(name, StringComparison.OrdinalIgnoreCase));

                if (name == null) {
                    throw new InvalidOperationException($"{name} no Embedded Resource found in assembly {a.FullName}");
                }

                return a.GetManifestResourceStream(name);

                //using (var s = a.GetManifestResourceStream(name))
                //{
                //    using (var ms = new MemoryStream())
                //    {
                //        await s.CopyToAsync(ms);
                //        d = ms.ToArray();
                //        //Cache.Add(key, d);
                //    }
                //}
                //return d;
            }

            if (uri.Scheme.StartsWith("http", StringComparison.OrdinalIgnoreCase))
            {
                try
                {
                    return await Client.GetStreamAsync(uri.ToString());
                }
                catch (Exception ex) {
                    System.Diagnostics.Debug.Fail(ex.Message + " for " + uri.ToString(), ex.ToString());
                    throw;
                }
            }

            if (uri.Scheme.Equals("file", StringComparison.OrdinalIgnoreCase)) {
                return Task.Run(() => File.ReadAllBytes(uri.AbsolutePath));
            }

            throw new NotImplementedException($"No url loading found for {uri}");

        }


        public abstract Task<string> CropAsync(string source, CropRect cropRect);


        static Dictionary<string, MemberInfo> membersCache = new Dictionary<string, MemberInfo>();
        private static MemberInfo ResolveStaticPropertyOrField(Uri uri)
        {
            string url = uri.ToString();

            MemberInfo m = null;

            if (membersCache.TryGetValue(url, out m))
                return m; 

            string assemblyName = uri.Host;
            string path = uri.PathAndQuery;
            string field = null;
            string query = "";
            int index = path.IndexOf('?');
            if (index != -1)
            {
                query = path.Substring(index + 1);
                path = path.Substring(0, index);
            }

            index = path.LastIndexOf('.');
            field = path.Substring(index + 1);
            path = path.Substring(0, index);

            if (path.StartsWith("/"))
            {
                path = path.Substring(1);
            }

            //var a = AppDomain.CurrentDomain.GetAssemblies().FirstOrDefault(x => x.FullName == assemblyName);




            Assembly a = FindAssembly(assemblyName);//var a = Assembly.Load(new AssemblyName(assemblyName));
            var type = a.DefinedTypes.First(x => x.FullName == path);

            object v = null;


            FieldInfo f = type.GetField(field);
            if (f == null)
            {
                m = type.GetProperty(field);
                membersCache[url] = m;
                return m;
            }
            membersCache[url] = f;
            return f;
        }


        private static Dictionary<string, Assembly> assemblyByName = new Dictionary<string, Assembly>();
        private static Assembly FindAssembly(string assemblyName)
        {
            Assembly a;
            if (assemblyByName.TryGetValue(assemblyName, out a))
                return a;
            var currentDomain = typeof(string).GetTypeInfo().Assembly.GetType("System.AppDomain").GetRuntimeProperty("CurrentDomain").GetMethod.Invoke(null, new object[] { });
            var getAssemblies = currentDomain.GetType().GetRuntimeMethod("GetAssemblies", new Type[] { });
            var assemblies = getAssemblies.Invoke(currentDomain, new object[] { }) as Assembly[];

            a = assemblies.FirstOrDefault(x => string.Equals(x.GetName().Name, assemblyName, StringComparison.OrdinalIgnoreCase));//.SelectMany(aa => aa.DefinedTypes);
            if (a == null) {
                throw new InvalidOperationException($"No assembly found with name {assemblyName} from \r\n\t{string.Join("\r\n\t",assemblies.Select(x=>x.GetName().Name))}");
            }
            assemblyByName[assemblyName] = a;
            return a;
        }
    }
}
