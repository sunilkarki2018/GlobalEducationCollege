using System;
using System.Linq;
using System.Reflection;
using GlobalCollege.Entity;


namespace GlobalCollege.Infrastructure
{
    public static class MapperHelper
    {
        public static T1 GetEntityForAutoAuthoriser<T1, T2>(T1 destination, T2 source, bool ForUpdate)
        {
            try
            {
                var ModuleSetup = ModuleHelper.GetModuleSetup<T1>();

                PropertyInfo[] sourceProprties = typeof(T2).GetProperties(BindingFlags.Instance | BindingFlags.Public);
                PropertyInfo[] targetProprties = typeof(T1).GetProperties(BindingFlags.Instance | BindingFlags.Public);

                foreach (var sourceProp in sourceProprties)
                {
                    if (ModuleSetup.ModuleBussinesLogicSetups.Any(c => c.ColumnName == sourceProp.Name && c.CanUpdate) || !ForUpdate)
                    {
                        object osourceVal = sourceProp.GetValue(source, null);
                        var targetProp = targetProprties.Where(t => t.Name == sourceProp.Name).FirstOrDefault();
                        if (targetProp != null)
                        {
                            targetProp.SetValue(destination, osourceVal);
                        }
                    }
                }



                return destination;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public static T1 Get<T1, T2>(T1 destination, T2 source, CurrentAction currentAction)
        {
            try
            {
                var ModuleSetup = ModuleHelper.GetModuleSetup<T1>();

                PropertyInfo[] sourceProprties = typeof(T2).GetProperties(BindingFlags.Instance | BindingFlags.Public);
                PropertyInfo[] targetProprties = typeof(T1).GetProperties(BindingFlags.Instance | BindingFlags.Public);

                foreach (var sourceProp in sourceProprties)
                {
                    if (ModuleSetup != null)
                    {
                        if (ModuleSetup.ModuleBussinesLogicSetups.Any(c => c.ColumnName == sourceProp.Name && c.CanUpdate && currentAction == CurrentAction.Edit) || ModuleSetup.ModuleBussinesLogicSetups.Any(c => c.ColumnName == sourceProp.Name && currentAction == CurrentAction.Create))
                        {
                            object osourceVal = sourceProp.GetValue(source, null);
                            var targetProp = targetProprties.Where(t => t.Name == sourceProp.Name).FirstOrDefault();
                            if (targetProp != null)
                            {
                                targetProp.SetValue(destination, osourceVal);
                            }
                        }
                    }
                    else
                    {
                        object osourceVal = sourceProp.GetValue(source, null);
                        var targetProp = targetProprties.Where(t => t.Name == sourceProp.Name).FirstOrDefault();
                        if (targetProp != null)
                        {
                            targetProp.SetValue(destination, osourceVal);
                        }
                    }
                }

                return destination;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public static T1 Get<T1, T2>(T1 destination, T2 source)
        {
            try
            {
                PropertyInfo[] sourceProprties = typeof(T2).GetProperties(BindingFlags.Instance | BindingFlags.Public);
                PropertyInfo[] targetProprties = typeof(T1).GetProperties(BindingFlags.Instance | BindingFlags.Public);

                foreach (var sourceProp in sourceProprties)
                {

                    object osourceVal = sourceProp.GetValue(source, null);
                    var targetProp = targetProprties.Where(t => t.Name == sourceProp.Name).FirstOrDefault();
                    if (targetProp != null)
                    {
                        targetProp.SetValue(destination, osourceVal);
                    }

                }

                return destination;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

    }
}
