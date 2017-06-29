using System;
using System.Reflection;

namespace NeuroSpeech.UIAtoms
{
    public class PropertyBinding
    {

        public PropertyBinding(FormFieldAttribute a, object value, PropertyInfo p)
        {
            this.FormField = a;
            this.Value = value;
            this.Property = p;
        }

        public FormFieldAttribute FormField { get; private set; }
        public PropertyInfo Property { get; private set; }
        public object Value { get; private set; }
    }
}