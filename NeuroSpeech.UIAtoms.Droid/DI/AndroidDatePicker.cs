using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using NeuroSpeech.UIAtoms.DI;
using Xamarin.Forms;

[assembly: Dependency(typeof(AndroidDatePicker))]

namespace NeuroSpeech.UIAtoms.DI
{
    public class AndroidDatePicker : IAtomDatePicker
    {
        public Task<DateTime?> PickDateAsync(DateTime? selectedDate, DateTime? startDate, DateTime? endDate, string dateFormat)
        {

            TaskCompletionSource<DateTime?> src = new TaskCompletionSource<DateTime?>();

            DateTime se = selectedDate == null ? DateTime.Now : selectedDate.Value;

            var dp = new DatePickerDialog(Xamarin.Forms.Forms.Context, (s, e) => {



            }, se.Year, se.Month, se.Day);

            dp.CancelEvent += (s, e) => {
                src.TrySetCanceled();
            };

            


            return src.Task;
        }
    }


}