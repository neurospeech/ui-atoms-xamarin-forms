using System;
using System.Linq;
using System.Collections.Generic;
using System.Collections;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.Globalization;
using System.Runtime.CompilerServices;
using System.Text;
using Xamarin.Forms;
using NeuroSpeech.UIAtoms.Validation;

namespace NeuroSpeech.UIAtoms.Controls
{

    ///// <summary>
    ///// 
    ///// </summary>
    //public class AtomField : Element {

    //    #region Property Category

    //    /// <summary>
    //    /// Bindable Property Category
    //    /// </summary>
    //    public static readonly BindableProperty CategoryProperty = BindableProperty.Create(
    //      "Category",
    //      typeof(string),
    //      typeof(AtomField),
    //      null,
    //      BindingMode.OneWay,
    //      // validate value delegate
    //      // (sender,value) => true
    //      null,
    //      // property changed, delegate
    //      // (sender,oldValue,newValue) => {}
    //      null,
    //      // property changing delegate
    //      // (sender,oldValue,newValue) => {}
    //      null,
    //      // coerce value delegate 
    //      // (sender,value) => value
    //      null,
    //      // create default value delegate
    //      // () => Default(T)
    //      null
    //    );


    //    /*
    //    /// <summary>
    //    /// On Category changed
    //    /// </summary>
    //    /// <param name="oldValue">Old Value</param>
    //    /// <param name="newValue">New Value</param>
    //    protected virtual void OnCategoryChanged(object oldValue, object newValue)
    //    {
            
    //    }*/


    //    /// <summary>
    //    /// Property Category
    //    /// </summary>
    //    public string Category
    //    {
    //        get
    //        {
    //            return (string)GetValue(CategoryProperty);
    //        }
    //        set
    //        {
    //            SetValue(CategoryProperty, value);
    //        }
    //    }
    //    #endregion

    //    #region Property Keywords

    //    /// <summary>
    //    /// Bindable Property Keywords
    //    /// </summary>
    //    public static readonly BindableProperty KeywordsProperty = BindableProperty.Create(
    //      "Keywords",
    //      typeof(string),
    //      typeof(AtomField),
    //      null,
    //      BindingMode.OneWay,
    //      // validate value delegate
    //      // (sender,value) => true
    //      null,
    //      // property changed, delegate
    //      // (sender,oldValue,newValue) => {}
    //      null,
    //      // property changing delegate
    //      // (sender,oldValue,newValue) => {}
    //      null,
    //      // coerce value delegate 
    //      // (sender,value) => value
    //      null,
    //      // create default value delegate
    //      // () => Default(T)
    //      null
    //    );

    //    /*
    //    /// <summary>
    //    /// On Keywords changed
    //    /// </summary>
    //    /// <param name="oldValue">Old Value</param>
    //    /// <param name="newValue">New Value</param>
    //    protected virtual void OnKeywordsChanged(object oldValue, object newValue)
    //    {
            
    //    }*/


    //    /// <summary>
    //    /// Property Keywords
    //    /// </summary>
    //    public string Keywords
    //    {
    //        get
    //        {
    //            return (string)GetValue(KeywordsProperty);
    //        }
    //        set
    //        {
    //            SetValue(KeywordsProperty, value);
    //        }
    //    }
    //    #endregion

    //    #region Property CategoryKeywords

    //    /// <summary>
    //    /// Bindable Property CategoryKeywords
    //    /// </summary>
    //    public static readonly BindableProperty CategoryKeywordsProperty = BindableProperty.Create(
    //      "CategoryKeywords",
    //      typeof(string),
    //      typeof(AtomField),
    //      null,
    //      BindingMode.OneWay,
    //      // validate value delegate
    //      // (sender,value) => true
    //      null,
    //      // property changed, delegate
    //      // (sender,oldValue,newValue) => {}
    //      null,
    //      // property changing delegate
    //      // (sender,oldValue,newValue) => {}
    //      null,
    //      // coerce value delegate 
    //      // (sender,value) => value
    //      null,
    //      // create default value delegate
    //      // () => Default(T)
    //      null
    //    );

    //    /*
    //    /// <summary>
    //    /// On CategoryKeywords changed
    //    /// </summary>
    //    /// <param name="oldValue">Old Value</param>
    //    /// <param name="newValue">New Value</param>
    //    protected virtual void OnCategoryKeywordsChanged(object oldValue, object newValue)
    //    {
            
