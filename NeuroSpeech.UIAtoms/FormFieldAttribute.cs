using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace NeuroSpeech.UIAtoms
{

    public abstract class FormFieldAttribute : Attribute {
        public string Label { get; set; }

        public string Description { get; set; }

        public string Category { get; set; }

        public string Keywords { get; set; }

        protected abstract View CreateView(object value, string propertyName, PropertyInfo p);

        public View CreateView(object value, PropertyInfo p) {
            Grid grid = new Grid();
            grid.BindingContext = value;
            grid.RowDefinitions.Add(new RowDefinition { Height = GridLength.Auto });
            grid.RowDefinitions.Add(new RowDefinition { });

            Label label = new Xamarin.Forms.Label();
            label.Text = string.IsNullOrWhiteSpace(Label) ? p.Name : Label;
            grid.Children.Add(label);

            View view = CreateView(value, "Value." + p.Name , p);
            Grid.SetRow(view, 1);
            grid.Children.Add(view);

            if (!string.IsNullOrWhiteSpace(Description)) {
                grid.RowDefinitions.Add(new RowDefinition { Height = GridLength.Auto });
                label = new Xamarin.Forms.Label();
                label.Text = Description;
                Grid.SetRow(label, 2);
                grid.Children.Add(label);
            }


            return grid;
        }

        internal virtual bool HasText(string value) {
            if (Keywords.HasText(value) || Label.HasText(value) || Description.HasText(value) || Category.HasText(value))
                return true;
            return false;
        }
    }

    [System.AttributeUsage(AttributeTargets.Property, Inherited = true, AllowMultiple = false)]
    public class TextFieldAttribute : FormFieldAttribute
    {

        public string Placeholder { get; set; }

        internal override bool HasText(string value)
        {
            if (Placeholder.HasText(value))
                return true;
            return base.HasText(value);
        }

        protected override View CreateView(object value, string propertyName, PropertyInfo p)
        {
            Entry cell = new Entry();
            cell.BindingContext = value;
            cell.Placeholder = Placeholder;
            cell.SetBinding(Entry.TextProperty, new Binding { Path = propertyName });
            return cell;
        }

    }

    [System.AttributeUsage(AttributeTargets.Property, Inherited = true, AllowMultiple = false)]
    public class DateTimeFieldAttribute : FormFieldAttribute
    {

        protected override View CreateView(object value, string propertyName, PropertyInfo p)
        {
            DatePicker cell = new DatePicker();
            cell.BindingContext = value;
            cell.SetBinding(DatePicker.DateProperty, new Binding { Path = propertyName });
            return cell;
        }

    }

    [System.AttributeUsage(AttributeTargets.Property, Inherited = true, AllowMultiple = false)]
    public class BirthDateFieldAttribute : FormFieldAttribute
    {

        protected override View CreateView(object value, string propertyName, PropertyInfo p)
        {
            DatePicker cell = new DatePicker();
            cell.BindingContext = value;
            cell.MaximumDate = DateTime.Today;
            cell.MinimumDate = DateTime.Today.AddYears(-100);
            cell.SetBinding(DatePicker.DateProperty, new Binding { Path = propertyName });
            return cell;
        }

    }

    [System.AttributeUsage(AttributeTargets.Property, Inherited = true, AllowMultiple = false)]
    public class ExpiryDateFieldAttribute : FormFieldAttribute
    {

        public int MaxDays { get; set; } = 365;

        protected override View CreateView(object value, string propertyName, PropertyInfo p)
        {
            DatePicker cell = new DatePicker();
            cell.BindingContext = value;
            cell.MinimumDate = DateTime.Today;
            cell.MaximumDate = DateTime.Today.AddDays(MaxDays);
            cell.SetBinding(DatePicker.DateProperty, new Binding { Path = propertyName });
            return cell;
        }

    }

    [System.AttributeUsage(AttributeTargets.Property, Inherited = true, AllowMultiple = false)]
    public class SwitchFieldAttribute : FormFieldAttribute
    {

        protected override View CreateView(object value, string propertyName, PropertyInfo p)
        {
            Switch cell = new Switch();
            cell.BindingContext = value;
            cell.SetBinding(Switch.IsToggledProperty, new Binding { Path = propertyName  });
            return cell;
        }

    }
}
