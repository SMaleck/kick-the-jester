using UnityEngine;
using UnityEditor;
using UnityEngine.TestTools;
using NUnit.Framework;
using System.Collections;

public class ExampleTest {

	[Test]
	public void ExampleTestSimplePasses() {
        // Use the Assert class to test conditions.
        Assert.IsTrue(true);
	}

    [Test]
    public void ExampleTestSimpleFailure()
    {
        Assert.IsTrue(true);
    }

	// A UnityTest behaves like a coroutine in PlayMode
	// and allows you to yield null to skip a frame in EditMode
	[UnityTest]
	public IEnumerator ExampleTestWithEnumeratorPasses() {
		// Use the Assert class to test conditions.
		// yield to skip a frame
		yield return null;
	}
}
