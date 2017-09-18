using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace NeuroSpeech.UIAtoms.Controls
{

    /// <summary>
    /// 
    /// </summary>
    public class AtomCalendar : Grid
    {

        #region Property CurrentYear

        /// <summary>
        /// Bindable Property CurrentYear
        /// </summary>
        public static readonly BindableProperty CurrentYearProperty = BindableProperty.Create(
          nameof(CurrentYear),
          typeof(int),
          typeof(AtomCalendar),
          DateTime.Now.Year,
          BindingMode.TwoWay,
          // validate value delegate
           (sender,value) => value is int && ((int)value)>0,
          
          // property changed, delegate
           //(sender,oldValue,newValue) => ((AtomCalendar)sender).OnCurrentYearChanged(oldValue,newValue),
           null,
          
          // property changing delegate
          // (sender,oldValue,newValue) => {}
          null,
          // coerce value delegate 
          // (sender,value) => value
          null,
          // create default value delegate
           (v) => DateTime.Now.Year
          
        );

        

        /// <summary>
        /// Property CurrentYear
        /// </summary>
        public int CurrentYear
        {
            get
            {
                return (int)GetValue(CurrentYearProperty);
            }
            set
            {
                SetValue(CurrentYearProperty, value);
            }
        }
        #endregion

        #region Property CurrentMonth

        /// <summary>
        /// Bindable Property CurrentMonth
        /// </summary>
        public static readonly BindableProperty CurrentMonthProperty = BindableProperty.Create(
          nameof(CurrentMonth),
          typeof(int),
          typeof(AtomCalendar),
          1,
          BindingMode.TwoWay,
          // validate value delegate
          //(sender,value) => value is int && ((int)value)>0 && ((int)value)<13,
          null,
          // property changed, delegate
          //(sender,oldValue,newValue) => {}
          null,
          // property changing delegate
          // (sender,oldValue,newValue) => {}
          null,
          // coerce value delegate 
          // (sender,value) => value
          null,
          // create default value delegate
           (s) => DateTime.Now.Month
        );


        /// <summary>
        /// Property CurrentMonth
        /// </summary>
        public int CurrentMonth
        {
            get
            {
                return (int)GetValue(CurrentMonthProperty);
            }
            set
            {
                SetValue(CurrentMonthProperty, value);
            }
        }
        #endregion

        #region Property DateModelFactory

        /// <summary>
        /// Bindable Property DateModelFactory
        /// </summary>
        public static readonly BindableProperty DateModelFactoryProperty = BindableProperty.Create(
          nameof(DateModelFactory),
          typeof(Func<DateTime,AtomDateModel>),
          typeof(AtomCalendar),
          (Func<DateTime,AtomDateModel>)OnNewDateModel,
          BindingMode.OneWay,
          // validate value delegate
          // (sender,value) => true
          null,
          // property changed, delegate
          //(sender,oldValue,newValue) => ((AtomCalendar)sender).OnDateModelFactoryChanged(oldValue,newValue),
          null,
          // property changing delegate
          // (sender,oldValue,newValue) => {}
          null,
          // coerce value delegate 
          // (sender,value) => value
          null,
          // create default value delegate
          // () => Default(T)
          null
        );

        private static AtomDateModel OnNewDateModel(DateTime date)
        {
            return new AtomDateModel(date);
        }

        /*
        /// <summary>
        /// On DateModelFactory changed
        /// </summary>
        /// <param name="oldValue">Old Value</param>
        /// <param name="newValue">New Value</param>
        protected virtual void OnDateModelFactoryChanged(object oldValue, object newValue)
        {
            
        }*/


        /// <summary>
        /// Property DateModelFactory
        /// </summary>
        public Func<DateTime,AtomDateModel> DateModelFactory
        {
            get
            {
                return (Func<DateTime,AtomDateModel>)GetValue(DateModelFactoryProperty);
            }
            set
            {
                SetValue(DateModelFactoryProperty, value);
            }
        }
        #endregion

        #region Property StartDate

        /// <summary>
        /// Bindable Property StartDate
        /// </summary>
        public static readonly BindableProperty StartDateProperty = BindableProperty.Create(
          nameof(StartDate),
          typeof(DateTime),
          typeof(AtomCalendar),
          DateTime.Now.AddYears(-100),
          BindingMode.TwoWay,
          // validate value delegate
          // (sender,value) => true
          null,
          // property changed, delegate
          //(sender,oldValue,newValue) => ((AtomCalendar)sender).OnStartDateChanged(oldValue,newValue),
          null,
          // property changing delegate
          // (sender,oldValue,newValue) => {}
          null,
          // coerce value delegate 
           null,
          // create default value delegate
           s => DateTime.Now.AddYears(-100)
          //null
        );

        /*
        /// <summary>
        /// On StartDate changed
        /// </summary>
        /// <param name="oldValue">Old Value</param>
        /// <param name="newValue">New Value</param>
        protected virtual void OnStartDateChanged(object oldValue, object newValue)
        {
            
        }*/


        /// <summary>
        /// Property StartDate
        /// </summary>
        public DateTime StartDate
        {
            get
            {
                return (DateTime)GetValue(StartDateProperty);
            }
            set
            {
                SetValue(StartDateProperty, value);
            }
        }
        #endregion

        #region Property EndDate

        /// <summary>
        /// Bindable Property EndDate
        /// </summary>
        public static readonly BindableProperty EndDateProperty = BindableProperty.Create(
          nameof(EndDate),
          typeof(DateTime),
          typeof(AtomCalendar),
          DateTime.MaxValue,
          BindingMode.TwoWay,
          // validate value delegate
          // (sender,value) => true
          null,
          // property changed, delegate
          //(sender,oldValue,newValue) => ((AtomCalendar)sender).OnEndDateChanged(oldValue,newValue),
          null,
          // property changing delegate
          // (sender,oldValue,newValue) => {}
          null,
          // coerce value delegate 
          null,
          // create default value delegate
          s => DateTime.Now.AddYears(100)
          
        );

        /*
        /// <summary>
        /// On EndDate changed
        /// </summary>
        /// <param name="oldValue">Old Value</param>
        /// <param name="newValue">New Value</param>
        protected virtual void OnEndDateChanged(object oldValue, object newValue)
        {
            
        }*/


        /// <summary>
        /// Property EndDate
        /// </summary>
        public DateTime EndDate
        {
            get
            {
                return (DateTime)GetValue(EndDateProperty);
            }
            set
            {
                SetValue(EndDateProperty, value);
            }
        }
        #endregion


        #region Property SelectedDate

        /// <summary>
        /// Bindable Property SelectedDate
        /// </summary>
        public static readonly BindableProperty SelectedDateProperty = BindableProperty.Create(
          nameof(SelectedDate),
          typeof(DateTime?),
          typeof(AtomCalendar),
          null,
          BindingMode.OneWay,
          // validate value delegate
          // (sender,value) => true
          null,
          // property changed, delegate
          (sender,oldValue,newValue) => ((AtomCalendar)sender).OnSelectedDateChanged(oldValue,newValue),
          //null,
          // property changing delegate
          // (sender,oldValue,newValue) => {}
          null,
          // coerce value delegate 
          // (sender,value) => value
          null,
          // create default value delegate
           s => DateTime.Today
          
        );


        private bool isDateChanging = false;
        
        /// <summary>
        /// On SelectedDate changed
        /// </summary>
        /// <param name="oldValue">Old Value</param>
        /// <param name="newValue">New Value</param>
        protected virtual void OnSelectedDateChanged(object oldValue, object newValue)
        {
            if (isDateChanging)
                return;
            var sd = (DateTime?)newValue;
            if (sd != null) {
                CurrentMonth = sd.Value.Month;
                CurrentYear = sd.Value.Year;
            }
        }


        /// <summary>
        /// Property SelectedDate
        /// </summary>
        public DateTime? SelectedDate
        {
            get
            {
                return (DateTime?)GetValue(SelectedDateProperty);
            }
            set
            {
                SetValue(SelectedDateProperty, value);
            }
        }
        #endregion

        #region Property ItemTemplate

        /// <summary>
        /// Bindable Property ItemTemplate
        /// </summary>
        public static readonly BindableProperty ItemTemplateProperty = BindableProperty.Create(
          nameof(ItemTemplate),
          typeof(DataTemplate),
          typeof(AtomCalendar),
          null,
          BindingMode.OneWay,
          // validate value delegate
          // (sender,value) => true
          null,
          // property changed, delegate
          //(sender,oldValue,newValue) => ((AtomCalendar)sender).OnItemTemplateChanged(oldValue,newValue),
          null,
          // property changing delegate
          // (sender,oldValue,newValue) => {}
          null,
          // coerce value delegate 
          // (sender,value) => value
          null,
           // create default value delegate
           (s) => new DataTemplate(typeof(AtomDateView))
          //null
        );

        /*
        /// <summary>
        /// On ItemTemplate changed
        /// </summary>
        /// <param name="oldValue">Old Value</param>
        /// <param name="newValue">New Value</param>
        protected virtual void OnItemTemplateChanged(object oldValue, object newValue)
        {
            
        }*/


        /// <summary>
        /// Property ItemTemplate
        /// </summary>
        public DataTemplate ItemTemplate
        {
            get
            {
                return (DataTemplate)GetValue(ItemTemplateProperty);
            }
            set
            {
                SetValue(ItemTemplateProperty, value);
            }
        }
        #endregion



        #region Property TapCommand

        /// <summary>
        /// Bindable Property TapCommand
        /// </summary>
        public static readonly BindableProperty TapCommandProperty = BindableProperty.Create(
          nameof(TapCommand),
          typeof(System.Windows.Input.ICommand),
          typeof(AtomCalendar),
          null,
          BindingMode.OneWay,
          // validate value delegate
          // (sender,value) => true
          null,
          // property changed, delegate
          //(sender,oldValue,newValue) => ((AtomCalendar)sender).OnTapCommandChanged(oldValue,newValue),
          null,
          // property changing delegate
          // (sender,oldValue,newValue) => {}
          null,
          // coerce value delegate 
          // (sender,value) => value
          null,
          // create default value delegate
          // () => Default(T)
          null
        );

        /*
        /// <summary>
        /// On TapCommand changed
        /// </summary>
        /// <param name="oldValue">Old Value</param>
        /// <param name="newValue">New Value</param>
        protected virtual void OnTapCommandChanged(object oldValue, object newValue)
        {
            
        }*/


        /// <summary>
        /// Property TapCommand
        /// </summary>
        public System.Windows.Input.ICommand TapCommand
        {
            get
            {
                return (System.Windows.Input.ICommand)GetValue(TapCommandProperty);
            }
            set
            {
                SetValue(TapCommandProperty, value);
            }
        }
        #endregion




        #region Property DateTypes

        /// <summary>
        /// Bindable Property DateTypes
        /// </summary>
        public static readonly BindableProperty DateTypesProperty = BindableProperty.Create(
          nameof(DateTypes),
          typeof(IEnumerable<KeyValuePair<DateTime,string>>),
          typeof(AtomCalendar),
          null,
          BindingMode.OneWay,
          // validate value delegate
          // (sender,value) => true
          null,
          // property changed, delegate
          (sender,oldValue,newValue) => ((AtomCalendar)sender).OnDateTypesChanged(oldValue,newValue),
          //null,
          // property changing delegate
          // (sender,oldValue,newValue) => {}
          null,
          // coerce value delegate 
          // (sender,value) => value
          null,
          // create default value delegate
          // () => Default(T)
          null
        );

        
        /// <summary>
        /// On DateTypes changed
        /// </summary>
        /// <param name="oldValue">Old Value</param>
        /// <param name="newValue">New Value</param>
        protected virtual void OnDateTypesChanged(object oldValue, object newValue)
        {
            INotifyCollectionChanged inc = oldValue as INotifyCollectionChanged;
            if(inc!=null)
            {
                inc.CollectionChanged -= Inc_CollectionChanged;
            }
            inc = newValue as INotifyCollectionChanged;
            if(inc!=null)
            {
                inc.CollectionChanged += Inc_CollectionChanged;
            }
            if (newValue != null) {
                Inc_CollectionChanged(newValue, new NotifyCollectionChangedEventArgs(NotifyCollectionChangedAction.Reset));
            }
        }

        private void Inc_CollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            var items = DateTypes;
            List<AtomDateModel> list = listView.ItemsSource as List<AtomDateModel>;
            if (list == null || items==null)
                return;
            foreach(var item in list)
            {
                item.Type= items.Where(x => x.Key == item.Value).Select(x => x.Value).FirstOrDefault();
            }
        }


        /// <summary>
        /// Property DateTypes
        /// </summary>
        public IEnumerable<KeyValuePair<DateTime,string>> DateTypes
        {
            get
            {
                return (IEnumerable<KeyValuePair<DateTime,string>>)GetValue(DateTypesProperty);
            }
            set
            {
                SetValue(DateTypesProperty, value);
            }
        }
        #endregion





        private readonly AtomGridView listView;
        private AtomChooser monthPicker;
        private AtomChooser yearPicker;

        private AtomList<AtomData<int>> yearList = new AtomList<AtomData<int>>();
        private AtomList<AtomData<int>> monthList = new AtomList<AtomData<int>>();

        /// <summary>
        /// 
        /// </summary>
        /// <param name="propertyName"></param>
        protected override void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            base.OnPropertyChanged(propertyName);

            if (isDateChanging)
                return;

            if (propertyName == nameof(StartDate) || propertyName == nameof(EndDate)) {
                UpdateYears();
            }

            UIAtomsApplication.Instance.TriggerOnce(UpdateList);
        }


        private Button leftButton = new Button() {  Text = "<" };
        private Button rightButton = new Button() {  Text = ">" };


        /// <summary>
        /// 
        /// </summary>
        public AtomCalendar()
        {


            RowSpacing = 5;
            ColumnSpacing = 5;
            Padding = new Thickness(5);

            this.RowDefinitions.Add(new RowDefinition { Height = GridLength.Auto });
            this.RowDefinitions.Add(new RowDefinition { });

            this.ColumnDefinitions.Add(new ColumnDefinition { Width = new GridLength(2, GridUnitType.Star) });
            this.ColumnDefinitions.Add(new ColumnDefinition { Width = new GridLength(8, GridUnitType.Star) });
            this.ColumnDefinitions.Add(new ColumnDefinition { Width = new GridLength(5, GridUnitType.Star) });
            this.ColumnDefinitions.Add(new ColumnDefinition { Width = new GridLength(2, GridUnitType.Star) });


            listView = new AtomGridView();
            /*listView.SetBinding(AtomListView.TapCommandProperty, new Binding() {
                Source = this,
                Path = nameof(TapCommand)
            });*/

            listView.TapCommand = new AtomCommand(o => {
                try
                {
                    isDateChanging = true;
                    this.SelectedDate = ((AtomDateModel)o).Value;
                }
                finally {
                    isDateChanging = false;
                }
                TapCommand?.Execute(o);
                return Task.CompletedTask;
            });

            monthPicker = new AtomChooser() {
                ValuePath = "Value"
            };

            DateTime start = new DateTime(2012, 1, 1);
            for (int i = 0; i < 12; i++) {
                monthList.Add( new AtomData<int>(start.ToString("MMMMMM"),i+1));
                start = start.AddMonths(1);
            }

            //monthPicker.SelectedIndexChanged += (s, e) => {
            //    CurrentMonth = monthPicker.SelectedIndex + 1;
            //};

            //monthPicker.SelectedIndex = CurrentMonth - 1;




            

            yearPicker = new AtomChooser() {
                ValuePath = "Value"
            };

            yearPicker.LabelPath = monthPicker.LabelPath = "Label";

            yearPicker.ItemTemplate = monthPicker.ItemTemplate = new DataTemplate(() => {
                Label label = new Label();
                label.SetBinding(Label.TextProperty, new Binding() {
                    Path = "Label"
                });
                return label;
            });



            //yearPicker.SelectedIndexChanged += (s, e) => {
            //    CurrentYear = int.Parse(yearPicker.Items[yearPicker.SelectedIndex]);
            //};

            UpdateYears();





            monthPicker.SetBinding(AtomChooser.ValueProperty, new Binding()
            {
                Source = this,
                Path = "CurrentMonth",
                Mode = BindingMode.TwoWay

            });

            monthPicker.ShowSearch = false;
            monthPicker.ShowAsPopupButton = true;

            yearPicker.ShowSearch = false;
            yearPicker.ShowAsPopupButton = true;

            yearPicker.SetBinding(AtomChooser.ValueProperty, new Binding() {
                Source = this,
                Path = "CurrentYear",
                Mode = BindingMode.TwoWay
            });

            yearPicker.ItemsSource = yearList;
            monthPicker.ItemsSource = monthList;


            // first row..
            SetColumn(monthPicker, 1);
            SetColumn(yearPicker, 2);
            SetColumn(rightButton, 3);


            // second row
            SetColumnSpan(listView, 4);
            SetRow(listView, 1);


            this.Children.Add(leftButton);
            this.Children.Add(monthPicker);
            this.Children.Add(yearPicker);
            this.Children.Add(listView);
            this.Children.Add(rightButton);



            UpdateList();

            leftButton.Command = new AtomCommand(()=> {
                if (CurrentMonth == 1)
                {
                    CurrentMonth = 12;
                    CurrentYear = CurrentYear - 1;
                }
                else {
                    CurrentMonth = CurrentMonth - 1;
                }
                return Task.CompletedTask;
            });


            rightButton.Command = new AtomCommand(() => {
                if (CurrentMonth == 12)
                {
                    CurrentMonth = 1;
                    CurrentYear = CurrentYear + 1;
                }
                else
                {
                    CurrentMonth = CurrentMonth + 1;
                }
                return Task.CompletedTask;
            });
        }


        private void UpdateList()
        {

            if (Width <= 0) {
                UIAtomsApplication.Instance.SetTimeout(UpdateList, TimeSpan.FromMilliseconds(100));
                return;
            }


            listView.ItemWidth = ( Math.Floor( Width / 7) ) - 2*ColumnSpacing;

            System.Diagnostics.Debug.WriteLine($"Width is {Width} and Item Width is {listView.ItemWidth}");

            int currentMonth = CurrentMonth;

            DateTime start = new DateTime(CurrentYear, currentMonth, 1);

            while (start.DayOfWeek != DayOfWeek.Monday) {
                start = start.AddDays(-1);
            }

            List<AtomDateModel> list = new List<AtomDateModel>();

            for (int i = 0; i < 5*7; i++)
            {
                list.Add(DateModelFactory( start));
                start = start.AddDays(1);
            }

            
            listView.ItemTemplate = ItemTemplate;
            listView.ItemHeight = 50;
            listView.ItemsSource = list;

            Inc_CollectionChanged(null, new NotifyCollectionChangedEventArgs(NotifyCollectionChangedAction.Reset));


            listView.HeightRequest = 5 * 52;

        }

        private void UpdateYears()
        {
            int start = StartDate.Year;
            int end = EndDate.Year;

            List<AtomData<int>> list = new List<AtomData<int>>();

            for (int i = end; i >= start; i--)
            {
                list.Add(new AtomData<int>(i.ToString(),i));
            }

            yearList.Replace(list);

            //yearPicker.SelectedIndex = -1;
            
            

            //SetCurrentYear();
        }

        //private void SetCurrentYear()
        //{
        //    string y = CurrentYear.ToString();

        //    int i = yearPicker.Items.IndexOf(y);
        //    if (i == -1)
        //        i = 0;
        //    yearPicker.SelectedIndex = i;
        //}
    }

    /// <summary>
    /// 
    /// </summary>
    public class AtomDateView : Grid {

        /// <summary>
        /// 
        /// </summary>
        public AtomDateView()
        {

            Label label = new Label();
            Children.Add(label);

            label.SetBinding(Label.TextProperty, new Binding(nameof(AtomDateModel.Label)) );

            var weekendTriggerLabel = new DataTrigger(typeof(Label)) {
                Binding = new Binding(nameof(AtomDateModel.IsWeekend)),
                Value = true
            };

            weekendTriggerLabel.Setters.Add(new Setter {
                Property = Label.TextColorProperty,
                Value = Color.FromRgba(0.5,0.5,0.5,0.5)
            });


            var todayTriggerLabel = new DataTrigger(typeof(Label)) {
                Binding = new Binding(nameof(AtomDateModel.IsToday)),
                Value = true
            };



            todayTriggerLabel.Setters.Add(new Setter
            {
                Property = Label.TextColorProperty,
                Value = Color.White
            });

            label.Triggers.Add(todayTriggerLabel);
            label.Triggers.Add(weekendTriggerLabel);

            var backgroundTodayTrigger = new DataTrigger(typeof(Grid)) {
                Binding = new Binding(nameof(AtomDateModel.IsToday)),
                Value = true
            };
            backgroundTodayTrigger.Setters.Add(new Setter
            {
                Property = Label.BackgroundColorProperty,
                Value = Color.Red
            });

            this.Triggers.Add(backgroundTodayTrigger);

            this.Padding = new Thickness(5);
        }



    }

    /// <summary>
    /// 
    /// </summary>
    public class AtomDateModel: AtomModel {


        /// <summary>
        /// 
        /// </summary>
        /// <param name="date"></param>
        public AtomDateModel(
            DateTime date)
        {
            Value = date;

            DateTime today = DateTime.Today;

            IsWeekend = date.DayOfWeek == DayOfWeek.Saturday || date.DayOfWeek == DayOfWeek.Sunday;

            this.IsOtherMonth = today.Month != date.Month;
            this.IsToday = today == date;

            Label = date.Day.ToString();

            

        }


        #region Property Label

        private string _Label = "";
        /// <summary>
        /// 
        /// </summary>
        public string Label
        {
            get
            {
                return _Label;
            }
            set
            {
                SetProperty(ref _Label, value);
            }
        }
        #endregion

        #region Property Type   

        private string _Type = "";
        /// <summary>
        /// 
        /// </summary>
        public string Type
        {
            get
            {
                return _Type;
            }
            set
            {
                SetProperty(ref _Type, value);
            }
        }
        #endregion

        #region Property IsWeekend

        /// <summary>
        /// 
        /// </summary>
        public bool IsWeekend
        {
            get;
        }
        #endregion

        #region Property IsOtherMonth

        /// <summary>
        /// 
        /// </summary>
        public bool IsOtherMonth
        {
            get;
        }
        #endregion

        #region Property IsToday

        private bool _IsToday = false;
        /// <summary>
        /// 
        /// </summary>
        public bool IsToday
        {
            get
            {
                return _IsToday;
            }
            set
            {
                SetProperty(ref _IsToday, value);
            }
        }
        #endregion



        #region Property Value

        /// <summary>
        /// 
        /// </summary>
        public DateTime Value
        {
            get;
        }
        #endregion



    }

    
}
