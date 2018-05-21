using System;
using System.Collections.Generic;
using System.Linq;
using Xbim.Ifc4.Interfaces;

namespace BIM_Query_Interface
{
    public static  class LINQExtension
    {
        public static  IEnumerable<IIfcTask> Gettasksondate(this IEnumerable<IIfcTask> model, DateTime SelectDate)
        {
            foreach (var s in model )
            {
                DateTime start = s.TaskTime.ScheduleStart.Value.ToDateTime();
                DateTime finish = s.TaskTime.ScheduleFinish.Value.ToDateTime();
                if (DateTime.Compare(start, SelectDate) <= 0)
                {
                    finish = SelectDate;
                }
            }

            IEnumerable<IIfcTask> taskondate;
            taskondate = from objects in model
                where DateTime.Compare(objects.TaskTime.ScheduleStart.Value.ToDateTime() , SelectDate) <= 0 &&
                      DateTime.Compare(objects.TaskTime.ScheduleFinish.Value.ToDateTime() , SelectDate) >= 0
                select objects;
            return taskondate;
        }
    }
}
