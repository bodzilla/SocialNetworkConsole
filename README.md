# Social Network Console

## Summary
This project can be found at: https://github.com/bodzilla/SocialNetworkConsole

Challenge source: https://github.com/sandromancuso/social_networking_kata

This is a .NET console application which has the following functionalities:
- Posts something to a user's timeline.
- Read a user's timeline.
- Make a user follow another user.
- View a user's wall (this means being able to see the posts of the user's subscriptions).

## Setting up the database
The database creation script can be found under `SocialNetworkConsole\DbScripts`. Ensure that you have SQL Server installed and the filenames for these in the sourcecode are valid. The order of running these scripts agaisnt your database is:
1) `CreateDbScript.sql`
2) `CreateTableAndDataScript.sql`

## Running the application
- Ensure that the `App.config` file points to your database.
- You should be able to run the commands found on the readme @ `https://github.com/sandromancuso/social_networking_kata`

## Development choices
- I decided to use a DB to make storing objects/data less of a hassle (other option was to store these as JSON/XML files).
- Used NUnit testing, which allowed modular development (i.e. following SOLID principles within reason) - the tests are not all encompassing as they should be, this was due to time constraints.
- Seperated layers of concern such as Data Access layer, Business Logic layer and the actual application layer, this is to allow the business logic layer to act as an "API" should I ever decide implement another type of service, such as a web application, without needing to repeat or create any new code (possibly just implement async calls).
- If 3rd party frameworks were allowed in this development, I would've added some type of logging framework such as Log4Net as well as an ORM such as EF/NHibernate.
