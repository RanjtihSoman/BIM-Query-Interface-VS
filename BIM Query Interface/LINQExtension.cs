using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BIM_Query_Interface
{
    public static class LINQExtension
    {
        public static IFCparser Getobjectfromlineno(this IList<IFCparser> source, int lineno)
        {

            //return source.FirstOrDefault(x => x.lineno == lineno);
            IEnumerable<IFCparser> ifcLines = from ifcobjects in source
                                              where ifcobjects.lineno == lineno
                                              select ifcobjects;
            return ifcLines.Any() ? ifcLines.First() : null;
        }

        public static IEnumerable<IFCparser> EnrichIfCparser(this IList<IFCparser> source)
        {
            int count = 0;
            float percent;
            try
            {
                IEnumerable< IFCparser > Relassignlist = from ifcobject in source
                    where (string.Compare(ifcobject.IFCclass, "IFCRELASSIGNSTASKS", true) == 0)
                    select ifcobject;
                int total = Relassignlist.Count();
                foreach (var ifcobject in Relassignlist)
                {
                    count++;
                    //to check status
                    percent = count/total *100;
                    ifcobject.taskassignment=new IFCrelassignstask(ifcobject.IFCdata,source,ifcobject.lineno );
                }

                return Relassignlist;

            }
            catch (Exception e)
            {
                Console.WriteLine("Failed to classiffy the lines. {0}", e.ToString());
                throw;
            }
        }

        public static IEnumerable<IFCrelassignstask > returntasksondate(this IEnumerable< IFCparser> source,DateTime selecteddate)
        {
            
            IEnumerable<IFCrelassignstask> taskassignments = from ifcobject in source
                                                             where (DateTime.Compare(ifcobject.taskassignment.IfcScheduleTimeControlInstance.scheduledstartdate.date.date , selecteddate )<=0) && (DateTime.Compare(ifcobject.taskassignment.IfcScheduleTimeControlInstance.scheduledenddate.date.date, selecteddate)>=0)
                                                             select ifcobject.taskassignment;

            return taskassignments ;
        }

    }
}

//        //    switch (instance.IFCclass)
//        //    {
//        //        case "IFCRELASSIGNSTASKS":
//        //            instance.taskassignment = new IFCrelassignstask(instance.IFCdata);
//        //            break;
//        //        case "IFCTASK":
//        //            instance.task = new IFCtask(instance.IFCdata);
//        //            break;
//        //        case "IFCSCHEDULETIMECONTROL":
//        //            instance.scheduleTimeControl = new IFCScheduleTimeControl(instance.IFCdata);
//        //            break;
//        //        case "IFCDATEANDTIME":
//        //            instance.dateandtime = new IFCDateAndTime(instance.IFCdata);
//        //            break;
//        //        case "IFCCALENDARDATE":
//        //            instance.date = new IFCdate(instance.IFCdata);
//        //            break;
//        //        default:
//        //            break;


//public static IEnumerable<IFCparser> tasksfordate(this IList<IFCparser> source,DateTime date )
//{


//    IEnumerable<IFCparser> schedulecontrolLines = from ifcobjects in source
//        where ifcobjects.IFCclass.Equals("IFCSCHEDULETIMECONTROL", StringComparison.OrdinalIgnoreCase)
//        select ifcobjects;

//    IEnumerable<IFCparser> schedulecontrolwithintime ;
//    /* = from ifcobjects in schedulecontrolLines
//        where (DateTime.Compare(
//                   source.Getobjectfromlineno(source
//                           .Getobjectfromlineno(ifcobjects.scheduleTimeControl.scheduledstartdate).dateandtime
//                           .date)
//                       .date.date, date) <= 0) && (DateTime.Compare(
//                  source.Getobjectfromlineno(source
//                          .Getobjectfromlineno(ifcobjects.scheduleTimeControl.scheduledenddate ).dateandtime
//                          .date)
//                      .date.date, date)>=0)
//        select ifcobjects;
//        */
//    try
//    {
//        foreach (IFCparser S in schedulecontrolLines)
//        {


//        }
//    }
//    catch (Exception e)
//    {
//        Console.WriteLine("Failed to parse the lines. {0}", e.ToString());
//    }




//    return schedulecontrolLines ;

//}