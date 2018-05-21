using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xbim.Ifc;
using Xbim.Ifc4.Interfaces;
namespace BIM_Query_Interface
{
   public class XBIM_IFC_Parser
    {
       public IfcStore model;

        public XBIM_IFC_Parser(string filename)
        {
            model = IfcStore.Open(filename);
           string temp = filename;

        }

    }
}
