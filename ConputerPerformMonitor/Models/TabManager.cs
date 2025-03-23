using ConputerPerformMonitor.ViewModels;
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
        static ConcurrentDictionary<Type,object> Tabs = new();
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
            //throw new NotImplementedException();
        }
        
    }
}