    //    }*/


    //    /// <summary>
    //    /// Property CategoryKeywords
    //    /// </summary>
    //    public string CategoryKeywords
    //    {
    //        get
    //        {
    //            return (string)GetValue(CategoryKeywordsProperty);
    //        }
    //        set
    //        {
    //            SetValue(CategoryKeywordsProperty, value);
    //        }
    //    }
    //    #endregion

    //    #region Property Label

    //    /// <summary>
    //    /// Bindable Property Label
    //    /// </summary>
    //    public static readonly BindableProperty LabelProperty = BindableProperty.Create(
    //      "Label",
    //      typeof(string),
    //      typeof(AtomField),
    //      null,
    //      BindingMode.OneWay,
    //      // validate value delegate
    //      // (sender,value) => true
    //      null,
    //      // property changed, delegate
    //      // (sender,oldValue,newValue) => {}
    //      null,
    //      // property changing delegate
    //      // (sender,oldValue,newValue) => {}
    //      null,
    //      // coerce value delegate 
    //      // (sender,value) => value
    //      null,
    //      // create default value delegate
    //      // () => Default(T)
    //      null
    //    );

    //    /*
    //    /// <summary>
    //    /// On Label changed
    //    /// </summary>
    //    /// <param name="oldValue">Old Value</param>
    //    /// <param name="newValue">New Value</param>
    //    protected virtual void OnLabelChanged(object oldValue, object newValue)
    //    {
            
    //    }*/


    //    /// <summary>
    //    /// Property Label
    //    /// </summary>
    //    public string Label
    //    {
    //        get
    //        {
    //            return (string)GetValue(LabelProperty);
    //        }
    //        set
    //        {
    //            SetValue(LabelProperty, value);
    //        }
    //    }
    //    #endregion

    //    #region Property LabelColor

    //    /// <summary>
    //    /// Bindable Property LabelColor
    //    /// </summary>
    //    public static readonly BindableProperty LabelColorProperty = BindableProperty.Create(
    //      "LabelColor",
    //      typeof(Color),
    //      typeof(AtomField),
    //      Color.Default,
    //      BindingMode.OneWay,
    //      // validate value delegate
    //      // (sender,value) => true
    //      null,
    //      // property changed, delegate
    //      // (sender,oldValue,newValue) => {}
    //      null,
    //      // property changing delegate
    //      // (sender,oldValue,newValue) => {}
    //      null,
    //      // coerce value delegate 
    //      // (sender,value) => value
    //      null,
    //      // create default value delegate
    //      // () => Default(T)
    //      null
    //    );

    //    /*
    //    /// <summary>
    //    /// On LabelColor changed
    //    /// </summary>
    //    /// <param name="oldValue">Old Value</param>
    //    /// <param name="newValue">New Value</param>
    //    protected virtual void OnLabelColorChanged(object oldValue, object newValue)
    //    {
            
    //    }*/


    //    /// <summary>
    //    /// Property LabelColor
    //    /// </summary>
    //    public Color LabelColor
    //    {
    //        get
    //        {
    //            return (Color)GetValue(LabelColorProperty);
    //        }
    //        set
    //        {
    //            SetValue(LabelColorProperty, value);
    //        }
    //    }
    //    #endregion

    //    #region Property ErrorColor

    //    /// <summary>
    //    /// Bindable Property ErrorColor
    //    /// </summary>
    //    public static readonly BindableProperty ErrorColorProperty = BindableProperty.Create(
    //      "ErrorColor",
    //      typeof(Color),
    //      typeof(AtomField),
    //      Color.White,
    //      BindingMode.OneWay,
    //      // validate value delegate
    //      // (sender,value) => true
    //      null,
    //      // property changed, delegate
    //      //(sender,oldValue,newValue) => ((AtomField)sender).OnErrorColorChanged(oldValue,newValue),
    //      null,
    //      // property changing delegate
    //      // (sender,oldValue,newValue) => {}
    //      null,
    //      // coerce value delegate 
    //      // (sender,value) => value
    //      null,
    //      // create default value delegate
    //      // () => Default(T)
    //      null
    //    );

