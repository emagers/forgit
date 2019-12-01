# forgit
A git CLI helper to allow running git commands for any repository from any directory

## Usage

Before a repository can be used with forgit, it must first be registered in the settings.json file. You can manually edit this file, or simply run `frgt register -n {repositoryName} -p {path}`.

You can also clone using forgit which will also register the repository:

`frgt clone https://github.com/emagers/forgit.git`

### forgit commands

* list
   * Shows all the repositories currently registered.
* show 
   * args
      * -n, --name
	     * (required) the name of the repository to display.
   * Displays the given repository's path.
* register
   * args
      * -p, --path
	     * (optional) the path to the repository (defaults to the current directory).
      * -n, --name
	     * (optional) the name to use to reference the repository (defaults to the path directory name).
   * Registers the provided repository with forgit.
* clone
   * args
      * -u, --url
	     * (required) the clone url for the repository.
      * -p, --path
	     * (optional) the path to the repository (defaults to the current directory).
      * -n, --name
	     * (optional) the name to use to reference the repository (defaults to the path directory name).
   * Clones the provided repository at the given path, then registers it with forgit.
* unregister
   * args
      * -n, --name
	     * (required) the name of the repository to unregister.
   * Unregisters a repository with forgit.

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