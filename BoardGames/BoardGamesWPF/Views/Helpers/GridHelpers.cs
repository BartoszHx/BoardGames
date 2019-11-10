using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace BoardGamesWPF.Views.Helpers
{
    public class GridHelpers
    {
        #region RowAutoCount Property

        public static readonly DependencyProperty RowAutoCountProperty =
        DependencyProperty.RegisterAttached(
            "RowAutoCount", typeof(int), typeof(GridHelpers),
            new PropertyMetadata(-1, RowAutoCountChanged));

        // Get
        public static int GetRowAutoCount(DependencyObject obj)
        {
            return (int)obj.GetValue(RowAutoCountProperty);
        }

        // Set
        public static void SetRowAutoCount(DependencyObject obj, int value)
        {
            obj.SetValue(RowAutoCountProperty, value);
        }

        // Change Event - Adds the Rows
        public static void RowAutoCountChanged(
            DependencyObject obj, DependencyPropertyChangedEventArgs e)
        {
            if (!(obj is Grid) || (int)e.NewValue < 0)
                return;

            Grid grid = (Grid)obj;
            grid.RowDefinitions.Clear();

            for (int i = 0; i < (int)e.NewValue; i++)
                grid.RowDefinitions.Add(
                    new RowDefinition() { Height = GridLength.Auto });
        }

        #endregion

        #region ColumnAutoCount Property

        public static readonly DependencyProperty ColumnAutoCountProperty =
            DependencyProperty.RegisterAttached(
                "ColumnAutoCount", typeof(int), typeof(GridHelpers),
                new PropertyMetadata(-1, ColumnAutoCountChanged));

        // Get
        public static int GetColumnAutoCount(DependencyObject obj)
        {
            return (int)obj.GetValue(ColumnAutoCountProperty);
        }

        // Set
        public static void SetColumnAutoCount(DependencyObject obj, int value)
        {
            obj.SetValue(ColumnAutoCountProperty, value);
        }

        public static void ColumnAutoCountChanged(
            DependencyObject obj, DependencyPropertyChangedEventArgs e)
        {
            if (!(obj is Grid) || (int)e.NewValue < 0)
                return;

            Grid grid = (Grid)obj;
            grid.ColumnDefinitions.Clear();

            for (int i = 0; i < (int)e.NewValue; i++)
                grid.ColumnDefinitions.Add(
                    new ColumnDefinition() { Width = GridLength.Auto });

        }
        #endregion
    }
}
