# MyMessagePortal

*Read this in other languages: [Polish](README.pl-PL.md)

MyMessagePortal is simple application that allows all logged users to create channels and post messages on them.

## Installation:

In order to run the application, a MS SQL database is required. New database can be created or can be restored from provided backup with sample data.

### I. New database 

1. create new database ex. MyMessagePortal using following script ```create database MyMessagePortalDb```
1. change database in application config file `appsettings.xml` if necessary
1. do `Update-Database` in VisualStudio in **Package Manager Console**
1. add sample data by running script **\scripts\Sample_data.sql**


### II. SampleDB

1. restore database backup with sample data. Backup can be found in folder **SampleDB**

## Sample data:

Sample data contains:
1. two users (password: _Qazwsx1@_):
    * `marcin`
    * `tomek`
1. collection of sample:
    * `channels`
    * `messages`
    
## Available modules:

### Channels

Channel module allows for creation, deletion and browsing created channels. Each channel is highlighted with random color and can be marked by any users as Observed. Channel can be deleted only by user who created it. To each channel users can posts messages.

#### Functionalities
* Creation of new channel
* Deletion of created channel if it not user default channel and user created channel that is trying to delete
* Browsing available channels
* Marking channel as Obsered

### Messages

Message module allows for creation, deletion and browsing created message. Messages can be posted to any channel and can be removed from channel if it has not been more than 10 minutes since its creation.

#### Functionalities
* Creation of new message
* Deletion of a new message if it has not been more than 10 minutes since its creation 
* Browsing created messages

## Application:

Application does not require from user any specific persmissions to create and post messages. Any user that register will get his default channel on which he and other users can post messages. After login users will be presented with his home page on which he will see all channels that he is observing.

##### to be continued.
