using System;
using BIM_Query_Interface;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace UnitTestProject_parser
{
    [TestClass]
    public class IFCparsertest
    {
        [TestMethod]
        public void IFCparserconstructortest()
        {
            //arrange
            string inputline =
                "#841998= IFCRELASSIGNSTASKS('0FEOTYMU12Iu9SuQSI69LP',#5,$,$,(#841990),$,#841688,#841991)";
            IFCparser testobject;
            //act
                testobject =new IFCparser(inputline);
            //assert
            Assert.AreEqual(841998,testobject.lineno,"Not processed correctly" );
            Assert.AreEqual("IFCRELASSIGNSTASKS", testobject.IFCclass, "Not processed correctly");
            Assert.AreEqual("'0FEOTYMU12Iu9SuQSI69LP',#5,$,$,(#841990),$,#841688,#841991", testobject.IFCdata, "Not processed correctly");
        }
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
