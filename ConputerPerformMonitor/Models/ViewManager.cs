using ConputerPerformMonitor.ViewModels;
using ConputerPerformMonitor.Views;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConputerPerformMonitor.Model
{
    public class ViewManager
    {
        static Dictionary<Type,object> View = new();
        public ViewManager()
        {
            SetCurrentView(typeof(HomeView));


        }
        public static void SetCurrentView(Type type)
        {
            if (!View.ContainsKey(type)&&type != null) 
            {
                View.Add(type, Activator.CreateInstance(type));
            }

            MainWindow.vm.SetView(View[type]);
        }
        
        
    }
}