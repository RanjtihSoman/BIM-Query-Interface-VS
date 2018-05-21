using System;
using BIM_Query_Interface;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace UnitTestProject_parser
{
    [TestClass]
    public class IFCparsertest
    {
       
        
        [TestMethod]
        public void Openfilefunctiontest()
        {
            XBIM_IFC_Parser model;
            
            string filename = "F:\\Code repo\\BIM Query Interface\\IFC Schependomlaan incl planningsdata.ifc";
            model=new XBIM_IFC_Parser(filename );
            Assert.AreEqual("F:\\Code repo\\BIM Query Interface\\IFC Schependomlaan incl planningsdata.ifc", filename);
        }
    }
}
