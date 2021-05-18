# Minesweeper API	

This is a Restfull API to play Minesweeper. 

This is a test exercise by Software Engineer Daniel Silva Jiménez

## Main Functions

The API provides functions as:
	* See the list of created games. 
	* Create a game.
	* Play a created game. 
	* Remove a game.
	* Reset a game (set as new with the same parameters).
	
The API also provides API functions to use with an specifica game, as: 
	* Reveal cell.
	* Mark cell as bomb.
	* Mark cell as question mark.

It was deployed to azure. To see the list of functions and test them you can go to:
	[Minesweeper API functions](https://minemosquestapi.azurewebsites.net/swagger/index.html)

You can play the game with a web site that is using this API, here:
	[Minesweeper Web App](https://webminesweepergame.azurewebsites.net/)

## Disclaimer
This version does not include "Time tracking" neither "Ability to support multiple users/accounts". That could be included in a next step if it is needed. 

* An interesting thing of this approach is that you can play the same game with multiple users at the same time in different browsers, although, of course that could be  do it better with automatically refresh and users selection.

## Desing
A service layer was created to have there all the business rules isolated of the controller with the API functions. It is the class "MineSweeperAPI\Services\MineSweeperService.cs"

To store the data a document database in MongoDb is used. To be able to easily change the data source, a repository interface was created, so all the operations are against "IMineSweeperRepository", so if we want to change to SQL or other data source we just need to inject a new implementation of this interface.

## Unit Testing
In the MineSweeperApi.Test project you can find unit tests for the main functions.



