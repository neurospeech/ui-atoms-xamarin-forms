using NeuroSpeech.UIAtoms;
using System;
using System.Collections.Generic;
using System.Text;

namespace UIAtomsDemo.ViewModels
{
    public class CalendarPageViewModel : AtomViewModel
    {



        #region Property StartDate

        private DateTime _StartDate = DateTime.Now.AddYears(-10);

        public DateTime StartDate
        {
            get
            {
                return _StartDate;
            }
            set
            {
                SetProperty(ref _StartDate, value);
            }
        }
        #endregion



        #region Property EndDate

        private DateTime _EndDate = DateTime.Now.AddYears(10);

        public DateTime EndDate
        {
            get
            {
                return _EndDate;
            }
            set
            {
                SetProperty(ref _EndDate, value);
            }
        }
        #endregion





    }
}
