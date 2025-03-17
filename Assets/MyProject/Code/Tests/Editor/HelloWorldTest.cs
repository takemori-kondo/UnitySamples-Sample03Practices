using Assets.MyProject.Code.Scripts;
using NUnit.Framework;

public class HelloWorldTest
{
    [Test]
    public void HelloWorldTestSimplePasses()
    {
        // Arrange
        var obj = new HelloWorld();

        // Act
        var result = obj.Hello();

        // Assert
        Assert.AreEqual("hello, world", result);
    }
}
