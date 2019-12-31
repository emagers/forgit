# forgit
![](https://github.com/emagers/forgit/workflows/build/badge.svg)

A git CLI helper to allow running git commands for any repository from any directory.

## Building From Source

Simply pull the project and run `dotnet build` (requires [dotnetcore 3.1.100](https://dotnet.microsoft.com/download))

If you need to compile the project for an runtime not already supported, add the runtime identifier to the forgit/forgit.csproj file. You can then run the command `dotnet publish -c Release -r \<your new rID\> --self-contained`.

Currently supported runtimes:

* win10-x64
* osx-x64
* linux-x64

## Contributing

Open an issue, fork the project, and open a pull request.

## Usage

Before a repository can be used with forgit, it must first be registered in the settings.json file. You can manually edit this file, or simply run `frgt register -n {repositoryName} -p {path}`.

You can also clone using forgit which will also register the repository:

`frgt clone https://github.com/emagers/forgit.git`

### forgit commands

|Command|Description|
|---|---|
|list|Shows all the repositories currently registered.|
|show|Displays the given repository's path.|
|register|Registers the provided repository with forgit.|
|unregister|Unregisters a repository with forgit.|
|clone|Clones the provided repository at the given path, then registers it with forgit.|
|help|Displays the command list and the arguments for each command.|

### git commands with forgit

Any git command can be run with forgit. By default the git command will be run in the current directory. You can specify the registered name of a repository to run the git command in that repository's directory.

Example:

``` bash

~: frgt register -n forgit -p C:/projects/forgit
~: frgt register -n sudoku -p C:/Users/myuser/source/repos/sudoku

~: frgt forgit checkout master
~: frgt forgit pull
~: frgt sudoku pull

```