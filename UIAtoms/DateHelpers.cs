using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using Xamarin.Forms;

namespace NeuroSpeech.UIAtoms
{
    /// <summary>
    /// 
    /// </summary>
    public static class DateHelpers
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="date"></param>
        /// <returns></returns>
        public static long MillSecondsSince1970(this DateTime date)
        {
            return (long)(date - new DateTime(1970, 1, 1)).TotalMilliseconds;
        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static DateTime ToDate(this long value)
        {
            var epoch = new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc);
            return epoch.AddMilliseconds(value);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="date"></param>
        /// <returns></returns>
        public static string DisplayDay(this DateTime date) {
            return date.ToString("dd MMM yyyy");
        }
    }

    /// <summary>
    /// 
    /// </summary>
    public static class NumericExHelper {


        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="val"></param>
        /// <param name="min"></param>
        /// <param name="max"></param>
        /// <returns></returns>
        public static T Clamp<T>(this T val, T min, T max) where T : IComparable<T>
        {
            if (val.CompareTo(min) < 0) return min;
            else if (val.CompareTo(max) > 0) return max;
            else return val;
        }
    }

    /// <summary>
    /// 
    /// </summary>
    public static class ViewHelper {


        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="view"></param>
        /// <returns></returns>
        public static T GetParentOfType<T>(this Element view)
            where T:Element
        {
            Type type = typeof(T);
            while (view != null)
            {

                Element p = view.Parent;
                if (p == null)
                    break;
                if (type.IsAssignableFrom(p.GetType())){
                    return (T)(p);
                }
                view = p;
            }
            return null;
        }

    }


    /// <summary>
    /// 
    /// </summary>
    public static class GridHelper {


        /// <summary>
        /// Adds new row with specified gridlength, note this will add new row to row definitions, please note you must add columns before calling this
        /// </summary>
        /// <param name="grid"></param>
        /// <param name="height"></param>
        /// <param name="views"></param>
        public static void AddRowItems (this Grid grid, GridLength? height = null, params View[] views){
            var row = new RowDefinition { };
            if (height != null)
            {
                row.Height = height.Value;
            }
            int index = grid.RowDefinitions.Count;
            grid.RowDefinitions.Add(row);

            for (int i = 0; i < views.Length; i++)
            {
                var item = views[i];
                Grid.SetRow(item, index);
                Grid.SetColumn(item, i);
                grid.Children.Add(item);
            }
        }

        /// <summary>
        /// Adds single item at new row, new row will be added to Grid
        /// </summary>
        /// <param name="grid"></param>
        /// <param name="item"></param>
        /// <param name="height"></param>
        /// <param name="column">default is -1, it will add item with colspan equal to number of columns</param>
        public static void AddRowItem(
            this Grid grid, 
            View item, 
            GridLength? height = null,
            int column = -1
            )
        {
            var row = new RowDefinition { };
            if (height != null) {
                row.Height = height.Value;
            }
            int index = grid.RowDefinitions.Count;
            grid.RowDefinitions.Add(row);
            Grid.SetRow(item, index);
            if (column == -1)
            {
                Grid.SetColumnSpan(item, grid.ColumnDefinitions.Count);
            }
            else {
                Grid.SetColumn(item, column);
            }
            grid.Children.Add(item);
        }

    }


}
