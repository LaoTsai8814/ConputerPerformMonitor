using ConputerPerformMonitor.ViewModels;
using ConputerPerformMonitor.Views.Tabs;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConputerPerformMonitor.Models
{
    public class TabManager
    {
        static ConcurrentDictionary<Type, object> Tabs = new();
        public TabManager()
        {

        }

        public static object GetTab(Type type)
        {
            if (Tabs.ContainsKey(type))
            {
                return Tabs[type];
            }
            else
            {
                try
                {
                    Tabs.TryAdd(type, Activator.CreateInstance(type));

                    return Tabs[type];
                }
                catch
                {
                    return null;
                }

            }
            HomeVM.UpdateTab?.Invoke(type);
            //throw new NotImplementedException();
        }
        public static void MaunallyAddTab(Type type, object tab)
        {
            if (Tabs.ContainsKey(type))
            {
                Tabs.TryUpdate(type, tab, Tabs[type]);
                HomeVM.UpdateTab?.Invoke(type);
                
            }
            else
            {
                try
                {
                    Tabs.TryAdd(type, tab);
                    HomeVM.UpdateTab?.Invoke(type);
                    
                }
                catch
                {
                    
                }

            }
            

        }
    }
}
