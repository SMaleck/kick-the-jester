using UnityEngine;
using System.Collections;

public class Test
{
    private string what;

    public Test (string what)
    {
        Debug.Log("Constructor Test for what? " + what);
        this.what = what;
    }

    public void Speak()
    {
        Debug.Log("Speak what? => " + what);
    }
}
