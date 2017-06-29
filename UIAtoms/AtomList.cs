using System;
using System.Linq;
using System.Collections.Generic;
using System.Text;
using System.Collections;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Collections.Concurrent;
using System.Reflection;
using System.Collections.ObjectModel;
using System.Threading.Tasks;

namespace NeuroSpeech.UIAtoms
{

    /// <summary>
    /// 
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class AtomList<T> : Collection<T>,
        INotifyCollectionChanged,
        INotifyPropertyChanged
    {


        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public IDisposable BeginEdit() {
            IsChanging = true;
            return new AtomDisposableAction(() => {
                IsChanging = false;
            });
        }


        private bool _IsChanging;

        /// <summary>
        /// 
        /// </summary>
        public event NotifyCollectionChangedEventHandler CollectionChanged;

        /// <summary>
        /// 
        /// </summary>
        public event PropertyChangedEventHandler PropertyChanged;

        /// <summary>
        /// 
        /// </summary>
        public bool IsChanging
        {
            get {
                return _IsChanging;
            }
            private set {
                bool notify = _IsChanging && !value;
                if (notify) {
                    //OnCollectionChanged(new NotifyCollectionChangedEventArgs(NotifyCollectionChangedAction.Reset));

                    CollectionChanged?.Invoke(this, new NotifyCollectionChangedEventArgs(NotifyCollectionChangedAction.Reset));
                    OnPropertyChanged(new PropertyChangedEventArgs("Count"));
                }
                _IsChanging = value;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="items"></param>
        [Obsolete("Please use Replace or ReplaceAsync if possible for better performance")]
        public void AddRange(IEnumerable<T> items) {
            try {
                _IsChanging = true;
                foreach (var item in items) {
                    this.Add(item);
                }
            } finally {
                IsChanging = false;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="e"></param>
        protected virtual void OnCollectionChanged(NotifyCollectionChangedEventArgs e)
        {
            if (_IsChanging)
                return;
            CollectionChanged?.Invoke(this,e);
            OnPropertyChanged(new PropertyChangedEventArgs("Count"));
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="e"></param>
        protected virtual void OnPropertyChanged(PropertyChangedEventArgs e)
        {
            if (_IsChanging)
                return;
            PropertyChanged?.Invoke(this, e);
        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="index"></param>
        /// <param name="item"></param>
        protected override void InsertItem(int index, T item)
        {
            base.InsertItem(index, item);
            if (!_IsChanging)
                OnCollectionChanged(new NotifyCollectionChangedEventArgs(NotifyCollectionChangedAction.Reset));
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="index"></param>
        protected override void RemoveItem(int index)
        {
            base.RemoveItem(index);
            if (!_IsChanging)
                OnCollectionChanged(new NotifyCollectionChangedEventArgs(NotifyCollectionChangedAction.Reset));
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="index"></param>
        /// <param name="item"></param>
        protected override void SetItem(int index, T item)
        {
            base.SetItem(index, item);
            if (!_IsChanging)
                OnCollectionChanged(new NotifyCollectionChangedEventArgs(NotifyCollectionChangedAction.Reset));
        }

        /// <summary>
        /// 
        /// </summary>
        protected override void ClearItems()
        {
            base.ClearItems();
            if(!_IsChanging)
                OnCollectionChanged(new NotifyCollectionChangedEventArgs(NotifyCollectionChangedAction.Reset));
        }



        /// <summary>
        /// Merge will compare individual items and add if item does not exist in the list.. this is faster for paged items
        /// </summary>
        /// <param name="items"></param>
        /// <param name="keyProperty"></param>
        public void Merge(IEnumerable<T> items, string keyProperty = null) {



            using (BeginEdit()) {


                MergeInternal(items, keyProperty);

            }
        }

        private Task RunLocked(Action action) {
            return Task.Run(() => {
                lock (this) {
                    action();
                }
            });
        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="items"></param>
        /// <returns></returns>
        public async Task AddRangeAsync(IEnumerable<T> items) {
            using (BeginEdit()) {
                await RunLocked(() => {
                    foreach (var item in items) {
                        Add(item);
                    }
                });
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="items"></param>
        /// <param name="keyProperty"></param>
        /// <returns></returns>
        public async Task MergeAsync(IEnumerable<T> items, string keyProperty = null) {
            using (BeginEdit()) {
                await RunLocked(()=> {
                    MergeInternal(items, keyProperty);
                });
            }
        }

        private void MergeInternal(IEnumerable<T> items, string keyProperty)
        {
            int n = this.Count;
            if (n == 0)
            {
                foreach (var item in items)
                {
                    this.Add(item);
                }
                return;
            }




            if (keyProperty == null)
            {
                keyProperty = typeof(T).GetKeyProperty();
            }



            List<T> itemsToAdd = new List<T>();

            foreach (var item in items)
            {
                bool replaced = false;
                for (int i = 0; i < n; i++)
                {

                    var old = this[i];

                    var ov = old.GetPropertyValue(keyProperty);

                    var kv = item.GetPropertyValue(keyProperty);

                    if (ov == kv || (ov != null && ov.Equals(kv)))
                    {
                        this[i] = item;
                        replaced = true;
                        break;
                    }

                }
                if (!replaced)
                {
                    itemsToAdd.Add(item);
                }

            }

            foreach (var item in itemsToAdd)
            {
                this.Add(item);
            }
        }

        /// <summary>
        /// Replace will replace all items, it will remove additional items from the list, this is faster for all items
        /// </summary>
        /// <param name="items"></param>
        public void Replace(IEnumerable<T> items) {

            using (BeginEdit())
            {
                ReplaceInternal(items);

            }

        }

        private void ReplaceInternal(IEnumerable<T> items)
        {
            int n = Count;

            int i = 0;

            foreach (var item in items)
            {

                if (i < n)
                {
                    this[i] = item;
                    //replaced.Add(item);
                }
                else
                {
                    this.Add(item);
                    //added.Add(item);
                }

                i++;

            }


            if (i < n)
            {

                while (this.Count > i)
                {
                    this.RemoveAt(i);
                }
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="items"></param>
        public async Task ReplaceAsync(IEnumerable<T> items) {
            using (BeginEdit()) {
                await RunLocked(()=> {
                    ReplaceInternal(items);
                });
            }
        }


        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="TKey"></typeparam>
        /// <param name="grouping"></param>
        /// <returns></returns>
        public AtomGroupList<TKey, T> GroupedList<TKey>(Func<T, TKey> grouping) {
            return new AtomGroupList<TKey, T>(this, grouping);
        }


    }


    /// <summary>
    /// 
    /// </summary>
    /// <typeparam name="TKey"></typeparam>
    /// <typeparam name="T"></typeparam>
    public class AtomGroupList<TKey, T> : AtomList<IGrouping<TKey, T>>
    {
        private Func<T, TKey> grouping;
        private IEnumerable<T> source;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="source"></param>
        /// <param name="groupBy"></param>
        public AtomGroupList(IEnumerable<T> source, Func<T, TKey> groupBy)
        {
            this.source = source;
            this.grouping = groupBy;


            INotifyCollectionChanged incc = source as INotifyCollectionChanged;
            if (incc != null)
            {
                incc.CollectionChanged += (s, e) =>
                {
                    UIAtomsApplication.Instance.TriggerOnce(Reset);
                };
            }
            Reset();
        }

        private void Reset()
        {
            this.Replace(source.GroupBy(grouping));
        }
    }

}