    //    /*
    //    /// <summary>
    //    /// On ErrorColor changed
    //    /// </summary>
    //    /// <param name="oldValue">Old Value</param>
    //    /// <param name="newValue">New Value</param>
    //    protected virtual void OnErrorColorChanged(object oldValue, object newValue)
    //    {
            
    //    }*/


    //    /// <summary>
    //    /// Property ErrorColor
    //    /// </summary>
    //    public Color ErrorColor
    //    {
    //        get
    //        {
    //            return (Color)GetValue(ErrorColorProperty);
    //        }
    //        set
    //        {
    //            SetValue(ErrorColorProperty, value);
    //        }
    //    }
    //    #endregion

    //    #region Property ErrorBackgroundColor

    //    /// <summary>
    //    /// Bindable Property ErrorBackgroundColor
    //    /// </summary>
    //    public static readonly BindableProperty ErrorBackgroundColorProperty = BindableProperty.Create(
    //      "ErrorBackgroundColor",
    //      typeof(Color),
    //      typeof(AtomField),
    //      Color.Red,
    //      BindingMode.OneWay,
    //      // validate value delegate
    //      // (sender,value) => true
    //      null,
    //      // property changed, delegate
    //      //(sender,oldValue,newValue) => ((AtomField)sender).OnErrorBackgroundColorChanged(oldValue,newValue),
    //      null,
    //      // property changing delegate
    //      // (sender,oldValue,newValue) => {}
    //      null,
    //      // coerce value delegate 
    //      // (sender,value) => value
    //      null,
    //      // create default value delegate
    //      // () => Default(T)
    //      null
    //    );

    //    /*
    //    /// <summary>
    //    /// On ErrorBackgroundColor changed
    //    /// </summary>
    //    /// <param name="oldValue">Old Value</param>
    //    /// <param name="newValue">New Value</param>
    //    protected virtual void OnErrorBackgroundColorChanged(object oldValue, object newValue)
    //    {
            
    //    }*/


    //    /// <summary>
    //    /// Property ErrorBackgroundColor
    //    /// </summary>
    //    public Color ErrorBackgroundColor
    //    {
    //        get
    //        {
    //            return (Color)GetValue(ErrorBackgroundColorProperty);
    //        }
    //        set
    //        {
    //            SetValue(ErrorBackgroundColorProperty, value);
    //        }
    //    }
    //    #endregion

    //    #region Property ErrorPadding

    //    /// <summary>
    //    /// Bindable Property ErrorPadding
    //    /// </summary>
    //    public static readonly BindableProperty ErrorPaddingProperty = BindableProperty.Create(
    //      "ErrorPadding",
    //      typeof(Thickness),
    //      typeof(AtomField),
    //      new Thickness(3,3,3,3),
    //      BindingMode.OneWay,
    //      // validate value delegate
    //      // (sender,value) => true
    //      null,
    //      // property changed, delegate
    //      //(sender,oldValue,newValue) => ((AtomField)sender).OnErrorPaddingChanged(oldValue,newValue),
    //      null,
    //      // property changing delegate
    //      // (sender,oldValue,newValue) => {}
    //      null,
    //      // coerce value delegate 
    //      // (sender,value) => value
    //      null,
    //      // create default value delegate
    //      // () => Default(T)
    //      null
    //    );

    //    /*
    //    /// <summary>
    //    /// On ErrorPadding changed
    //    /// </summary>
    //    /// <param name="oldValue">Old Value</param>
    //    /// <param name="newValue">New Value</param>
    //    protected virtual void OnErrorPaddingChanged(object oldValue, object newValue)
    //    {
            
    //    }*/


    //    /// <summary>
    //    /// Property ErrorPadding
    //    /// </summary>
    //    public Thickness ErrorPadding
    //    {
    //        get
    //        {
    //            return (Thickness)GetValue(ErrorPaddingProperty);
    //        }
    //        set
    //        {
    //            SetValue(ErrorPaddingProperty, value);
    //        }
    //    }
    //    #endregion

    //    #region Property Description

