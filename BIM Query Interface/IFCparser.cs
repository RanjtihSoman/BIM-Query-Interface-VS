using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace BIM_Query_Interface
{
    public class IFCparser
    {


        public int lineno;
        public string IFCclass;
        public String IFCdata;
        public IFCrelassignstask taskassignment;
        public IFCtask task;
        public IFCScheduleTimeControl scheduleTimeControl;
        public IFCDateAndTime dateandtime;
        public IFCdate date;

        public IFCparser(string InputData)
        {
            int first_paranthesis;
            int last_paranthesis;
            int first_hash;
            int first_equal;
            int startof_IFC;

            try
            {
                first_hash = InputData.IndexOf("#") + 1;
                first_equal = InputData.IndexOf("=");
                startof_IFC = InputData.IndexOf("IFC");
                first_paranthesis = InputData.IndexOf("(") + 1;
                last_paranthesis = InputData.LastIndexOf(")");

                lineno = Int32.Parse(InputData.Substring(first_hash, first_equal - first_hash));
                IFCclass = InputData.Substring(startof_IFC, first_paranthesis - startof_IFC - 1);
                IFCdata = InputData.Substring(first_paranthesis, last_paranthesis - first_paranthesis);


                //Assigntask = new IFCrelassignstask(IFCdata);
            }
            catch (Exception e)
            {
                Console.WriteLine("Failed to parse the lines. {0}", e.ToString());
                throw;
            }
        }

    }



    public class IFCrelassignstask
    {

        public string globalid;
        public string ownerdescription;
        public IFCScheduleTimeControl IfcScheduleTimeControlInstance;
        public IFCtask IFCtaskinstance;
        public IFCparser IFCobject;

        public IFCrelassignstask(String InputData, IList<IFCparser> source, int lineno)
        {
            try
            {
                dataparser o = new dataparser();
                string[] tempstringarray;
                tempstringarray = InputData.Split(',');
                IFCobject = source.Getobjectfromlineno(Int32.Parse(o.Getlinenodata(tempstringarray[4])));
                IFCobject.task = new IFCtask(IFCobject.IFCdata, IFCobject.lineno);
                IFCtaskinstance = IFCobject.task;

                IFCobject = source.Getobjectfromlineno(Int32.Parse(o.Getlinenodata(tempstringarray[7])));
                IFCobject.scheduleTimeControl = new IFCScheduleTimeControl(IFCobject.IFCdata, source, IFCobject.lineno);
                IfcScheduleTimeControlInstance = IFCobject.scheduleTimeControl;
                //foreach (var s in source)
                //{

                //    if (s.lineno == lineno)
                //        s.taskassignment = new IFCrelassignstask(IFCtaskinstance, IfcScheduleTimeControlInstance);
                //    else if (s.lineno == IFCtaskinstance.tasklinelineno )
                //        s.task = IFCtaskinstance;
                //    else if (s.lineno == IfcScheduleTimeControlInstance.scheduletimecontrollineno )
                //        s.scheduleTimeControl = IfcScheduleTimeControlInstance;
                //    else if (s.lineno == IfcScheduleTimeControlInstance.scheduledstartdate.dateandtimelineno)
                //        s.dateandtime = IfcScheduleTimeControlInstance.scheduledstartdate;
                //    else if (s.lineno == IfcScheduleTimeControlInstance.scheduledenddate.dateandtimelineno)
                //        s.dateandtime = IfcScheduleTimeControlInstance.scheduledenddate;
                //    else if (s.lineno == IfcScheduleTimeControlInstance.scheduledstartdate.date.datelineno )
                //        s.date = IfcScheduleTimeControlInstance.scheduledstartdate.date ;
                //    else if (s.lineno == IfcScheduleTimeControlInstance.scheduledenddate.date.datelineno)
                //        s.date = IfcScheduleTimeControlInstance.scheduledenddate.date;

                //    else
                //    {

                //    }

                //}
            }
            catch (Exception e)
            {
                throw;
            }
        }
        public IFCrelassignstask(IFCtask taskinput, IFCScheduleTimeControl schedulecControlinput)
        {
            try
            {
                IFCtaskinstance = taskinput;
                IfcScheduleTimeControlInstance = schedulecControlinput;
            }

            catch (Exception e)
            {
                throw;
            }

        }
    }

    public class IFCtask
    {
        public string name;
        public string taskID;
        public int tasklinelineno;

        public IFCtask(string inputdata, int lineno)
        {
            try
            {
                tasklinelineno = lineno;
                string[] tempstringarray;
                tempstringarray = inputdata.Split(',');
                name = tempstringarray[2];
                taskID = tempstringarray[5];
            }
            catch (Exception e)
            {
                throw;
            }

        }

    }

    public class IFCScheduleTimeControl
    {
        public IFCparser IFCobject;
        public IFCDateAndTime scheduledstartdate;
        public IFCDateAndTime scheduledenddate;
        public int scheduletimecontrollineno;

        public IFCScheduleTimeControl(string inputdata, IList<IFCparser> source, int lineno)
        {
            try
            {
                scheduletimecontrollineno = lineno;
                dataparser o = new dataparser();
                string[] tempstringarray;
                tempstringarray = inputdata.Split(',');
                //   scheduledstartdate = Int32.Parse(o.Getlinenodata(tempstringarray[8]));
                IFCobject = source.Getobjectfromlineno(Int32.Parse(o.Getlinenodata(tempstringarray[8])));
                IFCobject.dateandtime = new IFCDateAndTime(IFCobject.IFCdata, source, IFCobject.lineno);
                scheduledstartdate = IFCobject.dateandtime;
                IFCobject = source.Getobjectfromlineno(Int32.Parse(o.Getlinenodata(tempstringarray[12])));
                IFCobject.dateandtime = new IFCDateAndTime(IFCobject.IFCdata, source, IFCobject.lineno);
                scheduledenddate = IFCobject.dateandtime;
                
            }
            catch (Exception e)
            {
                throw;
            }

        }
    }

    public class IFCDateAndTime
    {
        public int dateandtimelineno;
        public IFCdate date;
        public int time;
        private IFCparser ifcobject;
        public IFCDateAndTime(string inputdata, IList<IFCparser> source, int lineno)
        {
            try
            {
                dateandtimelineno = lineno;
                int datelineno ;
                dataparser o = new dataparser();
                string[] tempstringarray;
                tempstringarray = inputdata.Split(',');
                datelineno = Int32.Parse(o.Getlinenodata(tempstringarray[0]));
                ifcobject = source.Getobjectfromlineno(datelineno );
                ifcobject.date = new IFCdate(ifcobject.IFCdata,datelineno);
                date = ifcobject.date;
            }
            catch (Exception e)
            {
                throw;
            }


        }
    }

    public class IFCdate
    {
        public DateTime date;
        public int datelineno;

        public IFCdate(string inputdata, int linenoinput)
        {
            try
            {
                datelineno = linenoinput;
                string[] tempstringarray;
                tempstringarray = inputdata.Split(',');
                date = new DateTime(Int32.Parse(tempstringarray[2]), Int32.Parse(tempstringarray[1]), Int32.Parse(tempstringarray[0]));
            }
            catch (Exception e)
            {
                throw;
            }
        }
    }

    class dataparser
    {
        public dataparser()
        {
        }
        public string Getlinenodata(string tempstring)
        {
            try
            {
                IEnumerable<string> substringarray;
                substringarray = tempstring.Split(new[] { '(', ')' }, StringSplitOptions.RemoveEmptyEntries)
                    .Where(x => !string.IsNullOrWhiteSpace(x))
                    .Select(s => s.Trim());
                tempstring = substringarray.First();
                tempstring = tempstring.Substring(1, tempstring.Length - 1);
                return tempstring;
            }
            catch (Exception e)
            {
                Console.WriteLine("Failed to parse the lines. {0}", e.ToString());
                throw;
            }

        }
    }

}
