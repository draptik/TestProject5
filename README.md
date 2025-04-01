# Minimal Verify Example

Useful for IDE tests...

## Prerequisites

- .NET 8
- VsCode
- VsCode Extensions: C# Dev Kit

## Question about VsCode usage

When executing the ["Test1" in UnitTest1.cs](./TestProject5/UnitTest1.cs) in VsCode,

I expect that the failing test opens the VsCode diff viewer,

because it is defined in [`ModuleInitializer.cs`](./TestProject5/ModuleInitializer.cs).

### On Linux

...this works as expected.

### On Windows

- the diff viewer is opened when running `dotnet test` from the cli
- the diff viewer is **not opened** when running the test from within VsCode

The last bullet point is the part I don't understand...

VsCode on Windows is installed here:

```sh
$ which code
/c/Program Files/Microsoft VS Code/bin/code
```

Details of installation:

```txt
C:\Program Files\Microsoft VS Code>dir
 Volume in drive C is Windows
 Volume Serial Number is C046-3BB3

 Directory of C:\Program Files\Microsoft VS Code

25/03/2025  17:41    <DIR>          .
27/03/2025  23:41    <DIR>          ..
25/03/2025  17:41    <DIR>          bin
12/03/2025  15:01           147.398 chrome_100_percent.pak
12/03/2025  15:01           219.772 chrome_200_percent.pak
12/03/2025  15:32       182.525.000 Code.exe
12/03/2025  15:00               367 Code.VisualElementsManifest.xml
12/03/2025  15:29         4.927.168 d3dcompiler_47.dll
12/03/2025  15:32         2.832.928 ffmpeg.dll
12/03/2025  15:01        10.464.144 icudtl.dat
12/03/2025  15:32           503.864 libEGL.dll
12/03/2025  15:29         8.122.936 libGLESv2.dll
12/03/2025  15:01        10.984.370 LICENSES.chromium.html
25/03/2025  17:41    <DIR>          locales
25/03/2025  17:41    <DIR>          policies
19/07/2024  11:01    <DIR>          resources
12/03/2025  15:01         5.615.907 resources.pak
12/03/2025  15:01           320.614 snapshot_blob.bin
25/03/2025  17:41    <DIR>          tools
25/03/2025  17:41         8.202.090 unins000.dat
25/03/2025  17:41         3.494.944 unins000.exe
25/03/2025  17:41            24.367 unins000.msg
12/03/2025  15:01           693.457 v8_context_snapshot.bin
12/03/2025  15:29         5.518.392 vk_swiftshader.dll
12/03/2025  15:01               106 vk_swiftshader_icd.json
12/03/2025  15:29           910.392 vulkan-1.dll
              19 File(s)    245.508.216 bytes
               7 Dir(s)  231.549.911.040 bytes free

C:\Program Files\Microsoft VS Code>cd bin

C:\Program Files\Microsoft VS Code\bin>dir
 Volume in drive C is Windows
 Volume Serial Number is C046-3BB3

 Directory of C:\Program Files\Microsoft VS Code\bin

25/03/2025  17:41    <DIR>          .
25/03/2025  17:41    <DIR>          ..
12/03/2025  15:00             2.001 code
12/03/2025  15:29        19.604.512 code-tunnel.exe
12/03/2025  15:00               178 code.cmd
               3 File(s)     19.606.691 bytes
               2 Dir(s)  231.549.796.352 bytes free
```

Content of `C:\Program Files\Microsoft VS Code\bin\code.cmd`:

```txt
@echo off
setlocal
set VSCODE_DEV=
set ELECTRON_RUN_AS_NODE=1
"%~dp0..\Code.exe" "%~dp0..\resources\app\out\cli.js" %*
IF %ERRORLEVEL% NEQ 0 EXIT /b %ERRORLEVEL%
```

Content of `C:\Program Files\Microsoft VS Code\bin\code`:

```sh
#!/usr/bin/env sh
#
# Copyright (c) Microsoft Corporation. All rights reserved.
# Licensed under the MIT License. See License.txt in the project root for license information.
if [ "$VSCODE_WSL_DEBUG_INFO" = true ]; then
        set -x
fi

COMMIT="ddc367ed5c8936efe395cffeec279b04ffd7db78"
APP_NAME="code"
QUALITY="stable"
NAME="Code"
SERVERDATAFOLDER=".vscode-server"
VSCODE_PATH="$(dirname "$(dirname "$(realpath "$0")")")"
ELECTRON="$VSCODE_PATH/$NAME.exe"

IN_WSL=false
if [ -n "$WSL_DISTRO_NAME" ]; then
        # $WSL_DISTRO_NAME is available since WSL builds 18362, also for WSL2
        IN_WSL=true
else
        WSL_BUILD=$(uname -r | sed -E 's/^[0-9.]+-([0-9]+)-Microsoft.*|.*/\1/')
        if [ -n "$WSL_BUILD" ]; then
                if [ "$WSL_BUILD" -ge 17063 ]; then
                        # WSLPATH is available since WSL build 17046
                        # WSLENV is available since WSL build 17063
                        IN_WSL=true
                else
                        # If running under older WSL, don't pass cli.js to Electron as
                        # environment vars cannot be transferred from WSL to Windows
                        # See: https://github.com/microsoft/BashOnWindows/issues/1363
                        #      https://github.com/microsoft/BashOnWindows/issues/1494
                        "$ELECTRON" "$@"
                        exit $?
                fi
        fi
fi
if [ $IN_WSL = true ]; then

        export WSLENV="ELECTRON_RUN_AS_NODE/w:$WSLENV"
        CLI=$(wslpath -m "$VSCODE_PATH/resources/app/out/cli.js")

        # use the Remote WSL extension if installed
        WSL_EXT_ID="ms-vscode-remote.remote-wsl"

        ELECTRON_RUN_AS_NODE=1 "$ELECTRON" "$CLI" --locate-extension $WSL_EXT_ID >/tmp/remote-wsl-loc.txt 2>/dev/null </dev/null
        WSL_EXT_WLOC=$(cat /tmp/remote-wsl-loc.txt)

        if [ -n "$WSL_EXT_WLOC" ]; then
                # replace \r\n with \n in WSL_EXT_WLOC
                WSL_CODE=$(wslpath -u "${WSL_EXT_WLOC%%[[:cntrl:]]}")/scripts/wslCode.sh
                "$WSL_CODE" "$COMMIT" "$QUALITY" "$ELECTRON" "$APP_NAME" "$SERVERDATAFOLDER" "$@"
                exit $?
        fi

elif [ -x "$(command -v cygpath)" ]; then
        CLI=$(cygpath -m "$VSCODE_PATH/resources/app/out/cli.js")
else
        CLI="$VSCODE_PATH/resources/app/out/cli.js"
fi
ELECTRON_RUN_AS_NODE=1 "$ELECTRON" "$CLI" "$@"
exit $?
```

## Expected behaviour

The following 2 screenshots show the expected behaviour of VsCode on Linux.

![screenshot of test](./documentation/screenshot-test.png)

Clicking on the red cross symbol to the left of line 7 in the above screenshot opens the VsCode diff viewer:

![screenshot of diff](./documentation/screenshot-diff.png)

The diff viewer is not openend on Windows. 😥