    //    /// <summary>
    //    /// Bindable Property Description
    //    /// </summary>
    //    public static readonly BindableProperty DescriptionProperty = BindableProperty.Create(
    //      "Description",
    //      typeof(FormattedString),
    //      typeof(AtomField),
    //      null,
    //      BindingMode.OneWay,
    //      // validate value delegate
    //      // (sender,value) => true
    //      null,
    //      // property changed, delegate
    //      // (sender,oldValue,newValue) => {}
    //      null,
    //      // property changing delegate
    //      // (sender,oldValue,newValue) => {}
    //      null,
    //      // coerce value delegate 
    //      // (sender,value) => value
    //      null,
    //      // create default value delegate
    //      // () => Default(T)
    //      null
    //    );

    //    /*
    //    /// <summary>
    //    /// On Description changed
    //    /// </summary>
    //    /// <param name="oldValue">Old Value</param>
    //    /// <param name="newValue">New Value</param>
    //    protected virtual void OnDescriptionChanged(object oldValue, object newValue)
    //    {
            
    //    }*/


    //    /// <summary>
    //    /// Property Description
    //    /// </summary>
    //    public FormattedString Description
    //    {
    //        get
    //        {
    //            return (FormattedString)GetValue(DescriptionProperty);
    //        }
    //        set
    //        {
    //            SetValue(DescriptionProperty, value);
    //        }
    //    }
    //    #endregion

    //    internal AtomForm Form;

    //    #region Property Error

    //    /// <summary>
    //    /// Bindable Property Error
    //    /// </summary>
    //    public static readonly BindableProperty ErrorProperty = BindableProperty.Create(
    //      "Error",
    //      typeof(string),
    //      typeof(AtomField),
    //      null,
    //      BindingMode.OneWay,
    //      // validate value delegate
    //      // (sender,value) => true
    //      null,
    //      // property changed, delegate
    //       (sender,oldValue,newValue) => ((AtomField)sender).OnErrorChanged(oldValue,newValue),
    //      //null,
    //      // property changing delegate
    //      // (sender,oldValue,newValue) => {}
    //      null,
    //      // coerce value delegate 
    //      // (sender,value) => value
    //      null,
    //      // create default value delegate
    //      // () => Default(T)
    //      null
    //    );

        
    //    /// <summary>
    //    /// On Error changed
    //    /// </summary>
    //    /// <param name="oldValue">Old Value</param>
    //    /// <param name="newValue">New Value</param>
    //    protected virtual void OnErrorChanged(object oldValue, object newValue)
    //    {
    //        //Form?.UpdateIsValid();   
    //    }


    //    /// <summary>
    //    /// Property Error
    //    /// </summary>
    //    public string Error
    //    {
    //        get
    //        {
    //            return (string)GetValue(ErrorProperty);
    //        }
    //        set
    //        {
    //            SetValue(ErrorProperty, value);
    //        }
    //    }
    //    #endregion

    //    #region Property Warning

    //    /// <summary>
    //    /// Bindable Property Warning
    //    /// </summary>
    //    public static readonly BindableProperty WarningProperty = BindableProperty.Create(
    //      "Warning",
    //      typeof(string),
    //      typeof(AtomField),
    //      null,
    //      BindingMode.OneWay,
    //      // validate value delegate
    //      // (sender,value) => true
    //      null,
    //      // property changed, delegate
    //      // (sender,oldValue,newValue) => {}
    //      null,
    //      // property changing delegate
    //      // (sender,oldValue,newValue) => {}
    //      null,
    //      // coerce value delegate 
    //      // (sender,value) => value
    //      null,
    //      // create default value delegate
    //      // () => Default(T)
    //      null
    //    );

    //    /*
    //    /// <summary>
    //    /// On Warning changed
    //    /// </summary>
    //    /// <param name="oldValue">Old Value</param>
    //    /// <param name="newValue">New Value</param>
    //    protected virtual void OnWarningChanged(object oldValue, object newValue)
    //    {
            
    //    }*/


    //    /// <summary>
    //    /// Property Warning
    //    /// </summary>
    //    public string Warning
    //    {
    //        get
    //        {
    //            return (string)GetValue(WarningProperty);
    //        }
    //        set
    //        {
    //            SetValue(WarningProperty, value);
    //        }
    //    }
    //    #endregion

    //    #region Property IsRequired

