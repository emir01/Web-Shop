using System.Linq;
using NUnit.Framework;
using WS.Core.Toolbox.Reflection;

namespace WS.Testing.Core.Toolbox
{
    public class TypeReflectionToolboxTests
    {
        #region Inernal Test Classes

        internal class InternalTestBase
        {
            public string BaseProp { get; set; }
        }

        internal class InternalTestChild : InternalTestBase
        {
            public string ChildProp { get; set; }
        }


        #endregion

        #region CopyPublicProperties Tests
        
        [Test]
        public void ReflectionCopy_ShouldCopy_PropertyValues()
        {
            // ARRANGE
            var source = new InternalTestChild()
            {
                BaseProp = "BaseProp",
                ChildProp = "ChildProp"
            };

            var destination = new InternalTestChild();

            // ACT
            TypeReflectionToolbox.CopyPublicProperties(source, destination);

            //ASSERT
            Assert.AreEqual(source.BaseProp, destination.BaseProp);
            Assert.AreEqual(source.ChildProp, destination.ChildProp);
        }
        
        [Test]
        public void ReflectionCopy_ShouldNotCopy_ExcludedValues()
        {
            // ARRANGE
            var source = new InternalTestChild()
            {
                BaseProp = "BaseProp",
                ChildProp = "ChildProp"
            };

            var destination = new InternalTestChild()
            {
                BaseProp = "OriginalBaseProp"
            };

            var baseProperties = typeof(InternalTestBase).GetProperties();
            // ACT
            TypeReflectionToolbox.CopyPublicProperties(source, destination, baseProperties.ToList());

            //ASSERT
            Assert.AreNotEqual(source.BaseProp, destination.BaseProp);
            Assert.AreEqual(destination.BaseProp, "OriginalBaseProp");
            Assert.AreEqual(source.ChildProp, destination.ChildProp);
        }

        #endregion
    }
}
