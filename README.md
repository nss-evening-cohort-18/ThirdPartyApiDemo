# ThirdPartyApiDemo

This demo used the Stars Wars API found at https://swapi.dev/
Feel free to familiarize yourself with the endpoints and data structures returned.

The flow of this app is as follows:
- the Users endpoint will return a view model for the front end to use as a profile page
- this view model includes the user model, a list of options to populate a dropdown, and the Star Wars character currently saved as the user's favorite character.

The important bits to notice if you're only interested in a generic answer of how to use a 3rd party API are the setup of the HttpClient in the Services in Program.cs and the use of that client in the SwapiClientRepository.
