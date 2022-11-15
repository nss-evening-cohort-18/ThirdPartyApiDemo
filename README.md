# ThirdPartyApiDemo

This demo used the Stars Wars API found at https://swapi.dev/ .  Feel free to familiarize yourself with the endpoints and data structures returned.  You can also click around throught the API directly in your browser by navigating to https://swapi.dev/api/ . 

A brief explanation of what this app does:
- the Users endpoint will return a view model for the front end intended for use with a profile page.
- the view model includes the user model, a list of options to populate a dropdown, and the Star Wars character currently saved as the user's favorite character.
- the data about the user is hard coded in this app to simplify things
- the data concerning the Star Wars character and the options for the dropdown all come from Swapi at runtime

The important bits to notice if you're only interested in a generic answer of how to use a 3rd party API are the setup of the HttpClient in the Services in Program.cs and the use of that client in the SwapiClientRepository.  Any model that you're wanting returned from your HttpClient needs to have property names matching the JSON property returned from Swapi.
