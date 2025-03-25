# Minimal Verify Example

Useful for IDE tests...

## Question usage with VsCode

When executing the test in VsCode,

I expect that the failing test opens the VsCode diff viewer,

because it is defined in `ModuleInitializer.cs`.

On Linux, this works as expected.

On Windows:

- the diff viewer is opened when running `dotnet test` from the cli
- the diff viewer is **not opened** when running the test from within VsCode

The last bullet point is the part I don't understand...

VsCode is installed here:

```sh
$ which code
/c/Program Files/Microsoft VS Code/bin/code
```