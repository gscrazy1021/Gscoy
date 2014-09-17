using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Gscoy.Common;
using Microsoft.VisualStudio.TestTools.UnitTesting;
namespace Gscoy.Common.Tests
{
    [TestClass()]
    public class ExecetionExtensionTests
    {
        [TestMethod()]
        public void ToStringExTest()
        {
            try
            {
                try
                {
                    try
                    {
                        try
                        {
                            int a = 0;
                            int b = 1;
                            int c = b / a;
                        }
                        catch (Exception)
                        {
                            throw;
                        }
                    }
                    catch (Exception)
                    {

                        throw;
                    }
                }
                catch (Exception ex)
                {
                    string result = ex.ToStringEx();
                }
            }
            catch (Exception)
            {

                throw;
            }
            Assert.Fail();
        }
    }
}
