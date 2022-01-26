using UnityEngine;

// Keep all names relevant, descriptive and logical. If they are too long you are doing something wrong
// Keep everything in english

// Class names upper CamelCasing this also applies to filenames
public class CodeConvention
{
    // Order Variables/Properties/Functions (Constructor, Monobehaviour, Custom, Destructor) -> Winner

    // Grouping based on functionality (eg. all movement related variables together)

    // Variables lower camelCasing, order of variables, based on grouping
    public float someVariableIsCool = 1f;
    private float someVariableIsCool2 = 1f;

    // Properties upper CamelCasing
    private float SomeProperty1 { get; set; }

    // The following is okay
    private int xSpeed, ySpeed, zSpeed;
    // The following is not okay;
    private int speedX = 1, speedY = 2, speedZ = 3;

    // Functions:
    // Always define privates, even with build in functions
    // Parameters have no underscores
    public bool DoSomething(float someVariableIsCool, int arg2)
    {
        this.someVariableIsCool = someVariableIsCool;

        // One liners like this are acceptable as long as the line size does not exceed this width
        return (someVariableIsCool > 2) ? true : false;
        // One liners are only allowed to have 1 function
        // Shorthands are loved <3
    }

    // Expression statements are good as well
    private void DoSomething() => Debug.Log(1 + 1);

    // Brackets:
    // Brackets always on next line, use brackets also for one line statements
    // Functions always have no whitespace before the brackets whereas statements do have them
    private void SomeFunc()
    {
        if (true)
        {
            SomeFunc(); // Next line
        }
        else if (false)
        {
            SomeFunc();
        }

        // Always use white spacing unless they are related such as "else if" or if they are a follow up on each other
        if (true)
        {
            for (int i = 0; i < 3; i++)
            {

            }
        }
    }
}

// Comments, always begin with a space
// Keep the comment indents on the same place as the next line
// Comments should explain complex code, clear/understandable code dont require comments

// Use summaries for complex functions or with large functions

// Interfaces start with an I
public interface IStartInterfaceWIthI
{

}

// Files and classes are NOT the same, eg. interfacecollection.cs has all interfaces