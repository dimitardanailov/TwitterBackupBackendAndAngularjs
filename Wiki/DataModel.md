# DataModel

### Table of Contents
  1. [Revision history](#revision-history)
  1. [Database information](#database-information)
  1. [AspNetUsers](#aspnetusers)
  1. [Messages](#messages)
  1. [Followers](#followers)

### Revision history

| Date          | Version   | Author      | Revision     |
|:-------------:|:----------|:-----------:|:-------------|
|01 October 2015|0.1|Dimitar Danailov| Document creation and first documentation iteration. Tables - Messages and Followers|

**[⬆ back to top](#table-of-contents)**

### Definitions, Acronyms, and Abbreviations

  1. **SYSTEM** - All software components of the the current project as a whole, which are to be programmed by the developers
  1. **USER** - Any individual who interacts with the **SYSTEM**.
  1. **Oauth provider** - External system, like `facebook`, `twitter` and etc.
  1. **Guest** - A user who does not have a profile.

**[⬆ back to top](#table-of-contents)**

### Database information

#### Multi language / Translation support

Database is not Multi language.

**[⬆ back to top](#table-of-contents)**

### AspNetUsers

Database use (Asp.net mvc - Identity)[http://www.asp.net/identity]

**[⬆ back to top](#table-of-contents)**

### Messages

This table store information for `Messages`.

| Field         | Data Type       | Constraints | Default value |
| :------------ |:----------------| :-----------| :-------------|
|MessageID|integer|Primary Key|NOT NULL|
|UserID|NVARCHAR(128)|Foreign Key|NOT NULL|
|Text|NVARCHAR(140)||NOT NULL|
|CreatedAt|DATETIME||NOT NULL|
|UpdatedAt|DATETIME||NOT NULL|

  1. **MessageID** - the unique ID of the row which is used to access the data.
  1. **UserID** - Reference to AspNetUser
  1. **Text** - Message length can be with between 10 and 140 characters. 
  1. **CreatedAt** - Time and date when the value was first created.
  1. **UpdatedAt** - Time and date of the last change of field’s value.
  
  **[⬆ back to top](#table-of-contents)**
  
  ### Followers
  
  This table store information for `Followers`.

| Field         | Data Type       | Constraints | Default value |
| :------------ |:----------------| :-----------| :-------------|
|FollowerID|integer|Primary Key|NOT NULL|
|FollowedUserID|NVARCHAR(128)|Foreign Key|NOT NULL|
|FollowerUserID|NVARCHAR(128)|Foreign Key|NOT NULL|
|CreatedAt|DATETIME||NOT NULL|
|UpdatedAt|DATETIME||NOT NULL|

  1. **FollowerID** - the unique ID of the row which is used to access the data.
  1. **FollowedUserID** - Reference to [`AspNetUser`]((#aspnetusers)
  1. **FollowerUserID** - Reference to [`AspNetUser`]((#aspnetusers)
  1. **CreatedAt** - Time and date when the value was first created.
  1. **UpdatedAt** - Time and date of the last change of field’s value.
  
  **[⬆ back to top](#table-of-contents)**