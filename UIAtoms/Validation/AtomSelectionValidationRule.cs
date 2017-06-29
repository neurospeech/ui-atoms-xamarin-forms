using NeuroSpeech.UIAtoms.Controls;
using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace NeuroSpeech.UIAtoms.Validation
{
    public class AtomSelectionValidationRule : AtomValidationRule
    {



        public override AtomValidationError Validate(View view, BindableProperty property, object value)
        {
            if (AtomForm.GetIsRequired(view))
            {
                if (value == null)
                    return new AtomValidationError
                    {
                        Source = view,
                        Property = property,
                        Message = AtomForm.GetMissingValueMessage(view)
                    };
            }
            return null;
        }
    }

    public class AtomDateValidationRule : AtomValidationRule {
        public override AtomValidationError Validate(View view, BindableProperty property, object value)
        {
            if (AtomForm.GetIsRequired(view))
            {
                var dt = DateTime.MinValue;
                if (value != null) {
                    dt = Convert.ToDateTime(value);
                }
                if (dt == DateTime.MinValue)
                    return new AtomValidationError
                    {
                        Source = view,
                        Property = property,
                        Message = AtomForm.GetMissingValueMessage(view)
                    };
            }
            return null;
        }
    }
}
