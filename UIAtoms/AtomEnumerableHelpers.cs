using System;
using System.Linq;
using System.Collections.Generic;
using System.Text;
using System.Collections;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Collections.Concurrent;
using System.Reflection;

namespace NeuroSpeech.UIAtoms
{

    [AttributeUsage(AttributeTargets.Class, AllowMultiple = false, Inherited = true)]
    public class AtomKeyPropertyAttribute: Attribute {
        public AtomKeyPropertyAttribute(string name)
        {
            this.Name = name;
        }

        public string Name { get; private set; }
    }

    [AttributeUsage(AttributeTargets.Property, AllowMultiple = false, Inherited = true)]
    public class AtomKeyAttribute : Attribute {
    }



    /// <summary>
    /// 
    /// </summary>
    public static class AtomEnumerableHelpers {


        private static ConcurrentDictionary<string, PropertyInfo> properties =
            new ConcurrentDictionary<string, PropertyInfo>();


        private static ConcurrentDictionary<Type, string> keyProperties 
            = new ConcurrentDictionary<Type, string>();

        public static string GetKeyProperty(this Type type) {
            return keyProperties.GetOrAdd(type, x => {

                var k = x.GetCustomAttribute<AtomKeyPropertyAttribute>();
                if (k != null)
                    return k.Name;

                foreach (var p in x.GetProperties()) {
                    var pk = p.GetCustomAttribute<AtomKeyAttribute>();
                    if (pk != null)
                        return p.Name;
                }

                throw new InvalidOperationException($"No AtomKey or AtomKeyProperty attribute defined in class {x.FullName}");

            });
        }

        public static object GetPropertyValue(this object obj, string propertyName) {
            if (obj == null)
                return null;
            if (string.IsNullOrWhiteSpace(propertyName))
                throw new ArgumentException("Property name cannot be empty");
            Type type = obj.GetType();
            string key = type.FullName + "[.]" + propertyName;
            var p = properties.GetOrAdd(key, x => type.GetProperty(propertyName));
            return p?.GetValue(obj);
        }


        public static void SetPropertyValue(this object obj, string propertyName, object value)
        {
            if (obj == null)
                return;
            Type type = obj.GetType();
            string key = type.FullName + "[.]" + propertyName;
            var p = properties.GetOrAdd(key, x => type.GetProperty(propertyName));
            p?.SetValue(obj,value);
        }


        /// <summary>
        /// Slices given Enumerable into Enumerable of slices..
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="items"></param>
        /// <param name="slices"></param>
        /// <returns></returns>
        public static IEnumerable<IEnumerable<T>> Slice<T>(this IEnumerable<T> items, int slices = 2)
        {
            return Observable(items, () => _Slice<T>(items, slices));
        }

        private static IEnumerable<IEnumerable<T>> _Slice<T>(this IEnumerable<T> items, int slices = 2) {

            while (items.Any()) {
                var slice = items.Take(slices);
                items = items.Skip(slices);
                yield return slice;
            }

        }

        /// <summary>
        /// Slices given Enumerable into Enumerable of slices..
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="items"></param>
        /// <param name="slices"></param>
        /// <returns></returns>
        public static IEnumerable<IGrouping<TKey,IEnumerable<T>>> Slice<TKey,T>(this IEnumerable<IGrouping<TKey,T>> items, int slices = 2)
        {
            return Observable(items, () => items.Select( x => new GroupedSlice<TKey,T>(x.Key,x,slices)));
        }

        public static IEnumerable<T> Observable<T>(
            object items, 
            Func<IEnumerable<T>> results) {
            if (items is INotifyCollectionChanged)
                return new ObservableEnumerable<T>(items, results);
            return results();
        }


        public class ObservableEnumerable<T> : IEnumerable<T>, INotifyCollectionChanged
        {
            private Func<IEnumerable<T>> items;

            public ObservableEnumerable(object originalItems, Func<IEnumerable<T>> items)
            {
                this.items = items;

                var incc = originalItems as INotifyCollectionChanged;
                if (incc != null) {
                    incc.CollectionChanged += (s, e) => {
                        
                        CollectionChanged?.Invoke(this, new NotifyCollectionChangedEventArgs(NotifyCollectionChangedAction.Reset));
                    };
                }
            }

            public event NotifyCollectionChangedEventHandler CollectionChanged;

            public IEnumerator<T> GetEnumerator()
            {
                return items().GetEnumerator();
            }

            IEnumerator IEnumerable.GetEnumerator()
            {
                return GetEnumerator();
            }
        }

    }

    public class GroupedSlice<TKey, T> : IGrouping<TKey, IEnumerable<T>>
    {

        internal GroupedSlice(TKey key, IEnumerable<T> items, int slices)
        {
            this.Key = key;
            this.Items = items;
            this.Slices = slices;
        }

        public TKey Key
        {
            get;
        }

        public IEnumerable<T> Items
        {
            get;
        }

        public int Slices { get; }

        public IEnumerator<IEnumerable<T>> GetEnumerator()
        {
            return Items.Slice(Slices).GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }


}