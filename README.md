# Dev Rhythm 🍃

A forum for tech enthusiasts and developers. Dive deep into the community to exchange knowledge, get new ideas and networking opportunities! 🚀

## Use cases

1. ***User registration and profile management.*** New users can register on the forum to become members and take part in discussions.😎

2. ***Discussion threads with reactions.*** Users can start new discussion threads or participate in existing ones, as well as upvote/downvote the posts.👍👎

3. ***Tags and posts.*** Users tag posts with relevant keywords, making it easier to categorize and search for specific content. 🔍

4. ***Search functionality.*** Users can search for specific topics or posts to quickly find relevant information within the forum. 🔍

5. ***Filters.*** Users can filter posts according to different criteria.⚙️

6. ***Saving threads.*** Users can save a discussion they are interested in, as well as look at all posts of their own. 📚

9. ***Notifications.*** Users receive notifications for new replies or other activities to stay informed and engaged with ongoing discussions. 📬😊

## Feature plan

- ***User Accounts***
    - A dedicated page for user's profile, as well as pages for login, logout and registration
    - ASP.NET Core Identity
- ***Tagging system***
    - A collection of tags for categorizing posts
    - Development of tag design
- ***Posts***
    - The main page with a collection of posts
    - Post preview cards displaying titles, basic author info, post statistics (eg. votes)
    - Infinite scroll for posts
    - Dedicated place on the post preview card and post page to display tags
    - Opportunity to perform CRUD operations on posts
- ***Filtering***
    - A panel for filtering posts on the main page
    - Filtering by date, by votes, by tags
- ***Search***
    - Search panel in the header
    - Search by keywords
- ***Comments***
    - CRUD operations for manaing comments on posts
    - Infinite scroll for comment section on the post page
- ***Replies***
    - CRUD operations for replying on comments in the comment section
- ***Votes***
    - Voting functionality for posts, comments and replies
- ***Notifications***
    - Developing business logic for notification system
    - Implementing a SignalR hub fot sending notifications
- ***Subscription on post discussions***
    - Implement a button on every post for user to click if they want to get notifications about the discussion of a particular post
    - Integrate within the notification system
- ***Limiting access to some features for unauthorized users***
    - Developing logic to limit access
    - Implementing logic on controllers
- **Hangfire for automation of certain tasks**
