using NeuroSpeech.UIAtoms.DI;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;

namespace NeuroSpeech.UIAtoms
{


    /// <summary>
    /// 
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class AtomCommand<T> : INotifyPropertyChanged, ICommand
    {
        Func<T,Task> task;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="task"></param>
        public AtomCommand(Func<T,Task> task)
        {
            this.task = task;
        }

        //public AtomCommand(Func<T,Task> task)
        //{
        //    this.action = i => {
        //        Device.BeginInvokeOnMainThread(async ()=> await task(i));
        //    };
        //}

        /// <summary>
        /// 
        /// </summary>
        public event EventHandler CanExecuteChanged;

        /// <summary>
        /// 
        /// </summary>
        public event PropertyChangedEventHandler PropertyChanged;


        private bool _IsBusy = false;
        /// <summary>
        /// 
        /// </summary>
        public bool IsBusy {
            get
            {
                return _IsBusy;
            }
            private set {
                _IsBusy = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(IsBusy)));
                CanExecuteChanged?.Invoke(this, EventArgs.Empty);
            }
        }



        private bool _IsEnabled = true;
        /// <summary>
        /// 
        /// </summary>
        public bool IsEnabled {
            get {
                return _IsEnabled;
            }
            set {
                _IsEnabled = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(IsEnabled)));
                CanExecuteChanged?.Invoke(this, EventArgs.Empty);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="parameter"></param>
        /// <returns></returns>
        public bool CanExecute(object parameter)
        {
            return _IsEnabled && !_IsBusy;
        }

        private static MethodInfo castMethod = null;


        /// <summary>
        /// 
        /// </summary>
        /// <param name="parameter"></param>
        public void Execute(object parameter)
        {
            Device.BeginInvokeOnMainThread(async () => {
                try
                {
                    if (IsBusy)
                        return;
                    IsBusy = true;
                    if (parameter is System.Collections.IEnumerable)
                    {
                        // we might need to convert ...
                        Type pt = typeof(T);

                        Type it = parameter.GetType();

                        if (!pt.IsAssignableFrom(it))
                        {

                            Type et = pt.GetGenericArguments()[0];

                            if (castMethod == null)
                            {
                                castMethod = typeof(Enumerable).GetMethod("Cast");
                            }

                            parameter = castMethod.MakeGenericMethod(et).Invoke(null, new object[] { parameter });
                        }
                    }

                    await this.task((T)parameter);
                }
                catch (TaskCanceledException) {
                }
                catch (Exception ex)
                {
                    System.Diagnostics.Debug.Fail(ex.Message, ex.ToString());
                    await DependencyService.Get<INotificationService>().NotifyAsync(ex.Message);
                }
                finally
                {
                    IsBusy = false;
                }
            });
        }
    }

    /// <summary>
    /// 
    /// </summary>
    public class AtomCommand : AtomCommand<object> {

        /// <summary>
        /// 
        /// </summary>
        /// <param name="action"></param>
        public AtomCommand(Func<Task> action):base(a=> action())
        {

        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="action"></param>
        public AtomCommand(Func<object,Task> action) : base(action) {
        }

        //public AtomCommand(Func<Task> task): base(x=> task())
        //{

        //}
    }

    /// <summary>
    /// 
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class AtomData<T>: AtomModel {

        /// <summary>
        /// 
        /// </summary>
        public AtomData()
        {

        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="label"></param>
        /// <param name="value"></param>
        public AtomData(string label, T value)
        {
            this._Label = label;
            this._Value = value;
        }

        private string _Label;

        /// <summary>
        /// 
        /// </summary>
        public string Label
        {
            get { return _Label; }
            set { SetProperty(ref _Label, value);
            }
        }

        private T _Value;

        /// <summary>
        /// 
        /// </summary>
        public T Value
        {
            get { return _Value; }
            set { SetProperty(ref _Value, value); }
        }


    }
}
