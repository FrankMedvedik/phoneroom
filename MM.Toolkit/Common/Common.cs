using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Media;

namespace MM.Toolkit
{
    public static class Common
    {
        public static Boolean FindVisualChild(DependencyObject control, UIElement findcontrol)
        {
            Boolean retVal = false;

            if ((control == null) || (findcontrol == null))
                return false;

            if (control == findcontrol)
                return true;

            if (control is Popup)
            {
                Popup popup = control as Popup;

                if ((popup != null) && (popup.Child != null))
                    control = popup.Child;
            }

            //foreach (DependencyObject child in control.GetVisualChildren())
            for (Int32 idx = 0; idx < VisualTreeHelper.GetChildrenCount(control); idx++)
            {
                DependencyObject child = VisualTreeHelper.GetChild(control, idx);

                if (control == findcontrol)
                    retVal = true;
                else
                {
                    if ((findcontrol is ListBoxItem) && (child is ListBox))
                    {
                        ListBox listbox = child as ListBox;

                        foreach (Object item in listbox.Items)
                        {
                            if (findcontrol == listbox.ItemContainerGenerator.ContainerFromItem(item))
                            {
                                retVal = true;
                                break;
                            }
                        }

                        listbox = null;
                    }

                    if (!retVal)
                        retVal = FindVisualChild(child, findcontrol);
                }

                if (retVal)
                    break;
            }

            return retVal;
        }

        public static IEnumerable<DependencyObject> GetAllVisualChildren(this DependencyObject parent)
        {
            return parent.GetAllVisualChildren<DependencyObject>();
        }
        public static IEnumerable<DependencyObject> GetAllVisualChildren<T>(this DependencyObject parent) where T : DependencyObject //, new()
        {
            List<DependencyObject> retVal = null;
            DependencyObject childobj = null;

            retVal = new List<DependencyObject>();

            if (parent == null)
                return retVal;

            for (Int32 i = 0; i < VisualTreeHelper.GetChildrenCount(parent); i++)
            {
                childobj = VisualTreeHelper.GetChild(parent, i);

                if (childobj is T)      // if (childobj.GetType().Equals(typeof(T)))
                    retVal.Add(childobj);

                retVal.AddRange(childobj.GetAllVisualChildren<T>());
            }

            return retVal;
        }
    }
}
