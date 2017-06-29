using System;
using System.Linq;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Globalization;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using System.Reflection;
using System.Diagnostics;

namespace NeuroSpeech.UIAtoms
{
    /// <summary>
    /// 
    /// </summary>
    public class PropertyListViewModel : UIViewModel
    {

        #region Search Property

        /// <summary>
        /// 
        /// </summary>
        private string _Search;

        /// <summary>
        /// 
        /// </summary>
        public string Search {
            get {
                return _Search;
            }
            set {
                SetProperty(ref _Search, value, onChanged: ()=>
                {
                    FilterList(value);
                });
            }
        }

        private void FilterList(string value)
        {

            IEnumerable<PropertyBinding> q = allProperties;

            if (!string.IsNullOrWhiteSpace(value))
            {
                q = q.Where(x => x.FormField.HasText(value));
            }
            Items = q.GroupBy(x => x.FormField.Category).Select(x => new GroupList(x.Key, x)).ToList();
        }

        #endregion



        #region Source Property
        /// <summary>
        /// 
        /// </summary>
        private object _Source;
        /// <summary>
        /// 
        /// </summary>
        public object Source { get
            {
                return _Source;
            } set {
                SetProperty(ref _Source, value, onChanged: () => {
                    allProperties = CreatePropertyBindings(value);
                    FilterList(_Search);
                });
            }
        }

        private List<PropertyBinding> allProperties = new List<PropertyBinding>();

        private List<PropertyBinding> CreatePropertyBindings(object value)
        {
            var result = new List<PropertyBinding>();
            if (value == null) {
                return result;
            }

            foreach (PropertyInfo p in value.GetType().GetRuntimeProperties()) {
                var a = p.GetCustomAttribute<FormFieldAttribute>();
                if (a == null)
                    continue;
                result.Add(new PropertyBinding(a,value, p));
            }

            return result;
        }
        #endregion


        #region Items Property
        /// <summary>
        /// 
        /// </summary>
        private List<GroupList> _Items;

        /// <summary>
        /// 
        /// </summary>
        public List<GroupList> Items
        {
            get
            {
                return _Items;
            }
            protected set
            {
                SetProperty(ref _Items, value);
            }
        }

        #endregion


        /// <summary>
        /// 
        /// </summary>
        /// <param name="source"></param>
        public PropertyListViewModel(Object source)
        {
            this.Source = source;
        }



    }


    public class GroupList : List<PropertyBinding> {

        public GroupList(string key, IEnumerable<PropertyBinding> x)
        {
            GroupName = key;
            this.AddRange(x);
        }

        public string GroupName { get; set; }

    }
}