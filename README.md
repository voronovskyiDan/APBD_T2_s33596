In this project i tried to follow some Domain Driven Design rules. 
In case of this project I omitted some unnecessary parts such as DTOs.
My basic structure looks like this:
Controller, Repository -> Service -> Entities

I put business logic inside Entities and all logical interaction between objects is performed by theis methods.
Services in my solution are intermediate between Repository and Entities itself. They orchestrate interaction between several Entities.
Also work directly with Repository that provides a persistence and acWScess of data.
Controller is kind of Facade(pattern) that provide functions which are performed by different Services.
Also it validate and map raw data to objects and then call proper method from Service class.

File structure I separated by 4 layers: Domain, Application, API, Infrastructure: 
Domain -> Entities
Application -> Services that work with Entities
API -> Controller
Infrastructure -> Repository, FileService(with which works Repository thats we it is located here)

Responsibilities:
Entities – contains business rules and domain logic
Application Services – coordinates interactions between multiple entities
Repository – provides access to stored data
(Infrastructure Service) FileService – handles file operations used by Repository
Controller – validates input, maps data, and calls Services

Coupling:
Low coupling is maintained by separating responsibilities between layers.(As I have written in Responsibilities)

Of course I have one thing bad -> my entities depends on [JsonConstructor] which is basically
Infrastructure level thing, but separating this and creating Mapper and Different Objects for Serialization
would be an overkill in this situation.

Cohesion:
As all of my classes has one and clear single responsibility the Cohesion is high because everything inside one class
is related to one specific purpose.
