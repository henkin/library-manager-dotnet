# Library Manager

## Summary
A simple library inventory manager. It should be structured as a simple database-backed CRUD application.
> Note: The first phase of this is going to be just the REST API - we are doing API-first.
> 
## Deliverables
Hosted on Github [link]

To keep the project as simple as possible, it is implemented using server-side 
rendering. The database access methodology is left to your preference, but the 
data itself should be stored using SQLite.

# Features
- Persistence - SQLite

# Application 
The application lets you manage three entity types: **books**, **authors**, **publishers**.
 
## Books
### Properties:
- ISBN (unique ID)
- Title
- DatePublished
- [Publisher](#publisher)
- [Author](#author) 

### Pages
- `/books` - list of books
- `/books?publisherId={publisherId}`
- `/books?authorId={authorId}`
- `/books/{id}` - details about single book

## Authors
Authors can write books
- AuthorId
- FirstName
- LastName

- `/authors` - list of authors
- `/author/{id}` - details about a single author

## Publisher
Publishers publish multiple books.
Properties:
- PublisherId
- Name

- `/publishers`
- `/publishers/{id}`

These objects should include properties that are typically relevant.
There should be 
- a form for entering details about each of the objects
- a page to view the details of each object
- and a list for each object collection.

There should be two additional lists: 
books written by an author
books published by a publisher. 

The book entry form should validate the ISBN using your own validator (assume a library isn't available). 
Bonus: support a single book image upload per book and display it on the details page.