    //    /// <summary>
    //    /// Bindable Property IsRequired
    //    /// </summary>
    //    public static readonly BindableProperty IsRequiredProperty = BindableProperty.Create(
    //      "IsRequired",
    //      typeof(bool),
    //      typeof(AtomField),
    //      false,
    //      BindingMode.OneWay,
    //      // validate value delegate
    //      // (sender,value) => true
    //      null,
    //      // property changed, delegate
    //      // (sender,oldValue,newValue) => {}
    //      null,
    //      // property changing delegate
    //      // (sender,oldValue,newValue) => {}
    //      null,
    //      // coerce value delegate 
    //      // (sender,value) => value
    //      null,
    //      // create default value delegate
    //      // () => Default(T)
    //      null
    //    );
    //    internal readonly View Content;

    //    /*
    //    /// <summary>
    //    /// On IsRequired changed
    //    /// </summary>
    //    /// <param name="oldValue">Old Value</param>
    //    /// <param name="newValue">New Value</param>
    //    protected virtual void OnIsRequiredChanged(object oldValue, object newValue)
    //    {
            
    //    }*/


    //    /// <summary>
    //    /// Property IsRequired
    //    /// </summary>
    //    public bool IsRequired
    //    {
    //        get
    //        {
    //            return (bool)GetValue(IsRequiredProperty);
    //        }
    //        set
    //        {
    //            SetValue(IsRequiredProperty, value);
    //        }
    //    }
    //    #endregion



    //    #region Field Attached Property
    //    /// <summary>
    //    /// Field Attached property
    //    /// </summary>
    //    public static readonly BindableProperty FieldProperty =
    //        BindableProperty.CreateAttached("Field", typeof(AtomField),
    //        typeof(AtomField),
    //        null,
    //        BindingMode.OneWay,
    //        null,
    //        OnFieldChanged);

    //    private static void OnFieldChanged(BindableObject bindable, object oldValue, object newValue)
    //    {

    //    }

    //    /// <summary>
    //    /// Set Field for bindable object
    //    /// </summary>
    //    /// <param name="bindable"></param>
    //    /// <param name="newValue"></param>
    //    public static void SetField(BindableObject bindable, AtomField newValue)
    //    {
    //        bindable.SetValue(FieldProperty, newValue);
    //    }

    //    /// <summary>
    //    /// Get Field for bindable object
    //    /// </summary>
    //    /// <param name="bindable"></param>
    //    /// <returns></returns>
    //    public static AtomField GetField(BindableObject bindable)
    //    {
    //        return (AtomField)bindable.GetValue(FieldProperty);
    //    }
    //    #endregion




    //    static AtomField()
    //    {

            

    //    }


    //    /// <summary>
    //    /// 
    //    /// </summary>
    //    public AtomField(Element content)
    //    {
    //        this.Content = (View)content;
    //        content.PropertyChanged += View_PropertyChanged;
    //        SetField(content, this);
    //    }

        
    //    public AtomValidationError Validate() {
    //        return AtomValidationRule.Validate(Content);
    //    }

    //    protected override void OnPropertyChanging([CallerMemberName] string propertyName = null)
    //    {
    //        base.OnPropertyChanging(propertyName);

    //        if (!EnableUpdateSize)
    //            return;

    //        switch (propertyName) {
    //            case nameof(Error):
    //            case nameof(Description):
    //            case nameof(Label):
    //                UpdateCell();
    //                break;
                        
    //        }
    //    }

    //    private void View_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
    //    {
    //        if (e.PropertyName == "IsVisible") {
    //            if (!EnableUpdateSize)
    //                return;
    //            Form?.UpdateItems();
    //        }
    //    }


    //    /*protected override void OnChildMeasureInvalidated()
    //    {
    //        base.OnChildMeasureInvalidated();
    //        if (!EnableUpdateSize)
    //            return;
    //        UpdateCell();
    //    }*/

    //    public bool EnableUpdateSize { get; set; } = false;

    //    private void UpdateCell()
    //    {
    //        ViewCell cell = Content.GetParentOfType<ViewCell>();
    //        if (cell != null)
    //        {
    //            UIAtomsApplication.Instance.TriggerOnce(() =>
    //            {
    //                cell.ForceUpdateSize();

    //            });
    //        }
    //    }

    //}


    //public class GroupedFieldList : List<AtomField>, IGrouping<string, AtomField>
    //{
    //    public string Key
    //    {
    //        get;set;
    //    }
    //}

}