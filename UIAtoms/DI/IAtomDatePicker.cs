using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace NeuroSpeech.UIAtoms.DI
{
    public interface IAtomDatePicker
    {

        Task<DateTime?> PickDateAsync(DateTime? selectedDate, DateTime? startDate, DateTime? endDate, string dateFormat);

    }
}
