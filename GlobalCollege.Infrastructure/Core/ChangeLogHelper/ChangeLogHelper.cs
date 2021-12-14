using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using GlobalCollege.Entity;

namespace GlobalCollege.Infrastructure
{
    public static class ChangeLogHelper
    {
        public static string CreateChangeLog<O1, O2>(O1 Object1, O2 Object2, string ChangeLog)
        {
            try
            {
                ChangeLogList changeLogList = !string.IsNullOrEmpty(ChangeLog) ? XMLConverter.Deserialize<ChangeLogList>(ChangeLog) : new ChangeLogList();

                if (changeLogList == null)
                    changeLogList = new ChangeLogList();

                var ModuleSetupInformation = ModuleHelper.GetModuleSetup<O1>();

                if (ModuleSetupInformation != null)
                {
                    PropertyInfo[] sourceProprties = typeof(O2).GetProperties(BindingFlags.Instance | BindingFlags.Public);
                    PropertyInfo[] targetProprties = typeof(O1).GetProperties(BindingFlags.Instance | BindingFlags.Public);

                    RecordChangeLog recordChangeLogs = new RecordChangeLog()
                    {
                        ChangeDate = DateTime.Now,
                        ModificationNumber = changeLogList != null && changeLogList.ChangeLogs.Count() > 0 ? changeLogList.ChangeLogs.Max(m => m.ModificationNumber) + 1 : 1,
                        ChangeStatus = RecordStatus.Unauthorized
                    };

                    foreach (var businessProperty in ModuleSetupInformation.ModuleBussinesLogicSetups)
                    {

                        var sourceProp = sourceProprties.Where(x => x.Name == businessProperty.ColumnName).FirstOrDefault();

                        object o2Val = sourceProp.GetValue(Object2, null);

                        var targetProp = targetProprties.Where(x => x.Name == businessProperty.ColumnName).FirstOrDefault();

                        object o1Val = targetProp.GetValue(Object1, null);

                        switch (businessProperty.DataType.ToLower())
                        {
                            case "guid":
                                Guid? guid1 = o1Val != null ? (Guid?)Convert.ChangeType(o1Val, typeof(Guid)) : null;
                                Guid? guid2 = o2Val != null ? (Guid?)Convert.ChangeType(o2Val, typeof(Guid)) : null;

                                if (guid1 != guid2)
                                {
                                    PropertyChangeLog propertyChangeLog = new PropertyChangeLog()
                                    {
                                        PropertyName = sourceProp.Name,
                                        NewValue = o2Val,
                                        OldValue = o1Val

                                    };

                                    recordChangeLogs.PropertyChangeLogs.Add(propertyChangeLog);
                                }

                                break;

                            case "string":
                                string str1 = o1Val != null ? (string)Convert.ChangeType(o1Val, typeof(string)) : null;
                                string str2 = o2Val != null ? (string)Convert.ChangeType(o2Val, typeof(string)) : null;

                                if (str1 != str2)
                                {
                                    PropertyChangeLog propertyChangeLog = new PropertyChangeLog()
                                    {
                                        PropertyName = sourceProp.Name,
                                        NewValue = o2Val,
                                        OldValue = o1Val

                                    };

                                    recordChangeLogs.PropertyChangeLogs.Add(propertyChangeLog);
                                }

                                break;

                            case "int":
                                int? int1 = o1Val != null ? (int?)Convert.ChangeType(o1Val, typeof(int)) : null;
                                int? int2 = o2Val != null ? (int?)Convert.ChangeType(o2Val, typeof(int)) : null;

                                if (int1 != int2)
                                {
                                    PropertyChangeLog propertyChangeLog = new PropertyChangeLog()
                                    {
                                        PropertyName = sourceProp.Name,
                                        NewValue = o2Val,
                                        OldValue = o1Val

                                    };

                                    recordChangeLogs.PropertyChangeLogs.Add(propertyChangeLog);
                                }

                                break;

                            case "decimal":
                                decimal? decimal1 = o1Val != null ? (decimal?)Convert.ChangeType(o1Val, typeof(decimal)) : null;
                                decimal? decimal2 = o2Val != null ? (decimal?)Convert.ChangeType(o2Val, typeof(decimal)) : null;

                                if (decimal1 != decimal2)
                                {
                                    PropertyChangeLog propertyChangeLog = new PropertyChangeLog()
                                    {
                                        PropertyName = sourceProp.Name,
                                        NewValue = o2Val,
                                        OldValue = o1Val

                                    };

                                    recordChangeLogs.PropertyChangeLogs.Add(propertyChangeLog);
                                }

                                break;

                            case "datetime":
                                DateTime? DateTime1 = o1Val != null ? (DateTime?)Convert.ChangeType(o1Val, typeof(DateTime)) : null;
                                DateTime? DateTime2 = o2Val != null ? (DateTime?)Convert.ChangeType(o2Val, typeof(DateTime)) : null;

                                if ((DateTime1.HasValue ? DateTime1.Value.Date : DateTime.MinValue) != (DateTime2.HasValue ? DateTime2.Value.Date : DateTime.MinValue))
                                {
                                    PropertyChangeLog propertyChangeLog = new PropertyChangeLog()
                                    {
                                        PropertyName = sourceProp.Name,
                                        NewValue = o2Val,
                                        OldValue = o1Val

                                    };

                                    recordChangeLogs.PropertyChangeLogs.Add(propertyChangeLog);
                                }

                                break;

                            case "bool":
                                bool bool1 = o1Val != null ? (bool)Convert.ChangeType(o1Val, typeof(bool)) : false;
                                bool bool2 = o2Val != null ? (bool)Convert.ChangeType(o2Val, typeof(bool)) : false;

                                if (bool1 != bool2)
                                {
                                    PropertyChangeLog propertyChangeLog = new PropertyChangeLog()
                                    {
                                        PropertyName = sourceProp.Name,
                                        NewValue = o2Val,
                                        OldValue = o1Val

                                    };

                                    recordChangeLogs.PropertyChangeLogs.Add(propertyChangeLog);
                                }

                                break;


                        }

                    }

                    if (recordChangeLogs.PropertyChangeLogs.Count > 0)
                    {
                        changeLogList.ChangeLogs.Add(recordChangeLogs);
                        return XMLConverter.Serialize<ChangeLogList>(changeLogList);
                    }
                    else
                        throw new Exception("No any changes detected to update the record.");


                }
                else
                {
                    throw new Exception("No ModuleSetup Information found.");
                }

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public static Tuple<T, string> GetTByChangeApplied<T>(T entity, string ChangeLog)
        {
            try
            {
                ChangeLogList changeLogList = XMLConverter.Deserialize<ChangeLogList>(ChangeLog);

                RecordChangeLog currentRecordChangeLog = changeLogList.ChangeLogs.Where(c => c.ChangeStatus == RecordStatus.Unauthorized).OrderByDescending(x => x.ChangeDate).First();

                var ModuleSetup = ModuleHelper.GetModuleSetup<T>();

                PropertyInfo[] sourceProprties = typeof(T).GetProperties(BindingFlags.Instance | BindingFlags.Public);

                changeLogList.ChangeLogs.Where(c => c.ChangeStatus == RecordStatus.Unauthorized).ToList().ForEach(s => { s.ChangeStatus = RecordStatus.Active; });

                foreach (var sourceProp in sourceProprties)
                {
                    if (ModuleSetup.ModuleBussinesLogicSetups.Any(c => c.ColumnName == sourceProp.Name && c.CanUpdate))
                    {
                        PropertyChangeLog propertyChangeLog = currentRecordChangeLog.PropertyChangeLogs.Where(p => p.PropertyName == sourceProp.Name).FirstOrDefault();

                        if (propertyChangeLog != null)
                        {
                            sourceProp.SetValue(entity, propertyChangeLog.NewValue);
                        }
                    }
                }

                string _changeLog = XMLConverter.Serialize<ChangeLogList>(changeLogList);

                return new Tuple<T, string>(entity, _changeLog);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public static string RevertChangeLog(string ChangeLog)
        {
            try
            {
                ChangeLogList changeLogList = XMLConverter.Deserialize<ChangeLogList>(ChangeLog);
                changeLogList.ChangeLogs.Where(c => c.ChangeStatus == RecordStatus.Unauthorized).ToList().ForEach(s => { s.ChangeStatus = RecordStatus.Reverted; });

                return XMLConverter.Serialize<ChangeLogList>(changeLogList);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public static string DiscardChangeLog(string ChangeLog)
        {
            try
            {
                ChangeLogList changeLogList = XMLConverter.Deserialize<ChangeLogList>(ChangeLog);
                changeLogList.ChangeLogs.Where(c => c.ChangeStatus == RecordStatus.Unauthorized).ToList().ForEach(s => { s.ChangeStatus = RecordStatus.Discarded; });

                return XMLConverter.Serialize<ChangeLogList>(changeLogList);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public static RecordChangeLog GetLatestRecordChangeLogs(string ChangeLog)
        {
            try
            {
                ChangeLogList changeLogList = XMLConverter.Deserialize<ChangeLogList>(ChangeLog);

                RecordChangeLog currentRecordChangeLog = changeLogList.ChangeLogs.Where(c => c.ChangeStatus == RecordStatus.Unauthorized).OrderByDescending(x => x.ChangeDate).First();

                return currentRecordChangeLog;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
